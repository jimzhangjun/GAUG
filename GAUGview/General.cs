//=================================================================================================
//  Project:    RM312/SIPRO SUPERvisor
//  Module:     General.cs                                                                         
//  Author:     Andrew Powell
//  Date:       31/03/2006
//  
//  Details:    Definition of general data types 
//  
//=================================================================================================
using System;

namespace GAUGview
{
    //-- DigitalIO data structure -----------------------------------------------------------------
    public class DigitalIO
    {
        public int LastStatus = 0;
        public string[] Names = new string[32];
        public int[] Monitor = new int[32];
        public int[] Expected = new int[32];
    }
    public class DigitalIONew
    {
        public int LastStatus = 0;
        public string[] Names = new string[80];
        public int[] Monitor = new int[80];
        public int[] Expected = new int[80];
    }

    //-- Time constants 
    public class TIME
    {
        public const int FiveMins = 300;
        public const int OneMin = 60;
        public const int OneHour = 3600;
    }

    //-- User Login  
    public class LOGIN
    {
        public static bool OP = false;
        public static bool TECH = false;
        public static bool ENG = false;
        public static bool THERMO = true;
    }

    //-- Counter ----------------------------------------------------------------------------------
    public class Counter
    {
        public int Value = 0;
        public void Increment()
        {
            this.Value++;
        }
    }

    //-- Date & time class ------------------------------------------------------------------------
    class FormatDateTime
    {
        //-- Display date DD/MM/YY or MM/DD/YY in US
        public static string DisplayDate()
        {
            return (DateTime.Now.ToString("dd/MM/yyyy"));
        }
        //-- Display time 24 hours HH:MM:SS
        public static string DisplayTime()
        {
            return (DateTime.Now.ToString("HH:mm:ss"));
        }
        //-- Display date time DD/MM/YYYY HH:MM:SS
        public static string DisplayDateTime()
        {
            return (DateTime.Now.ToString("dd/MM/yyyy") + " " + DateTime.Now.ToString("HH:mm:ss"));
        }
        //-- Date stamp YYMMDDHHMMSS for telegrams
        public static string MsgDateTimeStamp()
        {
            return (DateTime.Now.ToString("yyMMddHHmmss"));
        }
        //-- Date stamp YYYYMMDD for file names, ordering, etc
        public static string DateStamp()
        {
            return (DateTime.Now.ToString("yyyyMMdd"));
        }
        //-- Time stamp HHMMSS for filenames, ordering, etc
        public static string TimeStamp()
        {
            return (DateTime.Now.ToString("HHmmss"));
        }
        //-- Date time stamp YYYYMMDD_HHMMSS for file names, ordering, etc
        public static string DateTimeStamp()
        {
            return (DateTime.Now.ToString("yyyyMMdd_HHmmss"));
        }
        //-- Accurate time stamp HH:MM:SS.MMM
        public static string AccurateTimeStamp()
        {
            return (DateTime.Now.ToString("HH:mm:ss.") + DateTime.Now.Millisecond.ToString("D3"));
        }
        //-- Formal date time stamp Tuesday 10 February 1969 for prints etc
        public static string FormalDate()
        {
            return (DateTime.Now.ToString("dddd dd MMMM yyyy"));
        }
        //-- Formal date time stamp Tuesday 10 February 1969 - HH:MM:SS for prints etc
        public static string FormalDateTime()
        {
            return (DateTime.Now.ToString("dddd dd MMMM yyyy  -  HH:mm:ss"));
        }
        //-- Date stamp YYYY/MM for csv file path
        public static string CSVPATH()
        {
            return (DateTime.Now.ToString("/yyyy/MM/"));
        }
        //-- Date time stamp YYYYMMDD_HHMM for file names, ordering, etc
        public static string CSVNAME()
        {
            return (DateTime.Now.ToString("yyyyMMdd_HHmm"));
        }
    }
    //=============================================================================================
}

