//=================================================================================================
//  Project:    QCS m1com Remote Load Application
//  Module:     IniFileData.cs                                                                         
//  Author:     Jim Zhang
//  Date:       16/12/2019
//  
//  Details:    Definition of INI file data types 
//  
//=================================================================================================
using System;
//using System.Collections.Generic;
using System.Text;
//using System.Drawing;
using System.Runtime.InteropServices;
//using System.ComponentModel;
//using System.Threading;
using System.IO;
using System.Data;
using System.Net;
//using System.Net.Sockets;
using System.Diagnostics;
using M1ComNET;
//using M1ComNET.M1;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using GAUGlib;

namespace GAUGm1com
{
    public enum GROUP
    {
        FAST,
        NORMAL,
        TOTAL_GROUP
    }
    //-- Common directory path listings -----------------------------------------------------------
    public class FileClass
    {
        //-- INI file configuration parameters
        public static string rootDir = @"C:\Thermo";
        public static string filePath = @"\GAUGload\Configuration\";
        public static string diagPath = @"\GAUGload\\Diagnostic\";
        public static string fileName = "GAUGm1com.INI";
        public static string pidName = "GAUGpid.INI";
        public static IniFile iniFile;  // = new IniFile(rootDir + filePath + fileName);        

        //-- Create directory if it doesn't exist
        public static void CreateDir(System.IO.DirectoryInfo oDirInfo)
        {
            if (oDirInfo.Parent != null)
                CreateDir(oDirInfo.Parent);
            if (!oDirInfo.Exists)
                oDirInfo.Create();
        }
        //-- Return diagnostic directory path
        public static string Diagnostic()
        {
            DateTime date = DateTime.Today;
            string dirName = rootDir + diagPath + date.ToString("yyyyMMdd");
            CreateDir(new System.IO.DirectoryInfo(dirName));
            return dirName + "\\";
        }
    }

    //-- Application configuration settings
    public class AppCfgClass
    {
        public string title;
        public string gaugeId = "SPxxxx";
        public ProcessPriorityClass priority = ProcessPriorityClass.Normal;
        public int affinity = 1;
        //public bool m1Connect = false;
    }
    //-- REMOTING configuration settings
    public class RemoteCfgClass
    {
        public IPAddress address;
        public bool autoConnect = false;
        public int port = 9001;
    }
    //-- M1Com configuration settings   
    public class M1ComCfgClass
    {
        public IPAddress address;
        public int protocol;
        public int timeout;
        public string username;
        public string password;
        public bool autoConnect = false;
        public int[] circletime = new int[(int)GROUP.TOTAL_GROUP];
    }
    //-- PID configuration settings -------------------------------------------------------
    public class PIDCfgClass
    {
        public int[] pidnumber = new int[IniFile.INIFmt.MaxFieldCount];
        //public int[] pid_0 = new int[SIZE.PID_0];
    }
    //-- Configuration data class
    public class ConfigDataClass
    {
        public static AppCfgClass app = new AppCfgClass();
        public static RemoteCfgClass remoting = new RemoteCfgClass();
        public static M1ComCfgClass m1com = new M1ComCfgClass();
        public static PIDCfgClass pid = new PIDCfgClass();
    }

    public class IniFile
    {
        //-----------------------------------------------------------------------------------------
        //-- CLASS VARIABLES ----------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------
        //-- Float Data Number definitions --------------------------------------------------------------
        public enum FILES
        {
            INI,
            PID,
            TOTAL_FILES
        }

        public static IniFormat INIFmt = new IniFormat();

        public string filePath;
        public string pidFilePath;

        //public static bool fileLoaded = false;

        public DataTable iniDataTable = new DataTable();
        public DataTable pidDataTable = new DataTable();

        public class IniFormat
        {
            public string[] PIDSectionName = { "PID_BOOL", "PID_INTEGER", "PID_FLOAT", "PID_STRING", "PID_FLOATARRAY", "PID_COMBINATION" };

            public string[] SectionName = { "APPLICATION", "REMOTE", "M1COM", "PVAR" };
            public string[][] FieldName = new string[][]
            {
                new string[] { "Customer", "GaugeId", "TaskPriority", "ProcessorAffinity" },
                new string[] { "Address", "Port", "AutoConnect" },
                new string[] { "Address", "Protocol", "Timeout", "UserName", "Password", "AutoConnect", "CycleTime" },
                new string[] { "PIDNumber" }
            };
            public int[][] FieldCount = new int[][]
            {
                new int[] { 1, 1, 1, 1 },
                new int[] { 1, 1, 1 },
                new int[] { 1, 1, 1, 1, 1, 1, 2 },
                new int[] { 6 },
            };
            public int MaxFieldCount = 6;

