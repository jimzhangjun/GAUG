//=================================================================================================
//  Project:    SIPRO-library
//  Module:     PyroData.cs                                                                         
//  Author:     Andrew Powell
//  Date:       04/01/2007
//  
//  Details:    Definition of pyrometer temperature data types 
//  
//=================================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GAUGlib
{
    //-- Pyrometer data class 
    [SerializableAttribute()]
    public class PyroDataClass
    {
        public enum SOURCE
        {
            SETUP,
            LANDSCAN,
            MILL,          
        }
        public TempClass tempLUT = new TempClass();
        public ProfileArray tempProf = new ProfileArray();
        public float[] tempComp = new float[SIZE.PROF];
        //public float[] unscaledTempProf = new float[SIZE.PROF];
        public int pyroSource = 0;
        //-- Width comp is half thickness comp as only in one dimension and opposite sign
        public float widthTempComp = 1;
        public float centreTemp = 0;
        public float centreComp = 0;
        //public int pyroFail = 650;
        public int centreOffset;
        //-- Additional diag incorporated at Beta Steel
        public int buffSize = 10000;
        //-- Ethernet Landscan data 
        public RawTemperatureArray netTempProf = new RawTemperatureArray();
        //-- Profile edge values
        public float[] oETemp = new float[SIZE.CWPOS];
        public float[] bETemp = new float[SIZE.CWPOS];
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- Data structure for analogue pyrometer conversion -----------------------------------------
    public struct ADCdata
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 670)]
        public short[] voltData;
        public int numPts;
        public int diagCount1;
        public int diagCount2;
    }
    //=============================================================================================
}
