//=================================================================================================
//  Project:    RM312/SIPRO SIPROview
//  Module:    CsvFile.cs                                                                         
//  Author:     Andrew Powell
//  Date:       26/04/2010
//  
//  Details:    Definition of INI file data types 
//  
//=================================================================================================
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Data;
using System.Net;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;

using GAUGlib;

namespace GAUGview
{
    public class ArchiveFile
    {
        //-----------------------------------------------------------------------------------------
        //-- CLASS VARIABLES ----------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------
        public ProductData productData = new ProductData();
        public class ProductData
        {
            public string gaugename;
            public string coilid="";
            public int passnumber;
            public int lastpass;            

            public float setpoint;
            public float ptol;
            public float mtol;
            public float alloyfactor;
            public float offset;
        }

        private string filePath;
        private string filename = "";
        private static FileInfo csvFileInfo;
        private static StreamWriter csvFileWriter;
        //-----------------------------------------------------------------------------
        //-- Open New File ------------------------------------------------------------
        public void CreateCoilReport()
        {
            try
            {
                //-- delete the old or invalid file
                //if (FileExists()) DeleteFile();

                //-- check the path
                filePath = ConfigDataClass.archive.database + FormatDateTime.CSVPATH();
                FileClass.CreateDir(new System.IO.DirectoryInfo(filePath));
                
                //-- make new file name
                filename = filePath + productData.coilid.ToString() + "_" 
                        + productData.passnumber.ToString() + "_" 
                        + productData.lastpass.ToString() + "_" 
                        + productData.gaugename + "_" 
                        + FormatDateTime.CSVNAME() + ".csv";

                //-- Create file
                csvFileInfo = new FileInfo(filename);                
                csvFileWriter = new StreamWriter(csvFileInfo.OpenWrite());

                //-- write the header and titile
                csvFileWriter.WriteLine("-------------------------------------------------------------------");
                csvFileWriter.WriteLine("Thermo Fisher Scientific");
                csvFileWriter.WriteLine("Gauge Location");
                csvFileWriter.WriteLine("Measurement report " + productData.gaugename);
                csvFileWriter.WriteLine("-------------------------------------------------------------------");
                csvFileWriter.WriteLine("");
                csvFileWriter.WriteLine("          Coil ID    ,   Setp  ,  +Tol   ,  -Tol   ,Alloyfactor, Offset, Passnumber, Lastpass,");
                csvFileWriter.WriteLine("          [#]        ,   [mm]  ,   [mm]  ,   [mm]  ,  [#]      ,  [mm] , [#]       , [#]     ,");
                csvFileWriter.WriteLine(productData.coilid.ToString() + "," 
                        + productData.setpoint.ToString() + "," 
                        + productData.ptol.ToString() + "," 
                        + productData.mtol.ToString() + "," 
                        + productData.alloyfactor.ToString() + "," 
                        + productData.offset.ToString() + "," 
                        + productData.passnumber.ToString() + "," 
                        + productData.lastpass.ToString());
                csvFileWriter.WriteLine("");
                csvFileWriter.WriteLine(" LengthPos, MeasVal, MeasStat,");
                csvFileWriter.WriteLine("    [m]   ,   [mm] ,    [#]  ,");
                csvFileWriter.Close();
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("Failed to open csv file", exc); ;
            }
        }
        //-- Set INIfile path and name ------------------------------------------------------------
        public void SaveCoilReport(float length, float meas, int status)
        {
            try
            {
                csvFileInfo = new FileInfo(filename);
                csvFileWriter = csvFileInfo.AppendText();
                csvFileWriter.WriteLine(length.ToString() + "," + meas.ToString() + "," + status.ToString());
                csvFileWriter.Close();
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("Failed to write csv file", exc);
            }
        }
        //-- Set INIfile path and name ------------------------------------------------------------
        public void SaveCoilSummary(float[] summary)
        {
            try
            {
                csvFileInfo = new FileInfo(filename);
                csvFileWriter = csvFileInfo.AppendText();
                
                csvFileWriter.Close();
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("Failed to write summary in csv file", exc);
            }
        }
        //-- Check if file already exists ---------------------------------------------------------
        public bool FileExists()
        {
            if (filename == "") return false;
            FileInfo File = new FileInfo(String.Format(filename));
            return File.Exists;
        }
        //-- Delete existing file -----------------------------------------------------------------
        private void DeleteFile()
        {
            FileInfo File = new FileInfo(String.Format(filename));
            File.Delete();
            filename = "";
        }
    }
}
//=================================================================================================
