//=================================================================================================
//  Project:    RM312/SIPRO SUPERvisor Data Server for Remote Applications
//  Module:     RepositoryClass.cs                                                                         
//  Author:     Andrew Powell
//  Date:       28/01/2012
//  
//  Details:    Global data repository for archiving all data handled through the SiPRO
//              .NET remoting interface
//
//
//=================================================================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

using GAUGlib;
using GAUGdata;

namespace GAUGdata
{
    

    public class RepoDataClass    
    {
        //-- Definition of queue data
        public const int SETU = 0;
        public const int DET1 = 1;
        public const int DET2 = 2;
        public const int TEMP = 3;
        public const int M1ST = 4;
        public const int ANAL = 5;
        public const int AVG1 = 10;
        public const int AVG2 = 11;
        public const int INF1 = 20;
        public const int INF2 = 21;
        public const int ZRO1 = 22;
        public const int ZRO2 = 23;
        public const int OFF1 = 24;
        public const int OFF2 = 25;

        //-- Definition of modes
        public const int REPO_MANUAL = 0;
        public const int REPO_MEASURE = 1;
        public const int REPO_SETUP = 2;
        public const int REPO_PRODUCT = 3;

        //-- Data queue length 5001 is ~20 seconds
        public static int maxRepoCount = 5001;
    }
    
    public class RepositoryClass
    {
        //-- Repository data queues
        private Queue<int> deliQueue = new Queue<int>();
        private Queue<M1Status> statusQueue = new Queue<M1Status>();
        private Queue<SetupData> setupQueue = new Queue<SetupData>();
        private Queue<AnalogInputClass> analogQueue = new Queue<AnalogInputClass>();
        private Queue<byte[]> s1DetSigQueue = new Queue<byte[]>();
        private Queue<byte[]> s2DetSigQueue = new Queue<byte[]>();
        private Queue<byte[]> pyroQueue = new Queue<byte[]>();

        private MeasureCalibrationClass measCalData = new MeasureCalibrationClass();
        private ArrayAverageClass s1MeanSignal = new ArrayAverageClass(2000);
        private ArrayAverageClass s2MeanSignal = new ArrayAverageClass(2000);
        
        private char[] lastCoilId;
        public static bool repoCapture = false;
        public static bool repoIdle = true;
        public static bool repoAutoTrigger = false;
        private bool srcOne = true;

