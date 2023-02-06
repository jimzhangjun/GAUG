//=================================================================================================
//  Project:    RM312/SIPRO SIPROview
//  Module:     IniFileData.cs                                                                         
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
    public class IniFile
    {
        //-----------------------------------------------------------------------------------------
        //-- CLASS VARIABLES ----------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------
        public static ViewIniFormat viewINIFmt = new ViewIniFormat();

        public static string[] FileName = { "GAUGview.INI", "GraphicPages.INI", "Properties.ini"};
        public static bool[] Loaded = { false, false, false };

        public enum INI
        {
            GAUGVIEW,
            GRAPHICPAGES,
            PROPERTIES
        }

        public string filePath;      
        public DataTable iniDataTable = new DataTable();

        public class ViewIniFormat
        {
            public string[] SectionName = { "APPLICATION", "REMOTE", "ARCHIVE" };
            public string[][] FieldName = new string[][]
            {
                new string[] { "Customer", "GaugeId", "TaskPriority", "ProcessorAffinity" },
                new string[] { "Address", "Port", "AutoConnect" },
                new string[] { "Database", "PathFormat", "FileType", "MVMode1" },
            };
            public int[][] FieldCount = new int[][]
            {
                new int[] { 1, 1, 1, 1 },
                new int[] { 1, 1, 1 },
                new int[] { 1, 1, 1, 1 },
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
        //-- Set INIfile path and name ------------------------------------------------------------
        public IniFile(string INIPath)
        {
            filePath = INIPath;
        }
        //-- Check if file already exists ---------------------------------------------------------
        public bool FileExists()
        {
            FileInfo File = new FileInfo(String.Format(filePath));
            return File.Exists;
        }
        //-- Delete existing file -----------------------------------------------------------------
        private void DeleteFile()
        {
            FileInfo File = new FileInfo(String.Format(filePath));
            File.Delete();
        }
        //-- Write Data to the INI File -----------------------------------------------------------
        private void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, filePath);
        }
        //-- Read Data Value From the Ini File ----------------------------------------------------
        private string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, filePath);
            return temp.ToString();
        }
        //-----------------------------------------------------------------------------------------
        //-- File handling methods ----------------------------------------------------------------
        //-----------------------------------------------------------------------------------------
        //-- Create datagrid and label columns ----------------------------------------------------
        private void CreateGrid(int Columns)
        {
            iniDataTable.Clear();
            iniDataTable.Columns.Add("Section");
            iniDataTable.Columns.Add("Field");
            iniDataTable.Columns.Add("Value");
            int columnCount = iniDataTable.Columns.Count;
            for (int i = columnCount; i <= columnCount + Columns; i++)
                iniDataTable.Columns.Add("");
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
                    //-- APP section 
                    if (iniDataTable.Rows[field][0].ToString() == IniFile.viewINIFmt.SectionName[0])
                    {
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.viewINIFmt.FieldName[0][0])
                            ConfigDataClass.app.title = iniDataTable.Rows[field][2].ToString();
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.viewINIFmt.FieldName[0][1])
                            ConfigDataClass.app.gaugeId = iniDataTable.Rows[field][2].ToString();
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.viewINIFmt.FieldName[0][2])
                        {
                            switch (Int32.Parse(iniDataTable.Rows[field][2].ToString()))
                            {
                                case 0:
                                    ConfigDataClass.app.priority = ProcessPriorityClass.Idle;
                                    break;
                                case 1:
                                    ConfigDataClass.app.priority = ProcessPriorityClass.BelowNormal;
                                    break;
                                case 2:
                                    ConfigDataClass.app.priority = ProcessPriorityClass.Normal;
                                    break;
                                case 3:
                                    ConfigDataClass.app.priority = ProcessPriorityClass.AboveNormal;
                                    break;
                                case 4:
                                    ConfigDataClass.app.priority = ProcessPriorityClass.High;
                                    break;
                                case 5:
                                    ConfigDataClass.app.priority = ProcessPriorityClass.RealTime;
                                    break;
                                default:
                                    ConfigDataClass.app.priority = ProcessPriorityClass.Normal;
                                    break;
                            }
                        }                    
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.viewINIFmt.FieldName[0][3])
                            ConfigDataClass.app.affinity = Int32.Parse(iniDataTable.Rows[field][2].ToString());
                    }
                    //-- REMOTE section
                    if (iniDataTable.Rows[field][0].ToString() == IniFile.viewINIFmt.SectionName[1])
                    {
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.viewINIFmt.FieldName[1][0])
                            ConfigDataClass.remoting.address = IPAddress.Parse(iniDataTable.Rows[field][2].ToString());
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.viewINIFmt.FieldName[1][1])
                            ConfigDataClass.remoting.port = Int32.Parse(iniDataTable.Rows[field][2].ToString());
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.viewINIFmt.FieldName[1][2])
                            ConfigDataClass.remoting.autoConnect = bool.Parse(iniDataTable.Rows[field][2].ToString());
                    }
                    //-- Archive Section
                    if (iniDataTable.Rows[field][0].ToString() == IniFile.viewINIFmt.SectionName[2])
                    {
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.viewINIFmt.FieldName[2][0])
                            ConfigDataClass.archive.database = iniDataTable.Rows[field][2].ToString();
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.viewINIFmt.FieldName[2][1])
                            ConfigDataClass.archive.pathformat = Convert.ToInt16(iniDataTable.Rows[field][2].ToString());
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.viewINIFmt.FieldName[2][2])
                            ConfigDataClass.archive.fileType = Convert.ToInt32(iniDataTable.Rows[field][2].ToString());
                        if (iniDataTable.Rows[field][1].ToString() == IniFile.viewINIFmt.FieldName[2][3])
                            ConfigDataClass.archive.measmode = Convert.ToInt16(iniDataTable.Rows[field][2].ToString());
                    }
                }
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("INI file read fault", exc);
            }
        }
        //-- Save datagrid values to INI file -----------------------------------------------------
        public void SaveData()
        {
            //-- Need to assign each config value to the related data table field - Remoting
            //-- Application
            //-- Remoting
            FileClass.iniFile.iniDataTable.Rows[4][2] = ConfigDataClass.remoting.address.ToString();
            FileClass.iniFile.iniDataTable.Rows[5][2] = ConfigDataClass.remoting.port.ToString();
            FileClass.iniFile.iniDataTable.Rows[6][2] = ConfigDataClass.remoting.autoConnect.ToString();
            //-- Save data table to INI file
            int sectionOffset = 0;
            if (FileExists())
            {
                try
                {
                    DeleteFile();

                    for (int section = 0; section < IniFile.viewINIFmt.SectionName.Length; section++)
                    {
                        if (section > 0) sectionOffset += IniFile.viewINIFmt.FieldName[section - 1].Length;
                        for (int field = 0; field < IniFile.viewINIFmt.FieldCount[section].Length; field++)
                        {
                            string fieldData = iniDataTable.Rows[field + sectionOffset][2].ToString();
                            for (int i = 1; i < IniFile.viewINIFmt.FieldCount[section][field]; i++)
                                fieldData = fieldData + "," + iniDataTable.Rows[field + sectionOffset][i + 2].ToString();
                            this.IniWriteValue(IniFile.viewINIFmt.SectionName[section], IniFile.viewINIFmt.FieldName[section][field], fieldData);
                        }
                    }
                }
                catch (Exception exc)
                {
                    ErrorHandlerClass.ReportException("INI file save fault", exc);
                }
            }
        }
        //-- Load data from INI file to datagrid --------------------------------------------------
        public void LoadData(IniFile.INI FileType)
        {           
            filePath = FileClass.rootDir + FileClass.filePath + IniFile.FileName[(int)FileType];
            IniFile INI = new IniFile(filePath);
            
            if (INI.FileExists())
            {
                switch (FileType)
                {
                    case IniFile.INI.GAUGVIEW:
                        CreateGrid(IniFile.viewINIFmt.MaxFieldCount);
                        for (int Section = 0; Section < IniFile.viewINIFmt.SectionName.Length; Section++)
                        {
                            for (int Field = 0; Field < IniFile.viewINIFmt.FieldCount[Section].Length; Field++)
                            {
                                AddToGrid(IniFile.viewINIFmt.SectionName[Section], IniFile.viewINIFmt.FieldName[Section][Field],
                                          IniFile.viewINIFmt.FieldCount[Section][Field]);
                            }
                        }

                        IniFile.Loaded[(int)IniFile.INI.GAUGVIEW] = true;
                        break;
                }               
            }
        }
        //-- Load data from INI file to datagrid --------------------------------------------------
        public void LoadINIData(IniFile.INI FileType)
        {
            char[] splitter = { ',' };
            string section = "";
            string field = "";
            string item;
            string[] valueData;
            int itemnum;

            try
            {
                filePath = FileClass.rootDir + FileClass.filePath + IniFile.FileName[(int)FileType];
                if(FileExists())
                {
                    switch (FileType)
                    {
                        case IniFile.INI.GRAPHICPAGES:
                            section = "GENERAL";                                                                                        // section - GENERAL
                            field = "EdgePosition";
                            item = IniReadValue(section, field);
                            valueData = item.Split(splitter);
                            itemnum = (valueData.Length > MAXNUM.EDGEPOSITION) ? MAXNUM.EDGEPOSITION : valueData.Length;
                            for (int k = 0; k < itemnum; ++k)
                                ConfigDataClass.view.edgePos[k] = int.Parse(valueData[k]);

                            field = "Mode";
                            item = IniReadValue(section, field);
                            valueData = item.Split(splitter);
                            ConfigDataClass.view.mode = int.Parse(valueData[0]);
                            ConfigDataClass.view.onlinemode = int.Parse(valueData[1]);
                            ConfigDataClass.view.offlinemode = int.Parse(valueData[2]);

                            field = "Inverted";
                            item = IniReadValue(section, field);
                            ConfigDataClass.view.inverted = bool.Parse(item.ToString());

                            field = "Language";
                            item = IniReadValue(section, field);
                            ConfigDataClass.view.language = int.Parse(item.ToString());

                            field = "HardcopyWaitTime";
                            item = IniReadValue(section, field);
                            ConfigDataClass.view.hardcopywaittime = int.Parse(item.ToString());

                            field = "PdfScale";
                            item = IniReadValue(section, field);
                            ConfigDataClass.view.pdfScale = float.Parse(item.ToString());

                            field = "Timers";
                            item = IniReadValue(section, field);
                            valueData = item.Split(splitter);
                            itemnum = (valueData.Length > MAXNUM.TIMERS) ? MAXNUM.TIMERS : valueData.Length;
                            for (int k = 0; k < itemnum; ++k)
                                ConfigDataClass.view.timer[k] = int.Parse(valueData[k]);

                            field = "UnitStyle";
                            item = IniReadValue(section, field);
                            valueData = item.Split(splitter);
                            itemnum = (valueData.Length > MAXNUM.UNIT) ? MAXNUM.UNIT : valueData.Length;
                            for (int k = 0; k < itemnum; ++k)
                                ConfigDataClass.view.unit.style[k] = int.Parse(valueData[k]);

                            field = "UnitFactor";
                            item = IniReadValue(section, field);
                            valueData = item.Split(splitter);
                            itemnum = (valueData.Length > MAXNUM.UNIT) ? MAXNUM.UNIT : valueData.Length;
                            for (int k = 0; k < valueData.Length; ++k)
                                ConfigDataClass.view.unit.factor[k] = double.Parse(valueData[k]);

                            field = "UnitOffset";
                            item = IniReadValue(section, field);
                            valueData = item.Split(splitter);
                            itemnum = (valueData.Length > MAXNUM.UNIT) ? MAXNUM.UNIT : valueData.Length;
                            for (int k = 0; k < valueData.Length; ++k)
                                ConfigDataClass.view.unit.offset[k] = double.Parse(valueData[k]);

                            field = "Extension";
                            item = IniReadValue(section, field);
                            valueData = item.Split(splitter);
                            ConfigDataClass.view.extmin = float.Parse(valueData[0]);
                            ConfigDataClass.view.rate4ext = float.Parse(valueData[1]);
                            ConfigDataClass.view.rate2ext = float.Parse(valueData[2]);

                            field = "ScaleY";
                            item = IniReadValue(section, field);
                            if (item != "")
                            valueData = item.Split(splitter);
                            ConfigDataClass.view.yScale.mode = int.Parse(valueData[0]);
                            ConfigDataClass.view.yScale.lowerlimit = float.Parse(valueData[1]);
                            ConfigDataClass.view.yScale.upperlimit = float.Parse(valueData[2]);
                            
                            section = "COMMON"; field = "FormNumber";
                            item = IniReadValue(section, field);
                            ConfigDataClass.view.formnumber = int.Parse(item.ToString());
                            int itemnumj = (ConfigDataClass.view.formnumber > MAXNUM.EXFORM) ? MAXNUM.EXFORM : ConfigDataClass.view.formnumber;                            
                            for (int j = 0; j < itemnumj; ++j)
                            {
                                section = "COMMON";                                                                             // section repeated - COMMON 
                                field = "FormName" + j.ToString();
                                item = IniReadValue(section, field);
                                ConfigDataClass.view.formCfg[j].section = item.ToString();

                                section = ConfigDataClass.view.formCfg[j].section;                                              // section - (FormName?)
                                field = "Name";                              
                                item = IniReadValue(section, field);
                                ConfigDataClass.view.formCfg[j].name = item.ToString();

                                field = "Inverted";
                                item = IniReadValue(section, field);
                                if (item != "")
                                    ConfigDataClass.view.formCfg[j].inverted = bool.Parse(item.ToString());
                                else
                                    ConfigDataClass.view.formCfg[j].inverted = ConfigDataClass.view.inverted;

                                field = "Timers";
                                item = IniReadValue(section, field);
                                if (item != "")
                                {
                                    valueData = item.Split(splitter);
                                    itemnum = (valueData.Length > MAXNUM.TIMERS) ? MAXNUM.TIMERS : valueData.Length;
                                    for (int k = 0; k < itemnum; ++k)
                                        ConfigDataClass.view.formCfg[j].timer[k] = int.Parse(valueData[k]);
                                }
                                else
                                {
                                    for (int k = 0; k < MAXNUM.TIMERS; ++k)
                                        ConfigDataClass.view.formCfg[j].timer[k] = ConfigDataClass.view.timer[k];
                                }

                                field = "Location";
                                item = IniReadValue(section, field);
                                valueData = item.Split(splitter);
                                ConfigDataClass.view.formCfg[j].location.left = int.Parse(valueData[0]);
                                ConfigDataClass.view.formCfg[j].location.top = int.Parse(valueData[1]);
                                ConfigDataClass.view.formCfg[j].location.width = int.Parse(valueData[2]);
                                ConfigDataClass.view.formCfg[j].location.height = int.Parse(valueData[3]);

                                field = "UnitStyle";
                                item = IniReadValue(section, field);
                                if (item != "")
                                {
                                    valueData = item.Split(splitter);
                                    itemnum = (valueData.Length > MAXNUM.UNIT) ? MAXNUM.UNIT : valueData.Length;
                                    for (int k = 0; k < itemnum; ++k)
                                        ConfigDataClass.view.formCfg[j].unit.style[k] = int.Parse(valueData[k]);
                                }
                                else
                                    for (int k = 0; k < MAXNUM.UNIT; ++k)
                                        ConfigDataClass.view.formCfg[j].unit.style[k] = ConfigDataClass.view.unit.style[k];

                                field = "UnitFactor";
                                item = IniReadValue(section, field);
                                if (item != "")
                                {
                                    valueData = item.Split(splitter);
                                    itemnum = (valueData.Length > MAXNUM.UNIT) ? MAXNUM.UNIT : valueData.Length;
                                    for (int k = 0; k < itemnum; ++k)
                                        ConfigDataClass.view.formCfg[j].unit.factor[k] = double.Parse(valueData[k]);
                                }
                                else
                                    for (int k = 0; k < MAXNUM.UNIT; ++k)
                                        ConfigDataClass.view.formCfg[j].unit.factor[k] = ConfigDataClass.view.unit.factor[k];

                                field = "UnitOffset";
                                item = IniReadValue(section, field);
                                if (item != "")
                                {
                                    valueData = item.Split(splitter);
                                    itemnum = (valueData.Length > MAXNUM.UNIT) ? MAXNUM.UNIT : valueData.Length;
                                    for (int k = 0; k < itemnum; ++k)
                                        ConfigDataClass.view.formCfg[j].unit.offset[k] = double.Parse(valueData[k]);
                                }
                                else
                                    for (int k = 0; k < MAXNUM.UNIT; ++k)
                                        ConfigDataClass.view.formCfg[j].unit.offset[k] = ConfigDataClass.view.unit.offset[k];

                                field = "FormCategory";             // eg. INFO - PRODUCT/POS/NAVE/ENQUIRE - index
                                item = IniReadValue(section, field);
                                valueData = item.Split(splitter);
                                ConfigDataClass.view.formCfg[j].who.sort = int.Parse(valueData[0]);
                                ConfigDataClass.view.formCfg[j].who.type = int.Parse(valueData[1]);
                                ConfigDataClass.view.formCfg[j].who.index = int.Parse(valueData[2]);

                                field = "DataCategory";             // eg. NONE/SIPRO/RM210/RM310 - NONE/DEMOFILE/REMOTING/M1COM/CSVFILE/SQLSERVER - ? - NONE/CSVFILE/SQLSERVER
                                item = IniReadValue(section, field);
                                valueData = item.Split(splitter);
                                ConfigDataClass.view.formCfg[j].data.type = int.Parse(valueData[0]);
                                //ConfigDataClass.view.formCfg[j].data.source = int.Parse(valueData[1]);
                                ConfigDataClass.view.formCfg[j].data.number = int.Parse(valueData[1]);
                                ConfigDataClass.view.formCfg[j].data.output = int.Parse(valueData[2]);
                                //ConfigDataClass.view.formCfg[j].data.online = int.Parse(valueData[4]);
                                //ConfigDataClass.view.formCfg[j].data.offline = int.Parse(valueData[5]);

                                if (ConfigDataClass.view.mode == (int)DataSort.Source.M1COM)
                                {
                                    for (int m = 0; m < ConfigDataClass.view.formCfg[j].data.number; m++)
                                    {
                                        field = "Source" + m.ToString();
                                        item = IniReadValue(section, field);
                                        valueData = item.Split(splitter);
                                        ConfigDataClass.view.formCfg[j].sourceData[m].type = UInt16.Parse(valueData[0]);
                                        ConfigDataClass.view.formCfg[j].sourceData[m].name = valueData[1];
                                        ConfigDataClass.view.formCfg[j].sourceData[m].aindex = UInt16.Parse(valueData[2]);
                                    }
                                }

                                // speical handling in (FormName?)
                                switch (ConfigDataClass.view.formCfg[j].who.sort)
                                {
                                    case (int)FormSort.Category.INFO:
                                        switch (ConfigDataClass.view.formCfg[j].who.type)
                                        {
                                            case (int)FormSort.INFO.PRODUCT:
                                            case (int)FormSort.INFO.ENQUIRE:
                                                field = "WidthScale";
                                                item = IniReadValue(section, field);
                                                valueData = item.Split(splitter);
                                                itemnum = (valueData.Length > MAXNUM.WIDTHSCALE) ? MAXNUM.WIDTHSCALE : valueData.Length;
                                                for (int k = 0; k < itemnum; ++k)
                                                    ConfigDataClass.view.formCfg[j].widthscale[k] = int.Parse(valueData[k]);
                                                break;
                                        }
                                        break;
                                    case (int)FormSort.Category.PROFILE:
                                        switch (ConfigDataClass.view.formCfg[j].who.type)
                                        {
                                            case (int)FormSort.Profile.ZNIC:
                                            case (int)FormSort.Profile.FE:
                                                field = "ZoneNumber";
                                                item = IniReadValue(section, field);
                                                ConfigDataClass.view.formCfg[j].zonenumber = int.Parse(item.ToString());

                                                field = "StaticsMode";
                                                item = IniReadValue(section, field);
                                                ConfigDataClass.view.formCfg[j].staticsmode = int.Parse(item.ToString());

                                                break;
                                        }
                                        break;
                                }
                            }

                            section = "COMMON"; field = "Grid";
                            item = IniReadValue(section, field);
                            valueData = item.Split(splitter);
                            ConfigDataClass.view.grid.width = int.Parse(valueData[0]);
                            ConfigDataClass.view.grid.height = int.Parse(valueData[1]);

                            field = "Location";
                            item = IniReadValue(section, field);
                            valueData = item.Split(splitter);
                            ConfigDataClass.view.tablocation.left = int.Parse(valueData[0]);
                            ConfigDataClass.view.tablocation.top = int.Parse(valueData[1]);
                            ConfigDataClass.view.tablocation.width = int.Parse(valueData[2]);
                            ConfigDataClass.view.tablocation.height = int.Parse(valueData[3]);

                            field = "TabNumber";
                            item = IniReadValue(section, field);
                            ConfigDataClass.view.tabnumber = int.Parse(item.ToString());
                            int itemnumi = (ConfigDataClass.view.tabnumber > MAXNUM.TAB) ? MAXNUM.TAB : ConfigDataClass.view.tabnumber;
                            for (int i = 0; i < itemnumi; ++i)
                            {
                                section = "COMMON";                                                                                 // section repeated - COMMON
                                field = "TabName" + i.ToString();
                                item = IniReadValue(section, field);

                                section = item.ToString();                                                                          // section - (TabName?)
                                field = "Name";
                                item = IniReadValue(section, field);
                                ConfigDataClass.view.tabCfg[i].name = item.ToString();

                                field = "Grid";
                                item = IniReadValue(section, field);
                                valueData = item.Split(splitter);
                                ConfigDataClass.view.tabCfg[i].grid.width = int.Parse(valueData[0]);
                                ConfigDataClass.view.tabCfg[i].grid.height = int.Parse(valueData[1]);

                                field = "FormNumber";
                                item = IniReadValue(section, field);
                                ConfigDataClass.view.tabCfg[i].formnumber = int.Parse(item.ToString());
                                itemnumj = (ConfigDataClass.view.tabCfg[i].formnumber > MAXNUM.FORM) ? MAXNUM.FORM : ConfigDataClass.view.tabCfg[i].formnumber;

                                string sectioni = section;
                                for (int j = 0; j < itemnumj; ++j)
                                {
                                    section = sectioni;                                                             // section repeated - (TabName?)
                                    field = "FormName" + j.ToString();
                                    item = IniReadValue(section, field);
                                    ConfigDataClass.view.tabCfg[i].formCfg[j].section = item.ToString();

                                    section = ConfigDataClass.view.tabCfg[i].formCfg[j].section;                    // section - (FormName?) of (TabName?)

                                    field = "Inverted";
                                    item = IniReadValue(section, field);
                                    if (item != "")
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].inverted = bool.Parse(item.ToString());
                                    else
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].inverted = ConfigDataClass.view.inverted;

                                    field = "Timers";
                                    item = IniReadValue(section, field);
                                    if (item != "")
                                    {
                                        valueData = item.Split(splitter);
                                        itemnum = (valueData.Length > MAXNUM.TIMERS) ? MAXNUM.TIMERS : valueData.Length;
                                        for (int k = 0; k < itemnum; ++k)
                                            ConfigDataClass.view.tabCfg[i].formCfg[j].timer[k] = int.Parse(valueData[k]);
                                    }
                                    else
                                    {
                                        for (int k = 0; k < MAXNUM.TIMERS; ++k)
                                            ConfigDataClass.view.tabCfg[i].formCfg[j].timer[k] = ConfigDataClass.view.timer[k];
                                    }

                                    field = "Name";
                                    item = IniReadValue(section, field);
                                    ConfigDataClass.view.tabCfg[i].formCfg[j].name = item.ToString();

                                    field = "Location";
                                    item = IniReadValue(ConfigDataClass.view.tabCfg[i].formCfg[j].section, "Location");
                                    valueData = item.Split(splitter);
                                    ConfigDataClass.view.tabCfg[i].formCfg[j].location.left = int.Parse(valueData[0]);
                                    ConfigDataClass.view.tabCfg[i].formCfg[j].location.top = int.Parse(valueData[1]);
                                    ConfigDataClass.view.tabCfg[i].formCfg[j].location.width = int.Parse(valueData[2]);
                                    ConfigDataClass.view.tabCfg[i].formCfg[j].location.height = int.Parse(valueData[3]);

                                    ConfigDataClass.view.tabCfg[i].formCfg[j].xScale.mode = SCALEMODE.AUTO;

                                    field = "ScaleY";
                                    item = IniReadValue(section, field);
                                    if (item != "")
                                    {
                                        valueData = item.Split(splitter);
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].yScale.mode = int.Parse(valueData[0]);
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].yScale.lowerlimit = float.Parse(valueData[1]);
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].yScale.upperlimit = float.Parse(valueData[2]);
                                    }
                                    else
                                    {
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].yScale.mode = ConfigDataClass.view.yScale.mode;
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].yScale.lowerlimit = ConfigDataClass.view.yScale.lowerlimit;
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].yScale.upperlimit = ConfigDataClass.view.yScale.upperlimit;
                                    }

                                    field = "UnitStyle";
                                    item = IniReadValue(section, field);
                                    if (item != "")
                                    {
                                        valueData = item.Split(splitter);
                                        itemnum = (valueData.Length > MAXNUM.UNIT) ? MAXNUM.UNIT : valueData.Length;
                                        for (int k = 0; k < itemnum; ++k)
                                            ConfigDataClass.view.tabCfg[i].formCfg[j].unit.style[k] = int.Parse(valueData[k]);
                                    }
                                    else
                                        for (int k = 0; k < MAXNUM.UNIT; ++k)
                                            ConfigDataClass.view.tabCfg[i].formCfg[j].unit.style[k] = ConfigDataClass.view.unit.style[k];

                                    field = "UnitFactor";
                                    item = IniReadValue(section, field);
                                    if (item != "")
                                    {
                                        valueData = item.Split(splitter);
                                        itemnum = (valueData.Length > MAXNUM.UNIT) ? MAXNUM.UNIT : valueData.Length;
                                        for (int k = 0; k < itemnum; ++k)
                                            ConfigDataClass.view.tabCfg[i].formCfg[j].unit.factor[k] = double.Parse(valueData[k]);
                                    }
                                    else
                                        for (int k = 0; k < MAXNUM.UNIT; ++k)
                                            ConfigDataClass.view.tabCfg[i].formCfg[j].unit.factor[k] = ConfigDataClass.view.unit.factor[k];

                                    field = "UnitOffset";
                                    item = IniReadValue(section, field);
                                    if (item != "")
                                    {
                                        valueData = item.Split(splitter);
                                        itemnum = (valueData.Length > MAXNUM.UNIT) ? MAXNUM.UNIT : valueData.Length;
                                        for (int k = 0; k < itemnum; ++k)
                                            ConfigDataClass.view.tabCfg[i].formCfg[j].unit.offset[k] = double.Parse(valueData[k]);
                                    }
                                    else
                                        for (int k = 0; k < MAXNUM.UNIT; ++k)
                                            ConfigDataClass.view.tabCfg[i].formCfg[j].unit.offset[k] = ConfigDataClass.view.unit.offset[k];

                                    field = "FormCategory";       // eg. PROFILE/TRENDLEN/TREENDTIME/MAP - last profile... - index
                                    item = IniReadValue(section, field);
                                    valueData = item.Split(splitter);
                                    ConfigDataClass.view.tabCfg[i].formCfg[j].who.sort = int.Parse(valueData[0]);
                                    ConfigDataClass.view.tabCfg[i].formCfg[j].who.type = int.Parse(valueData[1]);
                                    ConfigDataClass.view.tabCfg[i].formCfg[j].who.index = int.Parse(valueData[2]);

                                    field = "DataCategory";     // eg. NONE/SIPRO/RM210/RM310 - NONE/DEMOFILE/REMOTING/M1COM/CSVFILE/SQLSERVER - ? - NONE/CSVFILE/SQLSERVER
                                    item = IniReadValue(section, field);
                                    valueData = item.Split(splitter);
                                    ConfigDataClass.view.tabCfg[i].formCfg[j].data.type = int.Parse(valueData[0]);
                                    //ConfigDataClass.view.tabCfg[i].formCfg[j].data.source = int.Parse(valueData[1]);
                                    ConfigDataClass.view.tabCfg[i].formCfg[j].data.number = int.Parse(valueData[1]);
                                    ConfigDataClass.view.tabCfg[i].formCfg[j].data.output = int.Parse(valueData[2]);
                                    //ConfigDataClass.view.tabCfg[i].formCfg[j].data.online = int.Parse(valueData[4]);
                                    //ConfigDataClass.view.tabCfg[i].formCfg[j].data.offline = int.Parse(valueData[5]);

                                    if (ConfigDataClass.view.mode == (int)DataSort.Source.M1COM)
                                    {
                                        for (int m = 0; m < ConfigDataClass.view.tabCfg[i].formCfg[j].data.number; m++)
                                        {
                                            field = "Source" + m.ToString();
                                            item = IniReadValue(section, field);
                                            valueData = item.Split(splitter);
                                            ConfigDataClass.view.tabCfg[i].formCfg[j].sourceData[m].type = UInt16.Parse(valueData[0]);
                                            ConfigDataClass.view.tabCfg[i].formCfg[j].sourceData[m].name = valueData[1];
                                            ConfigDataClass.view.tabCfg[i].formCfg[j].sourceData[m].aindex = UInt16.Parse(valueData[2]);
                                        }
                                    }

                                    // speical handling in (FormName?) of (TabName?)
                                    switch (ConfigDataClass.view.tabCfg[i].formCfg[j].who.sort)
                                    {
                                        case (int)FormSort.Category.PROFILE:
                                            switch (ConfigDataClass.view.tabCfg[i].formCfg[j].who.type)
                                            {
                                                case (int)FormSort.Profile.LEFT:
                                                case (int)FormSort.Profile.RIGHT:
                                                    field = "DataIndex";
                                                    item = IniReadValue(section, field);
                                                    ConfigDataClass.view.tabCfg[i].formCfg[j].dataIndex[0] = int.Parse(item.ToString());
                                                    break;
                                                case (int)FormSort.Profile.ZNIC:
                                                case (int)FormSort.Profile.FE:
                                                    field = "ZoneNumber";
                                                    item = IniReadValue(section, field);
                                                    if (item != "")
                                                        ConfigDataClass.view.tabCfg[i].formCfg[j].zonenumber = int.Parse(item.ToString());
                                                    else
                                                        ConfigDataClass.view.tabCfg[i].formCfg[j].zonenumber = ConfigDataClass.view.formCfg[j].zonenumber;

                                                    field = "StaticsMode";
                                                    item = IniReadValue(section, field);
                                                    if (item != "")
                                                        ConfigDataClass.view.tabCfg[i].formCfg[j].staticsmode = int.Parse(item.ToString());
                                                    else
                                                        ConfigDataClass.view.tabCfg[i].formCfg[j].staticsmode = ConfigDataClass.view.formCfg[j].staticsmode;
                                                    break;
                                            }
                                            break;
                                        case (int)FormSort.Category.TRENDLEN:
                                            switch (ConfigDataClass.view.tabCfg[i].formCfg[j].who.type)
                                            {
                                                case (int)FormSort.TrendLen.EDGE: 
                                                case (int)FormSort.TrendLen.CROWN:
                                                case (int)FormSort.TrendLen.WEDGE:
                                                case (int)FormSort.TrendLen.CROWNWEDGE:
                                                case (int)FormSort.TrendLen.SYMASYM:
                                                case (int)FormSort.TrendLen.PROFILE:
                                                    field = "CheckBoxText";
                                                    item = IniReadValue(section, field);
                                                    if (item != "")
                                                    {
                                                        valueData = item.Split(splitter);
                                                        itemnum = (valueData.Length > MAXNUM.CHECKBOX) ? MAXNUM.CHECKBOX : valueData.Length;
                                                        for (int k = 0; k < itemnum; k++)
                                                            ConfigDataClass.view.tabCfg[i].formCfg[j].checkBoxText[k] = valueData[k].ToString();
                                                    }

                                                    field = "DataIndex";
                                                    item = IniReadValue(section, field);
                                                    if (item != "")
                                                    {
                                                        valueData = item.Split(splitter);
                                                        itemnum = (valueData.Length > MAXNUM.CHECKBOX) ? MAXNUM.CHECKBOX : valueData.Length;
                                                        for (int k = 0; k < itemnum; k++)
                                                            ConfigDataClass.view.tabCfg[i].formCfg[j].dataIndex[k] = int.Parse(valueData[k]);
                                                        for (int k = itemnum; k < MAXNUM.CHECKBOX; k++)
                                                            ConfigDataClass.view.tabCfg[i].formCfg[j].dataIndex[k] = -1;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                            break;

                        case IniFile.INI.PROPERTIES:
                            section = "COMMON";                                                                         // section - COMMON
                            field = "FormBackColor";
                            item = IniReadValue(section, field);
                            ConfigDataClass.view.formBackColor = ColorTranslator.FromHtml(item.ToString());

                            field = "TabBackColor";
                            item = IniReadValue(section, field);
                            ConfigDataClass.view.tabBackColorActived = ColorTranslator.FromHtml(item.ToString());

                            field = "ChartBackColor";
                            item = IniReadValue(section, field);
                            if (item != "")
                                ConfigDataClass.view.chartBackColor = ColorTranslator.FromHtml(item.ToString());
                            else
                                ConfigDataClass.view.chartBackColor = ConfigDataClass.view.formBackColor;

                            field = "Font";
                            item = IniReadValue(section, field);
                            valueData = item.Split(splitter);
                            ConfigDataClass.view.comFont.name = valueData[0];
                            ConfigDataClass.view.comFont.size = int.Parse(valueData[1]);
                            ConfigDataClass.view.comFont.style = (FontStyle)int.Parse(valueData[2]);

                            field = "FontColor";
                            item = IniReadValue(section, field);
                            ConfigDataClass.view.comFont.color = ColorTranslator.FromHtml(item.ToString());

                            field = "TextColorRead";
                            item = IniReadValue(section, field);
                            ConfigDataClass.view.txtBackColorRead = ColorTranslator.FromHtml(item.ToString());

                            field = "TextColorWrite";
                            item = IniReadValue(section, field);
                            ConfigDataClass.view.txtBackColorWrite = ColorTranslator.FromHtml(item.ToString());

                            field = "StatusColorOK";
                            item = IniReadValue(section, field);
                            ConfigDataClass.view.statColorOK = ColorTranslator.FromHtml(item.ToString());

                            field = "StatusColorFail";
                            item = IniReadValue(section, field);
                            ConfigDataClass.view.statColorFail = ColorTranslator.FromHtml(item.ToString());

                            field = "FastlineWidth";
                            item = IniReadValue(section, field);
                            ConfigDataClass.view.fastlineWidth = int.Parse(item.ToString());

                            field = "FastlineColor";
                            item = IniReadValue(section, field);
                            ConfigDataClass.view.fastlineColor = ColorTranslator.FromHtml(item.ToString());

                            itemnumi = (ConfigDataClass.view.formnumber > MAXNUM.FORM) ? MAXNUM.FORM : ConfigDataClass.view.formnumber;
                            for (int i = 0; i < itemnumi; ++i)
                            {
                                section = ConfigDataClass.view.formCfg[i].section;                                                  // Section - (FormName?)          
                                field = "FormBackColor";
                                item = IniReadValue(section, field);
                                if (item != "")
                                    ConfigDataClass.view.formCfg[i].formBackColor = ColorTranslator.FromHtml(item.ToString());
                                else
                                    ConfigDataClass.view.formCfg[i].formBackColor = ConfigDataClass.view.formBackColor;

                                field = "ChartBackColor";
                                item = IniReadValue(section, field);
                                if (item != "")
                                    ConfigDataClass.view.formCfg[i].chartBackColor = ColorTranslator.FromHtml(item.ToString());
                                else
                                    ConfigDataClass.view.formCfg[i].chartBackColor = ConfigDataClass.view.chartBackColor;

                                field = "LabBackColor";
                                item = IniReadValue(section, field);
                                if (item != "")
                                    ConfigDataClass.view.formCfg[i].labBackColor = ColorTranslator.FromHtml(item.ToString());

                                field = "LabFont";
                                item = IniReadValue(section, field);
                                if (item != "")
                                {
                                    valueData = item.Split(splitter);
                                    ConfigDataClass.view.formCfg[i].labFont.name = valueData[0];
                                    ConfigDataClass.view.formCfg[i].labFont.size = int.Parse(valueData[1]);
                                    ConfigDataClass.view.formCfg[i].labFont.style = (FontStyle)int.Parse(valueData[2]);
                                }
                                else
                                {
                                    ConfigDataClass.view.formCfg[i].labFont.name = ConfigDataClass.view.comFont.name;
                                    ConfigDataClass.view.formCfg[i].labFont.size = ConfigDataClass.view.comFont.size;
                                    ConfigDataClass.view.formCfg[i].labFont.style = ConfigDataClass.view.comFont.style;
                                }

                                field = "LabFontColor";
                                item = IniReadValue(section, field);
                                if (item != "")
                                    ConfigDataClass.view.formCfg[i].labFont.color = ColorTranslator.FromHtml(item.ToString());
                                else
                                    ConfigDataClass.view.formCfg[i].labFont.color = ConfigDataClass.view.comFont.color;

                                field = "FontBackColorRead";
                                item = IniReadValue(section, field);
                                if (item != "")                                
                                    ConfigDataClass.view.formCfg[i].txtBackColorRead = ColorTranslator.FromHtml(item.ToString());
                                else
                                    ConfigDataClass.view.formCfg[i].txtBackColorRead = ConfigDataClass.view.txtBackColorRead;

                                field = "FontBackColorWrite";
                                item = IniReadValue(section, field);
                                if (item != "")
                                    ConfigDataClass.view.formCfg[i].txtBackColorWrite = ColorTranslator.FromHtml(item.ToString());
                                else
                                    ConfigDataClass.view.formCfg[i].txtBackColorWrite = ConfigDataClass.view.txtBackColorWrite;

                                field = "StatusColorOK";
                                item = IniReadValue(section, field);
                                if (item != "")
                                    ConfigDataClass.view.formCfg[i].statColorOK = ColorTranslator.FromHtml(item.ToString());
                                else
                                    ConfigDataClass.view.formCfg[i].statColorOK = ConfigDataClass.view.statColorOK;

                                field = "StatusColorFail";
                                item = IniReadValue(section, field);
                                if (item != "")
                                    ConfigDataClass.view.formCfg[i].statColorFail = ColorTranslator.FromHtml(item.ToString());
                                else
                                    ConfigDataClass.view.formCfg[i].statColorFail = ConfigDataClass.view.statColorFail;

                                field = "TextFont";
                                item = IniReadValue(section, field);
                                if (item != "")
                                {
                                    valueData = item.Split(splitter);
                                    ConfigDataClass.view.formCfg[i].txtFont.name = valueData[0];
                                    ConfigDataClass.view.formCfg[i].txtFont.size = int.Parse(valueData[1]);
                                    ConfigDataClass.view.formCfg[i].txtFont.style = (FontStyle)int.Parse(valueData[2]);
                                }
                                else
                                {
                                    ConfigDataClass.view.formCfg[i].txtFont.name = ConfigDataClass.view.comFont.name;
                                    ConfigDataClass.view.formCfg[i].txtFont.size = ConfigDataClass.view.comFont.size;
                                    ConfigDataClass.view.formCfg[i].txtFont.style = ConfigDataClass.view.comFont.style;
                                }

                                field = "TextFontColor";
                                item = IniReadValue(section, field);
                                if (item != "")
                                    ConfigDataClass.view.formCfg[i].txtFont.color = ColorTranslator.FromHtml(item.ToString());
                                else
                                    ConfigDataClass.view.formCfg[i].txtFont.color = ConfigDataClass.view.comFont.color;
                            }
                            
                            itemnumi = (ConfigDataClass.view.tabnumber > MAXNUM.TAB) ? MAXNUM.TAB : ConfigDataClass.view.tabnumber;
                            for (int i = 0; i < itemnumi; ++i)
                            {
                                itemnumj = (ConfigDataClass.view.tabCfg[i].formnumber > MAXNUM.FORM) ? MAXNUM.FORM : ConfigDataClass.view.tabCfg[i].formnumber;
                                for (int j = 0; j < itemnumj; ++j)
                                {
                                    section = ConfigDataClass.view.tabCfg[i].formCfg[j].section;                                            // section - (FormName?) of (TabName?)
                                    field = "FormBackColor";
                                    item = IniReadValue(section, field);
                                    if (item != "")
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].formBackColor = ColorTranslator.FromHtml(item.ToString());
                                    else
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].formBackColor = ConfigDataClass.view.formBackColor;

                                    field = "ChartBackColor";
                                    item = IniReadValue(section, field);
                                    if (item != "")
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].chartBackColor = ColorTranslator.FromHtml(item.ToString());
                                    else
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].chartBackColor = ConfigDataClass.view.chartBackColor;

                                    field = "LabBackColor";
                                    item = IniReadValue(section, field);
                                    if (item != "")
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].labBackColor = ColorTranslator.FromHtml(item.ToString());

                                    field = "LabFont";
                                    item = IniReadValue(section, field);
                                    if (item != "")
                                    {
                                        valueData = item.Split(splitter);
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].labFont.name = valueData[0];
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].labFont.size = float.Parse(valueData[1]);
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].labFont.style = (FontStyle)int.Parse(valueData[2]);
                                    }
                                    else
                                    {
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].labFont.name = ConfigDataClass.view.comFont.name;
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].labFont.size = ConfigDataClass.view.comFont.size;
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].labFont.style = ConfigDataClass.view.comFont.style;
                                    }

                                    field = "LabFontColor";
                                    item = IniReadValue(section, field);
                                    if (item != "")
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].labFont.color = ColorTranslator.FromHtml(item.ToString());
                                    else
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].labFont.color = ConfigDataClass.view.comFont.color;

                                    field = "FontBackColorRead";
                                    item = IniReadValue(section, field);
                                    if (item != "")
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].txtBackColorRead = ColorTranslator.FromHtml(item.ToString());
                                    else
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].txtBackColorRead = ConfigDataClass.view.txtBackColorRead;

                                    field = "FontBackColorWrite";
                                    item = IniReadValue(section, field);
                                    if (item != "")
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].txtBackColorWrite = ColorTranslator.FromHtml(item.ToString());
                                    else
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].txtBackColorWrite = ConfigDataClass.view.txtBackColorWrite;

                                    field = "TextFont";
                                    item = IniReadValue(section, field);
                                    if (item != "")
                                    {
                                        valueData = item.Split(splitter);
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].txtFont.name = valueData[0];
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].txtFont.size = int.Parse(valueData[1]);
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].txtFont.style = (FontStyle)int.Parse(valueData[2]);
                                    }
                                    else
                                    {
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].txtFont.name = ConfigDataClass.view.comFont.name;
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].txtFont.size = ConfigDataClass.view.comFont.size;
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].txtFont.style = ConfigDataClass.view.comFont.style;
                                    }

                                    field = "TextFontColor";
                                    item = IniReadValue(section, field);
                                    if (item != "")
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].txtFont.color = ColorTranslator.FromHtml(item.ToString());
                                    else
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].txtFont.color = ConfigDataClass.view.comFont.color;

                                    field = "FastlineWidth";
                                    item = IniReadValue(section, field);
                                    if (item != "")
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].fastlineWidth[0] = int.Parse(item.ToString());
                                    else
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].fastlineWidth[0] = ConfigDataClass.view.fastlineWidth;

                                    int NumOfFastline = 0;
                                    field = "FastlineNumber";
                                    item = IniReadValue(section, field);
                                    if (item != "")                                    
                                        NumOfFastline = int.Parse(item.ToString());                                    
                                    if (NumOfFastline > 0)
                                    {
                                        itemnum = (NumOfFastline > MAXNUM.FASTLINES) ? MAXNUM.FASTLINES : NumOfFastline;
                                        for (int k = 0; k < itemnum; ++k)
                                        {
                                            field = "FastlineColor" + k.ToString();
                                            item = IniReadValue(section, field);
                                            ConfigDataClass.view.tabCfg[i].formCfg[j].fastlineColor[k] = ColorTranslator.FromHtml(item.ToString());
                                        }
                                    }
                                    else
                                        ConfigDataClass.view.tabCfg[i].formCfg[j].fastlineColor[0] = ConfigDataClass.view.fastlineColor;

                                    // speical handling in (FormName?) of (TabName?)
                                    switch (ConfigDataClass.view.tabCfg[i].formCfg[j].who.sort)
                                    {
                                        case (int)FormSort.Category.MAP:
                                            field = "PPTOColor";
                                            item = IniReadValue(section, field);
                                            ConfigDataClass.view.tabCfg[i].formCfg[j].gridColor[TOL.PPTO] = ColorTranslator.FromHtml(item.ToString());

                                            field = "PTOLColor";
                                            item = IniReadValue(section, field);
                                            ConfigDataClass.view.tabCfg[i].formCfg[j].gridColor[TOL.PTOL] = ColorTranslator.FromHtml(item.ToString());

                                            field = "INTOColor";
                                            item = IniReadValue(section, field);
                                            ConfigDataClass.view.tabCfg[i].formCfg[j].gridColor[TOL.INTO] = ColorTranslator.FromHtml(item.ToString());

                                            field = "MTOLColor";
                                            item = IniReadValue(section, field);
                                            ConfigDataClass.view.tabCfg[i].formCfg[j].gridColor[TOL.MTOL] = ColorTranslator.FromHtml(item.ToString());

                                            field = "MMTOColor";
                                            item = IniReadValue(section, field);
                                            ConfigDataClass.view.tabCfg[i].formCfg[j].gridColor[TOL.MMTO] = ColorTranslator.FromHtml(item.ToString());
                                            break;
                                    }
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception exc)
            {
                string errMsg = string.Format("LoadINIData Error in [{0}]'s {1} ", section, field);
                ErrorHandlerClass.ReportException(errMsg, exc);
            }
        }
        //-----------------------------------------------------------------------------------------
    }
}
//=================================================================================================
