//=================================================================================================================
//  Project:    RM312/SIPRO SUPERvisor
//  Module:     MeasureFormTrendLen.cs                                                                         
//  Author:     Andrew Powell
//  Date:       21/03/2006
//  
//  Details:    Display form for visualising measurement data
//  
//=================================================================================================================
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using GAUGdata;
using GAUGlib;

namespace GAUGview
{
    public partial class MeasureFormTrendLen : Form
    {
        //---------------------------------------------------------------------------------------------------------
        // CLASS VARIABLES
        //---------------------------------------------------------------------------------------------------------
        private enum SOURCE : Int16
        {
            LEN,
            MEAS,
            STAT,
            PS,
            COILNAME,
            SETPOINT,
            PTOL,
            MTOL,
            FACTOR,
            OFFSET,
            PASS,
            LAST,
            NUMSOURCE
        }
        private enum LAST : Int16
        {
            LEN,
            PS,
            NUMLAST
        }
        private const int CLOFFSET = 300;
        private const int RES = 5;
        private const int NUMDATA = 8;
        //private MeasDataClass[] avgData = new MeasDataClass[NUMDATA];
        private float[] lastData = new float[NUMDATA];
        private enum summary
        {
            MIN,
            MAX,
            AVG,
            ACT,
            NUMSUM
        }
        private MeasDataClass[] avgData = new MeasDataClass[NUMDATA];
        private float[] sumData = new float[(int)summary.NUMSUM];
        private float[] profileData = new float[NUMDATA];
        private int sumCounter = 0;
        private bool lastMeasStatus = false;

        private ViewMainForm myOwner = null;
        private TabCfgClass myTabCfg = null;
        private FormCfgClass myFormCfg = null;
        private int widthCfg;
        private int heightCfg;
        private int tabIndex = -1;
        private int formIndex = -1;

        private int myCount = 0;

        private static string[] DemoFileName = { "Strip.csv", "Coilreport.csv" };

        private ArchiveFile coilReport = new ArchiveFile();

        private DataTable aDataTable = new DataTable();
        //---------------------------------------------------------------------------------------------------------
        // GLOBAL PROCEDURES
        //---------------------------------------------------------------------------------------------------------
        public MeasureFormTrendLen(Form OwnerForm, int width, int height, int tindex, int findex)
        {
            tabIndex = tindex;
            formIndex = findex;
            myOwner = OwnerForm as ViewMainForm;
            myTabCfg = ConfigDataClass.view.tabCfg[tabIndex];
            myFormCfg = myTabCfg.formCfg[formIndex];
            widthCfg = width;
            heightCfg = height;
            coilReport.productData.gaugename = FileClass.textItem.GetTextItem(myFormCfg.name, 0);   // always English in the file name

            InitializeComponent();
        }
        //---------------------------------------------------------------------------------------------------------
        // LOCAL PROCEDURES
        //---------------------------------------------------------------------------------------------------------    
        //-- Update Language ----------------------------------------------------------
        private void UpdateLanguage()
        {
            // Unit
            int unitType;
            switch (myFormCfg.who.type)
            {
                case (int)FormSort.TrendLen.THICK:
                case (int)FormSort.TrendLen.CROWN:
                case (int)FormSort.TrendLen.WEDGE:
                case (int)FormSort.TrendLen.CROWNWEDGE:
                    unitType = 1;
                    break;
                case (int)FormSort.TrendLen.EDGE:
                case (int)FormSort.TrendLen.OFFSET:
                case (int)FormSort.TrendLen.WIDTH:
                    unitType = 2;
                    break;
                case (int)FormSort.TrendLen.TEMP:
                    unitType = 5;
                    break;
                default:
                    unitType = 0;
                    break;
            }
            
            labelUnit1.Text = UnitCfgClass.name[unitType][myFormCfg.unit.style[unitType]];
            labelUnit2.Text = UnitCfgClass.name[unitType][myFormCfg.unit.style[unitType]];
            labelUnit3.Text = UnitCfgClass.name[unitType][myFormCfg.unit.style[unitType]];
            //labelUnit4.Text = UnitCfgClass.name[unitType][myFormCfg.unit.style[unitType]];

            // Title
            labelText7.Text = FileClass.textItem.GetTextItem(myFormCfg.name, ConfigDataClass.view.language);

            // Label
            labelText3.Text = FileClass.textItem.GetTextItem(labelText3.Tag.ToString(), ConfigDataClass.view.language);
            labelText4.Text = FileClass.textItem.GetTextItem(labelText4.Tag.ToString(), ConfigDataClass.view.language);
            labelText5.Text = FileClass.textItem.GetTextItem(labelText5.Tag.ToString(), ConfigDataClass.view.language);
            //labelText6.Text = FileClass.textItem.GetTextItem(labelText6.Tag.ToString(), ConfigDataClass.view.language);

            // checkbox
            if (myFormCfg.checkBoxText[0] != "")
                checkBox1.Text = myFormCfg.checkBoxText[0];
            if (myFormCfg.checkBoxText[1] != "")
                checkBox2.Text = myFormCfg.checkBoxText[1];
            if (myFormCfg.checkBoxText[2] != "")
                checkBox3.Text = myFormCfg.checkBoxText[2];

            // TeeChart
            tChartProfile.Axes.Left.Title.Caption = FileClass.textItem.GetTextItem("TX_THICKNESS", ConfigDataClass.view.language);
            tChartProfile.Axes.Bottom.Title.Caption = FileClass.textItem.GetTextItem("TX_LENGTH", ConfigDataClass.view.language);
        }

