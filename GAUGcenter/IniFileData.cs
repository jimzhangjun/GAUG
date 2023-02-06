//=================================================================================================
//  Project:    GAUG Center
//  Module:     Config.cs                                                                         
//  Author:     Jim Zhang
//  Date:       16/12/2019
//  
//  Details:    Definition of INI file data types 
//  
//=================================================================================================================
using System;
//using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
//using System.ComponentModel;
//using System.Threading;
using System.IO;
using System.Data;

using GAUGlib;
//using GAUGdata;

namespace GAUGcenter
{
    //-- Common directory path listings -----------------------------------------------------------
    public class DIRPATH
    {
        public static void CreateDir(System.IO.DirectoryInfo oDirInfo)
        {
            if (oDirInfo.Parent != null)
                CreateDir(oDirInfo.Parent);
            if (!oDirInfo.Exists)
                oDirInfo.Create();
        }
        public static string ROOT = @"C:\Thermo";
        public static string CFG = @"\GAUGcenter\Configuration\";
        public static string DIAG = @"\GAUGcenter\Diagnostic\";
        //public static string GIF = @"\icons\";
        public static string APPS = @"\Program Files\";
        //public static string TESTAPPS = @"\Test Programs\";
        public static string NAME = "GAUGcenter.INI";
        public static IniFile iniFile; // = new IniFile(ROOT + CFG + NAME);
    }

    public class XMDCfgClass
    {
        public bool autoConnect = false;
        public int remotePort = 9002;
        public bool startXMDload = false;
        public bool startXMDgui = false;
        public bool startXMDview = false;
    }

    //-- Configuration data class
    public class ConfigDataClass
    {
        public static XMDCfgClass xmd = new XMDCfgClass();
    }

    public class IniFile
    {
        public string path;
        public static CenterIniFormat iniFmt = new CenterIniFormat();
        public static bool fileLoaded = false;
		public DataTable iniDataTable = new DataTable();
				
        public class CenterIniFormat
        {
            public string[] SectionName = { "XMDINTERFACE" };
            public string[][] FieldName = new string[][]
            {  
                new string[] { "AutoConnect", "RemotePort", "StartGAUGm1com", "StartGAUGgui", "StartGAUGview" },                                                       
            };
            public int[][] FieldCount = new int[][]
            {
                new int[] { 1, 1, 1, 1, 1 }
            };
            public int MaxFieldCount = 1;
        }

        //-----------------------------------------------------------------------------------------
        //-- INIfile API functions ----------------------------------------------------------------
        //-----------------------------------------------------------------------------------------
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public IniFile(string INIPath)
        {
            path = INIPath;
        }
        //-- Check if file already exists -------------------------------------------------------------------------
        public bool FileExists()
        {
            FileInfo File = new FileInfo(String.Format(this.path));
            return File.Exists;
        }
        //-- Delete existing file ---------------------------------------------------------------------------------
        public void DeleteFile()
        {
            FileInfo File = new FileInfo(String.Format(this.path));
            File.Delete();
        }
        //-- Move existing file -----------------------------------------------------------------------------------
        public void MoveFile()
        {
            FileInfo File = new FileInfo(String.Format(this.path));
            FileInfo FileCopy = new FileInfo(String.Format(this.path + ".bak"));
            if (FileCopy.Exists) FileCopy.Delete();
            File.MoveTo(String.Format(this.path + ".bak"));
        }
        //-- Recover existing file --------------------------------------------------------------------------------
        public void RecoverFile()
        {
            FileInfo File = new FileInfo(String.Format(this.path + ".bak"));
            File.MoveTo(String.Format(this.path));
        }
        //-- Write Data to the INI File ---------------------------------------------------------------------------
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }
        //-- Read Data Value From the Ini File --------------------------------------------------------------------
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, this.path);
            return temp.ToString();
        }
        //=========================================================================================================
        //-- File handling methods ----------------------------------------------------------------
        //-----------------------------------------------------------------------------------------
        //-- Create datagrid and label columns ----------------------------------------------------
        private void CreateGrid(int Columns)
        {
            iniDataTable.Clear();
            iniDataTable.Columns.Add("Section");
            iniDataTable.Columns.Add("Field");
            for (int i = 0; i < Columns; i++)
                iniDataTable.Columns.Add("Value " + i.ToString());
        }
        //-- Add a new field(s) to datagrid -------------------------------------------------------
        private void AddToGrid(string sectionName, string fieldName, int fieldCount)
        {
            try
            {
                //-- Define which character is separating fields
                char[] splitter = { ',' };
                string[] rowData = new string[fieldCount + 2];
                string item = IniReadValue(sectionName, fieldName);
                string[] valueData = item.Split(splitter);
                rowData[0] = sectionName;
                rowData[1] = fieldName;
                for (int i = 0; i < fieldCount; i++)
                    rowData[i + 2] = valueData[i];
                iniDataTable.Rows.Add(rowData);
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("INI file read fault", exc);
            }
        }
        //-- Put datagrid values to data structures -----------------------------------------------
        public void PutData()
        {
            int field = 0;
            try
            {
                for (field = 0; field < iniDataTable.Rows.Count; field++)
                {
                    //-- XMDInterface section 
                    if (iniDataTable.Rows[field][1].ToString() == IniFile.iniFmt.FieldName[0][0])
                        ConfigDataClass.xmd.autoConnect = bool.Parse(iniDataTable.Rows[field][2].ToString());
                    if (iniDataTable.Rows[field][1].ToString() == IniFile.iniFmt.FieldName[0][1])
                        ConfigDataClass.xmd.remotePort = Int32.Parse(iniDataTable.Rows[field][2].ToString());
                    if (iniDataTable.Rows[field][1].ToString() == IniFile.iniFmt.FieldName[0][2])
                        ConfigDataClass.xmd.startXMDload = bool.Parse(iniDataTable.Rows[field][2].ToString());
                    if (iniDataTable.Rows[field][1].ToString() == IniFile.iniFmt.FieldName[0][3])
                        ConfigDataClass.xmd.startXMDgui = bool.Parse(iniDataTable.Rows[field][2].ToString());
                    if (iniDataTable.Rows[field][1].ToString() == IniFile.iniFmt.FieldName[0][4])
                        ConfigDataClass.xmd.startXMDview = bool.Parse(iniDataTable.Rows[field][2].ToString());
                }
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("INI file read fault", exc);
            }
        }
        //-- Load data from INI file to datagrid --------------------------------------------------
        public void LoadData()
        {
            if (FileExists())
            {
                CreateGrid(iniFmt.MaxFieldCount);
                for (int Section = 0; Section < IniFile.iniFmt.SectionName.Length; Section++)
                {
                    for (int Field = 0; Field < IniFile.iniFmt.FieldCount[Section].Length; Field++)
                    {
                        AddToGrid(IniFile.iniFmt.SectionName[Section], IniFile.iniFmt.FieldName[Section][Field],
                                  IniFile.iniFmt.FieldCount[Section][Field]);
                    }
                }
                fileLoaded = true;
            }
        }
        //-----------------------------------------------------------------------------------------
    }
}
