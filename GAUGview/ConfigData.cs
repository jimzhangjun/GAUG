//=================================================================================================
//  Project:    RM312/SIPRO SIPROview
//  Module:     ConfigData.cs                                                                         
//  Author:     Andrew Powell
//  Date:       26/04/2010
//  
//  Details:    Definition of configuration data types 
//  
//=================================================================================================
using System;
using System.Net;
using System.Diagnostics;
using System.Data;
using System.Data.OleDb;
using System.Drawing;

namespace GAUGview
{
    public class MAXNUM
    {
        public static int EXFORM = 3;
        public static int TAB = 10;
        public static int FORM = 10;
        public static int UNIT = 10;         // same to UnitCfgClass.type.NUMUNIT
        public static int GRIDCOLOR = 5;
        public static int FASTLINES = 8;
        public static int TIMERS = 5;
        public static int EDGEPOSITION = 6;
        public static int SOURCE = 2;
        public static int HEIGHT = 3;
        public static int MDIAG = 500;
        public static int STRIP = 10000;
        public static int ANALOG = 1000;
        public static int CHECKBOX = 3;
        public static int WIDTHSCALE = 10;
        public static int COIL = 20000;
        public static int DATASOURCE = 100; // for the data in M1
    }

    public class DISPLAYPARAMS
    {
        public static int heightmenu = 35;
        public static int heighttab = 25;   //140;
        public static int offsetleft = 6;
        public static int offsettop = 0;
        public static int offsetright = 6;
        public static int offsetbottom = 10;
    }

    public class TOL
    {
        public static int INTO = 0;
        public static int PPTO = 1;
        public static int PTOL = 2;
        public static int MMTO = 3;
        public static int MTOL = 4;
    }

    public class SCALEMODE
    {
        public const int AUTO = 0;
        public const int MANUAL = 1;
        public const int ABS = 2;
        public const int PERC = 3;
    }

    //-- Coil report in RM210 (SP10874)
    public class CoilTable
    {
        public enum colProduct
        {
            Coilid = 0,
            Setpoint,
            PTol,
            MTol,
            Alloyfactor,
            Offset,
            Passnumber,
            Lastpass,
            Elemnumber
        }
        public enum colData
        {
            Length = 0,
            MeasThick,
            MeasStatus
        }
        public enum colSummary
        {
            Date = 0,
            Time,
            Measmode,
            Length,
            Setpoint,
            PTol,
            MTol,
            Alloyfactor,
            Offset,
            Min,
            Max,
            Avg,
            Sigma,
            CP,
            CPK,
        }
        public class row
        {
            public const int Product = 7;
            public const int Data = 13;
            public const int Summary = 1;
        }
    }

    //-- Profile report in RM310
    public class ProfileTable
    {
        //public const int HEADLINES = 12;
        //public const int SUMMARYLINES = 6;

        public enum colProduct
        {
            Coilid = 0,
            ProductCode,
            MeasMode,
            Setpoint1,
            PTol1,
            MTol1,
            Alloyfactor1,
            Offset1,
            Setpoint2,
            PTol2,
            MTol2,
            Alloyfactor2,
            Offset2,
        }
        public enum colData
        {
            Date = 0,
            Time,
            Measmode,
            Length,
            Min,
            Max,
            Avg,
            Sigma,
            Triple,
            Triplemid,
            Tripleright,
            Zone
        }
        public enum colSummary
        {
            Date = 0,
            Time,
            Measmode,
            Length,
            Setpoint1,
            PTol1,
            MTol1,
            Alloyfactor1,
            Offset1,
            Min1,
            Max1,
            Avg1,
            Sigma1,
            CP1,
            CPK1,
            Setpoint2,
            PTol2,
            MTol2,
            Alloyfactor2,
            Offset2,
            Min2,
            Max2,
            Avg2,
            Sigma2,
            CP2,
            CPK2
        }
        public class row
        {
            public const int HEADLINES = 12;
            public const int SUMMARYLINES = 6;
            public const int Product = 10;
            public const int Data = 14;
        }
    }

    //-- Mdiag file in SIPRO
    public class MdiagTable
    {
        public enum col
        {
            Value = 1,
            Det,
            S1SignalData,
            S2SignalData,
            S1EdgePos,
            S2EdgePos,
            S1XAData,
            S2XAData,
            S1StdzOffset,
            S2StdzOffset,
            AlloyComp,
            TempComp,
            Contour,
            Shape,
            S1BadDets,
            S2BadDets,
            Composite,
            Temperature
        }
    }