        private void UpdateColorAndFont()
        {
            //-- default color and font for lab/unit/text
            setControls(this);

            // Speical handling
            tChartProfile.BackColor = myFormCfg.chartBackColor;

            textBoxMin.BackColor = myFormCfg.txtBackColorRead;
            textBoxMax.BackColor = myFormCfg.txtBackColorRead;
            textBoxAvg.BackColor = myFormCfg.txtBackColorRead;
            //textBoxAct.BackColor = myFormCfg.txtBackColorRead;

            fastLine1.LinePen.Color = myFormCfg.fastlineColor[0];
            fastLine1.LinePen.Width = myFormCfg.fastlineWidth[0];
            checkBox1.BackColor = myFormCfg.fastlineColor[0];

            fastLine2.LinePen.Color = myFormCfg.fastlineColor[1];
            fastLine2.LinePen.Width = myFormCfg.fastlineWidth[1];
            checkBox2.BackColor = myFormCfg.fastlineColor[1];

            fastLine3.LinePen.Color = myFormCfg.fastlineColor[2];
            fastLine3.LinePen.Width = myFormCfg.fastlineWidth[2];
            checkBox3.BackColor = myFormCfg.fastlineColor[2];
        }

        private void setControls(Control con)
        {
            if (con.BackColor != Color.Transparent) con.BackColor = myFormCfg.formBackColor;
            con.Font = new Font(myFormCfg.labFont.name, myFormCfg.labFont.size, myFormCfg.labFont.style);
            con.ForeColor = myFormCfg.labFont.color;

            foreach (Control cons in con.Controls)
                setControls(cons);
        }

        //-- Update the size and location
        private void UpdateSizeAndLocation()
        {
            this.Left = (int)(widthCfg * (myFormCfg.location.left / myTabCfg.grid.width));
            this.Top = (int)(heightCfg * (myFormCfg.location.top / myTabCfg.grid.height));
            this.Width = (int)(widthCfg * (myFormCfg.location.width / myTabCfg.grid.width));
            this.Height = (int)(heightCfg * (myFormCfg.location.height / myTabCfg.grid.height));

            // Title
            labelText7.Left = this.Width - groupBox1.Width - DISPLAYPARAMS.offsetleft;
            labelText7.Top = DISPLAYPARAMS.offsettop;

            // Tee Chart
            tChartProfile.Top = 0;
            tChartProfile.Left = 0;
            tChartProfile.Width = this.Width - groupBox1.Width - DISPLAYPARAMS.offsetleft * 2;
            tChartProfile.Height = this.Height;
            tChartProfile.Axes.Bottom.AutomaticMaximum = false;
            tChartProfile.Axes.Bottom.Maximum = 100;

            switch (myFormCfg.who.type)
            {
                case (int)FormSort.TrendLen.THICK:
                case (int)FormSort.TrendLen.OFFSET:
                case (int)FormSort.TrendLen.TEMP:
                case (int)FormSort.TrendLen.WIDTH:
                case (int)FormSort.TrendLen.ASYM:
                case (int)FormSort.TrendLen.SYM:
                    groupBox2.Visible = false;

                    // Summary
                    //groupBox1.Left = this.Width - groupBox1.Width - DISPLAYPARAMS.offsetleft;
                    groupBox1.Left = tChartProfile.Width + DISPLAYPARAMS.offsetright;
                    groupBox1.Top = labelText7.Height + DISPLAYPARAMS.offsettop * 2;
                    break;
                case (int)FormSort.TrendLen.CROWN:
                case (int)FormSort.TrendLen.WEDGE:
                case (int)FormSort.TrendLen.CROWNWEDGE:
                case (int)FormSort.TrendLen.SYMASYM:
                case (int)FormSort.TrendLen.EDGE:
                    groupBox1.Visible = false;

                    groupBox2.Left = this.Width - groupBox1.Width - DISPLAYPARAMS.offsetleft;
                    groupBox2.Top = labelText7.Height + DISPLAYPARAMS.offsettop * 2;

                    if (myFormCfg.checkBoxText[0] != "") checkBox2.Checked = true;
                    else checkBox2.Checked = false;
                    if (myFormCfg.checkBoxText[1] != "") checkBox1.Checked = true;
                    else checkBox1.Checked = false;
                    break;
                case (int)FormSort.TrendLen.PROFILE:
                    groupBox1.Left = tChartProfile.Width + DISPLAYPARAMS.offsetright;
                    groupBox1.Top = labelText7.Height + DISPLAYPARAMS.offsettop * 2;

                    groupBox2.Left = tChartProfile.Width + DISPLAYPARAMS.offsetright;
                    groupBox2.Top = groupBox1.Top + groupBox1.Height + DISPLAYPARAMS.offsettop;
                    break;
            }
        }

