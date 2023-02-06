//=================================================================================================
//  Project:    RM312/SIPRO SUPERvisor Data Server for Remote Applications
//  Module:     InterfaceClass.cs                                                                         
//  Author:     Andrew Powell
//  Date:       15/08/2008
//  
//  Details:    Global data storage and remote application interface for XMD data distribution
//
//              Global data variables are accessed directly by Applications running on the MEC 
//              console PC - these variables adopt the following nomenclature prefixes:
//              
//                  SETup Data      set
//                  ConFiG Data     cfg
//                  STATus Data     stat
//                  MEASure Data    meas
//                  Analog IO Data  aio
//                  Digital IO Data dio
//                  PYROmeter Data  pyro
//
//              Remote applications access the data through the interface functions
//
//=================================================================================================
using System;
using M1ComNET;

using GAUGlib;

namespace GAUGdata
{
    public class XMDdata : MarshalByRefObject, remoteInterface
    {   
        //---------------------------------------------------------------------
        //-- Declaration of remote interface variables
        //---------------------------------------------------------------------
        //-- M1 data variables --------------------- 
        public static BoolClass bValue = new BoolClass();
        public static IntegerClass iValue = new IntegerClass();
        public static FloatClass fValue = new FloatClass();
        public static StringClass sValue = new StringClass();
        public static BoolArrayClass baValue = new BoolArrayClass();
        public static FloatArrayClass faValue = new FloatArrayClass();
        //-- SETup data variables ---------------------------------------------
        public static SetupData setPrevious = new SetupData();
        public static SetupData setCurrent = new SetupData();
        public static SetupData setNext = new SetupData();
        //-- STATus data variables --------------------------------------------
        public static SuperStatus statSV = new SuperStatus();
        public static M1Status statM1 = new M1Status();
        public static GulmayStatus statGUL = new GulmayStatus();
        //-- ConFiG data variables --------------------------------------------
        public static GeometryCfgDataClass cfgGeometry = new GeometryCfgDataClass();
        public static SpecLimitsCfgDataClass cfgSpecLimits = new SpecLimitsCfgDataClass();
        public static GaugingCfgDataClass cfgGauging = new GaugingCfgDataClass();
        public static CalCfgDataClass cfgCalibration = new CalCfgDataClass();
        public static ProfileCfgDataClass cfgProfile = new ProfileCfgDataClass();
        public static ContourCfgDataClass cfgContour = new ContourCfgDataClass();
        public static ShapeCfgDataClass cfgShape = new ShapeCfgDataClass();
        public static PyroCfgDataClass cfgPyro = new PyroCfgDataClass();
        public static AgtCfgDataClass cfgAGT = new AgtCfgDataClass();
        public static NetworkCfgDataClass cfgNetwork = new NetworkCfgDataClass();
        public static RFMCfgDataClass cfgRFM = new RFMCfgDataClass();
        public static DiagsCfgDataClass cfgDiags = new DiagsCfgDataClass();
        public static XMDCfgDataClass cfgXMD = new XMDCfgDataClass();
        public static IOCfgDataClass cfgIO = new IOCfgDataClass();
        //-- DETector data variables ------------------------------------------
        public static DetControlClass detControl= new DetControlClass();
        public static DetStatusClass detStatus = new DetStatusClass();
        public static DetSignalClass detSignal = new DetSignalClass();
        public static DetDiagsClass detDiags = new DetDiagsClass();
        //-- CALibration data variables ---------------------------------------
        public static CalDataStruct calData = new CalDataStruct();
        public static CalConfigStruct calCfg = new CalConfigStruct();
        //-- PYRO data variables ----------------------------------------------
        public static PyroDataClass pyroData = new PyroDataClass();
        //-- MEASurement data variables ---------------------------------------
        public static MeasureConfigClass measConfig = new MeasureConfigClass();
        public static MeasureCalibrationClass measCal = new MeasureCalibrationClass();
        public static MeasureParametersClass measParams = new MeasureParametersClass();
        public static MeasureCompensationClass measComp = new MeasureCompensationClass();
        public static StripLength measLength = new StripLength();
        public static MeasDataClass measFiveRaw = new MeasDataClass();
        public static MeasDataClass measDisplayAvg = new MeasDataClass();
        public static MeasDataClass measCommsAvg = new MeasDataClass();
        public static MeasDataClass measTrend = new MeasDataClass();
        public static CoilDataClass measGauge1 = new CoilDataClass();   // Top
        public static CoilDataClass measGauge2 = new CoilDataClass();   // Bottom
        public static CoilDataClass measGauge3 = new CoilDataClass();   // Total    
        public static CoilProfClass profGauge1 = new CoilProfClass();   // Top
        public static CoilProfClass profGauge2 = new CoilProfClass();   // Bottom
        public static CoilProfClass profGauge3 = new CoilProfClass();   // Total    
        //-- ANAlog data variables --------------------------------------------
        public static AnalogInputClass anaInput = new AnalogInputClass();
        public static AnalogInputClass analogInput
        {
            get
            {
                return anaInput;
            }
            set
            {
                anaInput = value;
                repoMan.StoreAnalog(value);
            }
        }
        public static rfmFastInputDataClass rfmFastInput = new rfmFastInputDataClass();
        //private string name;
        //public string Name
        //{
        //    get
        //    {
        //        return this.name;
        //    }
        //    set
        //    {
        //        this.name = value;
        //    }
        //}
        //-- Scatter Correction data ------------------------------------------
        public static ScatterData SCData = new ScatterData();
        //---SUMMARY RESULTS DATA ---------------------------------------------
        public static SummaryResultsData LatestSummaryData = new SummaryResultsData();
        public static NewErrorData LatestErrorData = new NewErrorData();
        //-- Data Repository
        public static RepositoryClass repoMan = new RepositoryClass();
        //---------------------------------------------------------------------
        //-- Declaration of remote interface access functions
        //---------------------------------------------------------------------
        //-- M1 data access functions ------------------------------ 
        public bool IsModifiedData(UInt16 type, ref UInt16 index, string name, UInt16 aindex, Int16 checkmode, float target)
        {
            if (name != "")
            {
                switch (type)
                {
                    case (UInt16)VARTYPE_INDEX.BOOL:
                        {
                            for (UInt16 i = 0; i < VARTYPE_NUMBERS.BOOL; ++i)
                            {
                                if (bValue.Name[i] == name)
                                {
                                    index = i;
                                    if (checkmode==0) return true;
                                    else return bValue.Modified[i];
                                }
                            }   
                            return false;
                        }
                    case (UInt16)VARTYPE_INDEX.INTEGER:
                        {
                            for (UInt16 i = 0; i < VARTYPE_NUMBERS.INTEGER; ++i)
                            {
                                if (iValue.Name[i] == name)
                                {
                                    index = i;
                                    switch (checkmode)
                                    {
                                        case 1:                                     
                                            if (iValue.Value[i] >= target) return true;
                                            else return false;                                     
                                        default:
                                            return true;
                                    }
                                }
                            }
                            return false;
                        }
                    case (UInt16)VARTYPE_INDEX.FLOAT:
                        {
                            for (UInt16 i = 0; i < VARTYPE_NUMBERS.FLOAT; ++i)
                            {
                                if (fValue.Name[i] == name)
                                {
                                    index = i;
                                    switch (checkmode)
                                    {
                                        case 1:
                                            if (fValue.Value[i] >= target) return true;
                                            else return false;
                                        default:
                                            return true;
                                    }
                                }
                            }
                            return false;
                        }
                    case (UInt16)VARTYPE_INDEX.FLOATARRAY:
                        {
                            for (UInt16 i = 0; i < VARTYPE_NUMBERS.FLOATARRAY; ++i)
                            {
                                if (faValue.Name[i] == name)
                                {
                                    index = i;
                                    switch (checkmode)
                                    {
                                        case 1:
                                            if (faValue.Value[i][aindex] >= target) return true;
                                            else return false;
                                        default:
                                            return true;
                                    }
                                }
                            }
                            return false;
                        }
                    case (UInt16)VARTYPE_INDEX.STRING:
                        {
                            for (UInt16 i = 0; i < VARTYPE_NUMBERS.STRING; ++i)
                            {
                                index = i;
                                if (sValue.Name[i] == name)
                                {
                                    if (checkmode == 0) return true;
                                    else return sValue.Modified[i];
                                }
                            }
                            return false;
                        }
                    default:
                        return false;
                }
            }
            else
            {
                switch (type)
                {
                    case (UInt16)VARTYPE_INDEX.BOOL:
                        {
                            if (index >= 0 && index < VARTYPE_NUMBERS.BOOL)
                            {
                                if (checkmode == 0) return true;
                                else return bValue.Modified[index];
                            }
                            return false;
                        }
                    case (UInt16)VARTYPE_INDEX.INTEGER:
                        {
                            if (index >= 0 && index < VARTYPE_NUMBERS.INTEGER)
                            {
                                switch (checkmode)
                                {
                                    case 1:
                                        if (iValue.Value[index] >= target) return true;
                                        else return false;
                                    default:
                                        return true;
                                }
                            }
                            return false;
                        }
                    case (UInt16)VARTYPE_INDEX.FLOAT:
                        {
                            if (index >= 0 && index < VARTYPE_NUMBERS.FLOAT)
                            {
                                switch (checkmode)
                                {
                                    case 1:
                                        if (fValue.Value[index] >= target) return true;
                                        else return false;
                                    default:
                                        return true;
                                }                                
                            }
                            return false;
                        }
                    case (UInt16)VARTYPE_INDEX.FLOATARRAY:
                        {
                            if (index >= 0 && index < VARTYPE_NUMBERS.FLOATARRAY)
                            {
                                switch (checkmode)
                                {
                                    case 1:
                                        if (faValue.Value[index][aindex] >= target) return true;
                                        else return false;
                                    default:
                                        return true;
                                }
                            }
                            return false;
                        }
                    case (UInt16)VARTYPE_INDEX.STRING:
                        {
                            if (index >= 0 && index < VARTYPE_NUMBERS.STRING)
                            {
                                if (checkmode == 0) return true;
                                else return sValue.Modified[index];
                            }
                            return false;
                        }
                    default:
                        return false;
                }
            }
        }
        public void ModifyData(UInt16 type, UInt16 index, UInt16 n, bool newdata)
        {
            switch (type)
            {
                case (UInt16)VARTYPE_INDEX.BOOL:
                    {
                        if (index >= 0 && index < VARTYPE_NUMBERS.BOOL)
                        {
                            bValue.Modified[index] = newdata;
                        }
                        break;
                    }
                case (UInt16)VARTYPE_INDEX.INTEGER:
                    {
                        if (index >= 0 && index < VARTYPE_NUMBERS.INTEGER)
                        {
                            iValue.Modified[index] = newdata;
                        }
                        break;
                    }
                case (UInt16)VARTYPE_INDEX.FLOAT:
                    {
                        if (index >= 0 && index < VARTYPE_NUMBERS.FLOAT)
                        {
                            fValue.Modified[index] = newdata;
                        }
                        break;
                    }
            }
        }
        public void SetName(UInt16 type, UInt16 index, UInt16 aindex, string name)
        {
            switch (type)
            {
                case (UInt16)VARTYPE_INDEX.BOOL:
                    {
                        if (index >= 0 && index < VARTYPE_NUMBERS.BOOL)
                        {
                            bValue.Name[index] = name;
                        }
                        break;
                    }
                case (UInt16)VARTYPE_INDEX.INTEGER:
                    {
                        if (index >= 0 && index < VARTYPE_NUMBERS.INTEGER)
                        {
                            iValue.Name[index] = name;
                        }
                        break;
                    }
                case (UInt16)VARTYPE_INDEX.FLOAT:
                    {
                        if (index >= 0 && index < VARTYPE_NUMBERS.FLOAT)
                        {
                            fValue.Name[index] = name;
                        }
                        break;
                    }
                case (UInt16)VARTYPE_INDEX.STRING:
                    {
                        if (index >= 0 && index < VARTYPE_NUMBERS.STRING)
                        {
                            sValue.Name[index] = name;
                        }
                        break;
                    }
                case (UInt16)VARTYPE_INDEX.FLOATARRAY:
                    {
                        if (index >= 0 && index < VARTYPE_NUMBERS.FLOATARRAY)
                        {
                            faValue.Name[index] = name;
                        }
                        break;
                    }
            }
        }
        public string GetName(UInt16 type, UInt16 index)
        {
            string name = "";

            switch (type)
            {
                case (UInt16)VARTYPE_INDEX.BOOL:
                    {
                        if (index >= 0 && index < VARTYPE_NUMBERS.BOOL)
                        {
                            name = bValue.Name[index];
                        }
                        break;
                    }
                case (UInt16)VARTYPE_INDEX.INTEGER:
                    {
                        if (index >= 0 && index < VARTYPE_NUMBERS.INTEGER)
                        {
                            name = iValue.Name[index];
                        }
                        break;
                    }
                case (UInt16)VARTYPE_INDEX.FLOAT:
                    {
                        if (index >= 0 && index < VARTYPE_NUMBERS.FLOAT)
                        {
                            name = fValue.Name[index];
                        }
                        break;
                    }
            }

            return name;            
        }

