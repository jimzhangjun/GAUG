//=================================================================================================
//  Project:    QCS m1com Remote Load Application
//  Module:     M1ComInterfaceClass.cs                                                                         
//  Author:     Jim Zhang
//  Date:       16/12/2019
//  
//  Details:    Handling data with M1 
//  
//=================================================================================================
using System;
using System.Collections.Generic;
using M1ComNET;
using M1ComNET.M1;

using GAUGlib;

namespace GAUGm1com
{
    //-- Struct definitions ------------------------------------------------------------------------
    public class M1STRUCT
    {
        public const UInt16 bytePositionForValue = 16;
    }
    //-- Number definitions ------------------------------------------------------------------------
    public class SIZE
    {
        public const int PIDPARAMETER = 6;
        public const int DATATYPELENGTH = 3;
    }

    //-- PID definitions ------------------------------------------------------------------------
    public class PIDVARAIABLE
    {
        public ItemDescription itemDesc;
        public Item item;
        public int groupIndex;
        public int dataType;
        public int dataSize;
    }
    public class PIDVARAIBLELIB
    {
        public static List<PIDVARAIABLE> mList = new List<PIDVARAIABLE>();
    }
    public class INFORMATION
    {
        public static string logItem;
    }

    class M1ComInterface
    {
        private M1Device mDevice = null;
        private Group [] group = new Group [(int)GROUP.TOTAL_GROUP];

        public void start()
        {
            //creation of a m1 - device
            DeviceSettings deviceSettings = new DeviceSettings
            {
                Address = ConfigDataClass.m1com.address.ToString(),                         // "192.168.1.174",
                Protocol = (DeviceSettings.ProtocolType)ConfigDataClass.m1com.protocol,     // DeviceSettings.ProtocolType.TCP,
                Timeout = (uint)ConfigDataClass.m1com.timeout                               // 1000
            };
            mDevice = new M1Device(deviceSettings);

            //register a listener to device states
            mDevice.PropertyChanged += Device_Changed;

            //connect to the device
            M1Credentials credentials = new M1Credentials()
            {
                UserName = ConfigDataClass.m1com.username.ToString(),   // "M1",
                Password = ConfigDataClass.m1com.password.ToString()    // "bachmann" };
            };

            mDevice.Connect(credentials);

            //Alternatively creation  of a local device
            //A local device is a device without connection to a controller
            //All items of a local device are local items. As value they return what was written to them.
            //Device device = new LocalDevice();

            //creating groups of variables
            group[(int)GROUP.FAST] = mDevice.CreateGroup("group_0");
            group[(int)GROUP.NORMAL] = mDevice.CreateGroup("group_1");

            for (int i = 0; i < (int)GROUP.TOTAL_GROUP; i++)
            {
                group[i].CycleTime =   ConfigDataClass.m1com.circletime[i];
                group[i].ObservationMode = ObservationMode.PollingChanges;
            }

            List<Item> items = new List<Item>();
            foreach (PIDVARAIABLE mVar in PIDVARAIBLELIB.mList)
            {
                //adding an item to the items as an object
                mVar.item = mDevice.CreateItem(mVar.itemDesc);
                items.Add(mVar.item);
                UpdateXMDdata(mVar.item, true);

                switch (mVar.groupIndex)
                {
                    case 0:
                        group[0].Add(mVar.item);
                        break;
                    default:
                        group[1].Add(mVar.item);
                        break;
                }                
            }
            IEnumerable<Item> unresolved = mDevice.ResolveItems(items);

            //adding a grouplistener
            group[(int)GROUP.FAST].GroupUpdated += Group0_Changed;
            group[(int)GROUP.NORMAL].GroupUpdated += Group1_Changed;

            for (int i = 0; i < (int)GROUP.TOTAL_GROUP; i++)
            {
                //start observing the group
                group[i].Start();
                group[i].Update();
            }
            //Console.Read();
        }

        //------ Stop ------------------------------------------
        public void stop()
        {
            //stoping the group
            for(int i=0; i<(int)GROUP.TOTAL_GROUP; i++)
                if (group[i] != null) group[i].Stop();            

            //closing all connections to the device
            if (mDevice != null) mDevice.Close();

            //disposing the device
            if (mDevice != null) mDevice.Dispose();
        }
        //------ Update ------------------------------------------
        public void update(InfoPVar pvarInfo)
        {
            //-- not implemented yes ------------------------------------------ 
            //UpdateM1data(pvarInfo.Type, pvarInfo.Index, pvarInfo.No);
        }