        private void Reset()
        {
            sumData[(int)summary.AVG] = 0;
            sumData[(int)summary.MAX] = -9999.0f;
            sumData[(int)summary.MIN] = 9999.0f;
            sumCounter = 0;
            lastData[(int)LAST.LEN] = 0.0f;
            lastData[(int)LAST.PS] = 0.0f;

            fastLine1.Clear();
            fastLine2.Clear();
            fastLine3.Clear();

            tChartProfile.Axes.Bottom.Maximum = 100;
        }
        //-- Display the trend via the data in XMD...eg. a csv file
        private void DisplayTrend()
        {
            try
            {
                CoilDataClass coilData = RemoteInterfaceClass.XMD.GetCoilData(myFormCfg.who.index);
                int NumOfData = coilData.product.dataNumber;
                if (NumOfData > MAXNUM.STRIP) NumOfData = MAXNUM.STRIP;

                Reset();

                switch (myFormCfg.who.type)
                {
                    case (int)FormSort.TrendLen.THICK:
                        fastLine1.Clear();

                        for (int i = 0; i < NumOfData; ++i)
                        {
                            sumData[(int)summary.AVG] += coilData.data[i].measThick; sumCounter++;
                            if (sumData[(int)summary.MAX] < coilData.data[i].measThick) sumData[(int)summary.MAX] = coilData.data[i].measThick;
                            if (sumData[(int)summary.MIN] > coilData.data[i].measThick) sumData[(int)summary.MIN] = coilData.data[i].measThick;

                            fastLine1.Add(coilData.data[i].length, coilData.data[i].measThick);
                        }
                        if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;

                        textBoxMin.Text = sumData[(int)summary.MIN].ToString("F4");
                        textBoxMax.Text = sumData[(int)summary.MAX].ToString("F4");
                        textBoxAvg.Text = sumData[(int)summary.AVG].ToString("F4");

                        tChartProfile.Axes.Bottom.Maximum = coilData.data[NumOfData-1].length;
                        break;
                        
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.Publish(exc);
            }
        }
        //-- read a strip demo file and display a trend via a table, eg. sipro
        private void DemoStripFile(int fileIndex)
        {
            try
            {
                string filePath = FileClass.rootDir + FileClass.diagPath + DemoFileName[fileIndex];
                FileInfo aFile = new FileInfo(filePath);
                if (aFile.Exists)
                {
                    StreamReader sReader = new StreamReader(aFile.OpenRead());
                    CsvParserClass aCsvParser = new CsvParserClass();
                    aDataTable = CsvParserClass.Parse(sReader, true);

                    int NumOfData = aDataTable.Rows.Count;
                    if (NumOfData > MAXNUM.STRIP) NumOfData = MAXNUM.STRIP;

                    // Display
                    Reset();

                    switch (myFormCfg.who.type)
                    {
                        case (int)FormSort.TrendLen.THICK:
                            fastLine2.Clear();

                            for (int i = 0; i < NumOfData; ++i)
                            {
                                float data = Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.MeasThick]);
                                sumData[(int)summary.AVG] += data; sumCounter++;
                                if (sumData[(int)summary.MAX] < data) sumData[(int)summary.MAX] = data;
                                if (sumData[(int)summary.MIN] > data) sumData[(int)summary.MIN] = data;

                                fastLine2.Add(Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.Length]), data);
                            }
                            if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;

                            textBoxMin.Text = sumData[(int)summary.MIN].ToString("F4");
                            textBoxMax.Text = sumData[(int)summary.MAX].ToString("F4");
                            textBoxAvg.Text = sumData[(int)summary.AVG].ToString("F4");
                            textBoxAct.Text = sumData[(int)summary.AVG].ToString("F4");

                            break;
                        case (int)FormSort.TrendLen.OFFSET:
                            fastLine2.Clear();

                            for (int i = 0; i < NumOfData; ++i)
                            {
                                float data = Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.CLOffset]);
                                sumData[(int)summary.AVG] += data; sumCounter++;
                                if (sumData[(int)summary.MAX] < data) sumData[(int)summary.MAX] = data;
                                if (sumData[(int)summary.MIN] > data) sumData[(int)summary.MIN] = data;

