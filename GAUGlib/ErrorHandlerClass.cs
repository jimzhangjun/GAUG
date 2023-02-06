//=================================================================================================
//  Project:    SIPRO-library
//  Module:     ErrorHandlerClass.cs                                                                         
//  Author:     Andrew Powell
//  Date:       01/12/2010
//  
//  Details:    Error handler for XMD applications
//  
//=================================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace GAUGlib
{
    //-- Error logging structure ------------------------------------------------------------------
    public struct ErrorMsg
    {
        public string desc;
        public string detail;
        public int count;
        public string appName;
        public string DT; // For Mult Errors
    }
    //-- Error handling class ---------------------------------------------------------------------
    public class ErrorHandlerClass
    {
        //-- Class variable definitions
        private const string ROOTDIR = @"C:\Thermo\Logs\";
        private const string FNAME = "GAUGEVENT.LOG";
        private static StreamWriter swInternal;
        private static bool fileBeingEdited = false;
        private static int errorCount = 0;
        private static int maxErrorLog = 1000;
        private static string lastErrorDesc = "";
        private static string lastErrorDT = "";
        private static string lastErrorappName = "";
        private static int repeatedErrorCount = 0;

        //-- Error handling queue
        public static Queue<ErrorMsg> errorQueue = new Queue<ErrorMsg>();


        //-- Create directory if it doesn't exist -------------------------------------------------
        public static void CreateDir(System.IO.DirectoryInfo oDirInfo)
        {
            if (oDirInfo.Parent != null)
                CreateDir(oDirInfo.Parent);
            if (!oDirInfo.Exists)
                oDirInfo.Create();
        }
        //-- Return current directory path --------------------------------------------------------
        public static string CurrentDir()
        {
            DateTime date = DateTime.Today;
            string dirName = ROOTDIR + date.ToString("yyyyMMdd");
            CreateDir(new System.IO.DirectoryInfo(dirName));
            return dirName + "\\";
        }
        //-- Create copy of existing logfile and create new blank file ----------------------------
        public static void CreateLogFile(string prefix)
        {
            try
            {
                DateTime date = DateTime.Today;
                string dirName = ROOTDIR + date.ToString("yyyyMMdd") + "\\";
                FileInfo newFile = new FileInfo(dirName + FNAME);
                FileInfo oldFile = new FileInfo(dirName + prefix + FNAME);

                if (newFile.Exists)
                {
                    if (newFile.Length > 2000)
                    {
                        if (oldFile.Exists) oldFile.Delete();
                        newFile.CopyTo(oldFile.FullName);
                        newFile.Delete();
                    }
                }
                swInternal = new StreamWriter(newFile.OpenWrite());
                swInternal.WriteLine("GAUG applications event log created " + DateTime.Now.ToString("HH:mm:ss  dd-MM-yyyy"));
                swInternal.WriteLine();
                swInternal.Close();
            }
            catch (Exception exc)
            {
                ExceptionManager.Publish(exc);
            }
        }
        //-- Write time stamped error to logfile ------------------------------------------------------------------
        private static void WriteLog(ErrorMsg wrError)
        {
            errorCount++;
            //-- Create file and directory path
            FileInfo logFile = new FileInfo(CurrentDir() + FNAME);
            if (!logFile.Exists || (errorCount > maxErrorLog))
            {
                CreateLogFile(DateTime.Now.ToString("yyyyMMdd_HHmm_"));
                errorCount = 0;
            }
            //-- Lock to prevent multiple calls
            if (!fileBeingEdited)
            {
                try
                {
                    fileBeingEdited = true;
                    //-- Write message to log file
                    swInternal = logFile.AppendText();
                    //String appName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
                    if (wrError.count > 1) swInternal.Write(wrError.DT + wrError.appName + "   ");
                    else swInternal.Write(DateTime.Now.ToString("yyyyMMdd  HH:mm:ss  ") + wrError.appName + "   ");

                    //swInternal.WriteLine(wrError.desc + "  " + wrError.detail);
                    if (wrError.count > 1) swInternal.WriteLine(wrError.desc + "  " + wrError.detail + " count:" + wrError.count.ToString() );
                    else swInternal.WriteLine(wrError.desc + "  " + wrError.detail );
                    swInternal.Close();
                    fileBeingEdited = false;
                }
                catch (Exception exc)
                {
                    ExceptionManager.Publish(exc);
                }
            }
        }
        //-- Check error queue and return oldest error --------------------------------------------
        public static bool CheckError(ref ErrorMsg error)
        {
            try
            {
                if (errorQueue.Count > 0)
                {
                    error = errorQueue.Dequeue();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception exc)
            {
                ExceptionManager.Publish(exc);
                return false;
            }
        }
        //-- Get error count ----------------------------------------------------------------------
        public static int ErrorCount()
        {
            return errorQueue.Count;
        }
        //-- Add an exception to the queue and report exception -----------------------------------
        public static void ReportException(string desc, Exception except)
        {
            ErrorMsg newError = new ErrorMsg();
            try
            {
                //-- Stop repeated exceptions filling event viewer
                if (desc != lastErrorDesc)
                {
                    if (repeatedErrorCount > 1)
                    {
                        // Log last of repeated event with count.
                        ErrorMsg lastofNCountException = new ErrorMsg();
                        lastofNCountException.desc = lastErrorDesc;
                        lastofNCountException.DT = lastErrorDT;
                        lastofNCountException.appName = lastErrorappName;
                        lastofNCountException.count = repeatedErrorCount;
                        lastofNCountException.detail = repeatedErrorCount.ToString() + " repeated except";
                        WriteLog(lastofNCountException); // Log last of repeated.
                    }
                    ExceptionManager.Publish(except);
                    repeatedErrorCount = 1;
                }
                else
                {
                    repeatedErrorCount++;                   
                }
                newError.desc = desc;
                newError.detail = except.Message;
                string myAppName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
                newError.appName = myAppName;
                string ExcDT = DateTime.Now.ToString("yyyyMMdd  HH:mm:ss  ");
                newError.DT = ExcDT;
                newError.count = repeatedErrorCount;
                //WriteLog(newError);
                if (repeatedErrorCount == 1) WriteLog(newError); // Lets not log them all (first here last above).
                errorQueue.Enqueue(newError);
                lastErrorDesc = desc;
                lastErrorDT = ExcDT;
                lastErrorappName = myAppName;
            }
            catch (Exception exc)
            {
                ExceptionManager.Publish(exc);
            }
        }
        //-- Add an error to the queue and report exception ---------------------------------------
        public static void ReportEvent(string desc)
        {
            ErrorMsg newEvent = new ErrorMsg();
            try
            {
                //-- Stop repeated errors filling error grids
                if (desc != lastErrorDesc)
                {
                    if (repeatedErrorCount > 1)
                    {
                        // Log last of repeated event with count.
                        ErrorMsg lastofNCountEvent = new ErrorMsg();
                        lastofNCountEvent.desc = lastErrorDesc;
                        lastofNCountEvent.DT = lastErrorDT;
                        lastofNCountEvent.appName = lastErrorappName;
                        lastofNCountEvent.count = repeatedErrorCount;
                        WriteLog(lastofNCountEvent); // Log last of n repeated.
                    }
                    repeatedErrorCount = 1;
                }
                else repeatedErrorCount++;

                string myAppName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
                newEvent.appName = myAppName;
                newEvent.desc = desc;
                newEvent.count = repeatedErrorCount;
                string ExcDT = DateTime.Now.ToString("yyyyMMdd  HH:mm:ss  ");
                newEvent.DT = ExcDT;
                if (repeatedErrorCount == 1 ) WriteLog(newEvent); // Lets not log them all (first here last above).
                errorQueue.Enqueue(newEvent);
                lastErrorDesc = desc;
                lastErrorDT = ExcDT;
                lastErrorappName = myAppName;
                
            }
            catch (Exception exc)
            {
                ExceptionManager.Publish(exc);
            }
        }
    }
    //=============================================================================================
}

