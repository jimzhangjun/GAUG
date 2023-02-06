//=================================================================================================
//  Project:    SIPRO-library
//  Module:     RFMData.cs                                                                         
//  Author:     John Poulain && Andrew Powell
//  Date:       28/05/2008
//  
//  Details:    Definition of Refective Memory interface structures.
//  
//=================================================================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using GAUGlib;

namespace SUPERvisor.DataStructures
{
    //-- RFM Profile data array sizes -------------------------------------------------------------
    public class RFMSIZE
    {
        public const int PROF = 400;
        public const int HOTPROF = 400;
        public const int HEIGHT = 301;
        public const int OB = 20;
        public const int DIAGS = 18;
        public const int MAXDETS= 704;//Kumz
        public const int RIDGES = 5;
        public const int DETECTORS = 512;
    }
    //-- RFM conversions --------------------------------------------------------------------------
    public class RFMCONV
    {
        public const int THICK = 1000;
        public const int WIDTH = 10;
    }
    //-- RFM Board Configuration Data -------------------------------------------------------------
    public struct RFMConfig
    {
        public int nodeID;
        public int boardID;
        public int memoryOffset;
        public int dmaThreshold;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
        public char[] deviceName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
        public char[] driverVersion;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
        public char[] errorMsg;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
        public char[] DriverVersion;
    }
    //-- RFM THickness and Temperature Profile Data Structure -------------------------------------
    public class RFMProfileIN
    {
        public static RFMProfile inPROF = new RFMProfile();
    }
    public class RFMProfileOUT
    {
        public static RFMProfile outPROF = new RFMProfile();
    }
    public class RFMShapeIN
    {
        public static RFMShape inSHAPE = new RFMShape();
    }
    public class RFMShapeOUT
    {
        public static RFMShape outSHAPE = new RFMShape();
    }
    public class RFMtestIN
    {
        public static RFMTEST testIN = new RFMTEST();
    }
    public class RFMtestOUT
    {
        public static RFMTEST testOUT = new RFMTEST();
    }
    public struct RFMTEST
    {
        public int ProfAliveCount;
        public int ProfStripCount;
        public int ProfProductId;
        public int ProfAlloyName;
        public int ProfNomThick;
        public int ProfMeasCount;
        public int StripLength;
        public int ProfNomWidth;
        public int ProfHotNomThick;
        public int CLThick;
        public int HotWidth;
        public int ColdWidth;
        public int CLOffset;
        public int EdgePos1;
        public int EdgePos2;
        public int EdgeDropBE;
        public int EdgeDropOE;
        public int Crown1;
        public int Wedge1;
        public int Crown2;
        public int Wedge2;
        public int NumPoints;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.PROF)]
        public int[] ProfData;// = new int[400];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.PROF)]
        public int[] TempData;//  = new int[400];
        public int StripSpeed;
        public int DetTemp;
        public int DewPoint;
        public int TopArmTemp1;
        public int TopArmTemp2;
        public int TubeTemp1;
        public int TubeTemp2;
        public int AlloyComp;
        public int CentreComp;
        public int CentreTemp;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public char[] ProfProductId_STR;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] ProfAlloyName_STR;
    }
    //-- RFM THickness and Temperature Profile Data Structure -------------------------------------
    //[StructLayout(LayoutKind.Sequential)]
    public struct RFMProfile
    {
        public float ProfAliveCount;
        public float ProfStripCount;
        public float ProfProductId;
        public float ProfAlloyName;
        public float ProfNomThick;
        public float ProfMeasCount;
        public float StripLength;
        public float ProfNomWidth;
        public float ProfHotNomThick;
        public float CLThick;
        public float HotWidth;
        public float ColdWidth;
        public float CLOffset;
        public float EdgePos1;
        public float EdgePos2;
        public float EdgeDropBE;
        public float EdgeDropOE;
        public float Crown1;
        public float Wedge1;
        public float Crown2;
        public float Wedge2;
        public float NumPoints;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.PROF)]
        public int[] ProfData;// = new int[400];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.PROF)]
        public int[] TempData;//  = new int[400];
        public float StripSpeed;
        public float DetTemp;
        public float DewPoint;
        public float TopArmTemp1;
        public float TopArmTemp2;
        public float TubeTemp1;
        public float TubeTemp2;
        public float AlloyComp;
        public float CentreComp;
        public float CentreTemp;
    }
    //-- RFM Flatness and Shape Data Structure ----------------------------------------------------
    public struct RFMShape
    {
        public float ShapeCount;
        public float FirstZone;
        public float LastZone;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.SHAPE)]
        public float[] ShapeData;// = new float[SIZE.SHAPE];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.HEIGHT)]
        public float[] HeightData;// = new float[301];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.OB)]
        public float[] OpenBeamData;// = new float[20];              //Pobably not req'd. Old VME diags.
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.DIAGS)]
        public float[] DSPDiags;// = new float[18];                  //Pobably not req'd. Old VME diags.
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.SHAPE)]
        public int[] ThreadHeightData;// = new int[SIZE.SHAPE];      //Height data at Shape thread Positions
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.HOTPROF)]
        public int[] HotThickData;// = new int[400];                 //Note last two are int's!
    }
    //-- Structure to Read Data from RFM ----------------------------------------------------------
    public struct RFMRead
    {
        public UInt16 System;
        public UInt16 Meas;
        public UInt16 Error;
        public UInt16 Warning;
    }
    //-- RFM Gauge Status Data Structure ----------------------------------------------------------
    public struct RFMStatus
    {
        // See FRM spreadsheet for individual bit definitions
        public UInt16 SystemBits;
        public UInt16 MeasBits;
        public UInt16 ErrorBits;
        public UInt16 WarningBits;
    }

    public class RFMClass
    {
        //-- RFM Board Data -----------------------------------------------------------------------
        public static int RFMCardAddress = 3000000;         //RFM Card physical address
        public int RFMGaugeOffset = 0x0;                    //RFM Gauge Base Offset Defined by config       
        public int profileAddr;
        public int shapeAddr;
        public int statusAddr;
        //-- RFM Profile data ---------------------------------------------------------------------
        public static RFMProfile aRFMprofile = new RFMProfile();
        //-- RFM Shape Data  ----------------------------------------------------------------------
        public static RFMShape aRFMshape = new RFMShape();
        //-- RFM Status Data ----------------------------------------------------------------------
        public static RFMStatus aRFMstatus = new RFMStatus();
    }
    //=============================================================================================
    //=============================================================================================
    public class RFMClass_V2
    {
        //-- RFM Board Data ---------------------------------------------------
        public static int RFMCardAddress = 3000000;         //RFM Card physical address
        public int RFMGaugeOffset = 0x0;                    //RFM Gauge Base Offset Defined by config       
        public int setupAddr;
        public int statusAddr;
        public int fastMeasAddr;
        public int profileAddr;
        public int shapeAddr;
        public int diagAddr;

        public int RFMGaugeInputOffset = 3400000;                    //RFM Gauge Base Offset Defined by config       RFMGaugeInputOffset
        public int inSetupAddr;
        public int inControlAddr;
        public int inFastInputAddr;

        //-- RFM Setup data ----------------------------------------------------
        public static RFMSetup_V2 aRFMsetup = new RFMSetup_V2();
        //-- RFM Status Data --------------------------------------------------
        public static RFMStatus_V2 aRFMstatus = new RFMStatus_V2();
        //-- RFM FastMeas Data ------------------------------------------------
        public static RFMFastMeas_V2 aRFMfastmeas = new RFMFastMeas_V2();
        //-- RFM Profile data -------------------------------------------------
        public static RFMProfile_V2 aRFMprofile = new RFMProfile_V2();
        //-- RFM Shape Data  --------------------------------------------------
        public static RFMHeightShape_V2 aRFMheightshape = new RFMHeightShape_V2();
        ////-- RFM Status Data ------------------------------------------------
        public static RFMDiags_V2 aRFMdiags = new RFMDiags_V2();
        // RFM Input structures :
        public static RFMSetup_V2 aInRFMsetup = new RFMSetup_V2();
        public static RFMctrl_V2 aInRFMCtrl_V2 = new RFMctrl_V2();
        public static RFMFastInput_V2 aInRFMFastInput_V2 = new RFMFastInput_V2();
    }
    //=============================================================================================
    //-- RFM RFMSetup_V2Data Structure ------------------------------------------------------------
    public struct RFMSetup_V2
    {
        public int setupAliveCount;
        public int setupStripCount;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public char[] sCoilID;
        public float sNomThick;
        public float sNomThick2;
        public float sNomWidth;
        public float sVThinLim;
        public float sThinLim;
        public float sThickLim;
        public float sVThickLim;
        public float sStripTemp;
        public int sAlloyCompMode;
        public float sAlloyComp;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] sAlloyName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.SPECTRA)]
        public float[] sCompositionData;
        public float sMiscComp;
        public float sStripAngle;
        public float sTempComp;
        public float sProfSeq;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] sChargeNo;
        public float sInputAI;
        public int sSeqNum;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] sTempName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public char[] sMisc1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public char[] sMisc2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public char[] sMisc3;
        public float sNomThickHot;
        public float sNomWidthHot;
        
        public float HeadLength;
        public float TailLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.CWPOS)]
        public int[] CWEdgePos; // = new int[SIZE.CWPOS] { 100, 75, 50, 40, 25, 15 };
        public float TargetCrown;
        public float Crown_Tol_Upper;
        public float Crown_Tol_Lower;
        public float Target_Wedge;
        public float Wedge_Tol;
        public float Width_Tol;
        public float Strip_Temp_Tol;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.SHPOS)]
        public float[] XPOS_analogue_Shape_Height_Threads;// = new float[7];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public float[] sSpareFloats;// = new float[10];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public int[]   sSpareInts;// = new int[10];
    }

    //-- RFM RFMStatus_V2 Data Structure ----------------------------------------------------------
    public struct RFMStatus_V2
    {
        public int   statusAliveCount;
        public uint   SVStatus;
        public uint   M1Status;
        public uint   GulmayStatus;
        public uint   DetectorStatus;
        public int   SourceOPMode;// (NOB)
        public float S1KV;
        public float S2KV;
        public float S1mA;
        public float S2mA;
        //Digitals
        public int   DIO1;// (M1-SV)
        public int   DIO2;// (M1-SV)
        public int   DIO3;//  (M1-SV)
        public int   DIO4;//  (M1-SV)
        public int   DIO_A6_DIO16;
        public int   DIO_A7_DIO16;
        public int   GspecStatus;
        // Analogues
        public float StripSpeed;
        public float AGT;
        public float DetTemp;
        public float DewPoint;
        public float TopArmTemp1;
        public float TopArmTemp2;
        public float TubeTemp1;
        public float TubeTemp2;
        public float aMillPyroTemp;
        public float aMillStripAngle;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public float[] aMillAI;// = new float[8];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
        public char[] spareGaugeInfoString;
        public int ErrorNumber;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
        public char[] ErrorText;
        public int EventNumber;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public char[] SVEventText;
        // Add in the V2_DIO status Bytes
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] DIO1Status_V2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] DIO2Status_V2;
        // Add the Meas Params Scatter modes ot end of status for additional diag/debug
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
        public int stdzOffsetMode;
        public int waveCorrectMode;
        public int plateScatterMode;
        public int NRS1DiodeOE;
        public int NRS1DiodeBE;
        public int NRS2DiodeOE;
        public int NRS2DiodeBE;
    }
    //-- RFM RFMFastMeas_V2 Structure -------------------------------------------------------------
    //[StructLayout(LayoutKind.Sequential, Size = 236, Pack = 1, CharSet = CharSet.Ansi)]
    public struct RFMFastMeas_V2
    {
        public int fastMeasAliveCount;
        public float aFastCLThickness;
        public float aSlowCLThickness;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.CWPOS)]
        public float[] aOEThickness;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.CWPOS)]
        public float[] aBEThickness;
        public float aCLTemp;
        public float aHotWidth;
        public float aColdWidth;
        public float aCLStripOffset;
        public float aCentreHeight;
        public float aOEEdgeHeight;
        public float aBEEdgeHeight;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.SHPOS)]
        public float[] aShape;
        public float aFastCLThicknessHot;
        public float aSlowCLThicknessHot;
        public float aMaxThickness;
        public float aMaxThicknessPos;
        public float aSpareF1;
        public float aSpareF2;
        public int aSpareI1;
        public int aSpareI2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.CWPOS)]
        public float[] aOETemp;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.CWPOS)]
        public float[] aBETemp;
        public float StripLength_fast;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.SHPOS)]
        public float[] aHeight; 
        public float XPOS_aOEEdgeHeight;
        public float XPOS_aBEEdgeHeight;

    }
    //-- RFM RFMProfile_V2 Data Structure ---------------------------------------------------------
    public struct RFMProfile_V2
    {
        public int profAliveCount;
        public float MeasCLthick_Hot;
        public float MeasCLthick_Cold;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.CWPOS)]
        public float[] aOEThickness;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.CWPOS)]
        public float[] aBEThickness;
        public float MeasHotWidth;
        public float MeasColdWidth;
        public float MeasCLoffset;
        public float MeasCLTemp;
        public float AlloyComp;
        public float CentreComp;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.CWPOS)]
        public float[] MeasCrown;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.CWPOS)]
        public float[] MeasWedge;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (SIZE.CWPOS -1) )]
        public float[] BAEdgeDrops;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (SIZE.CWPOS -1) )]
        public float[] OEEdgeDrops;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.CWPOS)]
        public float[] MeasThermalCrowns;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.CWPOS)]
        public float[] MeasThermalWedge;
        public int NumPoints_hot;
        public float ProfileRes;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.PROF)]
        public float[] HotThicknessProfileData;// = new int[601];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.PROF)]
        public float[] TempData;// = new int[601];
        public int NumPoints_cold;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.PROF)]
        public float[] ColdThicknessProfile;// = new int[601];
        public float StripLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.RIDGES)]
        public float[] SignificantRidgeLocations;// = new int[5];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.RIDGES)]
        public float[] SignificantRidgeValues;// = new int[5];
    }
    //-- RFM RFMShape_V2 Structure ----------------------------------------------------------------
    public struct RFMHeightShape_V2
    {
        public int heightShapeAliveCount;
        public float CentreHeight;
        public float OE_Height;
        public float BA_Height;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.SHPOS)]
        public float[] aHeight;   // = new float[7];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.SHPOS)]
        public float[] aFlatness; // = new float[7];
        public float HeightThreadProfRes;
        public int Height_FirstZone;
        public int Height_LastZone;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.SHAPE)]
        public float[] HeightProfData;// = new int[601];
        public float ShapeThreadProfRes;
        public int Shape_FirstZone;
        public int Shape_LastZone;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIZE.SHAPE)]
        public float[] ShapeProfData;// = new int[601];
        public float StripLength;

    }
    //-- RFM RFMDiags_V2 Structure ----------------------------------------------------------------
    public struct RFMDiags_V2
    {
        public int diagsAliveCount;
        public int S1_OE_det;
        public int S1_BA_det;
        public int S2_OE_det;
        public int S2_BA_det;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.MAXDETS)]
        public int[] S1_Cal_Offsets;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.MAXDETS)]
        public int[] S2_Cal_Offsets;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.MAXDETS)]
        public int[] S1_Stdz_Zeros;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.MAXDETS)]
        public int[] S2_Stdz_Zeros;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.MAXDETS)]
        public int[] S1_Stdz_Zeros_diffs;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.MAXDETS)]
        public int[] S2_Stdz_Zeros_diffs;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.MAXDETS)]
        public int[] S1_Meas_Sigs;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.MAXDETS)]
        public int[] S2_Meas_Sigs;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.MAXDETS)]
        public float[] S1_Meas_Thick;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.MAXDETS)]
        public float[] S2_Meas_Thick;
    }

    //-- RFM RFMctrl_V2 Structure -----------------------------------------------------------------
    public struct RFMctrl_V2
    {
        public int ctrlAliveCount;
        public int ControlInput;
    }
    //-- RFM RFMDiags_V2 Structure ----------------------------------------------------------------
    public struct RFMFastInput_V2
    {
        public int fastInputAliveCount;
        public float StripSpeed;
        public float CLTemp;
        public float MillCLThick; //SP9421 mill CL gauge
        public float Spare1;
        public float Spare2;
        public float Spare3;
        public float Spare4;
        public float Spare5;
        public float Spare6;
    }
    //=============================================================================================
    //-- RFM RFMSetup_V3Data Structure ------------------------------------------------------------
    public class RFMtestOUT_V3
    {
        public static RFMTEST_V3 testOUT = new RFMTEST_V3();
    }
    public class RFMtestIN_V3
    {
        public static RFMTEST_V3 testIN = new RFMTEST_V3();
    }

    public struct RFMTEST_V3
    {
        public int ProfAliveCount;
        public int ProfStripCount;
        public int ProfProductId;
        public int ProfAlloyName;
        public int ProfNomThick;
        public int ProfMeasCount;
        public int StripLength;
        public int CastLength;
        public int RolledLength;
        public int CLThick;
        public int HotWidth;
        public int ColdWidth;
        public int CLOffset;
        public int EdgePos1;
        public int EdgePos2;
        public int EdgeDropBE;
        public int EdgeDropOE;
        public int Crown1;
        public int Wedge1;
        public int Crown2;
        public int Wedge2;
        public int NumPoints;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.PROF)]
        public int[] ProfData;// = new int[400];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.PROF)]
        public int[] TempData;//  = new int[400];
        public int StripSpeed;
        public int DetTemp;
        public int DewPoint;
        public int TopArmTemp1;
        public int TopArmTemp2;
        public int TubeTemp1;
        public int TubeTemp2;
        public int AlloyComp;
        public int CentreComp;
        public int CentreTemp;
        public int AverageWedge;
        public int CLPyroTemp;
        public int AverageCrown;
        public int S1OEDet;
        public int S1BADet;
        public int S2OEDet;
        public int S2BADet;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.DETECTORS)]
        public int[] S1CalOffsets;// = new int[512];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.DETECTORS)]
        public int[] S2CalOffsets;// = new int[512];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.DETECTORS)]
        public int[] S1StdzZero;// = new int[512];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.DETECTORS)]
        public int[] S2StdzZero;// = new int[512];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.DETECTORS)]
        public int[] S1MeasSigs;// = new int[512];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.DETECTORS)]
        public int[] S2MeasSigs;// = new int[512];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.DETECTORS)]
        public int[] S1MeasThick;// = new int[512];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RFMSIZE.DETECTORS)]
        public int[] S2MeasThick;// = new int[512];
    }
    //---------------------------------------------------------------------------------------------
}
    //-- RFM -> SUPER Analogue input data ----------------------------------------------------------
    namespace GAUGlib
    {
        [SerializableAttribute()]
        public class rfmFastInputDataClass
        {
            public float rfmStripSpeed;
            public float rfmCLTemp;
            public float rfmMillCLThick;
            public float rfmSpare1;
            public float rfmSpare2;
            public float rfmSpare3;
            public float rfmSpare4;
            public float rfmSpare5;
            public float rfmSpare6;
            //-- Shallow Copy using the IClonable interface
            public object Clone()
            {
                return this.MemberwiseClone();
            }
        }
    }
    //=================================================================================================

//=================================================================================================
