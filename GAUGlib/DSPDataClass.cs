//=================================================================================================
//  Project:    XMD-lib
//  Module:     DSPDataClass.cs from DSPhost.cs                                                                        
//  Author:     Andrew Powell
//  Date:       02/05/2006
//  
//  Details:    Definition of DSP interface structures
//  
//=================================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace XMD_lib
{
    public class DSPOffset
    {
        //-- SHARED MEMORY BOUNDARY OFFSETS -----------------------------------
        public const int HeaderSize = 2;     //Integer offset for start of data
        public static int DSP1 = 0x0;        //DSP1 SharedMemory location offset
        public static int DSP2 = 0x40;
        public static int DSP3 = 0x400;
        public static int DSP4 = 0x800;
        public static int DSP5 = 0x1400;
        public static int DSP6 = 0x2000;
        public static int C1 = 0x4000;       //C1 SharedMemory location offset
        public static int C2 = 0x4040;
        public static int C3 = 0x4080;
        public static int C4 = 0x4400;
        public static int C5 = 0x4C00;
        public static int C5_S1 = 0x4C10;
        public static int C5_S2 = 0x7910;
    }

    //-- DSP header data ------------------------------------------------------
    [SerializableAttribute()]
    public struct DSPHeader
    {
        public uint Align;        
        public int Size;                  
    }
    //-- XDM04 data capture definition
    [Serializable]
    public class DetUnitData
    {
        public string aTimeStamp = "";
        public int aDetSigsSource = 0;
        public float[] aDetSigs = new float[32];
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- PROCESSOR INFO -------------------------------------------------------
    [SerializableAttribute()]
    public class DSP1
    {
        public DSPHeader Header;
        public uint AlignCode = 0xDEADC0DE;         //D=DSP 0xDEC0DED1
        public int DataSize = 0;                    //D2=64bytes
        public int dsp1_healthy;                    //processor healthy
        public int dsp1_program_id;                 //processor primary version
        public int dsp1_program_id2;                //processor secondary version
        public int copyright_year1;                 //processor Copyright year 
        public int dsp2_healthy;                    //processor healthy
        public int dsp2_program_id;                 //processor primary version
        public int dsp2_program_id2;                //processor secondary version
        public int copyright_year2;                 //processor Copyright year 
        public int dsp3_healthy;                    //processor healthy
        public int dsp3_program_id;                 //processor primary version
        public int dsp3_program_id2;                //processor secondary version
        public int copyright_year3;                 //processor Copyright year 
        public int dsp4_healthy;                    //processor healthy
        public int dsp4_program_id;                 //processor primary version
        public int dsp4_program_id2;                //processor secondary version
        public int copyright_year4;                 //processor Copyright year 
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- FAST ANALOGUE UDP DATA -----------------------------------------------
    [SerializableAttribute()]
    public class DSP2
    {      
        public uint AlignCode = 0xDEADC0DE;        //D=DSP 0xDEC0DED8
        public int DataSize = 0;                   //D8=0bytes
        //-- The analogs are now self contained.....
        //public AnalogDataStruct Analog = new AnalogDataStruct();
        //-- Memory Map V3.12
        public float aGuaranteedCLThicknessHot = 0f;
        public float aGuaranteedCLThicknessCold = 0f;
        public int aS1OpenEdgeMissing = 0;
        public int aS1BackEdgeMissing = 0;
        public int aS2OpenEdgeMissing = 0;
        public int aS2BackEdgeMissing = 0;
        public int aOpenEdgePinned = 0;
        public int aBackEdgePinned = 0;
        //-- Memory Map V3.13
        public DetUnitData aDetData = new DetUnitData();
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- SLOW AVERAGE PROFILE TCP/IP DATA -------------------------------------
    [SerializableAttribute()]
    public class DSP3
    {      
        public uint AlignCode = 0xDEADC0DE;        //D=DSP 0xDEC0DED9
        public int DataSize = 0;                   //D9=0bytes
        public ProfileDataStruct ProfileData = new ProfileDataStruct();
        //public float[] SpatialData = new float[SIZE.SPAT];
        public int ClipUpperCount;
        public int ClipLowerCount;
        public float StripLength;
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- DSP4 RAW SIG DATA ----------------------------------------------------
    [SerializableAttribute()]
    public class DSP4
    {
        public uint AlignCode = 0xDEADC0DE;
        public int DataSize = 0;
        public int[] S1SignalData = new int[SIZE.RAW];
        public int[] S2SignalData = new int[SIZE.RAW];
        public int[] S1XAData = new int[SIZE.RAW];
        public int[] S2XAData = new int[SIZE.RAW];
        public int ScanNo;
        public int Err1Count;
        public int Err2Count;
        public int[] DetStatus = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public int[] DetTemp = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public int[] DetIntTime = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public int S1OEdgeDet;
        public int S1BEdgeDet;
        public int S2OEdgeDet;
        public int S2BEdgeDet;
        public float S1OEdgePos;
        public float S1BEdgePos;
        public float S2OEdgePos;
        public float S2BEdgePos;
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- MCBSP DIAG SERIAL DATA -----------------------------------------------
    [SerializableAttribute()]
    public class DSP5
    {       
        public uint AlignCode = 0xDEADC0DE;        //D=DSP 0xDEC0DED7
        public int DataSize = 0;                   //D7=0bytes
        public int MCBSPvalid;
        public int[] MCBSPchannel0 = new int[531];
        public int[] MCBSPchannel1 = new int[531];
        //Statistics on averaged source signal data DSP3 & DSP4
        public string AvgDesc;
        public float S1mean;
        public int S1numSamples;
        public float S1sigMin;
        public float S1sigMax;
        public float S1sigStdDev;
        public float S2mean;
        public int S2numSamples;
        public float S2sigMin;
        public float S2sigMax;
        public float S2sigStdDev;
        public float thickMean;
        public int thickNumSamples;
        public float thickMin;
        public float thickMax;
        public float thickStdDev;
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- DSP diagnostic check point data --------------------------------------
    [Serializable]
    public class DSP6 : ICloneable
    {
        public uint AlignCode = 0xDEADC0DE;
        public int DataSize = 0;
        public int procIdA;
        public int callCountA;
        public int[] dspCheckPointA = new int[16];
        public int procIdB;
        public int callCountB;
        public int[] dspCheckPointB = new int[16];
        public int procIdC;
        public int callCountC;
        public int[] dspCheckPointC = new int[16];
        public int procIdD;
        public int callCountD;
        public int[] dspCheckPointD = new int[16];
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- C1 MAIN CONFIG UPDATES ON READIN -------------------------------------
    [SerializableAttribute()]
    public class C1
    {
        public uint AlignCode = 0xDEC0DEC1;            //C=SUPERvisor 
        public int DataSize = 36;
        //Geometry
        public GeometryCfgDataClass Geometry = new GeometryCfgDataClass();
        public float GeomSpare;
        public int ClineAverage = 100;
        public int[] CWAverage = { 35, 30, 25, 20, 15, 10 };
        public int[] CWEdgePos = { 100, 75, 50, 40, 25, 15 };
        public int[] ShapePos = { -400, -200, -100, 0, 100, 200, 400 };
        public int SlowThickAveTime = 100;
        public int StripProfileAveTime = 200;
        public int OpSpare;
        public int MainDiagsOption;
        public int LateralSmoothMode = 0;
        public int LateralSmoothAvg = 15;
        public int ThickPolyOrder = 6;
        public int SignalClipLevel = 5000;
        public int ExtCalMode = 0;
        public int ThickSpare1;
        public int ThickSpare2;      
        public int ThickDiagsOption;
        public int ContPolyOrder;
        public int ContEdgeExclusion;
        public int ContSpare;
        public int ContSpare1;
        public int ContSpare2;
        public int ContSpare3;
        public int ContSpare4;
        public int ContDiagsOption;
        public int NumHeightPoints;
        public int ShpHeightFitPolyOrder;
        public int ShpNumElementsToOP;
        public float ShpElementLength;
        public int ShpThreadOPmode;
        public int ShpOpPolyOrder;
        public int ShpSpare;
        public int ShpDiagsMode;
    }
    //-- C2 -------------------------------------------------------------------
    [SerializableAttribute()]
    public class C2
    {
        public uint AlignCode = 0xDEC0DEC2;            //C=SUPERvisor 
        public int DataSize = 36;
        public int RestartStrip = 0;
        public int SingleSource = 0;
        public int ContourMode = 0;
        public int DiagsMode = 0;
        public int ProfileOff = 0;
        public float StripSpeed = 0;
        public float StripAngle = 0;
        public float SpareAI1 = 0;
        public float SpareAI2 = 0;
        public float AlloyComp = 0;
        public float WidthTempComp = 0;
        public float AGTComp = 0;
        public float SpareFloat2 = 0;
        public float[] TempComp = new float[SIZE.PROF];
    }
    //-- C3 -------------------------------------------------------------------
    [SerializableAttribute()]
    public class C3
    {       
        public uint AlignCode = 0xDEC0DEC3;            //C=SUPERvisor 
        public int DataSize = 36;
    }
    //-- C4 SCATTER PARAMETERS ------------------------------------------------
    [SerializableAttribute()]
    public class C4
    {       
        public uint AlignCode = 0xDEC0DEC4;            //C=SUPERvisor 
        public int DataSize = 5268;
        public ScatterData SC = new ScatterData();
        //-- These fields are based on range
        public float HeightMult;
        public float HeightSlope;
        public float HeightOffset;
        public float S1OpenEdgeDistFact;
        public float S1BackEdgeDistFact;
        public float S2OpenEdgeDistFact;
        public float S2BackEdgeDistFact;
        public float S1HysteresisMult;
        public float S1ThickSlope;
        public float S1ThickOffset;
        public float S2HysteresisMult;
        public float S2ThickSlope;
        public float S2ThickOffset;
        //-- These fields are new for signal edge scatter 
        public int signalEdgeScatterMode;
        public float s1OpenEdgeScale;
        public float[] s1OpenEdgeCoeffs = new float[4];
        public float s1BackEdgeScale;
        public float[] s1BackEdgeCoeffs = new float[4];
        public float s2OpenEdgeScale;
        public float[] s2OpenEdgeCoeffs = new float[4];
        public float s2BackEdgeScale;
        public float[] s2BackEdgeCoeffs = new float[4];
        //-- These fields are based on nominal thickness
        public float EdgeDistExpF;
        public float[] EdgeDistExp = new float[100];
        public float S1OpenEdgeEMult;
        public float S1BackEdgeEMult;
        public float S2OpenEdgeEMult;
        public float S2BackEdgeEMult;
        public float S1SlopeCorrFact;
        public float S2SlopeCorrFact;
        public float StripCorrFact;
    }
    //-- C5 MEAS PARAMS RECORD ------------------------------------------------
    [SerializableAttribute()]
    public class C5
    {
        
        public uint AlignCode = 0xDEC0DEC5;            //C=SUPERvisor 
        public int DataSize = 36;
        public float ApparentNominal = 5;
        public int NumCalPts = 12;
        public CalDataStruct aCalibration = new CalDataStruct();
        public CorrectStruct aFactor = new CorrectStruct();
        //Problem with edge finding if signals change too much
        public int[] S1EdgeFindZero = new int[SIZE.RAW];
        public int[] S2EdgeFindZero = new int[SIZE.RAW];
    }
    //-- DSP Diagnostic Data Class ----------------------------------------------------------------
    public class DSPDiagsClass
    {
        //-- Serialise data class and save to file ------------------------------------------------
        public void SerializeData(string filePath)
        {
            FileStream fStream = File.Create(filePath);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(fStream, archiveData);
            fStream.Close();
        }
        //-- Read data class from file and deserialize --------------------------------------------
        public void DeserializeData(string filePath)
        {
            FileStream fStream = File.Open(filePath, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            archiveData = (ArchiveDataClass)bFormatter.Deserialize(fStream);
            fStream.Close();
        }
        public ArchiveDataClass archiveData = new ArchiveDataClass();
    }

    //-- Data class to archive --------------------------------------------------------------------
    [Serializable]
    public class ArchiveDataClass
    {
        public string aTime;
        public DSP1 dDSP1 = new DSP1();
        public DSP2[] dDSP2 = new DSP2[1000];
        public DSP3[] dDSP3 = new DSP3[100];
        public DSP4[] dDSP4 = new DSP4[100];
    }
    //---------------------------------------------------------------------------------------------
}
//=================================================================================================
