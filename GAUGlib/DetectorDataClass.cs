//=================================================================================================
//  Project:    SIPRO-library
//  Module:     SetupDataClass.cs                                                                         
//  Author:     Andrew Powell
//  Date:       25/01/2010
//  
//  Details:    Definition of DETECTOR data structure
//  
//=================================================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace GAUGlib
{
    //-- Detector Signal Data Class -------------------------------------------
    [SerializableAttribute()]
    public class DetSignalClass
    {
        public int[] s1Data = new int[SIZE.RAW];
        public int s1ScanNo = 0;
        public int[] s2Data = new int[SIZE.RAW];
        public int s2ScanNo = 0;
        public int errorCount = 0;
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- Detector Control Data Class ------------------------------------------
    [SerializableAttribute()]
    public class DetControlClass
    {
        //-- Set-point values
        public int integration;
        public int gain;
        public int temperature;
        //-- Board addressing
        public int spAddr = 0;
        public int dhAddr;
        public int tcAddr;
        public int tmAddr = 0x7F;
    }
    //-- Detector Status Data Class -------------------------------------------
    [SerializableAttribute()]
    public class DetStatusClass
    {
        //-- Signal Processing parameters
        public int setInt;
        public string spVersion;
        public string spProduct;
        //-- Detector Head parameters
        public int[] setGain = new int[SIZE.DETMOD];
        public string[] dhVersion = new string[SIZE.DETMOD];
        public string[] dhProduct = new string[SIZE.DETMOD];        
        //-- Temperature Control parameters
        public float[] setTemp = new float[SIZE.DETMOD];
        public float[] measTemp = new float[SIZE.DETMOD];
        public float[] peltierV = new float[SIZE.DETMOD];
        public float[] peltierI = new float[SIZE.DETMOD];
        public float[] peltierMaxI = new float[SIZE.DETMOD];
        public string[] tcVersion = new string[SIZE.DETMOD];
        //-- Temperature Module parameters
        public float ambientTemp;
        public float humidity;
        public string tmVersion;
        //-- XDM03 legacy parameters
        public int[] xdm03Src = new int[SIZE.DETMOD];
        public int[] xdm03Int = new int[SIZE.DETMOD];
        public bool[] xdm03Temp = new bool[SIZE.DETMOD];
        //-- Misc
        public string flush;
    }
    //-- Detector Diagnostic Data Class ---------------------------------------
    [SerializableAttribute()]
    public class DetDiagsClass
    {
        public int NRS1DiodeOE;
        public int NRS1DiodeBE;
        public int NRS2DiodeOE;
        public int NRS2DiodeBE;
        public bool EdgesFound;
        public bool S1oeFound; // Added here for passing out to SiPRO-net for lost edge status.
        public bool S1beFound;
        public bool S2oeFound;
        public bool S2beFound;
        public int s1oe;
        public int s1be;
        public int s2oe;
        public int s2be;
        public bool SiPROMeas_GHOST_MEASURING;
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-------------------------------------------------------------------------
}
//=================================================================================================