        //Eventhandling Group
        void Group0_Changed(object sender, GroupUpdatedEventArgs e)
        {
            Group group = (Group)sender;
            //Console.WriteLine(group.Name + " " + e.ChangedItems.Count + " Items changed:");
            string Time = Convert.ToString(DateTime.Now);
            INFORMATION.logItem += Time + "  " + group.Name + " " + e.ChangedItems.Count + " Items changed:";
            if (RemoteInterfaceClass.connected)
            {
                foreach (Item item in e.ChangedItems) // Loop through List with foreach
                {
                    UpdateXMDdata(item, false);
                    INFORMATION.logItem += item.Name + ": " + item.ValueAsObject + "\r\n";
                    //Console.WriteLine(item.Name + ": " + item.ValueAsObject);
                }
            }
        }

        void Group1_Changed(object sender, GroupUpdatedEventArgs e)
        {
            Group group = (Group)sender;
            //Console.WriteLine(group.Name + " " + e.ChangedItems.Count + " Items changed:");
            string Time = Convert.ToString(DateTime.Now);
            INFORMATION.logItem += Time + "  " + group.Name + " " + e.ChangedItems.Count + " Items changed:";

            if (RemoteInterfaceClass.connected)
            {
                foreach (Item item in e.ChangedItems) // Loop through List with foreach
                {
                    UpdateXMDdata(item, false);
                    INFORMATION.logItem += item.Name + ": " + item.ValueAsObject + "\r\n";
                    //Console.WriteLine(item.Name + ": " + item.ValueAsObject);
                }
            }
        }

        //eventhandling Device
        void Device_Changed(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            M1Device device = (M1Device)sender;
            if (e.PropertyName == "State")
            {
                //Console.WriteLine("State of device changed to: " + device.State);
                string Time = Convert.ToString(DateTime.Now);
                INFORMATION.logItem += Time + "  " + "State of device changed to: " + device.State + "\r\n";
                while (device.State == ConnectionState.Error)
                {

                    M1Credentials credentials = new M1Credentials() { UserName = "M1", Password = "bachmann" };
                    try
                    {
                        device.Connect(credentials);
                        Group group = device.GetGroup("group_0");
                        device.ResolveItems(group.Items);
                        group.Start();

                        group = device.GetGroup("group_1");
                        device.ResolveItems(group.Items);
                        group.Start();
                    }
                    catch (Exception exc)
                    {
                        Console.Write(exc);
                    }
                    System.Threading.Thread.Sleep(1000);
                }

            }
            else if (e.PropertyName == "AppState")
            {
                //Console.WriteLine("AppState of device changed to: " + device.AppState);
                string Time = Convert.ToString(DateTime.Now);
                INFORMATION.logItem += Time + "  " + "AppState of device changed to: " + device.AppState + "\r\n";
            }
            else if (e.PropertyName == "RebootCounter")
            {
                //Console.WriteLine("RebootCounter of device changed to: " + device.RebootCount);
                string Time = Convert.ToString(DateTime.Now);
                INFORMATION.logItem += Time + "  " + "RebootCounter of device changed to: " + device.RebootCount + "\r\n";
            }
            else
            {
                //Console.WriteLine("unknown device event: " + e.PropertyName);
                string Time = Convert.ToString(DateTime.Now);
                INFORMATION.logItem += Time + "  " + "unknown device event: " + e.PropertyName + "\r\n";
            }
        }

