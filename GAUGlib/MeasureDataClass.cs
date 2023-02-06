//=================================================================================================
//  Project:    SIPRO-library
//  Module:     MeasDataClass.cs                                                                         
//  Author:     Andrew Powell
//  Date:       10/09/2008
//  
//  Details:    Definition of MEASUREMENT data structures
//  
//=================================================================================================
using System;
using System.Data;

namespace GAUGlib
{
    //-- Strip length class ---------------------------------------------------
    [SerializableAttribute()]
    public class StripLength
    {
        public enum SOURCE
        {
            AUTO,
            SV,
            DSP,
        }
        public int lengthSource = 1;
        public float[] length = new float[3];
    }
    //-- Diode profile data structure -----------------------------------------------
    [SerializableAttribute()]
    public class DiodeArray
    {
        public int scanNumber = 0;
        public int startIndex = 200;
        public int stopIndex = 400;
        public float[] data = new float[SIZE.RAW];
        public float[] coeffs = new float[SIZE.COEFF];
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- Profile data structure -----------------------------------------------
    [SerializableAttribute()]
    public class ProfileArray
    {
        public int scanNumber = 0;
        public int startIndex = 200;
        public int stopIndex = 400;
        public float[] data = new float[SIZE.PROF];
        public float[] coeffs = new float[SIZE.COEFF];
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- Raw temperature data structure ---------------------------------------
    [SerializableAttribute()]
    public class RawTemperatureArray
    {
        public int scanNumber = 0;
        public int startIndex = 200;
        public int stopIndex = 400;
        public float[] data = new float[SIZE.RAWTEMP];
        public float[] coeffs = new float[SIZE.COEFF];
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- Shape data structure -------------------------------------------------
    [SerializableAttribute()]
    public class ContourArray
    {
        public int startIndex;
        public int stopIndex;
        public float[] data = new float[SIZE.SHAPE];
        public float[] coeffs = new float[SIZE.COEFF];
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- Indivual source edge positions ---------------------------------------
    [SerializableAttribute()]
    public class EdgePosArray
    {
        public int s1_open = 0;
        public int s1_back = 0;
        public int s2_open = 0;
        public int s2_back = 0;
    }
    //-- Spatial data structure -----------------------------------------------
    [SerializableAttribute()]
    public class SpatialArray
    {
        public float openX = 0;
        public float openY = 0;
        public float backX = 0;
        public float backY = 0;
        public float centreX = 0;
        public float centreY = 0;
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- Measurement data structure maintained in XMDmeas ---------------------
    [SerializableAttribute()]
    public class MeasDataClass
    {
        public float aCLThickness;
        public float aCLThicknessHot;
        public float[] aOEThickness = new float[SIZE.CWPOS];
        public float[] aBEThickness = new float[SIZE.CWPOS];
        public float[] aWedge = new float[SIZE.CWPOS];
        public float[] aCrown = new float[SIZE.CWPOS];
        public ProfileArray thickProf = new ProfileArray();
        public DiodeArray s1ThkProf = new DiodeArray();
        public DiodeArray s2ThkProf = new DiodeArray();
        public float aWidth;
        public float aHotWidth;
        public float aCLStripOffset;
        public float aCLTemp;
        public float[] aOETemp = new float[SIZE.CWPOS];
        public float[] aBETemp = new float[SIZE.CWPOS];
        public float[] aThermalWedge = new float[SIZE.CWPOS];
        public float[] aThermalCrown = new float[SIZE.CWPOS];
        public ProfileArray tempProf = new ProfileArray();
        public float aCentreHeight;
        public float aOEEdgeHeight;
        public float aBEEdgeHeight;
        public float[] aHeight = new float[SIZE.SHPOS];
        public float[] aFlatness = new float[SIZE.SHPOS];
        public ContourArray contour = new ContourArray();
        public ContourArray shape = new ContourArray();
        public SpatialArray spatial = new SpatialArray();
        public float[] aOEThickPoly = new float[SIZE.CWPOS];
        public float[] aBEThickPoly = new float[SIZE.CWPOS];
        public float[] aWedgePoly = new float[SIZE.CWPOS];
        public float[] aCrownPoly = new float[SIZE.CWPOS];
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //---------------------------------------------------------------------------------------------
    //-- Coils data structure maintained in SIPROview ---------------------   
    [SerializableAttribute()]
    public class CoilProductClass
    {
        public string coilid;
        public int measmode;
        public float[] setpoint = new float[SIZE.MEAS];
        public float[] ptol = new float[SIZE.MEAS];
        public float[] mtol = new float[SIZE.MEAS];
        public float[] factor = new float[SIZE.MEAS];
        public float[] offset = new float[SIZE.MEAS];
        public int passNumber;
        public int lastPass;
        public int dataNumber;
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    [SerializableAttribute()]
    public class CoilMeasClass
    {
        public DateTime time;
        public float length;
        public float measThick;
        public int measStatus;
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    [SerializableAttribute()]
    public class CoilSummaryClass
    {
        public DateTime time;
        public int measmode;
        public string coilID;
        public float[] setpoint = new float[SIZE.MEAS];
        public float[] ptol = new float[SIZE.MEAS];
        public float[] mtol = new float[SIZE.MEAS];
        public float[] factor = new float[SIZE.MEAS];
        public float[] offset = new float[SIZE.MEAS];
        public float[] min = new float[SIZE.MEAS];
        public float[] max = new float[SIZE.MEAS];
        public float[] avg = new float[SIZE.MEAS];
        public float[] sigma = new float[SIZE.MEAS];
        public float[] cp = new float[SIZE.MEAS];
        public float[] cpk = new float[SIZE.MEAS];
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    [SerializableAttribute()]
    public class CoilProfClass
    {
        public bool updated = false;
        public int totalIndex = -1;
        public int updateIndex = -1;
        public int reqIndex = -1;

        public DateTime time;
        public string datatime;
        public int measmode;
        public float length;
        public float[] min = new float[SIZE.MEAS];
        public float[] max = new float[SIZE.MEAS];
        public float[] avg = new float[SIZE.MEAS];
        public float[] sigma = new float[SIZE.MEAS];
        public float[][] triple = new float[SIZE.MEAS][];
        public float[][] profile = new float[SIZE.MEAS][];

        public CoilProfClass()
        {
            for (int i = 0; i < SIZE.MEAS; ++i)
            {
                triple[i] = new float[SIZE.PROF];
                profile[i] = new float[SIZE.PROF];
            }
        }
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    [SerializableAttribute()]
    public class CoilDataClass
    {
        public bool updated = false;
        public int updateCount;
        public CoilProductClass product = new CoilProductClass();
        public CoilMeasClass[] data = new CoilMeasClass[SIZE.COILDATA];
        public CoilSummaryClass summary = new CoilSummaryClass();

        public CoilDataClass()
        {
            for (int i = 0; i < SIZE.COILDATA; ++i)
                data[i] = new CoilMeasClass();
        }
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //---------------------------------------------------------------------------------------------
}
//=================================================================================================
