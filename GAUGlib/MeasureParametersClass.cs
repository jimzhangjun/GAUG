//=================================================================================================
//  Project:    SIPRO-library
//  Module:     MeasureParametersClass.cs                                                                         
//  Author:     Andrew Powell
//  Date:       14/01/2011
//  
//  Details:    Definition of MEASUREMENT PARAMETERS data structures
//
//              Detail configuration parameters for the current set-up
//  
//=================================================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace GAUGlib
{
    //-- Defines the measurement configuration parameters data for the current measurement --------
    //-- Redefinition of the DSP C1 data class 
    [SerializableAttribute()]
    public class MeasureConfigClass
    {
        public int clineAverage = 100;
        public int[] cwAverage = { 35, 30, 25, 20, 15, 10 };
        public int[] cwEdgePos = { 100, 75, 50, 40, 25, 15 };
        public int[] shapePos = { -400, -200, -100, 0, 100, 200, 400 };
        public int displayAvgTime = 500;
        public int commsAvgTime = 1000;
        public bool lateralSmoothMode = false;
        public int thkPolyOrder = 6;
        public int edgeExclude = 2;
        public int tempPolyOrder = 4;
        public float polyFitWidthScale = 0;
        public int polyFitFilterCount = 2;
        public int polyFilterStdDevs = 3;
        public int signalClipLevel = 5000;
        public int extCalMode = 0;
        public int contResolution = 50;
        public int contPolyOrder;
        public int contEdgeExclude;
        public bool contCalcEnabled;
        public int numHeightPoints;
        public int shpHeightFitPolyOrder;
        public int shpCalcLength;
        public int shpThreadOPmode;
        public int shpSpare;
        public int shpDiagsMode;
        public bool shpCalcEnabled;
        public int pyroFail;
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- Defines the measurement compensation parameters data for the current measurement ---------
    //-- Redefinition of the DSP C2 data class 
    [SerializableAttribute()]
    public class MeasureCompensationClass
    {
        //-- Various cyclical modes           
        public int singleSource = 8;
        public int diagsMode = 0;
        public bool restartStrip = false;
        public bool contourMode = true;
        public bool profileOff = false;
        public bool measOff = true;
        public bool sledPresent = false;
        //-- Ancillary data
        public float internalStds = 0;
        public float stripSpeed = 0;
        public float stripAngle = 0;
        //-- Compensations
        public float alloyComp = 0;
        public float widthTempComp = 0;
        public float aGTComp = 0;
        public float[] tempComp = new float[SIZE.PROF];
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- Defines the measurement parameters data for the current measurement ----------------------
    //-- Redefinition of the DSP C4 data class 
    [SerializableAttribute()]
    public class MeasureParametersClass
    {
        public ScatterData SC = new ScatterData();
        //-- These fields are based on range
        public float heightMult;
        public float heightSlope;
        public float heightOffset;
        public float s1OpenEdgeDistFact;
        public float s1BackEdgeDistFact;
        public float s2OpenEdgeDistFact;
        public float s2BackEdgeDistFact;
        public float s1HysteresisPerc;
        public float s1ThickSlope;
        public float s1ThickOffset;
        public float s2HysteresisPerc;
        public float s2ThickSlope;
        public float s2ThickOffset;
        //-- These fields are new for signal edge scatter 
        //public int signalEdgeScatterMode;
        public int edgeScatterDist;
        public int WeightedEdges;
        //public float refThick;
        public EdgeScatterData s1OpenEdge = new EdgeScatterData();
        public EdgeScatterData s1BackEdge = new EdgeScatterData();
        public EdgeScatterData s2OpenEdge = new EdgeScatterData();       
        public EdgeScatterData s2BackEdge = new EdgeScatterData();
        //public float s1OpenEdgeOffset;
        //public float[] s1OpenEdgeCoeffs = new float[4];
        //public float s1BackEdgeOffset;
        //public float[] s1BackEdgeCoeffs = new float[4];
        //public float s2OpenEdgeOffset;
        //public float[] s2OpenEdgeCoeffs = new float[4];
        //public float s2BackEdgeOffset;
        //public float[] s2BackEdgeCoeffs = new float[4];
        //-- These fields are based on nominal thickness
        //public float EdgeDistExpF;
        //public float[] EdgeDistExp = new float[100];
        //public float S1OpenEdgeEMult;
        //public float S1BackEdgeEMult;
        //public float S2OpenEdgeEMult;
        //public float S2BackEdgeEMult;
        public float s1SlopeCorrFact;
        public float s2SlopeCorrFact;
        public float stripCorrFact;
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- Defines the calibration data for the current measurement ---------------------------------
    //-- Redefinition of the DSP C5 data class 
    [SerializableAttribute()]
    public class MeasureCalibrationClass
    {
        public int measCalPt = 0;
        public int numCalPts = 12;
        public CalDataStruct aCalibration = new CalDataStruct();
        public CorrectStruct aFactor = new CorrectStruct();
        //-- Problem with edge finding if signals change too much
        public int[] s1StdzZero = new int[SIZE.RAW];
        public int[] s2StdzZero = new int[SIZE.RAW];
        //-- Individual scan zero values for normalising
        public CalDataStruct aZeroData = new CalDataStruct();
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
//=================================================================================================