        public void SetItem(UInt16 type, UInt16 index, Item item)
        {
            switch (type)
            {
                case (UInt16)VARTYPE_INDEX.BOOL:
                    {
                        if (index >= 0 && index < VARTYPE_NUMBERS.BOOL)
                        {
                            bValue.Item[index] = item;
                        }
                        break;
                    }
                case (UInt16)VARTYPE_INDEX.INTEGER:
                    {
                        if (index >= 0 && index < VARTYPE_NUMBERS.INTEGER)
                        {
                            iValue.Item[index] = item;
                        }
                        break;
                    }
                case (UInt16)VARTYPE_INDEX.FLOAT:
                    {
                        if (index >= 0 && index < VARTYPE_NUMBERS.FLOAT)
                        {
                            fValue.Item[index] = item;
                        }
                        break;
                    }
            }
        }
        public Item GetItem(UInt16 type, UInt16 index)
        {
            switch (type)
            {
                case (UInt16)VARTYPE_INDEX.BOOL:
                    {
                        if (index >= 0 && index < VARTYPE_NUMBERS.BOOL)
                        {
                            return bValue.Item[index];
                        }
                        break;
                    }
                case (UInt16)VARTYPE_INDEX.INTEGER:
                    {
                        if (index >= 0 && index < VARTYPE_NUMBERS.INTEGER)
                        {
                            return iValue.Item[index];
                        }
                        break;
                    }
                case (UInt16)VARTYPE_INDEX.FLOAT:
                    {
                        if (index >= 0 && index < VARTYPE_NUMBERS.FLOAT)
                        {
                            return fValue.Item[index];
                        }
                        break;
                    }
            }

            return null;
        }