    //-- Strip file in SIPRO
    public class StripTable
    {
        public enum col
        {
            Time = 0,
            CoilID,
            CoilID2,
            Mode,
            Length,
            NomThick,
            MeasThick,
            NomWidth,
            MeasWidth,
            CLOffset,
            AlloyTable,
            InputAI,
            AlloyComp,
            TempTable,
            Temp,
            TempComp,
            S1StdzOffset,
            S2StdzOffset,
            S1Zero,
            S2Zero,
            OE0,
            OE1,
            OE2,
            OE3,
            OE4,
            OE5,
            BE0,
            BE1,
            BE2,
            BE3,
            BE4,
            BE5,
            OEHeight,
            CLHeight,
            BEHeight,
            SH0,
            SH1,
            SH2,
            SH3,
            SH4,
            SH5,
            SH6,
            S1OE,
            S1BA,
            S2OE,
            S2BA,
            thkProfStart,
            thkProfStop,
            OpenX,
            OpenY,
            CentreX,
            CentreY,
            BackX,
            BackY
        }
    }
    //-- Form Sort
    public class FormSort
    {
        public enum Category
        {
            PROFILE = 1,
            TRENDLEN,
            TREENDTIME,
            MAP,
            INFO
        }

        public enum Profile
        {
            SINGLE = 1,
            AVERAGE,
            LAST,
            LEFT,
            RIGHT,
            TEMPSINGLE,
            TEMPAVG,
            CONTOUR,
            SHAPE,
            SINGLEAVG,
            ZNIC,
            FE
        }

        public enum TrendLen
        {
            THICK = 1,
            EDGE,
            CROWN,
            WEDGE,
            OFFSET,
            WIDTH,
            SYM,
            ASYM,
            TEMP,
            CROWNWEDGE,
            SYMASYM,
            PROFILE
        }

        public class TrendTime
        {
            public enum Ao
            {
                NUMAO
            }

            public enum Ai
            {
                SPEED=100,
                AGT,
                DETTEMP,
                DEWPOINT,
                TUBETEMP1,
                TUBETEMP2,
                TOPARMTEMP1,
                TOPARMTEMP2,
                ACTS1KVS,
                ACTS1MAS,
                ACTS2KVS,
                ACTS2MAS,
                MILLPYROTEMP,
                MILLSTRIPANGLE,
                MILLAI3,
                MILLAI4,
                NUMAI
            }

            public enum Do
            {
                NUMDO
            }

            public enum Di
            {
                NUMDI
            }
        }

        public enum Map
        {
            THICK = 1,
            SHAPE,
            TEMP,
            HEIGHT
        }

        public enum INFO
        {
            PRODUCT = 1,
            POS,
            NAVE,
            ENQUIRE
        }
    }

    //-- Data Sort
    public class DataSort
    {
        public enum Type
        {
            NONE = 0,
            SIPRO,
            RM310,
            RM210
        }

        public enum Source
        {
            NONE = 0,
            DEMOFILE,            
            REMOTING,
            M1COM,
            CSVFILE,
            SQLSERVER
        }

        public enum Output
        {
            NONE = 0,
            CSVFILE,
            SQLSERVER
        }
    }
    public class FileClass
    {
        //-- INI file configuration parameters
        public static string rootDir = @"C:\Thermo";
        public static string archivePath = @"\Data\";
        public static string filePath = @"\GAUGview\Configuration\";
        public static string diagPath = @"\GAUGview\\Diagnostic\";
        public static string fileName = "GAUGview.INI";
        public static string langFileName = "HMIText.mdb";
        public static IniFile iniFile = new IniFile(rootDir + filePath + fileName);
        public static Translation textItem = new Translation(rootDir + filePath + langFileName);