        public RepositoryClass()
        {
            lastCoilId = "NewRepository".ToCharArray();
        }
        //-- Function to compare coil ids ---------------------------------------------------------
        private static bool CoilIdChanged(char[] coilID1, char[] coilID2)
        {
            if (coilID1.Length != coilID2.Length)
                return true;
            for (int i = 0; i < coilID1.Length; i++)
                if (coilID1[i] != coilID2[i])
                    return true;
            return false;
        }
        //-- Set/Reset capture state --------------------------------------------------------------
        public void SetCapture(bool capture)
        {
            repoCapture = capture;
        }
        //-- Set/Reset idle state --------------------------------------------------------------
        public void SetIdle(bool idle)
        {
            repoIdle = idle;
        }
        //-- Set/Reset repository auto trigger state --------------------------------------------------------------
        public void SetAutoTrigger(bool autoTrigger)
        {
            repoAutoTrigger = autoTrigger;
        }
        public bool GetAutoTrigger()
        {
            return repoAutoTrigger;
        }
        //-- Create directory if it doesn't exist -------------------------------------------------
        public static void CreateDir(System.IO.DirectoryInfo oDirInfo)
        {
            if (oDirInfo.Parent != null) CreateDir(oDirInfo.Parent);
            if (!oDirInfo.Exists) oDirInfo.Create();               
        }
        //-- Store new current setup data ---------------------------------------------------------
        public void StoreSetup(SetupData setup, bool alwaysAdd)
        {         
            ////-- Trigger file write on change in Coil Id
            //if (CoilIdChanged(setup.sCoilID, lastCoilId))
            //{
            //    stopRepoUpdate = true;
            //    WriteBinaryDataFile();
            //    stopRepoUpdate = false;
            //    lastCoilId = (char[])setup.sCoilID.Clone();
            //}
            if (repoCapture)
            {
                if ((deliQueue.Count < RepoDataClass.maxRepoCount) || alwaysAdd)
                {
                    setupQueue.Enqueue(setup);
                    deliQueue.Enqueue(RepoDataClass.SETU);                
                }
            }
        }
        //-- Compress a byte array ----------------------------------------------------------------
        private byte[] CompressArray(byte[] srcArray)
        {            
            //-- Compress the byte array
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(memory, CompressionMode.Compress, true))
                {
                    gzip.Write(srcArray, 0, srcArray.Length);
                }
                return memory.ToArray();
            }
        }
        //-- Store detector signal data -----------------------------------------------------------
        public void StoreDetSignal(DetSignalClass detSig)
        {
            if (repoCapture)
            {
                if ((deliQueue.Count >= RepoDataClass.maxRepoCount) && repoIdle)
                {
                    switch (deliQueue.Dequeue())
                    {
                        case RepoDataClass.SETU:
                            setupQueue.Dequeue();
                            break;
                        case RepoDataClass.DET1:
                            s1DetSigQueue.Dequeue();
                            break;
                        case RepoDataClass.DET2:
                            s2DetSigQueue.Dequeue();
                           break;
                        case RepoDataClass.TEMP:
                            pyroQueue.Dequeue();
                            break;
                        case RepoDataClass.M1ST:
                            statusQueue.Dequeue();
                            break;
                        case RepoDataClass.ANAL:
                            analogQueue.Dequeue();
                           break;
                        default:
                            break;
                    }
                }
                if ((deliQueue.Count < RepoDataClass.maxRepoCount) || !repoIdle)
                {
                    if (srcOne)
                    {
                        //-- Convert int array to byte array
                        byte[] byteArray = new byte[detSig.s1Data.Length * sizeof(int)];
                        Buffer.BlockCopy(detSig.s1Data, 0, byteArray, 0, byteArray.Length);
                        s1DetSigQueue.Enqueue(CompressArray(byteArray));
                        deliQueue.Enqueue(RepoDataClass.DET1);
                        //s1MeanSignal.AddData(detSig.s1Data);
                    }
                    else
                    {
                        //-- Convert int array to byte array
                        byte[] byteArray = new byte[detSig.s2Data.Length * sizeof(int)];
                        Buffer.BlockCopy(detSig.s2Data, 0, byteArray, 0, byteArray.Length);
                        s2DetSigQueue.Enqueue(CompressArray(byteArray));
                        deliQueue.Enqueue(RepoDataClass.DET2);
                        //s2MeanSignal.AddData(detSig.s2Data);
                    }
                    srcOne = !srcOne;
                }
            }
        }
        //-- Store pyro signal data ---------------------------------------------------------------
        public void StoreTempSignal(RawTemperatureArray tempArray)
        {
            if (repoCapture)
            {
                if (deliQueue.Count < RepoDataClass.maxRepoCount)
                {
                    //-- Convert float array to byte array
                    byte[] byteArray = new byte[tempArray.data.Length * sizeof(float)];
                    Buffer.BlockCopy(tempArray.data, 0, byteArray, 0, byteArray.Length);
                    pyroQueue.Enqueue(CompressArray(byteArray));
                    deliQueue.Enqueue(RepoDataClass.TEMP);
                }
            }
        }
        //-- Store status data --------------------------------------------------------------------
        public void StoreStatus(M1Status m1Stat)
        {
            if (repoCapture)
            {
                if (deliQueue.Count < RepoDataClass.maxRepoCount)
                {
                    statusQueue.Enqueue(m1Stat);
                    deliQueue.Enqueue(RepoDataClass.M1ST);
                }
            }
        }
        //-- Store analog input data --------------------------------------------------------------
        public void StoreAnalog(AnalogInputClass analog)
        {
            if (repoCapture)
            {
                if (deliQueue.Count < RepoDataClass.maxRepoCount)
                {
                    analogQueue.Enqueue(analog);
                    deliQueue.Enqueue(RepoDataClass.ANAL);
                }
            }
        }
        //-- Store calibration data ---------------------------------------------------------------
        public void StoreMeasCalData(MeasureCalibrationClass calData)
        {
            if (repoCapture)
            {
                measCalData = calData;
            }
        }
        //-- Write current data queue to binary file ----------------------------------------------
        public void WriteBinaryDataFile(string filePrefix)
        {
            const int FILEVERSION = 2;
            const int CALDATACOUNT = 6;
            int i = 0;
            int det = 0;
            int index = 0;
            //-- Stop repository updates
            repoCapture = false;
            try
            {
                //-- Create directory
                string dirName = @"C:\Thermo\Diagnostic\Repository";
                CreateDir(new System.IO.DirectoryInfo(dirName));
                //-- Create filename
                string fName = dirName + "\\" + filePrefix + DateTime.Now.ToString("_yyyyMMdd_HHmmss") + ".REP";
                FileStream binfile = new FileStream(fName, FileMode.Create);
                BinaryWriter binWriter = new BinaryWriter(binfile);

                index = deliQueue.Count;
                SetupData wrSetup = new SetupData();
                byte[] wrDetSig = new byte[SIZE.RAW * sizeof(int)];
                byte[] wrTempData = new byte[SIZE.RAWTEMP * sizeof(float)];
                M1Status wrStatus = new M1Status();
                AnalogInputClass wrAnal = new AnalogInputClass();

                //-- Write file header data
                binWriter.Write(FILEVERSION);
                binWriter.Write(index);
                //-- Write standardize/calibration data
                binWriter.Write(RepoDataClass.INF1);
                byte[] byteArray = new byte[measCalData.aCalibration.S1CalData.Infinity.Length * sizeof(int)];
                Buffer.BlockCopy(measCalData.aCalibration.S1CalData.Infinity, 0, byteArray, 0, byteArray.Length);
                byte[] wrCalData = CompressArray(byteArray);
                binWriter.Write(wrCalData.Length);
                for (det = 0; det < wrCalData.Length; det++)
                    binWriter.Write(wrCalData[det]);
                binWriter.Write(RepoDataClass.INF2);
                byteArray = new byte[measCalData.aCalibration.S2CalData.Infinity.Length * sizeof(int)];
                Buffer.BlockCopy(measCalData.aCalibration.S2CalData.Infinity, 0, byteArray, 0, byteArray.Length);
                wrCalData = CompressArray(byteArray);
                binWriter.Write(wrCalData.Length);
                for (det = 0; det < wrCalData.Length; det++)
                    binWriter.Write(wrCalData[det]);
                binWriter.Write(RepoDataClass.ZRO1);
                byteArray = new byte[measCalData.s1StdzZero.Length * sizeof(int)];
                Buffer.BlockCopy(measCalData.s1StdzZero, 0, byteArray, 0, byteArray.Length);
                wrCalData = CompressArray(byteArray);
                binWriter.Write(wrCalData.Length);
                for (det = 0; det < wrCalData.Length; det++)
                    binWriter.Write(wrCalData[det]);
                binWriter.Write(RepoDataClass.ZRO2);
                byteArray = new byte[measCalData.s2StdzZero.Length * sizeof(int)];
                Buffer.BlockCopy(measCalData.s2StdzZero, 0, byteArray, 0, byteArray.Length);
                wrCalData = CompressArray(byteArray);
                binWriter.Write(wrCalData.Length);
                for (det = 0; det < wrCalData.Length; det++)
                    binWriter.Write(wrCalData[det]);
                binWriter.Write(RepoDataClass.OFF1);
                byteArray = new byte[measCalData.aFactor.S1.Stdz.Offsets.Length * sizeof(int)];
                Buffer.BlockCopy(measCalData.aFactor.S1.Stdz.Offsets, 0, byteArray, 0, byteArray.Length);
                wrCalData = CompressArray(byteArray);
                binWriter.Write(wrCalData.Length);
                for (det = 0; det < wrCalData.Length; det++)
                    binWriter.Write(wrCalData[det]); ;
                binWriter.Write(RepoDataClass.OFF2);
                byteArray = new byte[measCalData.aFactor.S2.Stdz.Offsets.Length * sizeof(int)];
                Buffer.BlockCopy(measCalData.aFactor.S2.Stdz.Offsets, 0, byteArray, 0, byteArray.Length);
                wrCalData = CompressArray(byteArray);
                binWriter.Write(wrCalData.Length);
                for (det = 0; det < wrCalData.Length; det++)
                    binWriter.Write(wrCalData[det]);
                for (i = 0; i < index - CALDATACOUNT; i++)
                {
                    switch (deliQueue.Dequeue())
                    {
                        case RepoDataClass.SETU:
                            wrSetup = setupQueue.Dequeue();
                            binWriter.Write(RepoDataClass.SETU);
                            binWriter.Write(wrSetup.sCoilID);
                            binWriter.Write(wrSetup.sNomThick);
                            binWriter.Write(wrSetup.sNomWidth);
                            binWriter.Write(wrSetup.sAlloyName);
                            break;
                        case RepoDataClass.DET1:
                            wrDetSig = s1DetSigQueue.Dequeue();
                            binWriter.Write(RepoDataClass.DET1);
                            binWriter.Write(wrDetSig.Length);
                            for (det = 0; det < wrDetSig.Length; det++)
                                binWriter.Write(wrDetSig[det]);
                            break;
                        case RepoDataClass.DET2:
                            wrDetSig = s2DetSigQueue.Dequeue();
                            binWriter.Write(RepoDataClass.DET2);
                            binWriter.Write(wrDetSig.Length);
                            for (det = 0; det < wrDetSig.Length; det++)
                                binWriter.Write(wrDetSig[det]);
                            break;
                        case RepoDataClass.TEMP:
                            wrTempData = pyroQueue.Dequeue();
                            binWriter.Write(RepoDataClass.TEMP);
                            binWriter.Write(wrTempData.Length);
                            for (det = 0; det < wrTempData.Length; det++)
                                binWriter.Write(wrTempData[det]);
                            break;
                        case RepoDataClass.M1ST:
                            wrStatus = statusQueue.Dequeue();
                            binWriter.Write(RepoDataClass.M1ST);
                            binWriter.Write(wrStatus.S1XRAYSON);
                            binWriter.Write(wrStatus.S2XRAYSON);
                            break;
                        case RepoDataClass.ANAL:
                            wrAnal = analogQueue.Dequeue();
                            binWriter.Write(RepoDataClass.ANAL);
                            binWriter.Write(wrAnal.aStripSpeed);
                            break;
                        default:
                            break;
                    }
                }
                binWriter.Close();
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("Write Binary File", exc);
            }
            //-- Resume repository update
            repoCapture = true;
        }
        //-- Clear all data queues ----------------------------------------------------------------
        public void ClearRepository(int maxCount)
        {
            //-- Stop repository updates
            repoCapture = false;
            //-- Clear out all queues
            try
            {
                deliQueue.Clear();
                setupQueue.Clear();
                s1DetSigQueue.Clear();
                s2DetSigQueue.Clear();
                pyroQueue.Clear();
                statusQueue.Clear();
                s1MeanSignal.SetAvgCount(maxCount / 2);
                s2MeanSignal.SetAvgCount(maxCount / 2);                
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("Clear Repository", exc);
            }
            //-- Resume repository update
            RepoDataClass.maxRepoCount = maxCount;
            repoCapture = true;
        }
        //-- Get number of repo queued items ------------------------------------------------------
        public int RepoDataQueueCount()
        {
            return deliQueue.Count;
        }
    }
}
//=================================================================================================