        //eventhandling Item
        void Item_Changed(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Item item = (Item)sender;
            if (e.PropertyName == "State")
            {
                //Console.WriteLine("State of item " + item.Name + " changed to: " + item.State);
                string Time = Convert.ToString(DateTime.Now);
                INFORMATION.logItem += Time + "  " + "State of item " + item.Name + " changed to: " + item.State + "\r\n";
            }
            else if (e.PropertyName == "Value")
            {
                //Console.WriteLine("itemevent: " + item.Name + ": " + item.ValueAsObject);
                string Time = Convert.ToString(DateTime.Now);
                INFORMATION.logItem += Time + "  " + "itemevent: " + item.Name + ": " + item.ValueAsObject + "\r\n";
            }
            else
            {
                //Console.WriteLine("unknown item event: " + e.PropertyName);
                string Time = Convert.ToString(DateTime.Now);
                INFORMATION.logItem += Time + "  " + "unknown item event: " + e.PropertyName + "\r\n";
            }
        }
        //-- Update the item's Name or Value in XMDdata
        private void UpdateXMDdata(Item item, bool init)
        {
            UInt16 index = 0, aindex=0, type=0;
            string name="";
            if (GetItemProperties(item, ref type, ref index, ref aindex, ref name) == true)     
            {
                if(type == 1 || type == 10)         // basic bool, only in BOOL
                {
                    if (init) RemoteInterfaceClass.XMD.SetName((UInt16)VARTYPE_INDEX.BOOL, index, aindex, name);
                    else RemoteInterfaceClass.XMD.SetBoolData(type, index, 0, bool.Parse(item.ValueAsObject.ToString()));                    
                }
                else if (type > 1 && type < 8)      // basic integer, only in INTEGER 
                {
                    if (init) RemoteInterfaceClass.XMD.SetName((UInt16)VARTYPE_INDEX.INTEGER, index, aindex, name);
                    else RemoteInterfaceClass.XMD.SetIntegerData(index, int.Parse(item.ValueAsObject.ToString()));                    
                }
                else if (type == 8 || type == 9)    // basic float, only in FLOAT 
                {
                    if (init) RemoteInterfaceClass.XMD.SetName((UInt16)VARTYPE_INDEX.FLOAT, index, aindex, name);
                    else RemoteInterfaceClass.XMD.SetFloatData(index, float.Parse(item.ValueAsObject.ToString()));                    
                }
                else if (type == 11)                // string, only in STRING
                {
                    if (init) RemoteInterfaceClass.XMD.SetName((UInt16)VARTYPE_INDEX.STRING, index, aindex, name);
                    else RemoteInterfaceClass.XMD.SetStringData(index, item.ValueAsObject.ToString());
                }
                else if (type == 27)                // float artray, only in FLOATARRAY
                {
                    if (init) RemoteInterfaceClass.XMD.SetName((UInt16)VARTYPE_INDEX.FLOATARRAY, index, aindex, name);
                    else
                    {
                        byte[] bval = (byte[])item.ReadAsObject();
                        float fval = BitConverter.ToSingle(bval, M1STRUCT.bytePositionForValue + aindex);
                        RemoteInterfaceClass.XMD.SetFloatArrayData(index, aindex, fval);
                    }
                }
            }
        }
        //-- Update value in M1
        private void UpdateM1data(UInt16 type, UInt16 index, UInt16 n)
        {            
            Item item = null;
            if (GetItemByIndex(ref item, type, index) == true)
            {
                switch(item.DataType)
                {
                    case DataType.BOOL8:     // 
                        {
                            bool value = RemoteInterfaceClass.XMD.GetBoolData(type, index, 0);
                            item.Write(value);
                        }
                        break;
                    case DataType.SINT32:
                        {
                            Int32 value = RemoteInterfaceClass.XMD.GetIntegerData(index);
                            item.Write(value);
                        }
                        break;
                    case DataType.UINT32:
                        {
                            UInt32 value = (UInt32)RemoteInterfaceClass.XMD.GetIntegerData(index);
                            item.Write(value);
                        }
                        break;
                    case DataType.REAL32:
                        {
                            float value = RemoteInterfaceClass.XMD.GetFloatData(index);
                            item.Write(value);
                        }
                        break;
                    case DataType.STRING8:  // string
                        {
                            //item[0].StringLength 
                            //string value = RemoteInterfaceClass.XMD.GetStringData(index);
                            //item[0].Write(value);
                        }                        
                        break;
                    case DataType.BLOB:    // combination
                        {
                            //item[0].ArrayLength
                            //bool bvalue = RemoteInterfaceClass.XMD.GetBoolData(index);
                            //item[0].Write(bvalue);
                        }                        
                        break;
                }
            }
        }
        //-- looking for the type/index/name of the item in XMDdata
        private bool GetItemProperties(Item item, ref UInt16 type, ref UInt16 index, ref UInt16 aindex, ref string name)    // all items are availible to read
        {
            try
            {
                type = UInt16.Parse(item.Source.Substring(item.Source.ToString().Length - SIZE.DATATYPELENGTH));
                index = UInt16.Parse(item.Name.Substring(0, IniFile.INIFmt.NAME_INDEXOFTYPE));
                aindex = UInt16.Parse(item.Name.Substring(IniFile.INIFmt.NAME_INDEXOFTYPE, IniFile.INIFmt.NAME_INDEXOFARRAY));
                name = item.Name.Substring(IniFile.INIFmt.NAME_INDEXOFTYPE+ IniFile.INIFmt.NAME_INDEXOFARRAY);
                return true;
            }
            catch 
            {
                return false;
            }
        }
        //-- looking for an item by the type (float..) and the index (of the float..) in XMDdata
        private bool GetItemByIndex(ref Item item, UInt16 type, UInt16 index)    // only writable item are availible... basic/string/arrays/aio/dio
        {
            try
            {
                item = RemoteInterfaceClass.XMD.GetItem(type, index);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //-- looking for an item by the type (float..) and the name (of the float..) in XMDdata
        private bool GetIndexByName(string name, UInt16 type, ref UInt16 index)    // only writable item are availible... basic/string/arrays/aio/dio
        {
            try
            {
                bool ret = false;
                switch (type)
                {
                    case 2:
                        for (ushort i = 0; i < VARTYPE_NUMBERS.FLOAT; ++i)
                        {
                            if (RemoteInterfaceClass.XMD.GetName(2, i) == name)
                            {
                                index = i;
                                ret = true;
                                break;
                            }
                        }                            
                        break;
                }                
                return ret;
            }
            catch
            {
                return false;
            }
        }
    }
    //=============================================================================================
}