        //-- Create directory if it doesn't exist
        public static void CreateDir(System.IO.DirectoryInfo oDirInfo)
        {
            if (oDirInfo.Parent != null)
                CreateDir(oDirInfo.Parent);
            if (!oDirInfo.Exists)
                oDirInfo.Create();
        }
        //-- Return diagnostic directory path
        public static string Diagnostic()
        {
            DateTime date = DateTime.Today;
            string dirName = rootDir + diagPath + date.ToString("yyyyMMdd");
            CreateDir(new System.IO.DirectoryInfo(dirName));
            return dirName + "\\";
        }
    }
    //-- Application configuration settings
    public class AppCfgClass
    {
        public string title;
        public string gaugeId = "SPxxxx";
        public ProcessPriorityClass priority = ProcessPriorityClass.Normal;
        public int affinity = 1;
    }
    //-- REMOTING configuration settings
    public class RemoteCfgClass
    {
        public IPAddress address;
        public bool autoConnect = false;
        public int port = 9002;
    }
    //-- ARCHIVE configuration settings
    public class ArchiveCfgClass
    {
        public string database;
        public int pathformat;
        public int fileType;
        public int measmode;
    }
    //-- UNIT configuration settings
    public class UnitCfgClass
    {
        public static string[][] name = new string[][]
        {
            new string[] { "#" },
            new string[] { "mm", "in", "mils", "um" },
            new string[] { "mm", "m", "in" },
            new string[] { "m", "ft" },            
            new string[] { "mm/s", "m/min" },
            new string[] { "C", "F" },
            new string[] { "mm" },
            new string[] { "d" },
            new string[] { "i-unit"},
        };
        
        public enum type
        {
            NONE,
            THICK,
            WIDTH,
            LENGTH,
            SPEED,
            TEMPERATURE,
            HEIGHT,
            ANGLE,
            FLATNESS,
            NUMUNIT
        }

        public int[] style = new int [MAXNUM.UNIT];
        public double[] factor = new double[MAXNUM.UNIT];
        public double[] offset = new double[MAXNUM.UNIT];
    }
    //-- Classification
    public class FormCategory
    {
        public int sort;
        public int type;
        public int index;
    }
    public class DataCategory
    {
        public int type;
        //public int source;
        public int number;
        public int output;
        //public int online;
        //public int offline;
    }
    //-- DataSource from M1
    public class DataSource
    {
        public ushort type;
        public string name;
        public ushort index;
        public ushort aindex;
    }
    //-- DISPLAY configuration settings
    public class Rect
    {
        public float left;
        public float top;
        public float width;
        public float height;
    }
    // Font
    public class MyFont
    {
        public string name;
        public float size;
        public FontStyle style;
        public Color color;
    }
    // Scale
    public class Scale
    {
        public int mode = SCALEMODE.AUTO;
        public float upperlimit;
        public float lowerlimit;
    }
    public class ViewCfgClass
    {
        //-- General
        public int language = 0;
        public int[] timer = new int[MAXNUM.TIMERS];
        public int[] edgePos = new int[MAXNUM.EDGEPOSITION];
        public int mode = 0;    // 0: demo  1: live 2: replay     
        public int onlinemode;
        public int offlinemode;
        public int hardcopywaittime = 2000;
        public float pdfScale = 30.0f;
        public bool inverted = false;
        public UnitCfgClass unit = new UnitCfgClass();

        // Scale
        public Scale yScale = new Scale();        

        // extension 
        public float extmin = 100;
        public float rate4ext = 0.98f;
        public float rate2ext = 1.3f;

        // Layouts
        public Rect grid = new Rect();
        public int formnumber = MAXNUM.EXFORM;        
        public FormCfgClass[] formCfg = new FormCfgClass[MAXNUM.EXFORM];

        public int tabnumber = MAXNUM.TAB;
        public TabCfgClass[] tabCfg = new TabCfgClass[MAXNUM.TAB];        
        public Rect tablocation = new Rect ();

        //-- Properties
        public Color formBackColor;
        public Color tabBackColorActived;
        public Color chartBackColor;
        public MyFont comFont = new MyFont();
        public Color txtBackColorRead;
        public Color txtBackColorWrite;
        public Color statColorOK;
        public Color statColorFail;
        public int fastlineWidth;
        public Color fastlineColor = new Color();
        public Color[] gridColor = new Color[MAXNUM.GRIDCOLOR];

        public ViewCfgClass()
        {            
            for (int i = 0; i < MAXNUM.EXFORM; ++i)
                formCfg[i] = new FormCfgClass();
            for (int i = 0; i < MAXNUM.TAB; ++i)
                tabCfg[i] = new TabCfgClass();

            for (int i = 0; i < (int)MAXNUM.GRIDCOLOR; i++)            
                gridColor[i] = new Color();
        }
    }