        public bool GetBoolData(UInt16 type, UInt16 index, UInt16 aindex)
        {
            switch (type)
            {
                case 0:
                case 10:
                    {
                        if (index >= 0 && index < VARTYPE_NUMBERS.BOOL)
                        {
                            return bValue.Value[index];
                        }
                        return false;
                    }
                case 27:
                    {
                        if (index >= 0 && index < VARTYPE_NUMBERS.BOOLARRAY)
                        {
                            return baValue.Value[index][aindex];
                        }
                        return false;
                    }
            }
            return false;
        }

        public void SetBoolData(UInt16 type, UInt16 index, UInt16 n, bool newdata)
        {
            switch (type)
            {
                case 0:
                case 10:
                    {
                        if (index >= 0 && index < VARTYPE_NUMBERS.BOOL)
                        {
                            bValue.Value[index] = newdata;
                            bValue.Modified[index] = true;
                        }
                    }
                    break;
                case 27:
                    if (index >= 0 && index < VARTYPE_NUMBERS.BOOLARRAY)
                    {
                        bValue.Value[index] = newdata;
                        bValue.Modified[index] = true;
                    }
                    break;
            }
            return;
        }

        public int GetIntegerData(UInt16 index)
        {
            if (index >= 0 && index < VARTYPE_NUMBERS.INTEGER)
            {
                return iValue.Value[index];
            }
            return 0;
        }
        public void SetIntegerData(UInt16 index, int newdata)
        {
            if (index >= 0 && index < VARTYPE_NUMBERS.INTEGER)
            {
                iValue.Value[index] = newdata;
                iValue.Modified[index] = true;
            }
        }
        public float GetFloatData(UInt16 index)
        {
            if (index >= 0 && index < VARTYPE_NUMBERS.FLOAT)
            {
                return fValue.Value[index];
            }
            return 0;
        }
        public void SetFloatData(UInt16 index, float newdata)
        {
            if (index >= 0 && index < VARTYPE_NUMBERS.FLOAT)
            {
                fValue.Value[index] = newdata;
                fValue.Modified[index] = true;
            }
        }
        public string GetStringData(UInt16 index)
        {
            if (index >= 0 && index < VARTYPE_NUMBERS.STRING)
            {
                return sValue.Value[index];
            }
            return "";
        }
        public void SetStringData(UInt16 index, string newdata)
        {
            if (index >= 0 && index < VARTYPE_NUMBERS.STRING)
            {
                sValue.Value[index] = newdata;
                sValue.Modified[index] = true;
            }
        }
        public float GetFloatArrayData(UInt16 index, UInt16 aindex)
        {
            if (index >= 0 && index < VARTYPE_NUMBERS.FLOATARRAY)
            {
                return faValue.Value[index][aindex];
            }
            return 0;
        }
        public void SetFloatArrayData(UInt16 index, UInt16 aindex, float newdata)
        {
            if (index >= 0 && index < VARTYPE_NUMBERS.FLOATARRAY)
            {
                faValue.Value[index][aindex] = newdata;
                faValue.Modified[index][aindex] = true;
            }
        }
        //-- Setup data access functions --------------------------------------
        public SetupData GetCurrentSetup()
        {
            return setCurrent;
        }
        public SetupData GetNextSetup()
        {
            return setNext;
        }
        public SetupData GetPreviousSetup()
        {
            return setPrevious;
        }
        public void SetCurrentSetup(ref SetupData newSetup)
        {
            setCurrent = newSetup;
        }
        public void SetNextSetup(ref SetupData newSetup)
        {
            setNext = newSetup;
            repoMan.StoreSetup(newSetup, false);
        }
        public void SetPreviousSetup(ref SetupData newSetup)
        {
            setPrevious = newSetup;
        }
        public void SetEvent(ref int newEvent)
        {
            RXEventQ.EventQ[RXEventQ.Count] = newEvent;
            RXEventQ.Count += 1;
        }
        //-- Status data access functions -------------------------------------
        public SuperStatus GetSVStatus()
        {
            return statSV;
        }
        public M1Status GetM1Status()
        {
            return statM1;
        }
        public GulmayStatus GetGULStatus()
        {
            return statGUL;
        }
        //-- Configuration data access functions ------------------------------
        public GeometryCfgDataClass GetGeometryCfg()
        {
            return cfgGeometry;
        }
        public SpecLimitsCfgDataClass GetSpecLimitsCfg()
        {
            return cfgSpecLimits;
        }
        public GaugingCfgDataClass GetGaugingCfg()
        {
            return cfgGauging;
        }
        public MeasureConfigClass GetMeasConfig()
        {
            return measConfig;
        }
        public ProfileCfgDataClass GetProfileCfgData()
        {
            return (ProfileCfgDataClass)cfgProfile;
        }
        public void SetProfileCfgData(ref ProfileCfgDataClass newCfg)
        {
            cfgProfile = newCfg;
        }
        public MeasureParametersClass GetMeasParams()
        {
            return (MeasureParametersClass)measParams.Clone();
        }
        public MeasureCalibrationClass GetMeasCal()
        {
            MeasureCalibrationClass calData = (MeasureCalibrationClass)measCal.Clone();
            repoMan.StoreMeasCalData(calData);
            return calData;
        }
        public MeasureCompensationClass GetMeasComp()
        {
            return (MeasureCompensationClass)measComp.Clone();
        }
        public CalCfgDataClass GetCfgCalibration()
        {
            return cfgCalibration;
        }

