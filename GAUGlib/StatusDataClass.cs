//=================================================================================================
//  Project:    SIPRO-library
//  Module:     StatusDataClass.cs                                                                         
//  Author:     Andrew Powell
//  Date:       09/09/2008
//  
//  Details:    Definition of STATUS data structures
//  
//=================================================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace GAUGlib
{
    //-- Access to commonly used M1 status inputs -----------------------------
    [SerializableAttribute()]
    public class M1Status
    {
        public bool S1XRAYSON = false;
        public bool S2XRAYSON = false;
        public bool S1SHOPEN = false;
        public bool S2SHOPEN = false;
        public bool S1SHCLSD = false;
        public bool S2SHCLSD = false;
        public bool S1FLOW = false;
        public bool S2FLOW = false;
        public bool S1TEMP = false;
        public bool S2TEMP = false;
        public bool CFTEMP = false;
        public bool CFFLOW = false;
        public bool ONLINE = false;
        public bool OFFLINE = false;
        public bool HEALTHY = false;
        public bool SLOWSHUT = false;
        public bool SHUTTERINHIBITREMOTE = false;
        public bool SHUTTERINHIBIT = false;
        public bool CHOPSHUTTER = false;
        public bool S1SINGLEMODE_REQ = false;
        public bool S2SINGLEMODE_REQ = false;
        public bool DISABLE_PROFILE_REQ = false;
        public bool SETUPAVAILABLE = false;
        public bool DETPSU = false;
        public bool PELTIERPSU = false;
        public bool AIRDRYER = false;
        public bool AUTOSHUTTERON = false;
        public bool READY = false;
        public bool STDSINSERTED = false;
        public bool CHILLER1 = false;
    }
    //-- Access to commonly used SUPERvisor status outputs --------------------
    [SerializableAttribute()]
    public class SuperStatus
    {
        public bool XMDDETOK = false;
        public bool XMDMEASOK = false;
        public bool XMDPYROOK = false;
        public bool XMDNETOK = false;
        public bool XMDARCHIVEOK = false;
        public bool XMDREPORTOK = false;
        public bool CALDUE = false;
        public bool STDZDUE = false;
        //public bool DSPOK = false;
        public bool PYROOK = false;
        public bool TCP1OK = false;
        public bool TCP2OK = false;
        public bool TCP3OK = false;
        public bool UDP = false;
        public bool CAL = false;
        public bool INTCAL = false;
        public bool STDZ = false;
        public bool READ = false;
        public bool MEAS = false;
        public bool CLIP = false;
        public bool LOCALSIGS = false;
        public bool STDZFAIL = false;
        public bool STDZ_FAILZERO = false;
        public bool STDZ_FAILTHICK = false;
        public bool DEFAULTALLOYINUSE = false;
        public bool RESETSHUTTERTRIPREQ = false;
        public bool DETCOMMSERROR = false;
        public bool DETTIMEOUTERROR = false;
    }
    //-- Gulmay status --------------------------------------------------------
    [SerializableAttribute()]
    public class GulmayStatus
    {
        public bool S1COMMS = false;
        public bool S2COMMS = false;
        public bool S1WARM = false;
        public bool S2WARM = false;
        public bool S1CONFLICT = false;
        public bool S2CONFLICT = false;
        public bool S1COOL = false;
        public bool S2COOL = false;
        public bool S1XLOCK = false;
        public bool S2XLOCK = false;
        public bool S1SLOCK = false;
        public bool S2SLOCK = false;
        public bool ESTOP = false;
        public bool S1GULERRCTRL = false;
        public bool S2GULERRCTRL = false;
        public enum NOB
        {
            SIM = 1,
            BACK = 2,
            OPEN = 4,
            BOTH = 8
        }
        public int mode = 8;
    }
    ////-- Maintain current source operating mode -------------------------------
    //public class SourceMode
    //{
        
    //}
    //-------------------------------------------------------------------------
}
//=================================================================================================