                                fastLine2.Add(Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.Length]), data);
                            }
                            if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;

                            textBoxMin.Text = sumData[(int)summary.MIN].ToString("F1");
                            textBoxMax.Text = sumData[(int)summary.MAX].ToString("F1");
                            textBoxAvg.Text = sumData[(int)summary.AVG].ToString("F1");
                            textBoxAct.Text = sumData[(int)summary.AVG].ToString("F1");

                            break;
                        case (int)FormSort.TrendLen.TEMP:
                            fastLine2.Clear();

                            for (int i = 0; i < NumOfData; ++i)
                            {
                                float data = Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.Temp]);
                                sumData[(int)summary.AVG] += data; sumCounter++;
                                if (sumData[(int)summary.MAX] < data) sumData[(int)summary.MAX] = data;
                                if (sumData[(int)summary.MIN] > data) sumData[(int)summary.MIN] = data;

                                fastLine2.Add(Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.Length]), data);
                            }
                            if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;

                            textBoxMin.Text = sumData[(int)summary.MIN].ToString("F0");
                            textBoxMax.Text = sumData[(int)summary.MAX].ToString("F0");
                            textBoxAvg.Text = sumData[(int)summary.AVG].ToString("F0");
                            textBoxAct.Text = sumData[(int)summary.AVG].ToString("F0");

                            break;
                        case (int)FormSort.TrendLen.WIDTH:
                            fastLine2.Clear();

                            for (int i = 0; i < NumOfData; ++i)
                            {
                                float data = Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.MeasWidth]);
                                sumData[(int)summary.AVG] += data; sumCounter++;
                                if (sumData[(int)summary.MAX] < data) sumData[(int)summary.MAX] = data;
                                if (sumData[(int)summary.MIN] > data) sumData[(int)summary.MIN] = data;

                                fastLine2.Add(Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.Length]), data);
                            }
                            if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;

                            textBoxMin.Text = sumData[(int)summary.MIN].ToString("F1");
                            textBoxMax.Text = sumData[(int)summary.MAX].ToString("F1");
                            textBoxAvg.Text = sumData[(int)summary.AVG].ToString("F1");
                            textBoxAct.Text = sumData[(int)summary.AVG].ToString("F1");

                            break;
                        case (int)FormSort.TrendLen.SYM:
                            fastLine2.Clear();

                            for (int i = 0; i < NumOfData; ++i)
                            {
                                float data = Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.SH3]) - (Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.SH6]) + Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.SH0])) / 2;
                                sumData[(int)summary.AVG] += data; sumCounter++;
                                if (sumData[(int)summary.MAX] < data) sumData[(int)summary.MAX] = data;
                                if (sumData[(int)summary.MIN] > data) sumData[(int)summary.MIN] = data;

                                fastLine2.Add(Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.Length]), data);
                            }
                            if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;

                            textBoxMin.Text = sumData[(int)summary.MIN].ToString("F2");
                            textBoxMax.Text = sumData[(int)summary.MAX].ToString("F2");
                            textBoxAvg.Text = sumData[(int)summary.AVG].ToString("F2");
                            textBoxAct.Text = sumData[(int)summary.AVG].ToString("F2");

                            break;
                        case (int)FormSort.TrendLen.ASYM:
                            fastLine2.Clear();

                            for (int i = 0; i < NumOfData; ++i)
                            {
                                float data = Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.SH6]) - Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.SH0]);
                                sumData[(int)summary.AVG] += data; sumCounter++;
                                if (sumData[(int)summary.MAX] < data) sumData[(int)summary.MAX] = data;
                                if (sumData[(int)summary.MIN] > data) sumData[(int)summary.MIN] = data;

                                fastLine2.Add(Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.Length]), data);
                            }
                            if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;

                            textBoxMin.Text = sumData[(int)summary.MIN].ToString("F2");
                            textBoxMax.Text = sumData[(int)summary.MAX].ToString("F2");
                            textBoxAvg.Text = sumData[(int)summary.AVG].ToString("F2");
                            textBoxAct.Text = sumData[(int)summary.AVG].ToString("F2");

                            break;
                        case (int)FormSort.TrendLen.SYMASYM:
                            fastLine1.Clear();
                            fastLine2.Clear();

                            for (int i = 0; i < NumOfData; ++i)
                            {
                                float sym = Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.SH3]) - (Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.SH6]) + Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.SH0])) / 2;
                                float asym = Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.SH6]) - Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.SH0]);

                                fastLine1.Add(Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.Length]), sym);
                                fastLine2.Add(Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.Length]), asym);
                            }
                            break;
                        case (int)FormSort.TrendLen.CROWN:
                            fastLine1.Clear();
                            fastLine2.Clear();

                            for (int i = 0; i < NumOfData; ++i)
                            {
                                float c1 = Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.MeasThick]) - (Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.BE1]) + Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.OE1])) / 2;
                                float c2 = Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.MeasThick]) - (Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.BE4]) + Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.OE4])) / 2;

                                fastLine1.Add(Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.Length]), c1);
                                fastLine2.Add(Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.Length]), c2);
                            }
                            break;
                        case (int)FormSort.TrendLen.WEDGE:
                            fastLine1.Clear();
                            fastLine2.Clear();

                            for (int i = 0; i < NumOfData; ++i)
                            {
                                float w1 = Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.BE1]) - Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.OE1]);
                                float w2 = Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.BE4]) - Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.OE4]);

                                fastLine1.Add(Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.Length]), w1);
                                fastLine2.Add(Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.Length]), w2);
                            }
                            break;
                        case (int)FormSort.TrendLen.CROWNWEDGE:
                            fastLine1.Clear();
                            fastLine2.Clear();

                            for (int i = 0; i < NumOfData; ++i)
                            {
                                float c1 = Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.MeasThick]) - (Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.BE1]) + Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.OE1])) / 2;
                                float w1 = Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.BE1]) - Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.OE1]);

                                fastLine1.Add(Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.Length]), c1);
                                fastLine2.Add(Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.Length]), w1);
                            }
                            break;
                        case (int)FormSort.TrendLen.EDGE:
                            fastLine1.Clear();
                            fastLine2.Clear();

                            for (int i = 0; i < NumOfData; ++i)
                            {
                                float b1 = Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.BE1]);
                                float o1 = Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.OE1]);

                                fastLine1.Add(Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.Length]), b1);
                                fastLine2.Add(Convert.ToSingle(aDataTable.Rows[i][(int)StripTable.col.Length]), o1);
                            }
                            break;
                    }
                }


            }
            catch (Exception exc)
            {
                ExceptionManager.Publish(exc);
            }
        }
        //-- read a coil demo file and display a trend via a table, eg. coil report 
        private void DemoCoilreport(int fileIndex)
        {
            try
            {
                string filePath = FileClass.rootDir + FileClass.diagPath + DemoFileName[fileIndex];
                FileInfo aFile = new FileInfo(filePath);
                if (aFile.Exists)
                {
                    StreamReader sReader = new StreamReader(aFile.OpenRead());
                    CsvParserClass aCsvParser = new CsvParserClass();
                    aDataTable = CsvParserClass.Parse(sReader, true);

                    bool hasSummary = false;
                    if (aDataTable.Rows[aDataTable.Rows.Count - 3][4].ToString().Trim() == "length") hasSummary = true;

                    int NumOfData;
                    if (hasSummary) NumOfData = aDataTable.Rows.Count - ProfileTable.row.HEADLINES - ProfileTable.row.SUMMARYLINES;
                    else NumOfData = aDataTable.Rows.Count - ProfileTable.row.HEADLINES;                 
                    if (NumOfData > MAXNUM.STRIP) NumOfData = MAXNUM.STRIP;
                    
                    // Display
                    Reset();

                    switch (myFormCfg.who.type)
                    {
                        case (int)FormSort.TrendLen.THICK:
                            fastLine1.Clear();

                            for (int i = (int)CoilTable.row.Data; i < NumOfData; ++i)
                            {
                                float data = Convert.ToSingle(aDataTable.Rows[i][(int)CoilTable.colData.MeasThick]);
                                sumData[(int)summary.AVG] += data; sumCounter++;
                                if (sumData[(int)summary.MAX] < data) sumData[(int)summary.MAX] = data;
                                if (sumData[(int)summary.MIN] > data) sumData[(int)summary.MIN] = data;

                                fastLine1.Add(Convert.ToSingle(aDataTable.Rows[i][(int)CoilTable.colData.Length]), data);
                            }
                            if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;

                            textBoxMin.Text = sumData[(int)summary.MIN].ToString("F4");
                            textBoxMax.Text = sumData[(int)summary.MAX].ToString("F4");
                            textBoxAvg.Text = sumData[(int)summary.AVG].ToString("F4");
                            //textBoxAct.Text = sumData[(int)summary.AVG].ToString("F4");

                            break;

                        case (int)FormSort.TrendLen.PROFILE:
                            fastLine1.Clear();
                            fastLine2.Clear();
                            fastLine3.Clear();

                            for (int i = (int)ProfileTable.row.Data; i < NumOfData; ++i)
                            {
                                if (myFormCfg.dataIndex[0] != -1)
                                {
                                    float data = Convert.ToSingle(aDataTable.Rows[i][myFormCfg.dataIndex[0]]);
                                    fastLine1.Add(Convert.ToSingle(aDataTable.Rows[i][(int)ProfileTable.colData.Length]), data);
                                }
                                if (myFormCfg.dataIndex[1] != -1)
                                {
                                    float data = Convert.ToSingle(aDataTable.Rows[i][myFormCfg.dataIndex[1]]);
                                    fastLine2.Add(Convert.ToSingle(aDataTable.Rows[i][(int)ProfileTable.colData.Length]), data);
                                }
                                if (myFormCfg.dataIndex[2] != -1)
                                {
                                    float data = Convert.ToSingle(aDataTable.Rows[i][myFormCfg.dataIndex[2]]);
                                    fastLine3.Add(Convert.ToSingle(aDataTable.Rows[i][(int)ProfileTable.colData.Length]), data);
                                }
                            }
                            if (hasSummary)
                            {
                                textBoxMin.Text = aDataTable.Rows[aDataTable.Rows.Count - 1][10].ToString();       // col 'K', Min
                                textBoxMax.Text = aDataTable.Rows[aDataTable.Rows.Count - 1][12].ToString();       // col 'M', Avg
                                textBoxAvg.Text = aDataTable.Rows[aDataTable.Rows.Count - 1][11].ToString();       // col 'L', Max
                            }
                            else
                            {
                                textBoxMin.Text = "-";
                                textBoxMax.Text = "-";
                                textBoxAvg.Text = "-";
                            }

                            break;
                    }
                }

      
            }
            catch (Exception exc)
            {
                ExceptionManager.Publish(exc);
            }
        }
        //-- get the product data via M1com
        private void GetProductInformation()
        {
            UInt16 index = 0;

            RemoteInterfaceClass.XMD.IsModifiedData(myFormCfg.sourceData[(int)SOURCE.COILNAME].type, ref index, myFormCfg.sourceData[(int)SOURCE.COILNAME].name, 0, 0, 0.0f);
            coilReport.productData.coilid = RemoteInterfaceClass.XMD.GetStringData(index);
            RemoteInterfaceClass.XMD.IsModifiedData(myFormCfg.sourceData[(int)SOURCE.SETPOINT].type, ref index, myFormCfg.sourceData[(int)SOURCE.SETPOINT].name, 0, 0, 0.0f);
            coilReport.productData.setpoint = RemoteInterfaceClass.XMD.GetFloatData(index);
            RemoteInterfaceClass.XMD.IsModifiedData(myFormCfg.sourceData[(int)SOURCE.PTOL].type, ref index, myFormCfg.sourceData[(int)SOURCE.PTOL].name, 0, 0, 0.0f);
            coilReport.productData.ptol = RemoteInterfaceClass.XMD.GetFloatData(index);
            RemoteInterfaceClass.XMD.IsModifiedData(myFormCfg.sourceData[(int)SOURCE.MTOL].type, ref index, myFormCfg.sourceData[(int)SOURCE.MTOL].name, 0, 0, 0.0f);
            coilReport.productData.mtol = RemoteInterfaceClass.XMD.GetFloatData(index);
            RemoteInterfaceClass.XMD.IsModifiedData(myFormCfg.sourceData[(int)SOURCE.FACTOR].type, ref index, myFormCfg.sourceData[(int)SOURCE.FACTOR].name, 0, 0, 0.0f);
            coilReport.productData.alloyfactor = RemoteInterfaceClass.XMD.GetFloatArrayData(index, 0);
            RemoteInterfaceClass.XMD.IsModifiedData(myFormCfg.sourceData[(int)SOURCE.OFFSET].type, ref index, myFormCfg.sourceData[(int)SOURCE.OFFSET].name, 0, 0, 0.0f);
            coilReport.productData.offset = RemoteInterfaceClass.XMD.GetFloatArrayData(index, 0);
            RemoteInterfaceClass.XMD.IsModifiedData(myFormCfg.sourceData[(int)SOURCE.PASS].type, ref index, myFormCfg.sourceData[(int)SOURCE.PASS].name, 0, 0, 0.0f);
            coilReport.productData.passnumber = RemoteInterfaceClass.XMD.GetIntegerData(index);
            RemoteInterfaceClass.XMD.IsModifiedData(myFormCfg.sourceData[(int)SOURCE.LAST].type, ref index, myFormCfg.sourceData[(int)SOURCE.LAST].name, 0, 0, 0.0f);
            coilReport.productData.lastpass = RemoteInterfaceClass.XMD.GetIntegerData(index);
        }

        //---------------------------------------------------------------------------------------------------------
        // LOCAL EVENTS
        //--------------------------------------------------------------------------------------------------------- 
        private void MeasureFormTrendLen_Load(object sender, EventArgs e)
        {
            UpdateLanguage();            
            UpdateColorAndFont();
            UpdateSizeAndLocation();

            timer1.Interval = myFormCfg.timer[0];
            timer2.Interval = myFormCfg.timer[1];
            //timer3.Interval = myFormCfg.timer[2];

            for (int i = 0; i < NUMDATA; ++i)
                avgData[i] = new MeasDataClass();

            switch(ConfigDataClass.view.mode)
            {
                case (int)DataSort.Source.REMOTING:
                case (int)DataSort.Source.CSVFILE:
                case (int)DataSort.Source.M1COM:
                    timer1.Start();
                    timer2.Start();
                    break;
                case (int)DataSort.Source.DEMOFILE:
                    {
                        switch(myFormCfg.data.type)
                        {
                            case (int)DataSort.Type.SIPRO:
                                DemoStripFile((int)DataSort.Type.SIPRO-1);
                                break;
                            case (int)DataSort.Type.RM310:
                                break;
                            case (int)DataSort.Type.RM210:
                                DemoCoilreport((int)DataSort.Type.RM210-1);
                                break;
                        }
                    }
                    break;
            }
        }

        //-- Automated form update --------------------------------------------------------------------------------
        private void timer1_Tick(object sender, EventArgs e)    // fast update, XMD or M1
        {
            try
            {
                if (RemoteInterfaceClass.connected)
                {
                    switch (ConfigDataClass.view.mode)
                    {
                        case (int)DataSort.Source.REMOTING:
                            {
                                SuperStatus svStat = RemoteInterfaceClass.XMD.GetSVStatus();
                                StripLength svLength = RemoteInterfaceClass.XMD.GetStripLength();

                                if (svStat.MEAS != lastMeasStatus)
                                {
                                    if (svStat.MEAS) // reset
                                        Reset();

                                    lastMeasStatus = svStat.MEAS;
                                }

                                switch (myFormCfg.who.type)
                                {
                                    case (int)FormSort.TrendLen.THICK:
                                    case (int)FormSort.TrendLen.OFFSET:
                                    case (int)FormSort.TrendLen.TEMP:
                                    case (int)FormSort.TrendLen.WIDTH:
                                    case (int)FormSort.TrendLen.ASYM:
                                    case (int)FormSort.TrendLen.SYM:
                                        avgData[0] = RemoteInterfaceClass.XMD.GetFiveRawData();
                                        //-- Occasional TeeChart exceptions only update if measuring
                                        if (svStat.MEAS)
                                        {
                                            // if (!Single.IsNaN(avgData[0].aCLThickness))
                                            {
                                                switch (myFormCfg.who.type)
                                                {
                                                    case (int)FormSort.TrendLen.THICK:
                                                        sumData[(int)summary.ACT] = avgData[0].aCLThickness;
                                                        break;
                                                    case (int)FormSort.TrendLen.OFFSET:
                                                        sumData[(int)summary.ACT] = avgData[0].aCLStripOffset;
                                                        break;
                                                    case (int)FormSort.TrendLen.TEMP:
                                                        sumData[(int)summary.ACT] = avgData[0].aCLTemp;
                                                        break;
                                                    case (int)FormSort.TrendLen.WIDTH:
                                                        sumData[(int)summary.ACT] = avgData[0].aWidth;
                                                        break;
                                                    case (int)FormSort.TrendLen.ASYM:
                                                        sumData[(int)summary.ACT] = avgData[0].aFlatness[6] - avgData[0].aFlatness[0];
                                                        break;
                                                    case (int)FormSort.TrendLen.SYM:
                                                        sumData[(int)summary.ACT] = avgData[0].aFlatness[3] - (avgData[0].aFlatness[6] + avgData[0].aFlatness[0]) / 2;
                                                        break;
                                                }
                                                fastLine2.Add(svLength.length[(int)svLength.lengthSource], sumData[(int)summary.ACT]);

                                                sumCounter++;
                                                if (sumCounter > 0) sumData[(int)summary.AVG] = (sumData[(int)summary.AVG] * (sumCounter - 1) + sumData[(int)summary.ACT]) / sumCounter;
                                                if (sumData[(int)summary.MAX] < sumData[(int)summary.ACT]) sumData[(int)summary.MAX] = sumData[(int)summary.ACT];
                                                if (sumData[(int)summary.MIN] > sumData[(int)summary.ACT]) sumData[(int)summary.MIN] = sumData[(int)summary.ACT];
                                            }

                                            if (svLength.length[(int)svLength.lengthSource] > tChartProfile.Axes.Bottom.Maximum * myFormCfg.rate4ext)
                                            {
                                                tChartProfile.Axes.Bottom.Maximum *= myFormCfg.rate2ext;
                                            }
                                        }
                                        break;
                                    case (int)FormSort.TrendLen.CROWN:
                                    case (int)FormSort.TrendLen.WEDGE:
                                    case (int)FormSort.TrendLen.EDGE:
                                    case (int)FormSort.TrendLen.SYMASYM:
                                    case (int)FormSort.TrendLen.CROWNWEDGE:
                                        avgData[0] = RemoteInterfaceClass.XMD.GetFiveRawData();
                                        if (svStat.MEAS)
                                        {
                                            switch (myFormCfg.who.type)
                                            {
                                                case (int)FormSort.TrendLen.EDGE:
                                                    if (myFormCfg.dataIndex[0] > 0)
                                                        sumData[(int)summary.ACT] = avgData[0].aBEThickness[myFormCfg.dataIndex[0] - 1];
                                                    else
                                                        sumData[(int)summary.ACT] = avgData[0].aOEThickness[-myFormCfg.dataIndex[0] - 1];
                                                    if (myFormCfg.dataIndex[1] > 0)
                                                        sumData[(int)summary.AVG] = avgData[0].aBEThickness[myFormCfg.dataIndex[1] - 1];
                                                    else
                                                        sumData[(int)summary.AVG] = avgData[0].aOEThickness[-myFormCfg.dataIndex[1] - 1];
                                                    break;
                                                case (int)FormSort.TrendLen.CROWN:
                                                    sumData[(int)summary.ACT] = avgData[0].aCrown[myFormCfg.dataIndex[0]];
                                                    sumData[(int)summary.AVG] = avgData[0].aCrown[myFormCfg.dataIndex[1]];  // used for the 2nd value
                                                    break;
                                                case (int)FormSort.TrendLen.WEDGE:
                                                    sumData[(int)summary.ACT] = avgData[0].aWedge[myFormCfg.dataIndex[0]];
                                                    sumData[(int)summary.AVG] = avgData[0].aWedge[myFormCfg.dataIndex[1]];  // used for the 2nd value
                                                    break;
                                                case (int)FormSort.TrendLen.SYMASYM:
                                                    sumData[(int)summary.ACT] = avgData[0].aFlatness[3] - (avgData[0].aFlatness[6] + avgData[0].aFlatness[0]) / 2;
                                                    sumData[(int)summary.AVG] = avgData[0].aFlatness[6] - avgData[0].aFlatness[0];  // used for the 2nd value
                                                    break;
                                                case (int)FormSort.TrendLen.CROWNWEDGE:
                                                    sumData[(int)summary.ACT] = avgData[0].aCrown[myFormCfg.dataIndex[0]];
                                                    sumData[(int)summary.AVG] = avgData[0].aWedge[myFormCfg.dataIndex[1]];
                                                    break;
                                            }

                                            if (checkBox1.Checked)
                                                fastLine1.Add(svLength.length[(int)svLength.lengthSource], sumData[(int)summary.ACT]);
                                            if (checkBox2.Checked)
                                                fastLine2.Add(svLength.length[(int)svLength.lengthSource], sumData[(int)summary.AVG]);

                                            if (svLength.length[(int)svLength.lengthSource] > tChartProfile.Axes.Bottom.Maximum * myFormCfg.rate4ext)
                                            {
                                                tChartProfile.Axes.Bottom.Maximum *= myFormCfg.rate2ext;
                                            }
                                        }
                                        break;
                                }
                            }
                            break;
                        case (int)DataSort.Source.M1COM: 
                            {
                                UInt16 index = 0;
                                RemoteInterfaceClass.XMD.IsModifiedData(myFormCfg.sourceData[(int)SOURCE.PS].type, ref index, myFormCfg.sourceData[(int)SOURCE.PS].name, 0, 0, 0.0f);
                                bool reset = RemoteInterfaceClass.XMD.GetBoolData(myFormCfg.sourceData[(int)SOURCE.PS].type, index, 0);
                                if (reset) lastData[(int)LAST.PS] = 1.0f;
                                else if (lastData[(int)LAST.PS] > 0)    // reset at it's gone
                                {
                                    if ((myFormCfg.data.output == (int)DataSort.Output.CSVFILE) && coilReport.FileExists()) coilReport.SaveCoilSummary(sumData);
                                    Reset();
                                    if (myFormCfg.data.output == (int)DataSort.Output.CSVFILE)
                                    {
                                        GetProductInformation();
                                        coilReport.CreateCoilReport();
                                    }
                                }                               

                                UInt16 yindex = 0, xindex = 0;
                                if (RemoteInterfaceClass.XMD.IsModifiedData(myFormCfg.sourceData[(int)SOURCE.LEN].type, ref xindex, myFormCfg.sourceData[(int)SOURCE.LEN].name, 0, 1, lastData[(int)LAST.LEN] + 1.0f)) // 1m
                                {
                                    RemoteInterfaceClass.XMD.IsModifiedData(myFormCfg.sourceData[(int)SOURCE.MEAS].type, ref yindex, myFormCfg.sourceData[(int)SOURCE.MEAS].name, 0, 0, 0.0f);
                                    RemoteInterfaceClass.XMD.IsModifiedData(myFormCfg.sourceData[(int)SOURCE.STAT].type, ref index, myFormCfg.sourceData[(int)SOURCE.STAT].name, 0, 0, 0.0f);
                                    float data = RemoteInterfaceClass.XMD.GetFloatData(yindex);
                                    float position = RemoteInterfaceClass.XMD.GetFloatData(xindex);
                                    int status = RemoteInterfaceClass.XMD.GetIntegerData(index);
                                    sumData[(int)summary.AVG] += data; sumCounter++;
                                    if (sumData[(int)summary.MAX] < data) sumData[(int)summary.MAX] = data;
                                    if (sumData[(int)summary.MIN] > data) sumData[(int)summary.MIN] = data;

                                    //-- Display
                                    fastLine1.Add(position, data);
                                    if ((tChartProfile.Axes.Bottom.Maximum - position) < 10.0f) tChartProfile.Axes.Bottom.Maximum += 100.0f;   // 100m more

                                    //-- restore the last                                    
                                    lastData[(int)LAST.LEN] = position;

                                    if (myFormCfg.data.output == (int)DataSort.Output.CSVFILE)
                                    {
                                        if (!coilReport.FileExists())
                                        {
                                            GetProductInformation();
                                            coilReport.CreateCoilReport();
                                        }
                                        coilReport.SaveCoilReport(position, data, status);
                                    }
                                }
                            }
                            break;
                    }
                }
            }                      
            catch (Exception exc)
            {
                ExceptionManager.Publish(exc);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)    // slow update
        {
            // data updates
            try
            {
                if (RemoteInterfaceClass.connected)
                {
                    switch (ConfigDataClass.view.mode)
                    {
                        case (int)DataSort.Source.REMOTING:
                            {
                                switch (myFormCfg.who.type)
                                {
                                    case (int)FormSort.TrendLen.THICK:
                                        avgData[0] = (MeasDataClass)XMDdata.measDisplayAvg.Clone();
                                        if (XMDdata.statSV.MEAS)
                                        {
                                            textBoxMin.Text = (sumData[(int)summary.MIN] * myFormCfg.unit.factor[(int)UnitCfgClass.type.THICK] + myFormCfg.unit.offset[(int)UnitCfgClass.type.THICK]).ToString("F4");
                                            textBoxMax.Text = (sumData[(int)summary.MAX] * myFormCfg.unit.factor[(int)UnitCfgClass.type.THICK] + myFormCfg.unit.offset[(int)UnitCfgClass.type.THICK]).ToString("F4");
                                            textBoxAvg.Text = (sumData[(int)summary.AVG] * myFormCfg.unit.factor[(int)UnitCfgClass.type.THICK] + myFormCfg.unit.offset[(int)UnitCfgClass.type.THICK]).ToString("F4");
                                            textBoxAct.Text = (sumData[(int)summary.ACT] * myFormCfg.unit.factor[(int)UnitCfgClass.type.THICK] + myFormCfg.unit.offset[(int)UnitCfgClass.type.THICK]).ToString("F4");
                                        }
                                        else
                                        {
                                            textBoxMin.Text = "000.0";
                                            textBoxMax.Text = "000.0";
                                            textBoxAvg.Text = "000.0";
                                            textBoxAct.Text = "000.0";
                                        }
                                        break;
                                    case (int)FormSort.TrendLen.TEMP:
                                        avgData[0] = (MeasDataClass)XMDdata.measDisplayAvg.Clone();
                                        if (XMDdata.statSV.MEAS)
                                        {
                                            textBoxMin.Text = (sumData[(int)summary.MIN] * myFormCfg.unit.factor[(int)UnitCfgClass.type.TEMPERATURE] + myFormCfg.unit.offset[(int)UnitCfgClass.type.TEMPERATURE]).ToString("F1");
                                            textBoxMax.Text = (sumData[(int)summary.MAX] * myFormCfg.unit.factor[(int)UnitCfgClass.type.TEMPERATURE] + myFormCfg.unit.offset[(int)UnitCfgClass.type.TEMPERATURE]).ToString("F1");
                                            textBoxAvg.Text = (sumData[(int)summary.AVG] * myFormCfg.unit.factor[(int)UnitCfgClass.type.TEMPERATURE] + myFormCfg.unit.offset[(int)UnitCfgClass.type.TEMPERATURE]).ToString("F1");
                                            textBoxAct.Text = (sumData[(int)summary.ACT] * myFormCfg.unit.factor[(int)UnitCfgClass.type.TEMPERATURE] + myFormCfg.unit.offset[(int)UnitCfgClass.type.TEMPERATURE]).ToString("F1");
                                        }
                                        else
                                        {
                                            textBoxMin.Text = "000.0";
                                            textBoxMax.Text = "000.0";
                                            textBoxAvg.Text = "000.0";
                                            textBoxAct.Text = "000.0";
                                        }
                                        break;
                                    case (int)FormSort.TrendLen.WIDTH:
                                    case (int)FormSort.TrendLen.OFFSET:
                                        avgData[0] = (MeasDataClass)XMDdata.measDisplayAvg.Clone();
                                        if (XMDdata.statSV.MEAS)
                                        {
                                            textBoxMin.Text = (sumData[(int)summary.MIN] * myFormCfg.unit.factor[(int)UnitCfgClass.type.WIDTH] + myFormCfg.unit.offset[(int)UnitCfgClass.type.WIDTH]).ToString("F1");
                                            textBoxMax.Text = (sumData[(int)summary.MAX] * myFormCfg.unit.factor[(int)UnitCfgClass.type.WIDTH] + myFormCfg.unit.offset[(int)UnitCfgClass.type.WIDTH]).ToString("F1");
                                            textBoxAvg.Text = (sumData[(int)summary.AVG] * myFormCfg.unit.factor[(int)UnitCfgClass.type.WIDTH] + myFormCfg.unit.offset[(int)UnitCfgClass.type.WIDTH]).ToString("F1");
                                            textBoxAct.Text = (sumData[(int)summary.ACT] * myFormCfg.unit.factor[(int)UnitCfgClass.type.WIDTH] + myFormCfg.unit.offset[(int)UnitCfgClass.type.WIDTH]).ToString("F1");
                                        }
                                        else
                                        {
                                            textBoxMin.Text = "000.0";
                                            textBoxMax.Text = "000.0";
                                            textBoxAvg.Text = "000.0";
                                            textBoxAct.Text = "000.0";
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;

                        case (int)DataSort.Source.CSVFILE:  // one time for all
                            {
                                int curCount = RemoteInterfaceClass.XMD.GetUpdateCoilCount(myFormCfg.who.index);
                                if (myCount != curCount)
                                {
                                    DisplayTrend();
                                    myCount = curCount;
                                }
                            }
                            break;

                        case (int)DataSort.Source.M1COM:
                            {
                                if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;

                                textBoxMin.Text = sumData[(int)summary.MIN].ToString("F4");
                                textBoxMax.Text = sumData[(int)summary.MAX].ToString("F4");
                                textBoxAvg.Text = sumData[(int)summary.AVG].ToString("F4");
                            }
                            break;
                    }
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.Publish(exc);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) fastLine1.Visible = true;
            else fastLine1.Visible = false;
        }

        private void tChartProfile_DoubleClick(object sender, EventArgs e)
        {
            ChartForm aChartForm = new ChartForm(tChartProfile, myFormCfg);
            aChartForm.Location = Control.MousePosition;
            aChartForm.Visible = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked) fastLine2.Visible = true;
            else fastLine2.Visible = false;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked) fastLine3.Visible = true;
            else fastLine3.Visible = false;
        }
        //=========================================================================================================
    }
}