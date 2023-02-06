//=================================================================================================
//  Project:    SIPRO-library
//  Module:     CalibrationDataClass.cs                                                                         
//  Author:     Andrew Powell
//  Date:       12/09/2008
//  
//  Details:    Definition of CALIBRATION data structures
//  
//=================================================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace GAUGlib
{
    //-- Individual int/ext standard values class -----------------------------
    [SerializableAttribute()]
    public class StdsDataStruct
    {
        public int NumStds;
        public float[] Value = new float[12];
    }
    //-- Calibration class structures -----------------------------------------
    [SerializableAttribute()]
    public class CalStruct
    {
        //-- Configuration data for all data in range
        public CalConfigStruct Config = new CalConfigStruct();
        //-- Internal - current Ical signal data - saved to csv
        public CalDataStruct Internal = new CalDataStruct();
        //-- External - Ecal signal data - saved to csv - essential
        public CalDataStruct External = new CalDataStruct();
        //-- Plate Scatter Corrected External - modified Ecal signal data 
        public CalDataStruct pscExternal = new CalDataStruct();
        //-- Reference - Ical signal data at Ecal scantime - saved to csv - essential
        public CalDataStruct Reference = new CalDataStruct();
        //-- Zero - Open beam data at Ecal scantime - saved to csv - essential
        public CalDataStruct zeroData = new CalDataStruct();
        //-- ICalThickE - Ical thick data at Ecal - calculated when required
        public CalDataStruct IcalThickE = new CalDataStruct();
        //-- IcalThickI - Current Ical thick data - calculated when required
        public CalDataStruct IcalThickI = new CalDataStruct();
        //-- IcalDiffs - EICalThick v IICalThick - interpolated to calc stdz offset
        public CalDataStruct IcalDiffs = new CalDataStruct();
        public bool Loaded = false;
    }
    //-- Indivdual calibration structure --------------------------------------
    [SerializableAttribute()]
    public class CalDataStruct
    {
        public CalHistoryStruct History = new CalHistoryStruct();
        //public int NumCalPts = 12;
        public float[] CalPts = new float[SIZE.CALPT];
        public CalSourceDataStruct S1CalData = new CalSourceDataStruct();
        public CalSourceDataStruct S2CalData = new CalSourceDataStruct();
        //Under development
        public int[] SlopeAdjust = new int[SIZE.RAW];
        public float[] NormFactor = new float[SIZE.RAW];
        public float[] StdzNormFactor = new float[SIZE.RAW];
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- Individual source calibration structure ------------------------------
    [SerializableAttribute()]
    public class CalSourceDataStruct
    {
        public int[] Infinity = new int[SIZE.RAW];
        public int[] Zero = new int[SIZE.RAW];
        public int[][] CalData = new int[SIZE.CALPT][];
        public CalSourceDataStruct()
        {
            for (int i = 0; i < SIZE.CALPT; i++)
                CalData[i] = new int[SIZE.RAW];
        }
    }
    //-- Calibration history tag structure ------------------------------------
    [SerializableAttribute()]
    public class CalHistoryStruct
    {
        public string Date;
        public string Time;
        public float KvSetting;
        public float MaSetting;
        public bool SlowShutter;
        public int DetInt;
    }
    //-- Calibration configuration tag structure ------------------------------
    [SerializableAttribute()]
    public class CalConfigStruct
    {
        public float KvSetting;
        public float MaSetting;
        public bool SlowShutter;
        public int DetInt;
        public int DetGain; //XDM04
        public int NumCalPts;
        public int ExtThreshold;
        public int ExtMinSignal;
    }
    //-- Thickness correction data for STDZ task ------------------------------
    [SerializableAttribute()]
    public class CorrectStruct
    {
        public SourceCorrectStruct S1 = new SourceCorrectStruct();
        public SourceCorrectStruct S2 = new SourceCorrectStruct();
    }
    //-- Source thickness correction data for STDZ task -----------------------
    [SerializableAttribute()]
    public class SourceCorrectStruct
    {
        //-- Hysteresis values calculated from open beam data of other source
        public int[] HystValues = new int[SIZE.RAW];
        //-- Wave correction values for Aluminium gauges
        public int[][] WaveData = new int[SIZE.CALPT][];
        public SourceCorrectStruct()
        {
            for (int i = 0; i < SIZE.CALPT; i++)
                WaveData[i] = new int[SIZE.RAW];
        }
        //-- Live standardize values
        public StdzDataStruct Stdz = new StdzDataStruct();
    }
    //-- Standardize task data ------------------------------------------------
    [SerializableAttribute()]
    public class StdzDataStruct
    {
        //public string Date
        public string Time;
        public int[] ZeroSigs = new int[SIZE.RAW];
        public int[] StdSigs = new int[SIZE.RAW];
        public int[] StdThicks = new int[SIZE.RAW];
        public int[] Offsets = new int[SIZE.RAW];
        public int CentreAvgOffset;
        public float CentreAvgZero;
        public int DiodeFailCount;
    }

    // These are classes as classes are reference types rather than value types
    // This is so that any copying of these classes will be shallow copying i.e.
    // after aCalConfigData is assigned to CalConfigDataR1 any changes to
    // CalConfigDataR1 will also be noted in aCalConfigData as they both
    // refer to the same data on the heap.
    [SerializableAttribute()]
    public class CalConfigData
    {
        public float fKvSetting;
        public float fMaSetting;
        public int NumCalPts;
        public bool SlowShutter;
        public int DetInt;
        public float[] SlopeAdjust = new float[12];
    }
}
//=================================================================================================
