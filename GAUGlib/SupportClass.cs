//=================================================================================================
//  Project:    SIPRO-library
//  Module:     SupportClass.cs                                                                     
//  Author:     Andrew Powell
//  Date:       20/07/2011
//  
//  Details:    General classes without a specifc home!?!
//  
//
//=================================================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace GAUGlib
{
    //-- Profile data array sizes ---------------------------------------------
    public class SIZE
    {
        public const int PROF = 601;
        public const int RAW = 704; //-- Increased for Kumz SP9593
        public const int SHAPE = 51;
        public const int ELEMENTS = 25;
        public const int COEFF = 7;
        public const int DETMOD = 22; //-- Increased for Kumz SP9593
        public const int DIODE = 32;
        public const int STDS = 10;
        public const int CALPT = 12;
        public const int CWPOS = 6;
        public const int SHPOS = 7;
        public const int SPAT = 6;
        public const int INTERP = 5;
        public const int RAWTEMP = 2000;
        public const int SPECTRA = 32;
        public const int COILDATA = 2000;
        public const int TRIPLE = 3;        public const int MEAS = 2;

        public const int INTEGERARRAY = 10;    //-- max size of the int array  
        public const int FLOATARRAY = 10;    //-- max size of the float array        
        public const int BOOLARRAY = 10;    //-- max size of the bool array        
    }

    //-- Structure for fast UDP analogue data ---------------------------------
    [SerializableAttribute()]
    public class AnalogDataStruct
    {
        public float aFastCLThickness;
        public float aSlowCLThickness;
        public float[] aOEThickness = new float[SIZE.CWPOS];
        public float[] aBEThickness = new float[SIZE.CWPOS];
        public float aCLTemp;
        public float aHotWidth;
        public float aColdWidth;
        public float aCLStripOffset;
        public float aCentreHeight;
        public float aOEEdgeHeight;
        public float aBEEdgeHeight;
        public float[] aShape = new float[SIZE.SHPOS];
        public float aFastCLThicknessHot;
        public float aSlowCLThicknessHot;
        public float aMaxThickness;
        public float aMaxThicknessPos;
        public float aSpareF1;
        //V2 UDP data below here
        public float aSpareF2;
        public int aSpareI1;
        public int aSpareI2;
        public float[] aOETemp = new float[SIZE.CWPOS];
        public float[] aBETemp = new float[SIZE.CWPOS];
    }

    //-- Averaged profile measurement telegram --------------------------------
    [SerializableAttribute()]
    public class ProfileDataStruct
    {
        public float CentreThick;
        public ProfileArray ThickProf = new ProfileArray();
        public float Width;
        public float CentreTemp;
        public ProfileArray TempProf = new ProfileArray();
        public ContourArray Contour = new ContourArray();
        public ContourArray Shape = new ContourArray();
        public float[] OEdgeThk = new float[SIZE.CWPOS];
        public float[] BEdgeThk = new float[SIZE.CWPOS];
        public float[] Spatial = new float[SIZE.SPAT];
    }

}
//=================================================================================================