        //-- Detector data access functions -----------------------------------
        public DetStatusClass GetDetStatus()
        {
            return detStatus;
        }
        public void SetDetStatus(ref DetStatusClass newStatusData)
        {
            detStatus = newStatusData;
        }
        public DetControlClass GetDetControl()
        {
            return detControl;
        }
        public void SetDetControl(ref DetControlClass newControlData)
        {
            detControl = newControlData;
        }
        public DetSignalClass GetDetSignal()
        {
            return detSignal;
        }
        public void SetDetSignal(ref DetSignalClass newSignalData)
        {
            //-- Allow SiPROsuper to replay detector signals
            if (!statSV.LOCALSIGS)
            {
                detSignal = newSignalData;
                repoMan.StoreDetSignal(newSignalData);
            }
        }
        //-- Set/Reset repository auto trigger state --------------------------------------------------------------
        public static void SetRepoCapture(bool capture)
        {
            repoMan.SetCapture(capture);
        }
        public static void SetRepoIdle(bool idle)
        {
            repoMan.SetIdle(idle);
        }
        public void SetAutoTrigger(bool autoTrigger)
        {
            repoMan.SetAutoTrigger(autoTrigger);
        }
        public bool GetAutoTrigger()
        {
            return repoMan.GetAutoTrigger();
        }
        public DetDiagsClass GetDetDiags()
        {
            return detDiags;
        }
        public void SetDetDiags(ref DetDiagsClass newDiagData)
        {
            detDiags = newDiagData;
        }
        //-- Calibration data access functions --------------------------------
        public CalDataStruct GetCalData()
        {
            return calData;
        }
        public CalConfigStruct GetCalCfg()
        {
            return calCfg;
        }  
        //-- Pyrometer data access functions ----------------------------------
        public PyroDataClass GetPyroData()
        {
            return pyroData;
        }
        public void SetRawPyroData(ref RawTemperatureArray rawPyroData)
        {
            pyroData.netTempProf = (RawTemperatureArray)rawPyroData.Clone();
            repoMan.StoreTempSignal(rawPyroData);
        }
        //-- Measurement data access functions --------------------------------
        public StripLength GetStripLength()
        {
            return measLength;
        }
        public void SetFiveRawData(ref MeasDataClass newMeasData)
        {
            measFiveRaw = newMeasData;
        }
        public MeasDataClass GetFiveRawData()
        {
            return measFiveRaw;
        }
        public MeasDataClass GetDisplayData()
        {
            return measDisplayAvg;
        }
        public void SetDisplayData(ref MeasDataClass newMeasData)
        {
            measDisplayAvg = newMeasData;
        }
        public MeasDataClass GetCommsData()
        {
            return measCommsAvg;
        }
        public void SetCommsData(ref MeasDataClass newMeasData)
        {
            measCommsAvg = newMeasData;
        }
        public CoilDataClass GetCoilData(int whichGauge)
        {
            switch (whichGauge)
            {
                case 1: return measGauge2;
                case 2: return measGauge3;
                default: return measGauge1;
            }
        }
        public void SetCoilData(ref CoilDataClass newCoilData, int whichGauge )
        {
            switch (whichGauge)
            {
                case 1: measGauge2 = newCoilData; break;
                case 2: measGauge3 = newCoilData; break;
                default: measGauge1 = newCoilData; break;
            }
        }
        public CoilProfClass GetProfData(int whichGauge)
        {
            switch (whichGauge)
            {
                case 1: return profGauge2;
                case 2: return profGauge3;
                default: return profGauge1;
            }
        }
        public void SetProfData(ref CoilProfClass newCoilData, int whichGauge)
        {
            switch (whichGauge)
            {
                case 1: profGauge2 = newCoilData; break;
                case 2: profGauge3 = newCoilData; break;
                default: profGauge1 = newCoilData; break;
            }
        }
        public void RequestProfIndex(int whichGauge, int index)
        {
            switch (whichGauge)
            {
                case 1: profGauge2.reqIndex = index; break;
                case 2: profGauge3.reqIndex = index; break;
                default: profGauge1.reqIndex = index; break;
            }
        }
        public void UpdateCoilCount(int whichGauge)
        {
            switch (whichGauge)
            {
                case 1:
                    {
                        if (measGauge2.updateCount < 32000) measGauge2.updateCount++;
                        else measGauge2.updateCount = 0;
                        break;
                    }
                case 2:
                    {
                        if (measGauge3.updateCount < 32000) measGauge3.updateCount++;
                        else measGauge3.updateCount = 0;
                        break;
                    }
                default:
                    {
                        if (measGauge1.updateCount < 32000) measGauge1.updateCount++;
                        else measGauge1.updateCount = 0;
                        break;
                    }
            }
        }
        public int GetUpdateCoilCount(int whichGauge)
        {
            switch (whichGauge)
            {
                case 1: return measGauge2.updateCount;
                case 2: return measGauge3.updateCount;
                default: return measGauge1.updateCount;
            }
        }
        public int GetUpdateProfileIndex(int whichGauge)
        {
            switch (whichGauge)
            {
                case 1: return profGauge2.updateIndex;
                case 2: return profGauge3.updateIndex;
                default: return profGauge1.updateIndex;
            }
        }
        //-- Analog data access functions ------------------------------------
        public AnalogInputClass GetAnalogInputs()
        {
            return anaInput;
        }
        public rfmFastInputDataClass GetRFMFastData()
        {
            return rfmFastInput;
        }
        public SummaryResultsData GetSummaryResultsData()
        {
            return LatestSummaryData;
        }
        public void SetSummaryResultsData(ref SummaryResultsData newSummaryData)
        {
            LatestSummaryData = newSummaryData;
        }
        public void SetLatestErrorData(ref NewErrorData newErrorData)
        {
            LatestErrorData = newErrorData;
        }
        public NewErrorData GetLatestErrorData()
        {
            return LatestErrorData;
        }
        // Error Data for SiPRO-Net on Comms.
    }

