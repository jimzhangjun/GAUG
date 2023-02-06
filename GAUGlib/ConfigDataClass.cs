//=================================================================================================
//  Project:    SIPRO-library
//  Module:     ConfigDataClass.cs                                                                         
//  Author:     Andrew Powell
//  Date:       11/09/2008
//  
//  Details:    Definition of CONFIG data structures 
//  
//=================================================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace GAUGlib
{
    //-- Geometry config data store -------------------------------------------
    [SerializableAttribute()]
    public class GeometryCfgDataClass
    {
        public float sourceHeight;
        public float sourceToCline;
        public float passlineHeight;
        public float detSize;
        public int numDetectors;
        public int clineDetNo;
        //public int serialDMA0; //-- Obsolete
        //public int serialDMA1; //-- Obsolete
        public int xdmType;
    }
    //-- Spec Limits config data store -------------------------------------------
    [SerializableAttribute()]
    public class SpecLimitsCfgDataClass
    {
        public float maxWidth;
        public float maxThick;
        public float minThick;
        public float maxLift;
        public float maxOffset;
        public Boolean disableValidation;
    }

    //-- Gauging config data store --------------------------------------------
    [SerializableAttribute()]
    public class GaugingCfgDataClass
    {
        public bool useExternalCal = false;
        public bool usePscExtCal = false;
        public int detIntTime = 2;
        public int detTemperature = 5;
        public int autoRangeSelect = 0;
        public int autoSingleSource = 0;
        public int noiseRejectStyle = 0;
        public int interpolateStdzOffsets = 1;
    }
    //-- External calibration config data store -------------------------------
    [SerializableAttribute()]
    public class CalCfgDataClass
    {
        public float CaljigHeight;
        public float ShieldWidth;
        public float CalSimDets;
        public int SignalClipLevel;
        public int CalStdAvgTime;
        public int StdzStdAvgTime;
        public float R1R2Change;
        public float R2R3Change;
        public int KeepShutterOpen;
        public int calDueTime;
        public int stdzDueTime;
        public int ShutterOpenTimeout_sec;
        public int PercSignalChange;
        public int[] AbsThickChange = new int[3];
        public int LimitDiodeFail;
        public int ShutterWaitTime;
        public int StdWaitTime;
        public int XraySettleTime;
        public int s1DiodeLimitOE = 0;
        public int s1DiodeLimitBE = 447;
        public int s2DiodeLimitOE = 1;
        public int s2DiodeLimitBE = 446;
    }
    //-- Profile config data store --------------------------------------------
    [SerializableAttribute()]
    public class ProfileCfgDataClass
    {
        public int ClineAverage;
        public int[] CWAverage = new int[SIZE.CWPOS];
        public int[] CWEdgePos = new int[SIZE.CWPOS] { 125, 100, 75, 50, 40, 25 };
        public int displayAvgTime;
        public int commsAvgTime;
        public bool smoothMode;
        public int thkPolyOrder;
        public int edgeExclude;
        public int tempPolyOrder;
        public float polyFitWidthScale;
        public int polyFitFilterCount;
        public int polyFilterStdDevs;
    }
    //-- Contour config data store --------------------------------------------
    [SerializableAttribute()]
    public class ContourCfgDataClass
    {
        public int dataResolution;
        public int polyOrder;
        public int edgeExclude;        
        public int[] shapePos = new int[SIZE.SHPOS];
        public bool calcEnabled;
    }
    //-- Shape config data store ----------------------------------------------
    [SerializableAttribute()]
    public class ShapeCfgDataClass
    {
        public int numberHeightPoints;
        public int heightPolyOrder;
        public int calcLength;
        public int outputMode;
        public bool calcEnabled;
    }
    //-- Pyrometer config data store ------------------------------------------
    [SerializableAttribute()]
    public class PyroCfgDataClass
    {
        public int PyroEnabled = 0;
        public int PyroMinV = 600;
        public int PyroMaxV = 1200;
        public int PyroFail = 650;
        public float PyroScale = 1.35f;
        public int Reverse = 0;
    }
    //-- AGT config data store ------------------------------------------------
    [SerializableAttribute()]
    public class AgtCfgDataClass
    {
        public int Enabled = 0;
        public int Distance = 2500;
        public float Factor = 0.00028f;
        public float AgtRef = 25;
        public float AgtDiff = 0;
        public float DSPFactor = 0;
    }
    //-- Network config data store --------------------------------------------
    [SerializableAttribute()]
    public class NetworkCfgDataClass
    {
        public int UDPMsgVersion = 1;
        public int TCP1MsgVersion = 1;
        public int TCP2MsgVersion = 1;
        public int TCP3MsgVersion = 1;
        public int ProfTxRateM1 = 1000;
        public bool usePolyEdgeThick = false;
    }
    //-- RFM config data store ------------------------------------------------
    [SerializableAttribute()]
    public class RFMCfgDataClass
    {
        public int enabled = 0;
        public int memOffset = 500000;
        public int statusRate = 1000;
        public int profileRate = 2000;
        public int shapeRate = 4000;
        public int revProfile = 0;
        public int edgePos1 = 0;
        public int edgePos2 = 1;
        public int version = 1;
        public int setupDataRate = 1000;
        public int fastMeasRate = 40;
        public int diagRate = 100;
        public int memInputOffset = 3400000;
        public int dmaThreshold = 0;
        public int rfmSetupCtrlMode = 0;
        public int rfmUseMillPyro = 0;
        public int rfmUseMillSpeed = 0;
        public int rfmUseMillThick = 0;
    }
    //-- XMD config data store ------------------------------------------------
    [SerializableAttribute()]
    public class XMDCfgDataClass
    {
        public int enabled = 0;
        public bool autoConnect = false;
        public int remotePort = 9001;
        public bool startXMDdet = false;
        public bool startXMDmeas = false;
        public bool startXMDpyro = false;
        public bool startXMDnet = false;
        public bool startSiPROreport = false;
    }
    //-- IO config data store -------------------------------------------------
    [SerializableAttribute()]
    public class IOCfgDataClass
    {
        public int version = 1;
        public bool SiPRODriveEnable = false;
    }
    //-- Diagnostics config data store ----------------------------------------
    [SerializableAttribute()]
    public class DiagsCfgDataClass
    {
        public string calFileName = "CALSCAN";
        public int calSuffixMode = 0;
        public string stdzFileName = "STDZDIAGS";
        public int stdzSuffixMode = 0;
        public string mdiagsFileName = "MDIAGS";
        public int mdiagsSuffixMode = 0;
        public int mdiagsInterval = 10000;
        public int mdiagsAvgCount = 20;
        public int mdiagsDataRows = 600;
        public string stripsFileName = "STRIPS";
        public int stripsSuffixMode = 0;
        public int stripsInterval = 10000;
        public int stripsFileSize = 500000;
        public bool stripsBootEnable = false;
        public string dspFileName = "DSP";
        public int dspSuffixMode = 0;
        public int dspArraySize = 100;
        public string analogFileName = "ANALOG";
        public int analogSuffixMode = 0;
        public int analogInterval = 60000;
        public int analogFileSize = 500000;
        public bool analogBootEnable = false;
        public string perfFileName = "PERFORMANCE";
        public int perfSuffixMode = 0;
        public int perfAverageCount = 20;
        public int perfInterval = 1000;
        public int perfFileSize = 1024000;
        public string ibaFileName = "IBA";
        public int ibaSuffixMode = 0;
        public int ibaInterval = 20;
        public int ibaFileSize = 500000;
        public string repoFileName = "REPOSITORY";
        public int repoSuffixMode = 0;
        public int repoBootMode = 0;
        public bool repoBootEnable = false;
        public int repoMaxQueue = 5001;
        public int repoMaxFiles = 3;
    }
    //=========================================================================
}
//================================================================================================================
