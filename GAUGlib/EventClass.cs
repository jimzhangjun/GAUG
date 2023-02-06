//=================================================================================================
//  Project:    SIPRO-library
//  Module:     EventClass.cs                                                                         
//  Author:     Andrew Powell
//  Date:       09/09/2008
//  
//  Details:    Definition of Event data structure
//
//              Expanded to include the remoting event classes
//  
//=================================================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace GAUGlib
{
    //-- Queue of pvars updated in GUI  -----------------------------------
    public class RemotePVarQ
    {
        public static List<InfoPVar> pvarList = new List<InfoPVar>();

        public static bool Ready;

        //-- Function to call the server from the client
        public void InfoFunction(string name, UInt16 type, UInt16 index, UInt16 no)
        {
            InfoPVar info = new InfoPVar();
            info.Name = name;
            info.Type = type;
            info.Index = index;
            info.No = no;

            if (pvarList.Count == 0)
                pvarList.Add(info);
            else
            {
                bool alreadyExists = false;
                foreach (InfoPVar pvarInfo in pvarList)
                {
                    if (pvarInfo.Name == info.Name)
                    {
                        //versInfo.aliveCount = info.aliveCount;
                        alreadyExists = true;
                    }
                }
                if (!alreadyExists)
                    pvarList.Add(info);
            }
        }
    }

    //-- Client application information -----------------------------------------------------------
    [Serializable]
    public class InfoPVar
    {
        public string Name { get; set; }
        public UInt16 Type { get; set; }
        public UInt16 Index { get; set; }
        public UInt16 No { get; set; }

        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    //-- Action event class -------------------------------------------------
    [SerializableAttribute()]
    //-- Queue of events received from M1 -----------------------------------
    public class RXEventQ
    {
        //-- M1->SUPERVISOR
        public const int NULLCMD = 0x0;
        public const int READCMD = 0x4001;
        public const int STDZCMD = 0x4002;
        public const int INTCALCMD = 0x4003;
        public const int OPNSHCMD = 0x4004;
        public const int WREADCMD = 0x4005;
        public const int WSTDZCMD = 0x4006;
        public const int CLSSHCMD = 0x4007;
        public const int SRCWARMUP = 0x4008;
        //public const int REQSETUP = 0x4010;
        public const int ARCHIVEON = 0x4011;
        public const int ARCHIVEOFF = 0x4012;
        public const int GHOSTON_EV = 0x4013;
        public const int GHOSTOFF_EV = 0x4014;

        public const int INTCALR1 = 0x40031;
        public const int INTCALR2 = 0x40032;
        public const int INTCALR3 = 0x40033;
        public const int FWDCMD = 0x400B;
        public const int REVCMD = 0x400C;
        public const int STOPMOVE = 0x400D;

        //-- Remote app events
        public const int XMDREADCMD = 0x9001;

        public static bool blockM1 = false;
        public static int[] EventQ = new int[20];
        public static int Count = 0;
        public static int[] History = new int[20];
        public static int HCount = 0;
    }

    //-- Queue of events to transmit to M1-----------------------------------
    public class TXEventQ
    {
        //-- SUPERVISOR->M1
        public const int XONCMD = 0x3001;
        public const int XOFFCMD = 0x3002;
        public const int SHOPNCMD = 0x3003;
        public const int SHCLSCMD = 0x3004;
        public const int FWDCMD = 0x3005;
        public const int REVCMD = 0x3006;
        public const int WARM1CMD = 0x3007;
        public const int WARM2CMD = 0x3008;
        public const int CAXWARMCMD = 0x3009;
        public const int SETINTCMD = 0x300A;
        public const int JIGFWD = 0x300B;
        public const int JIGREV = 0x300C;
        public const int STOPMOVE = 0x300D;
        public const int SETSLOWSHUT = 0x300E;
        public const int SETFASTSHUT = 0x300F;
        //-- 20160926 New Siemens Frequency Controller
        public const int ENABLECHOPSHUTTER = 0x3010;
        public const int DISABLECHOPSHUTTER = 0x3011;
        public const int ENABLECFRAME = 0x3012;
        public const int DISABLECFRAME = 0x3013;

        public static int[] EventQ = new int[20];
        public static int Count = 0;
        public static int[] History = new int[20];
        public static int HCount = 0;
    }


    //-- Queue of events received from Anywhere -----------------------------------
    public class RemoteEvQ
    {
        //-- Event string definitions
        public const string NULL_EV = "NULL";
        public const string READ_EV = "READ";
        public const string SETINT_EV = "SETINT";
        public const string SETTEMP_EV = "SETTEMP";
        public const string COMP_EV = "COMP";
        public const string SOS_EV = "SOS";
        public const string EOS_EV = "EOS";
        public const string TAIL_EV = "TAILOUT";
        public const string SETUP_EV = "SETUPREQ";
        public const string ARCHIVEON_EV = "ARCHIVEON";
        public const string ARCHIVEOFF_EV = "ARCHIVEOFF";
        public const string DETSIGON_EV = "DETSIGON";
        public const string ERROR_EV = "ERROR";
        public const string GHOSTON_EV = "GHOSTON";
        public const string GHOSTOFF_EV = "GHOSTOFF";

        //-- Current event received from server
        public string eventToProcess;
        //-- Notification event for clients
        public event EventHandler NewEvent;
        //-- Invoke the event 
        protected virtual void OnEvent(EventArgs e)
        {
            if (NewEvent != null)
                NewEvent(this, e);
        }
        //-- Add event to process
        public void AddEvent(object value)
        {
            eventToProcess = (string)value;
            OnEvent(EventArgs.Empty);
        }
    }

    //-- Client application information -----------------------------------------------------------
    [Serializable]
    public class VersionInfoAlive
    {
        public string hostName;
        public string appName;
        public string appVers;
        public int aliveCount;
        public uint appStat;
        public uint failCount;
    }


    //-- Server event handler ---------------------------------------------------------------------
    public class XMDevent : MarshalByRefObject, ICallsToServer
    {
        public static List<VersionInfoAlive> infoList = new List<VersionInfoAlive>();

        private static int ClientsConnected;

        //-- Function to call the server from the client
        public void InfoFunction(VersionInfoAlive info)
        {
            if (infoList.Count == 0)
                infoList.Add(info);
            else
            {
                bool alreadyExists = false;
                foreach (VersionInfoAlive versInfo in infoList)
                {
                    if ((versInfo.hostName == info.hostName) && (versInfo.appName == info.appName))
                    {
                        versInfo.aliveCount = info.aliveCount;
                        versInfo.appStat = info.appStat;
                        alreadyExists = true;
                    }                
                }
                if (!alreadyExists)
                    infoList.Add(info);
            }
        }
        //-- Local copy of event holding a collection
        private static event NotifyCallback s_notify;

        //-- Add or remove callback destinations on the client
        public event NotifyCallback Notify
        {
            add { s_notify += value; }
            remove { s_notify -= value; }
        }

        //-- Call this method to send the string to the client           
        public static void FireNewBroadcastedMessageEvent(string s)
        {
            if (s_notify != null)
            {
                NotifyCallback a_notify = null;
                Delegate[] invocationList_ = null;
                try
                {
                    invocationList_ = s_notify.GetInvocationList();
                    ClientsConnected = invocationList_.Length;
                }
                catch (MemberAccessException ex)
                {
                    throw ex;
                }
                if (invocationList_ != null)
                {
                    foreach (Delegate del in invocationList_)
                    {
                        try
                        {
                            a_notify = (NotifyCallback)del;
                            try
                            {
                                a_notify(s);
                            }
                            //-- Client has gone or network is down
                            catch (Exception)
                            {
                                s_notify -= a_notify;
                                ClientsConnected--;
                            }
                        }
                        catch (Exception)
                        {
                            s_notify -= s_notify;
                        }
                    }
                }
            }
        }
    }

    //-- Delegate defining the event handler ------------------------------------------------------
    public delegate void NotifyCallback(string s);
    public interface ICallsToServer
    {
        //-- Function to call the server from the client
        void InfoFunction(VersionInfoAlive info);
        //-- Add or remove callback destinations on the client
        event NotifyCallback Notify;
    }
    // This class is used by client to provide delegates to the server that will
    // fire events back through these delegates
    // Overriding OnServerEvent to capture the callback from the server
    public abstract class NotifyCallbackSink : MarshalByRefObject
    {
        // Called by the server to fire the call back to the client
        public void FireNotifyCallback(string s)
        {
            OnNotifyCallback(s);
        }
        // Client overrides this method to receive the callback events from the server
        protected abstract void OnNotifyCallback(string s);
    }

}
//=================================================================================================