            public int PID_DIGITALSTART = 4;           
            public int NAME_INDEXOFTYPE = 3;
            public int NAME_INDEXOFARRAY = 3;
        }

        //-----------------------------------------------------------------------------------------
        //-- INIfile API functions ----------------------------------------------------------------
        //-----------------------------------------------------------------------------------------
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        //-- Set INIfile path and name ------------------------------------------------------------
        public IniFile(string INIPath, string PIDPath)
        {
            filePath = INIPath;
            pidFilePath = PIDPath;
        }
        //-- Check if file already exists ---------------------------------------------------------
        public bool FileExists(int whichFile)
        {
            FileInfo File = null;
            switch (whichFile)
            {
                case (int)FILES.INI:    
                    File = new FileInfo(String.Format(filePath));
                    break;
                case (int)FILES.PID:    
                    File = new FileInfo(String.Format(pidFilePath));
                    break;
            }
            if (File != null) return File.Exists;
            else return false;
        }
        //-- Delete existing file -----------------------------------------------------------------
        private void DeleteFile(int whichFile)
        {
            FileInfo File=null;
            switch (whichFile)
            {
                case (int)FILES.INI:  
                    File = new FileInfo(String.Format(filePath));
                    break;
                case (int)FILES.PID:  
                    File = new FileInfo(String.Format(pidFilePath));
                    break;
            }
            if(File != null) File.Delete();
        }
        //-- Write Data to the INI File -----------------------------------------------------------
        private void IniWriteValue(int whichFile, string Section, string Key, string Value)
        {
            switch (whichFile)
            {
                case (int)FILES.INI: 
                    WritePrivateProfileString(Section, Key, Value, filePath);
                    break;
                case (int)FILES.PID:   
                    WritePrivateProfileString(Section, Key, Value, pidFilePath);
                    break;
            }
        }
        //-- Read Data Value From the Ini File ----------------------------------------------------
        private string IniReadValue(int whichFile, string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            Int32 ret;
            switch (whichFile)
            {
                case (int)FILES.INI:
                    ret = GetPrivateProfileString(Section, Key, "", temp, 255, filePath);
                    break;
                case (int)FILES.PID:
                    ret = GetPrivateProfileString(Section, Key, "", temp, 255, pidFilePath);
                    break;
            }
            return temp.ToString();
        }
        //-----------------------------------------------------------------------------------------
        //-- File handling methods ----------------------------------------------------------------
        //-----------------------------------------------------------------------------------------
        //-- Create datagrid and label columns ----------------------------------------------------
        private void CreateGrid(int Columns, ref DataTable table)
        {
            table.Clear();
            table.Columns.Add("Section");
            table.Columns.Add("Field");
            for (int i = 0; i < Columns; i++)
                table.Columns.Add("Value " + i.ToString());
        }
        //-- Add a new field(s) to datagrid -------------------------------------------------------
        private void AddToGrid(int whichFile, string sectionName, string fieldName, int fieldCount, ref DataTable table)
        {
            try
            {
                // define which character is separating fields
                char[] splitter = { ',' };
                string[] rowData = new string[fieldCount + 2];
                string item = IniReadValue(whichFile, sectionName, fieldName);
                string[] valueData = item.Split(splitter);
                rowData[0] = sectionName;
                rowData[1] = fieldName;
                for (int i = 0; i < fieldCount; i++)
                    rowData[i + 2] = valueData[i];
                table.Rows.Add(rowData);
            }
            catch (Exception exc)
            {
                ExceptionManager.Publish(exc);
            }
        }
        //-- Put datagrid values to data structures -----------------------------------------------
        public void PutData()
        {
            int field = 0;
            try
            {
                for (field = 0; field < iniDataTable.Rows.Count; field++)
                {
                    //-- APP section 
                    if (iniDataTable.Rows[field][0].ToString() == IniFile.INIFmt.SectionName[0])
                    {
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.INIFmt.FieldName[0][0])
                            ConfigDataClass.app.title = iniDataTable.Rows[field][2].ToString();
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.INIFmt.FieldName[0][1])
                            ConfigDataClass.app.gaugeId = iniDataTable.Rows[field][2].ToString();
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.INIFmt.FieldName[0][2])
                        {
                            switch (Int32.Parse(iniDataTable.Rows[field][2].ToString()))
                            {
                                case 0:
                                    ConfigDataClass.app.priority = ProcessPriorityClass.Idle;
                                    break;
                                case 1:
                                    ConfigDataClass.app.priority = ProcessPriorityClass.BelowNormal;
                                    break;
                                case 2:
                                    ConfigDataClass.app.priority = ProcessPriorityClass.Normal;
                                    break;
                                case 3:
                                    ConfigDataClass.app.priority = ProcessPriorityClass.AboveNormal;
                                    break;
                                case 4:
                                    ConfigDataClass.app.priority = ProcessPriorityClass.High;
                                    break;
                                case 5:
                                    ConfigDataClass.app.priority = ProcessPriorityClass.RealTime;
                                    break;
                                default:
                                    ConfigDataClass.app.priority = ProcessPriorityClass.Normal;
                                    break;
                            }
                        }

                        if (iniDataTable.Rows[field][1].ToString() == IniFile.INIFmt.FieldName[0][3])
                            ConfigDataClass.app.affinity = Int32.Parse(iniDataTable.Rows[field][2].ToString());
                    }
                    //-- REMOTE section
                    if (iniDataTable.Rows[field][0].ToString() == IniFile.INIFmt.SectionName[1])
                    {
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.INIFmt.FieldName[1][0])
                            ConfigDataClass.remoting.address = IPAddress.Parse(iniDataTable.Rows[field][2].ToString());
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.INIFmt.FieldName[1][1])
                            ConfigDataClass.remoting.port = Int32.Parse(iniDataTable.Rows[field][2].ToString());
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.INIFmt.FieldName[1][2])
                            ConfigDataClass.remoting.autoConnect = bool.Parse(iniDataTable.Rows[field][2].ToString());
                    }
                    //-- M1COM Section
                    if (iniDataTable.Rows[field][0].ToString() == IniFile.INIFmt.SectionName[2])
                    {
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.INIFmt.FieldName[2][0])
                            ConfigDataClass.m1com.address = IPAddress.Parse(iniDataTable.Rows[field][2].ToString());
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.INIFmt.FieldName[2][1])
                            ConfigDataClass.m1com.protocol = Int32.Parse(iniDataTable.Rows[field][2].ToString());
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.INIFmt.FieldName[2][2])
                            ConfigDataClass.m1com.timeout = Int32.Parse(iniDataTable.Rows[field][2].ToString());
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.INIFmt.FieldName[2][3])
                            ConfigDataClass.m1com.username = iniDataTable.Rows[field][2].ToString();
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.INIFmt.FieldName[2][4])
                            ConfigDataClass.m1com.password = iniDataTable.Rows[field][2].ToString();
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.INIFmt.FieldName[2][5])
                            ConfigDataClass.m1com.autoConnect = bool.Parse(iniDataTable.Rows[field][2].ToString());
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.INIFmt.FieldName[2][6])
                        {
                            for(int i=0; i<(int)GROUP.TOTAL_GROUP; i++)
                                ConfigDataClass.m1com.circletime[i] = Int32.Parse(iniDataTable.Rows[field][2+i].ToString()); 
                        }
                    }
                    //-- PVAR section
                    if (iniDataTable.Rows[field][0].ToString() == IniFile.INIFmt.SectionName[3])
                    {
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.INIFmt.FieldName[3][0])
                        {
                            for (int i = 0; i < IniFile.INIFmt.MaxFieldCount; i++)
                                ConfigDataClass.pid.pidnumber[i] = Int32.Parse(iniDataTable.Rows[field][2+i].ToString());
                        }                        
                    }
                }
            }
            catch (Exception exc)
            {
                //MessageBox.Show("Problem reading GENERAL.INI field number " + field.ToString(), "XMDnet Error");
                ExceptionManager.Publish(exc);
            }
        }
        //-- Load data from INI file to datagrid --------------------------------------------------
        public void LoadData()
        {
            try
            {
                if (FileExists((int)FILES.INI))  //INI file
                {
                    CreateGrid(INIFmt.MaxFieldCount, ref iniDataTable);
                    for (int Section = 0; Section < IniFile.INIFmt.SectionName.Length; Section++)
                    {
                        for (int Field = 0; Field < IniFile.INIFmt.FieldCount[Section].Length; Field++)
                        {
                            AddToGrid((int)FILES.INI, IniFile.INIFmt.SectionName[Section], IniFile.INIFmt.FieldName[Section][Field],
                                      IniFile.INIFmt.FieldCount[Section][Field], ref iniDataTable);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                //MessageBox.Show("Problem reading GENERAL.INI field number " + field.ToString(), "XMDnet Error");
                ExceptionManager.Publish(exc);
            }
        }

        //-- Load PID data from INI file to datagrid --------------------------------------------------
        public void LoadPID()
        {
            try
            {
                if (FileExists((int)FILES.PID))
                {
                    int totalnumber = 0;
                    for (int i = 0; i < IniFile.INIFmt.MaxFieldCount - 1; i++) totalnumber += ConfigDataClass.pid.pidnumber[i];

                    if (totalnumber > 0) // PID file
                    {
                        CreateGrid(SIZE.PIDPARAMETER, ref pidDataTable);
                        for (int i = 0; i < IniFile.INIFmt.MaxFieldCount - 1; i++)
                        {
                            for (int Field = 0; Field < ConfigDataClass.pid.pidnumber[i]; Field++)
                            {
                                AddToGrid((int)FILES.PID, IniFile.INIFmt.PIDSectionName[i], "PID-" + Field.ToString(), SIZE.PIDPARAMETER, ref pidDataTable);
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                //MessageBox.Show("Problem reading GENERAL.INI field number " + field.ToString(), "XMDnet Error");
                ExceptionManager.Publish(exc);
            }
        }
        //-- Put datagrid values to data structures -----------------------------------------------
        public void PutPID()
        {
            int field_start = 0;
            try
            {
                for (int i = 0; i < IniFile.INIFmt.MaxFieldCount - 1; i++)
                {
                    for (int field = 0; field < ConfigDataClass.pid.pidnumber[i]; field++)
                    {
                        if (Int32.Parse(pidDataTable.Rows[field_start + field][1].ToString().Substring(IniFile.INIFmt.PID_DIGITALSTART)) == field)
                        {   // (0)Section (1)Field (2)Source (3)Index (4)Size (5)Name (6)Writeable (7)Group
                            PIDVARAIABLE mVar = new PIDVARAIABLE();
                            mVar.itemDesc.Source = pidDataTable.Rows[field_start + field][2].ToString();
                            mVar.dataType = Int16.Parse(pidDataTable.Rows[field_start + field][2].ToString().Substring(pidDataTable.Rows[field_start + field][2].ToString().Length - SIZE.DATATYPELENGTH));
                            mVar.dataSize = Int16.Parse(pidDataTable.Rows[field_start + field][4].ToString());
                            mVar.itemDesc.Name = field.ToString("D3") + Int16.Parse(pidDataTable.Rows[field_start + field][3].ToString()).ToString("D3") + pidDataTable.Rows[field_start + field][5].ToString();  //i.ToString() + pidDataTable.Rows[field_start + field][1].ToString();       // MaxFieldCount < 11
                            mVar.itemDesc.Writeable = bool.Parse(pidDataTable.Rows[field_start + field][6].ToString());
                            mVar.itemDesc.Readable = true;
                            mVar.groupIndex = Int16.Parse(pidDataTable.Rows[field_start + field][7].ToString());
                            ConvertPIDType(ref mVar);
                            PIDVARAIBLELIB.mList.Add(mVar);
                        }
                    }
                    field_start += ConfigDataClass.pid.pidnumber[i];
                }
            }
            catch (Exception exc)
            {
                //MessageBox.Show("Problem reading GENERAL.INI field number " + field.ToString(), "XMDnet Error");
                ExceptionManager.Publish(exc);
            }
        }
        private void ConvertPIDType(ref PIDVARAIABLE var)
        {
            var.itemDesc.DataType = DataType.UNKNOWN;
            switch(var.dataType)
            {
                case 1:     // 
                case 10:
                    var.itemDesc.DataType = DataType.BOOL8; break;
                case 6:
                    var.itemDesc.DataType = DataType.SINT32; break;
                case 7:
                    var.itemDesc.DataType = DataType.UINT32; break;
                case 8:
                    var.itemDesc.DataType = DataType.REAL32; break;
                case 11:    // string
                    var.itemDesc.DataType = DataType.STRING8;
                    var.itemDesc.StringLength = var.dataSize;
                    break;
                case 13:    // profile
                case 14:    // trend
                case 27:    // float array
                case 36:    // boolean array
                    var.itemDesc.DataType = DataType.BLOB;
                    var.itemDesc.ArrayLength = var.dataSize;
                    break;
            }
        }
        //-----------------------------------------------------------------------------------------
    }
}
//=================================================================================================