    public class TabCfgClass
    {
        public int formnumber = MAXNUM.FORM;
        public string name;
        public Rect grid = new Rect(); 
        public FormCfgClass[] formCfg = new FormCfgClass[MAXNUM.FORM];
        public TabCfgClass()
        {
            for (int i = 0; i < MAXNUM.FORM; ++i)
                formCfg[i] = new FormCfgClass();
        }
    }

    public class FormCfgClass
    {
        //-- Layouts and data source
        public string name;         // used on HMI
        public string section;
        public Rect location = new Rect();
        public FormCategory who = new FormCategory();
        public UnitCfgClass unit = new UnitCfgClass();
        public int[] timer = new int[MAXNUM.TIMERS];
        public float extmin = 100;
        public float rate4ext = 0.98f;
        public float rate2ext = 1.3f;
        // Scale
        public Scale xScale = new Scale();
        public Scale yScale = new Scale();
        public int[] widthscale = new int[MAXNUM.WIDTHSCALE];
        public bool inverted = false;       
        // Data
        public int mode = 0;    // 0: demo  1: live 2: replay  
        public DataCategory data = new DataCategory();
        public int[] dataIndex = new int[MAXNUM.FASTLINES];
        public DataSource[] sourceData = new DataSource[MAXNUM.DATASOURCE];
        public int zonenumber;          // for profiles
        public int staticsmode = 0;     // 0: calculated on air     1: taken from the file
        //-- Properities
        public Color formBackColor;
        public Color chartBackColor;
        public Color labBackColor;
        public MyFont labFont = new MyFont();
        public Color txtBackColorRead;
        public Color txtBackColorWrite;
        public Color statColorOK;
        public Color statColorFail;
        public MyFont txtFont = new MyFont();
        public int[] fastlineWidth = new int[MAXNUM.FASTLINES];
        public Color[] fastlineColor = new Color[MAXNUM.FASTLINES];
        public Color[] gridColor = new Color[MAXNUM.GRIDCOLOR];
        public string[] checkBoxText = new string[MAXNUM.CHECKBOX];

        public FormCfgClass()
        {
            for (int i = 0; i < (int)MAXNUM.DATASOURCE; i++)
            {
                sourceData[i] = new DataSource();
            }
            for (int i = 0; i < (int)MAXNUM.FASTLINES; i++)
            {
                fastlineColor[i] = new Color();
            }
            for (int i = 0; i < (int)MAXNUM.GRIDCOLOR; i++)
            {
                gridColor[i] = new Color();
            }
        }
    }

    //-- Configuration data class
    public class ConfigDataClass
    {
        public static AppCfgClass app = new AppCfgClass();
        public static RemoteCfgClass remoting = new RemoteCfgClass();
        public static ViewCfgClass view = new ViewCfgClass();
        public static ArchiveCfgClass archive = new ArchiveCfgClass();
    }
    //-- Language
    public class Translation
    {
        //
        public enum language
        {
            ENGLISH,
            GERMANY,
            CHINESE
        }
        //-----------------------------------------------------------------------------------------
        //-- CLASS VARIABLES ----------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------
        public string filePath;
        public DataTable LanguageTable = new DataTable();
        //-----------------------------------------------------------------------------------------
        public Translation(string TEXTPath)
        {
            filePath = TEXTPath;
        }

        public void LoadLanguage()
        {
            filePath = FileClass.rootDir + FileClass.filePath +FileClass.langFileName;
            LanguageTable.Clear();

            // load the text from "HMIText.mdb"
            try
            {
                OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath); //Jet OLEDB:Database Password=

                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    //MessageBox.Show("MS ACCESS is opened ok!");

                    OleDbCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "select * from HMITEXT";

                    OleDbDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            LanguageTable.Columns.Add(dr.GetName(i));
                        }
                        LanguageTable.Rows.Clear();
                    }
                    while (dr.Read())
                    {
                        DataRow row = LanguageTable.NewRow();
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            row[i] = dr[i];
                        }
                        LanguageTable.Rows.Add(row);
                    }
                    cmd.Dispose();
                    conn.Close();
                }
            }
            catch
            {
                ;//MessageBox.Show("MS ACCESS is not found!");
            }
        }

        public string GetTextItem(string text, int language)
        {
            string strText = "@@" + text;
            for (int i = 0; i < LanguageTable.Rows.Count; i++)
            {
                if (text.Equals(LanguageTable.Rows[i][0]))
                {
                    strText = LanguageTable.Rows[i][language + 3].ToString();
                    break;
                }
            }
            return strText;
        }
    }
    //=============================================================================================
}