    //-------------------------------------------------------------------------
    //-- Interface for remote application domains -----------------------------
    //-------------------------------------------------------------------------
    public interface remoteInterface
    {
        //-- M1 data ------------------------------------ 
        bool IsModifiedData(UInt16 type, ref UInt16 index, string name, UInt16 aindex, Int16 checkmode, float target);
        void ModifyData(UInt16 type, UInt16 index, UInt16 n, bool newdata);
        string GetName(UInt16 type, UInt16 index);
        void SetName(UInt16 type, UInt16 index, UInt16 aindex, string name);
        Item GetItem(UInt16 type, UInt16 index);
        void SetItem(UInt16 type, UInt16 index, Item item);
        bool GetBoolData(UInt16 type, UInt16 index, UInt16 aindex);
        void SetBoolData(UInt16 type, UInt16 index, UInt16 n, bool newdata);
        int GetIntegerData(UInt16 index);
        void SetIntegerData(UInt16 index, int newdata);
        float GetFloatData(UInt16 index);
        void SetFloatData(UInt16 index, float newdata);
        string GetStringData(UInt16 index);
        void SetStringData(UInt16 index, string newdata);
        float GetFloatArrayData(UInt16 index, UInt16 aindex);
        void SetFloatArrayData(UInt16 index, UInt16 aindex, float newdata);
        //-- Setup data -------------------------------------------------------
        SetupData GetCurrentSetup();
        SetupData GetNextSetup();
        SetupData GetPreviousSetup();
        void SetCurrentSetup(ref SetupData newSetup);
        void SetNextSetup(ref SetupData newSetup);
        void SetPreviousSetup(ref SetupData newSetup);
        void SetEvent(ref int newEvent);
        //-- Status data ------------------------------------------------------
        SuperStatus GetSVStatus();
        M1Status GetM1Status();
        GulmayStatus GetGULStatus();
        //-- Configuration data -----------------------------------------------
        GeometryCfgDataClass GetGeometryCfg();
        SpecLimitsCfgDataClass GetSpecLimitsCfg();
        ProfileCfgDataClass GetProfileCfgData();        
        MeasureConfigClass GetMeasConfig();
        MeasureCalibrationClass GetMeasCal();
        MeasureParametersClass GetMeasParams();
        MeasureCompensationClass GetMeasComp();
        CalCfgDataClass GetCfgCalibration();
        void SetProfileCfgData(ref ProfileCfgDataClass newCfg);
        //-- Detector data ----------------------------------------------------
        DetStatusClass GetDetStatus();
        void SetDetStatus(ref DetStatusClass newStatusData);
        DetControlClass GetDetControl();
        void SetDetControl(ref DetControlClass newControlData);
        DetSignalClass GetDetSignal();
        void SetDetSignal(ref DetSignalClass newSignalData);
        DetDiagsClass GetDetDiags();
        void SetDetDiags(ref DetDiagsClass newDiagData);
        void SetAutoTrigger(bool trigger);
        bool GetAutoTrigger();
        //-- Calibration Data -------------------------------------------------
        CalDataStruct GetCalData();
        CalConfigStruct GetCalCfg();
        //-- Pyrometer data ---------------------------------------------------
        PyroDataClass GetPyroData();
        void SetRawPyroData(ref RawTemperatureArray rawPyroData);
        //-- Measurement data as required
        StripLength GetStripLength();
        MeasDataClass GetFiveRawData();
        void SetFiveRawData(ref MeasDataClass newMeasData);
        MeasDataClass GetDisplayData();
        void SetDisplayData(ref MeasDataClass newMeasData);
        MeasDataClass GetCommsData();
        void SetCommsData(ref MeasDataClass newMeasData);
        CoilDataClass GetCoilData(int whichGauge);
        void SetCoilData(ref CoilDataClass newCoilData, int whichGauge);
        CoilProfClass GetProfData(int whichGauge);
        void SetProfData(ref CoilProfClass newCoilData, int whichGauge);
        void UpdateCoilCount(int whichGauge);
        int GetUpdateCoilCount(int whichGauge);
        int GetUpdateProfileIndex(int whichGauge);
        void RequestProfIndex(int whichGauge, int index);
        //-- Analogue data ----------------------------------------------------
        AnalogInputClass GetAnalogInputs();
        //-- Summary Report Data
        SummaryResultsData GetSummaryResultsData();
        void SetSummaryResultsData(ref SummaryResultsData newSummaryData);
        // - Error Telegram Data
        NewErrorData GetLatestErrorData();
        void SetLatestErrorData(ref NewErrorData newErrorTelegramData);
    }
}
//=================================================================================================
