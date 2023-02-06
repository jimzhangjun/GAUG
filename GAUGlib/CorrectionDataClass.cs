//=================================================================================================
//  Project:    SIPRO-library
//  Module:     CorrectionDataClass.cs                                                                         
//  Author:     Andrew Powell
//  Date:       12/09/2008
//  
//  Details:    Definition of CORRECTION data structures
//  
//=================================================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace GAUGlib
{
    class CorrectionDataClass
    {
    }
    //-- Scatter configuration data -------------------------------------------
    [SerializableAttribute()]
    public class ScatterData
    {
        public int noiseRejectMode;
        public int badDetMode;
        public int hysteresisMode;
        public int edgeScatterMode;
        public int thickScatterMode;
        public int heightScatterMode;
        public int slopeScatterMode;
        public int stripScatterMode;
        public int tiltCorrectMode;
        public int contourCorrectMode;
        public int stdzOffsetMode = 0;
        public int waveCorrectMode = 0;
        public int plateScatterMode = 0;
        public int NRS1DiodeOE = 32;
        public int NRS1DiodeBE = 448;
        public int NRS2DiodeOE = 64;
        public int NRS2DiodeBE = 480;
        public int nrAverage = 100;
        public BadDetData badDets = new BadDetData();
        public float s1WaveScale = 1;
        public float s2WaveScale = 1;
        public RangeSData r1 = new RangeSData();
        public RangeSData r2 = new RangeSData();
        public RangeSData r3 = new RangeSData();
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- Individual range scatter data ----------------------------------------
    [SerializableAttribute()]
    public class RangeSData
    {
        public float heightMult;
        public float heightSlope;
        public float heightOffset;
        public float[] edgeNomThick = new float[4];       
        public float refThick;
        public int edgeScatterDist;
        public SourceSData s1 = new SourceSData();
        public SourceSData s2 = new SourceSData();
        public float[] stripCorrFact = new float[4];
        public float miscComp = 0;
        public int WeightedEdges;
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- Individual source scatter data ---------------------------------------
    [SerializableAttribute()]
    public class SourceSData
    {
        public float hysteresisPercOpenBeam;
        //public float[] OpenEdgeEMult = new float[4];
        //public float[] BackEdgeEMult = new float[4];
        //public float[] OpenEdgeDistFact = new float[4];
        //public float[] BackEdgeDistFact = new float[4];
        public float thickSlope;
        public float thickOffset;
        public float[] slopeCorrFact = new float[4];
        //-- Signal Edge Scatter Correction       
        public EdgeScatterData openEdge = new EdgeScatterData();
        public EdgeScatterData backEdge = new EdgeScatterData();
        //public float openEdgeOffset;
        //public float[] openEdgeCoeffs = new float[4];
        //public float backEdgeOffset;
        //public float[] backEdgeCoeffs = new float[4];
        //-- Plate scatter correction
        public float[] pscMulti = new float[SIZE.RAW];
        public float[] pscOffset = new float[SIZE.RAW];
    }
    //-- Edge scatter data for each edge --------------------------------------
    [SerializableAttribute()]
    public class EdgeScatterData
    {       
        public float thkFactor;
        public float edgeClearFactor;
        public float offset;
        public float[] coeffs = new float[4];
    }
    //-- Bad detector data storage --------------------------------------------
    [SerializableAttribute()]
    public class BadDetData
    {
        public int minSignal = 3600;
        public int maxSignal = 1025000;
        public int[] s1CfgBadDetList = new int[SIZE.RAW];
        public int[] s2CfgBadDetList = new int[SIZE.RAW];
        public int s1CfgBadDet = 0;
        public int s2CfgBadDet = 0;
        public int[] s1BadDetList = new int[SIZE.RAW];
        public int[] s2BadDetList = new int[SIZE.RAW];
        public int s1TotalBadDet = 0;
        public int s2TotalBadDet = 0;
    }
    //-- Temperature compensation class ---------------------------------------
    [SerializableAttribute()]
    public class TempClass
    {
        public int index = 0;
        public string name = "NONE    ";
        public int numPts = 2;
        public float[] temperature = new float[] { 600, 1200, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public float[] percComp = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    }
    //-- Plate scatter correction class ---------------------------------------
    [SerializableAttribute()]
    public class PlateScatterSrcData
    {

        public int[] infinity = new int[SIZE.RAW];
        public int[] zero = new int[SIZE.RAW];
        public int oeSampleIndex;
        public int beSampleIndex;
        public int oeValidIndex;
        public int beValidIndex;
        public int[] measRawSig = new int[SIZE.RAW];
        public float[] measNormSig = new float[SIZE.RAW];
        public float[] driftFactor = new float[SIZE.RAW];
        public float[] measDriftSig = new float[SIZE.RAW];
        public float[] trueSig = new float[SIZE.RAW];
        public float primaryOffset;
        public float primarySlope;

        public int[] plateSig = new int[SIZE.RAW];
        public int[] sledSig = new int[SIZE.RAW];
        public float[] factor = new float[SIZE.RAW];
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    [SerializableAttribute()]
    public class PlateScatterData
    {
        public PlateScatterSrcData s1 = new PlateScatterSrcData();
        public PlateScatterSrcData s2 = new PlateScatterSrcData();
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
//=================================================================================================
