//=================================================================================================================
//  Project:    RM312/SIPRO SUPERvisor
//  Module:     MeasureFormProfile.cs                                                                         
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
    public partial class MeasureFormProfile : Form
    {
        //---------------------------------------------------------------------------------------------------------
        // CLASS VARIABLES
        //---------------------------------------------------------------------------------------------------------
        private const int CLOFFSET = 300;
        private const int RES = 5;
        private const int NUMDATA = 8;
        private MeasDataClass[] avgData = new MeasDataClass[NUMDATA];
        private enum summary
        {
            MIN,
            MAX,
            AVG,
            ACT,
            NUMSUM
        }
        private float[] sumData = new float[(int)summary.NUMSUM];
        private int sumCounter = 0;
        private int sumProfile = 0;
        private bool lastMeasStatus = false;
        private int index;

        private ViewMainForm myOwner = null;
        private TabCfgClass myTabCfg = null;
        private FormCfgClass myFormCfg = null;
        private int widthCfg;
        private int heightCfg;
        //private int staticsmodeCfg;
        private int tabIndex = -1;
        private int formIndex = -1;

        private int myIndex = -1;

        private static string[] FileName = { "MDIAGS.csv", "Profile.csv", "" };
        private DataTable aDataTable = new DataTable();

        //---------------------------------------------------------------------------------------------------------
        // GLOBAL PROCEDURES
        //---------------------------------------------------------------------------------------------------------        
        public MeasureFormProfile(Form OwnerForm, int width, int height, int tindex, int findex)
        {
            tabIndex = tindex;
            formIndex = findex;
            myOwner = OwnerForm as ViewMainForm;
            myTabCfg = ConfigDataClass.view.tabCfg[tabIndex];
            myFormCfg = myTabCfg.formCfg[formIndex];
            widthCfg = width;
            heightCfg = height;

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
                case (int)FormSort.Profile.SINGLE:
                case (int)FormSort.Profile.AVERAGE:
                case (int)FormSort.Profile.LAST:
                case (int)FormSort.Profile.LEFT:
                case (int)FormSort.Profile.RIGHT:
                case (int)FormSort.Profile.SINGLEAVG:
                    unitType = 1;
                    break;
                case (int)FormSort.Profile.TEMPSINGLE:
                case (int)FormSort.Profile.TEMPAVG:
                    unitType = 5;
                    break;
                default:
                    unitType = 0;
                    break;
            }

            labelUnit1.Text = UnitCfgClass.name[unitType][myFormCfg.unit.style[unitType]];
            labelUnit2.Text = UnitCfgClass.name[unitType][myFormCfg.unit.style[unitType]];
            labelUnit3.Text = UnitCfgClass.name[unitType][myFormCfg.unit.style[unitType]];

            // Title
            labelText6.Text = FileClass.textItem.GetTextItem(myFormCfg.name, ConfigDataClass.view.language);
            //tChartProfile.Axes.Bottom.Title.Text = FileClass.textItem.GetTextItem("TX_DETECTORNUM", ConfigDataClass.view.language);
            //this.fastLine1.Title = FileClass.textItem.GetTextItem("TX_THICKNESS", ConfigDataClass.view.language);

            // CheckBox
            checkBox1.Text = FileClass.textItem.GetTextItem(checkBox1.Tag.ToString(), ConfigDataClass.view.language);
            checkBox2.Text = FileClass.textItem.GetTextItem(checkBox2.Tag.ToString(), ConfigDataClass.view.language);
            checkBox3.Text = FileClass.textItem.GetTextItem(checkBox3.Tag.ToString(), ConfigDataClass.view.language);
            checkBox4.Text = FileClass.textItem.GetTextItem(checkBox4.Tag.ToString(), ConfigDataClass.view.language);
            checkBox5.Text = FileClass.textItem.GetTextItem(checkBox5.Tag.ToString(), ConfigDataClass.view.language);
            checkBox6.Text = FileClass.textItem.GetTextItem(checkBox6.Tag.ToString(), ConfigDataClass.view.language);
            checkBox7.Text = FileClass.textItem.GetTextItem(checkBox7.Tag.ToString(), ConfigDataClass.view.language);
            checkBox8.Text = FileClass.textItem.GetTextItem(checkBox8.Tag.ToString(), ConfigDataClass.view.language);

            // Menu - none

            // Label
            labelText1.Text = FileClass.textItem.GetTextItem(labelText1.Tag.ToString(), ConfigDataClass.view.language);
            labelText2.Text = FileClass.textItem.GetTextItem(labelText2.Tag.ToString(), ConfigDataClass.view.language);
            labelText3.Text = FileClass.textItem.GetTextItem(labelText3.Tag.ToString(), ConfigDataClass.view.language);
            labelText4.Text = FileClass.textItem.GetTextItem(labelText4.Tag.ToString(), ConfigDataClass.view.language);
            labelText5.Text = FileClass.textItem.GetTextItem(labelText5.Tag.ToString(), ConfigDataClass.view.language);
        }
        //-- Update color and font
        private void UpdateColorAndFont()
        {
            //-- default color and font for lab/unit/text
            setControls(this);

            // Speical handling
            tChartProfile.BackColor = myFormCfg.chartBackColor;

            textBoxMin.BackColor = myFormCfg.txtBackColorRead;
            textBoxMax.BackColor = myFormCfg.txtBackColorRead;
            textBoxAvg.BackColor = myFormCfg.txtBackColorRead;

            fastLine8.LinePen.Color = myFormCfg.fastlineColor[0];
            fastLine1.LinePen.Color = myFormCfg.fastlineColor[1];
            fastLine2.LinePen.Color = myFormCfg.fastlineColor[2];
            fastLine3.LinePen.Color = myFormCfg.fastlineColor[3];
            fastLine4.LinePen.Color = myFormCfg.fastlineColor[4];
            fastLine5.LinePen.Color = myFormCfg.fastlineColor[5];
            fastLine6.LinePen.Color = myFormCfg.fastlineColor[6];
            fastLine7.LinePen.Color = myFormCfg.fastlineColor[7];

            fastLine8.LinePen.Width = myFormCfg.fastlineWidth[0];
            fastLine1.LinePen.Width = myFormCfg.fastlineWidth[0];
            fastLine2.LinePen.Width = myFormCfg.fastlineWidth[0];
            fastLine3.LinePen.Width = myFormCfg.fastlineWidth[0];
            fastLine4.LinePen.Width = myFormCfg.fastlineWidth[0];
            fastLine5.LinePen.Width = myFormCfg.fastlineWidth[0];
            fastLine6.LinePen.Width = myFormCfg.fastlineWidth[0];
            fastLine7.LinePen.Width = myFormCfg.fastlineWidth[0];

            checkBox8.BackColor = myFormCfg.fastlineColor[0];
            checkBox1.BackColor = myFormCfg.fastlineColor[1];
            checkBox2.BackColor = myFormCfg.fastlineColor[2];
            checkBox3.BackColor = myFormCfg.fastlineColor[3];
            checkBox4.BackColor = myFormCfg.fastlineColor[4];
            checkBox5.BackColor = myFormCfg.fastlineColor[5];
            checkBox6.BackColor = myFormCfg.fastlineColor[6];
            checkBox7.BackColor = myFormCfg.fastlineColor[7];
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
            labelText6.Left = this.Width - groupBox1.Width - DISPLAYPARAMS.offsetright;
            labelText6.Top = DISPLAYPARAMS.offsettop;

            // Tee Chart
            tChartProfile.Top = DISPLAYPARAMS.offsettop;
            tChartProfile.Left = DISPLAYPARAMS.offsetleft;
            tChartProfile.Width = this.Width - groupBox1.Width - DISPLAYPARAMS.offsetleft * 3;
            tChartProfile.Height = this.Height - DISPLAYPARAMS.offsettop * 2;

            // Open End 
            if (myFormCfg.inverted)
                labelText1.Left = tChartProfile.Width - DISPLAYPARAMS.offsetright - labelText2.Width;
            else
                labelText1.Left = DISPLAYPARAMS.offsetleft;


            labelText1.Top = this.Height - DISPLAYPARAMS.offsetbottom - labelText1.Height;
            // labelText1.BackColor = Color.Transparent;
            labelText1.Parent = tChartProfile;

            // Back Arm
            if (myFormCfg.inverted)
                labelText2.Left = DISPLAYPARAMS.offsetleft;
            else
                labelText2.Left = tChartProfile.Width - DISPLAYPARAMS.offsetright - labelText2.Width;
            labelText2.Top = this.Height - DISPLAYPARAMS.offsetbottom - labelText2.Height;
            labelText2.Parent = tChartProfile;

            switch (myFormCfg.who.type)
            {
                case (int)FormSort.Profile.LAST:
                    groupBox1.Visible = false;
                    groupBox2.Left = labelText6.Left;
                    groupBox2.Top = DISPLAYPARAMS.offsettop * 2 + labelText6.Height;
                    numericUpDown1.Visible = false;
                    break;
                case (int)FormSort.Profile.ZNIC:
                case (int)FormSort.Profile.FE:
                    groupBox1.Left = labelText6.Left;
                    groupBox1.Top = DISPLAYPARAMS.offsettop * 2 + labelText6.Height;
                    numericUpDown1.Left = labelText6.Left;
                    numericUpDown1.Top = DISPLAYPARAMS.offsettop * 3 + groupBox1.Top + groupBox1.Height;
                    groupBox2.Visible = false;
                    fastLine1.Visible = false;
                    fastLine2.Visible = false;
                    fastLine3.Visible = false;
                    fastLine4.Visible = false;
                    fastLine5.Visible = false;
                    fastLine6.Visible = false;
                    fastLine7.Visible = false;
                    break;
                default:
                    groupBox1.Left = labelText6.Left;
                    groupBox1.Top = DISPLAYPARAMS.offsettop * 2 + labelText6.Height;
                    groupBox2.Visible = false;
                    numericUpDown1.Visible = false;
                    fastLine1.Visible = false;
                    fastLine2.Visible = false;
                    fastLine3.Visible = false;
                    fastLine4.Visible = false;
                    fastLine5.Visible = false;
                    fastLine6.Visible = false;
                    fastLine7.Visible = false;
                    break;
            }
        }

        private void Reset(bool clearProf)
        {
            sumData[(int)summary.AVG] = 0;
            sumData[(int)summary.MAX] = -9999.0f;
            sumData[(int)summary.MIN] = 9999.0f;
            sumCounter = 0;

            if (clearProf)
            {
                for (int i = 0; i < SIZE.PROF; ++i)
                {
                    avgData[7].thickProf.data[i] = 0;
                    avgData[7].tempProf.data[i] = 0;
                }
                sumProfile = 0;
            }
        }
        //-- read a Mdiag file and display via a table, eg. sipro
        private void DemoMdiag(int fileIndex)
        {
            try
            {
                string filePath = FileClass.rootDir + FileClass.diagPath + FileName[fileIndex];
                FileInfo aFile = new FileInfo(filePath);
                if (aFile.Exists)
                {
                    StreamReader sReader = new StreamReader(aFile.OpenRead());
                    CsvParserClass aCsvParser = new CsvParserClass();
                    aDataTable = CsvParserClass.Parse(sReader, true);

                    int NumOfData = aDataTable.Rows.Count;
                    if (NumOfData > SIZE.RAW) NumOfData = SIZE.RAW;

                    // read data
                    avgData[0].thickProf.startIndex = -1;
                    avgData[0].thickProf.stopIndex = -1;
                    for (int i = 0; i < NumOfData; ++i)
                    {
                        avgData[0].thickProf.data[i] = Convert.ToSingle(aDataTable.Rows[i][(int)MdiagTable.col.Composite]);
                        avgData[0].tempProf.data[i] = Convert.ToSingle(aDataTable.Rows[i][(int)MdiagTable.col.Temperature]);
                        avgData[1].thickProf.data[i] = Convert.ToSingle(aDataTable.Rows[i][(int)MdiagTable.col.Contour]);

                        if (avgData[0].thickProf.startIndex < 0 && avgData[0].thickProf.data[i] > 0.5)
                        {
                            avgData[0].thickProf.startIndex = i;
                        }
                        else if (avgData[0].thickProf.startIndex > avgData[0].thickProf.stopIndex && avgData[0].thickProf.data[i] < 0.5)
                        {
                            avgData[0].thickProf.stopIndex = i;
                        }
                    }

                    // Display
                    DisplayProfile(0);
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.Publish(exc);
            }
        }
        //-- read a crossprofile file and display the first profile via a table
        private void DemoProfile(int fileIndex)
        {
            try
            {
                string filePath = FileClass.rootDir + FileClass.diagPath + FileName[fileIndex];
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

                    numericUpDown1.Minimum = 1;
                    if ((ConfigDataClass.archive.measmode > 0) && (Convert.ToInt16(aDataTable.Rows[8][2].ToString()) == ConfigDataClass.archive.measmode)) // col 'C', 
                        numericUpDown1.Maximum = NumOfData / 2;
                    else
                        numericUpDown1.Maximum = NumOfData;

                    // Display
                    DisplayProfileFromTable(1);
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.Publish(exc);
            }
        }
        private void DisplayProfileFromTable(int index)
        {
            try
            {
                Reset(false);

                switch (myFormCfg.who.type)
                {
                    case (int)FormSort.Profile.ZNIC:
                        {
                            fastLine8.Clear();

                            if ((ConfigDataClass.archive.measmode > 0) && (Convert.ToInt16(aDataTable.Rows[8][2].ToString()) == ConfigDataClass.archive.measmode)) // col 'C', 
                            {
                                numericUpDown1.Value = index;

                                for (int intX = 0; intX < myFormCfg.zonenumber; intX++)
                                {
                                    sumData[(int)summary.ACT] = Convert.ToSingle(aDataTable.Rows[ProfileTable.row.HEADLINES + index * 2][intX + 11].ToString());
                                    if (!Single.IsNaN(sumData[(int)summary.ACT]))
                                    {
                                        fastLine8.Add(intX, sumData[(int)summary.ACT]);
                                        if (myFormCfg.staticsmode == 0)
                                        {
                                            sumData[(int)summary.AVG] += sumData[(int)summary.ACT]; sumCounter++;
                                            if (sumData[(int)summary.MAX] < sumData[(int)summary.ACT]) sumData[(int)summary.MAX] = sumData[(int)summary.ACT];
                                            if (sumData[(int)summary.MIN] > sumData[(int)summary.ACT]) sumData[(int)summary.MIN] = sumData[(int)summary.ACT];
                                        }
                                    }
                                    else fastLine8.SetNull(intX);
                                }
                                if (myFormCfg.staticsmode == 0)
                                {
                                    if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;

                                    textBoxMin.Text = sumData[(int)summary.MIN].ToString("F4");
                                    textBoxMax.Text = sumData[(int)summary.MAX].ToString("F4");
                                    textBoxAvg.Text = sumData[(int)summary.AVG].ToString("F4");
                                }
                                else
                                {
                                    textBoxMin.Text = aDataTable.Rows[ProfileTable.row.HEADLINES + index * 2][4].ToString();                           // col 'E', Zinc
                                    textBoxMax.Text = aDataTable.Rows[ProfileTable.row.HEADLINES + index * 2][6].ToString();                             // col 'G'
                                    textBoxAvg.Text = aDataTable.Rows[ProfileTable.row.HEADLINES + index * 2][5].ToString();                             // col 'F'
                                }
                            }
                            else
                            {
                                numericUpDown1.Value = index;

                                for (int intX = 0; intX < myFormCfg.zonenumber; intX++)
                                {
                                    sumData[(int)summary.ACT] = Convert.ToSingle(aDataTable.Rows[ProfileTable.row.HEADLINES + index][intX + 11].ToString());
                                    if (!Single.IsNaN(sumData[(int)summary.ACT]))
                                    {
                                        fastLine8.Add(intX, sumData[(int)summary.ACT]);
                                        if (myFormCfg.staticsmode == 0)
                                        {
                                            sumData[(int)summary.AVG] += sumData[(int)summary.ACT]; sumCounter++;
                                            if (sumData[(int)summary.MAX] < sumData[(int)summary.ACT]) sumData[(int)summary.MAX] = sumData[(int)summary.ACT];
                                            if (sumData[(int)summary.MIN] > sumData[(int)summary.ACT]) sumData[(int)summary.MIN] = sumData[(int)summary.ACT];
                                        }
                                    }
                                    else fastLine8.SetNull(intX);
                                }
                                if (myFormCfg.staticsmode == 0)
                                {
                                    if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;

                                    textBoxMin.Text = sumData[(int)summary.MIN].ToString("F4");
                                    textBoxMax.Text = sumData[(int)summary.MAX].ToString("F4");
                                    textBoxAvg.Text = sumData[(int)summary.AVG].ToString("F4");
                                }
                                else
                                {
                                    textBoxMin.Text = aDataTable.Rows[ProfileTable.row.HEADLINES + index][4].ToString();                           // col 'E', Zinc
                                    textBoxMax.Text = aDataTable.Rows[ProfileTable.row.HEADLINES + index][6].ToString();                             // col 'G'
                                    textBoxAvg.Text = aDataTable.Rows[ProfileTable.row.HEADLINES + index][5].ToString();                             // col 'F'
                                }
                            }
                        }
                        break;
                    case (int)FormSort.Profile.FE:
                        {
                            fastLine8.Clear();

                            numericUpDown1.Value = index;

                            for (int intX = 0; intX < myFormCfg.zonenumber; intX++)
                            {
                                sumData[(int)summary.ACT] = Convert.ToSingle(aDataTable.Rows[ProfileTable.row.HEADLINES + index * 2 + 1][intX + 11].ToString());
                                if (!Single.IsNaN(sumData[(int)summary.ACT]))
                                {
                                    fastLine8.Add(intX, sumData[(int)summary.ACT]);
                                    if (myFormCfg.staticsmode == 0)
                                    {
                                        sumData[(int)summary.AVG] += sumData[(int)summary.ACT]; sumCounter++;
                                        if (sumData[(int)summary.MAX] < sumData[(int)summary.ACT]) sumData[(int)summary.MAX] = sumData[(int)summary.ACT];
                                        if (sumData[(int)summary.MIN] > sumData[(int)summary.ACT]) sumData[(int)summary.MIN] = sumData[(int)summary.ACT];
                                    }
                                }
                                else fastLine8.SetNull(intX);

                                if (myFormCfg.staticsmode == 0)
                                {
                                    if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;

                                    textBoxMin.Text = sumData[(int)summary.MIN].ToString("F4");
                                    textBoxMax.Text = sumData[(int)summary.MAX].ToString("F4");
                                    textBoxAvg.Text = sumData[(int)summary.AVG].ToString("F4");
                                }
                                else
                                {
                                    textBoxMin.Text = aDataTable.Rows[ProfileTable.row.HEADLINES + index * 2 + 1][(int)ProfileTable.colData.Min].ToString();                           // col 'E', Zinc
                                    textBoxMax.Text = aDataTable.Rows[ProfileTable.row.HEADLINES + index * 2 + 1][6].ToString();                             // col 'G'
                                    textBoxAvg.Text = aDataTable.Rows[ProfileTable.row.HEADLINES + index * 2 + 1][5].ToString();                             // col 'F'
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("EXCEPTION in DisplayProfileFromTable", exc);
            }
        }
        //-- Display Profile from the data except a table
        private void DisplayProfile(int index)
        {
            try
            {
                Reset(false);

                switch (myFormCfg.who.type)
                {
                    case (int)FormSort.Profile.ZNIC:
                    case (int)FormSort.Profile.FE:
                        {
                            fastLine8.Clear();
                            CoilProfClass aGaugeProf = RemoteInterfaceClass.XMD.GetProfData(myFormCfg.who.index);
                            numericUpDown1.Value = index;
                            int material = myFormCfg.who.type - (int)FormSort.Profile.ZNIC;

                            for (int intX = 0; intX < myFormCfg.zonenumber; intX++)
                            {
                                sumData[(int)summary.ACT] = aGaugeProf.profile[material][intX];
                                if (!Single.IsNaN(sumData[(int)summary.ACT]))
                                {
                                    fastLine8.Add(intX, sumData[(int)summary.ACT]);
                                    if (myFormCfg.staticsmode == 0)
                                    {
                                        sumData[(int)summary.AVG] += sumData[(int)summary.ACT]; sumCounter++;
                                        if (sumData[(int)summary.MAX] < sumData[(int)summary.ACT]) sumData[(int)summary.MAX] = sumData[(int)summary.ACT];
                                        if (sumData[(int)summary.MIN] > sumData[(int)summary.ACT]) sumData[(int)summary.MIN] = sumData[(int)summary.ACT];
                                    }
                                }
                                else fastLine8.SetNull(intX);
                            }
                            if (myFormCfg.staticsmode == 0)
                            {
                                if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;

                                textBoxMin.Text = sumData[(int)summary.MIN].ToString("F4");
                                textBoxMax.Text = sumData[(int)summary.MAX].ToString("F4");
                                textBoxAvg.Text = sumData[(int)summary.AVG].ToString("F4");
                            }
                            else
                            {
                                textBoxMin.Text = aGaugeProf.min[material].ToString();
                                textBoxMax.Text = aGaugeProf.max[material].ToString();
                                textBoxAvg.Text = aGaugeProf.avg[material].ToString();
                            }
                        }
                        break;
                    case (int)FormSort.Profile.LEFT:
                    case (int)FormSort.Profile.RIGHT:
                    case (int)FormSort.Profile.AVERAGE:
                        {
                            fastLine8.Clear();
                            int oePos = ForceRange.FrcInt(-1500, 0, (avgData[0].thickProf.startIndex - CLOFFSET + 1) * RES);
                            int bePos = ForceRange.FrcInt(0, 1500, (avgData[0].thickProf.stopIndex - CLOFFSET - 1) * RES);
                            for (int intX = oePos; intX <= bePos; intX += RES)
                            {
                                sumData[(int)summary.ACT] = avgData[0].thickProf.data[intX / RES + CLOFFSET];
                                if (!Single.IsNaN(sumData[(int)summary.ACT]))
                                {
                                    fastLine8.Add(intX, sumData[(int)summary.ACT]);
                                    sumData[(int)summary.AVG] += sumData[(int)summary.ACT]; sumCounter++;
                                    if (sumData[(int)summary.MAX] < sumData[(int)summary.ACT]) sumData[(int)summary.MAX] = sumData[(int)summary.ACT];
                                    if (sumData[(int)summary.MIN] > sumData[(int)summary.ACT]) sumData[(int)summary.MIN] = sumData[(int)summary.ACT];
                                }
                                else fastLine8.SetNull(intX);
                            }
                            if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;

                            switch (myFormCfg.who.type)
                            {
                                case (int)FormSort.Profile.LEFT:
                                    tChartProfile.Axes.Bottom.AutomaticMaximum = false;
                                    tChartProfile.Axes.Bottom.Maximum = oePos + myFormCfg.dataIndex[0];
                                    break;
                                case (int)FormSort.Profile.RIGHT:
                                    tChartProfile.Axes.Bottom.AutomaticMinimum = false;
                                    tChartProfile.Axes.Bottom.Minimum = bePos - myFormCfg.dataIndex[0];
                                    break;
                            }

                            textBoxMin.Text = sumData[(int)summary.MIN].ToString("F4");
                            textBoxMax.Text = sumData[(int)summary.MAX].ToString("F4");
                            textBoxAvg.Text = sumData[(int)summary.AVG].ToString("F4");
                        }
                        break;
                    case (int)FormSort.Profile.LAST:
                        fastLine1.Clear();
                        fastLine2.Clear();
                        fastLine3.Clear();
                        fastLine4.Clear();
                        fastLine5.Clear();
                        fastLine6.Clear();
                        fastLine7.Clear();
                        fastLine8.Clear();

                        for (int i = 0; i < 8; ++i)
                        {
                            int oePos = ForceRange.FrcInt(-1500, 0, (avgData[0].thickProf.startIndex - CLOFFSET + 1) * RES);
                            int bePos = ForceRange.FrcInt(0, 1500, (avgData[0].thickProf.stopIndex - CLOFFSET - 1) * RES);

                            for (int intX = oePos; intX <= bePos; intX += RES)
                            {
                                sumData[(int)summary.ACT] = avgData[0].thickProf.data[intX / RES + CLOFFSET] + i * 0.01f;
                                if (!Single.IsNaN(sumData[(int)summary.ACT]))
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            fastLine8.Add(intX, sumData[(int)summary.ACT]);
                                            break;
                                        case 1:
                                            fastLine1.Add(intX, sumData[(int)summary.ACT]);
                                            break;
                                        case 2:
                                            fastLine2.Add(intX, sumData[(int)summary.ACT]);
                                            break;
                                        case 3:
                                            fastLine3.Add(intX, sumData[(int)summary.ACT]);
                                            break;
                                        case 4:
                                            fastLine4.Add(intX, sumData[(int)summary.ACT]);
                                            break;
                                        case 5:
                                            fastLine5.Add(intX, sumData[(int)summary.ACT]);
                                            break;
                                        case 6:
                                            fastLine6.Add(intX, sumData[(int)summary.ACT]);
                                            break;
                                        case 7:
                                            fastLine7.Add(intX, sumData[(int)summary.ACT]);
                                            break;

                                    }
                                    sumData[(int)summary.AVG] += sumData[(int)summary.ACT]; sumCounter++;

                                    if (sumData[(int)summary.MAX] < sumData[(int)summary.ACT]) sumData[(int)summary.MAX] = sumData[(int)summary.ACT];
                                    if (sumData[(int)summary.MIN] > sumData[(int)summary.ACT]) sumData[(int)summary.MIN] = sumData[(int)summary.ACT];
                                }
                            }
                        }

                        if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;

                        textBoxMin.Text = sumData[(int)summary.MIN].ToString("F4");
                        textBoxMax.Text = sumData[(int)summary.MAX].ToString("F4");
                        textBoxAvg.Text = sumData[(int)summary.AVG].ToString("F4");

                        break;
                    case (int)FormSort.Profile.TEMPAVG:
                    case (int)FormSort.Profile.TEMPSINGLE:
                        {
                            fastLine8.Clear();
                            int oePos = ForceRange.FrcInt(-1500, 0, (avgData[0].thickProf.startIndex - CLOFFSET + 1) * RES);
                            int bePos = ForceRange.FrcInt(0, 1500, (avgData[0].thickProf.stopIndex - CLOFFSET - 1) * RES);
                            int sumCounter = 0;
                            for (int intX = oePos; intX <= bePos; intX += RES)
                            {
                                sumData[(int)summary.ACT] = avgData[0].tempProf.data[intX / RES + CLOFFSET];
                                fastLine8.Add(intX, sumData[(int)summary.ACT]);
                                sumData[(int)summary.AVG] += sumData[(int)summary.ACT]; sumCounter++;
                                if (sumData[(int)summary.MAX] < sumData[(int)summary.ACT]) sumData[(int)summary.MAX] = sumData[(int)summary.ACT];
                                if (sumData[(int)summary.MIN] > sumData[(int)summary.ACT]) sumData[(int)summary.MIN] = sumData[(int)summary.ACT];
                            }
                            if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;

                            textBoxMin.Text = sumData[(int)summary.MIN].ToString("F0");
                            textBoxMax.Text = sumData[(int)summary.MAX].ToString("F0");
                            textBoxAvg.Text = sumData[(int)summary.AVG].ToString("F0");
                        }
                        break;
                    case (int)FormSort.Profile.CONTOUR:
                        {
                            fastLine8.Clear();
                            int oePos = ForceRange.FrcInt(-1500, 0, (avgData[0].thickProf.startIndex - CLOFFSET + 1) * RES);
                            int bePos = ForceRange.FrcInt(0, 1500, (avgData[0].thickProf.stopIndex - CLOFFSET - 1) * RES);
                            for (int intX = oePos; intX <= bePos; intX += RES)
                            {
                                sumData[(int)summary.ACT] = avgData[1].thickProf.data[intX / RES + CLOFFSET];
                                if (!Single.IsNaN(sumData[(int)summary.ACT]))
                                {
                                    fastLine8.Add(intX, sumData[(int)summary.ACT]);
                                    sumData[(int)summary.AVG] += sumData[(int)summary.ACT]; sumCounter++;
                                    if (sumData[(int)summary.MAX] < sumData[(int)summary.ACT]) sumData[(int)summary.MAX] = sumData[(int)summary.ACT];
                                    if (sumData[(int)summary.MIN] > sumData[(int)summary.ACT]) sumData[(int)summary.MIN] = sumData[(int)summary.ACT];
                                }
                                else fastLine8.SetNull(intX);
                            }
                            if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;

                            textBoxMin.Text = sumData[(int)summary.MIN].ToString("F4");
                            textBoxMax.Text = sumData[(int)summary.MAX].ToString("F4");
                            textBoxAvg.Text = sumData[(int)summary.AVG].ToString("F4");
                        }
                        break;
                    case (int)FormSort.Profile.SHAPE:
                        break;
                }
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("EXCEPTION in DisplayProfile", exc);
            }
        }
        //---------------------------------------------------------------------------------------------------------
        // LOCAL EVENTS
        //--------------------------------------------------------------------------------------------------------- 
        private void MeasureFormProfile_Load(object sender, EventArgs e)
        {
            UpdateLanguage();
            UpdateColorAndFont();
            UpdateSizeAndLocation();

            for (int i = 0; i < NUMDATA; ++i)
                avgData[i] = new MeasDataClass();

            index = 0;

            timer1.Interval = myFormCfg.timer[0];
            timer2.Interval = myFormCfg.timer[1];

            switch (ConfigDataClass.view.mode)
            {
                case (int)DataSort.Source.REMOTING:
                case (int)DataSort.Source.CSVFILE:
                case (int)DataSort.Source.M1COM:
                    timer1.Start();
                    timer2.Start();
                    break;
                case (int)DataSort.Source.DEMOFILE:
                    {
                        switch (myFormCfg.data.type)
                        {
                            case (int)DataSort.Type.SIPRO:
                                DemoMdiag((int)DataSort.Type.SIPRO - 1);
                                break;
                            case (int)DataSort.Type.RM310:
                                DemoProfile((int)DataSort.Type.RM310 - 1);
                                break;
                            case (int)DataSort.Type.RM210:
                                break;
                        }
                    }
                    break;
            }
        }

        //-- Automated form update --------------------------------------------------------------------------------
        private void timer1_Tick(object sender, EventArgs e)
        {
            // tee chart updates
            try
            {
                if (RemoteInterfaceClass.connected)
                {
                    switch (ConfigDataClass.view.mode)
                    {
                        case (int)DataSort.Source.REMOTING:
                            {
                                SuperStatus svStat = RemoteInterfaceClass.XMD.GetSVStatus();
                                //StripLength svLength = RemoteInterfaceClass.XMD.GetStripLength();

                                // Display
                                if (svStat.MEAS != lastMeasStatus)
                                {
                                    if (svStat.MEAS) // reset
                                        Reset(true);

                                    lastMeasStatus = svStat.MEAS;
                                }
                                else Reset(false);

                                switch (myFormCfg.who.type)
                                {
                                    case (int)FormSort.Profile.SINGLE:
                                    case (int)FormSort.Profile.LEFT:
                                    case (int)FormSort.Profile.RIGHT:
                                    case (int)FormSort.Profile.AVERAGE:
                                        avgData[0] = RemoteInterfaceClass.XMD.GetDisplayData();

                                        if (myFormCfg.who.type == (int)FormSort.Profile.AVERAGE)
                                        {
                                            sumProfile++;
                                            for (int i = 0; i < SIZE.PROF; ++i)
                                            {
                                                avgData[7].thickProf.data[i] += avgData[0].thickProf.data[i];
                                                avgData[0].thickProf.data[i] = avgData[7].thickProf.data[i] / sumProfile;
                                            }
                                        }

                                        fastLine8.Clear();
                                        //-- Occasional TeeChart exceptions only update if measuring
                                        if (svStat.MEAS)
                                        {
                                            if (!Single.IsNaN(avgData[0].aCLThickness))
                                            {
                                                int oePos = ForceRange.FrcInt(-1500, 0, (avgData[0].thickProf.startIndex - CLOFFSET + 1) * RES);
                                                int bePos = ForceRange.FrcInt(0, 1500, (avgData[0].thickProf.stopIndex - CLOFFSET - 1) * RES);
                                                int sumCounter = 0;
                                                for (int intX = oePos; intX <= bePos; intX += RES)
                                                {
                                                    sumData[(int)summary.ACT] = avgData[0].thickProf.data[intX / RES + CLOFFSET];
                                                    if (!Single.IsNaN(sumData[(int)summary.ACT]))
                                                    {
                                                        fastLine8.Add(intX, sumData[(int)summary.ACT]);
                                                        sumData[(int)summary.AVG] += sumData[(int)summary.ACT]; sumCounter++;
                                                        if (sumData[(int)summary.MAX] < sumData[(int)summary.ACT]) sumData[(int)summary.MAX] = sumData[(int)summary.ACT];
                                                        if (sumData[(int)summary.MIN] > sumData[(int)summary.ACT]) sumData[(int)summary.MIN] = sumData[(int)summary.ACT];
                                                    }
                                                    else fastLine8.SetNull(intX);
                                                }
                                                if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;

                                                switch (myFormCfg.who.type)
                                                {
                                                    case (int)FormSort.Profile.LEFT:
                                                        tChartProfile.Axes.Bottom.AutomaticMaximum = false;
                                                        tChartProfile.Axes.Bottom.Maximum = oePos + myFormCfg.dataIndex[0];
                                                        break;
                                                    case (int)FormSort.Profile.RIGHT:
                                                        tChartProfile.Axes.Bottom.AutomaticMinimum = false;
                                                        tChartProfile.Axes.Bottom.Minimum = bePos - myFormCfg.dataIndex[0];
                                                        break;
                                                }
                                            }
                                        }
                                        break;
                                    case (int)FormSort.Profile.LAST:
                                        avgData[index] = RemoteInterfaceClass.XMD.GetDisplayData();

                                        //-- Occasional TeeChart exceptions only update if measuring
                                        if (svStat.MEAS)
                                        {
                                            if (!Single.IsNaN(avgData[index].aCLThickness))
                                            {
                                                int oePos = ForceRange.FrcInt(-1500, 0, (avgData[index].thickProf.startIndex - CLOFFSET + 1) * RES);
                                                int bePos = ForceRange.FrcInt(0, 1500, (avgData[index].thickProf.stopIndex - CLOFFSET - 1) * RES);
                                                switch (index)
                                                {
                                                    case 0:
                                                        fastLine8.Clear();
                                                        for (int intX = oePos; intX <= bePos; intX += RES)
                                                            if (!Single.IsNaN(avgData[0].thickProf.data[intX / RES + CLOFFSET]))
                                                                fastLine8.Add(intX, avgData[0].thickProf.data[intX / RES + CLOFFSET]);
                                                            else fastLine8.SetNull(intX);
                                                        break;
                                                    case 1:
                                                        fastLine1.Clear();
                                                        for (int intX = oePos; intX <= bePos; intX += RES)
                                                            if (!Single.IsNaN(avgData[1].thickProf.data[intX / RES + CLOFFSET]))
                                                                fastLine1.Add(intX, avgData[1].thickProf.data[intX / RES + CLOFFSET]);
                                                            else fastLine1.SetNull(intX);
                                                        break;
                                                    case 2:
                                                        fastLine2.Clear();
                                                        for (int intX = oePos; intX <= bePos; intX += RES)
                                                            if (!Single.IsNaN(avgData[2].thickProf.data[intX / RES + CLOFFSET]))
                                                                fastLine2.Add(intX, avgData[2].thickProf.data[intX / RES + CLOFFSET]);
                                                            else fastLine2.SetNull(intX);
                                                        break;
                                                    case 3:
                                                        fastLine3.Clear();
                                                        for (int intX = oePos; intX <= bePos; intX += RES)
                                                            if (!Single.IsNaN(avgData[3].thickProf.data[intX / RES + CLOFFSET]))
                                                                fastLine3.Add(intX, avgData[3].thickProf.data[intX / RES + CLOFFSET]);
                                                            else fastLine3.SetNull(intX);
                                                        break;
                                                    case 4:
                                                        fastLine4.Clear();
                                                        for (int intX = oePos; intX <= bePos; intX += RES)
                                                            if (!Single.IsNaN(avgData[4].thickProf.data[intX / RES + CLOFFSET]))
                                                                fastLine4.Add(intX, avgData[4].thickProf.data[intX / RES + CLOFFSET]);
                                                            else fastLine4.SetNull(intX);
                                                        break;
                                                    case 5:
                                                        fastLine5.Clear();
                                                        for (int intX = oePos; intX <= bePos; intX += RES)
                                                            if (!Single.IsNaN(avgData[5].thickProf.data[intX / RES + CLOFFSET]))
                                                                fastLine5.Add(intX, avgData[5].thickProf.data[intX / RES + CLOFFSET]);
                                                            else fastLine5.SetNull(intX);
                                                        break;
                                                    case 6:
                                                        fastLine6.Clear();
                                                        for (int intX = oePos; intX <= bePos; intX += RES)
                                                            if (!Single.IsNaN(avgData[6].thickProf.data[intX / RES + CLOFFSET]))
                                                                fastLine6.Add(intX, avgData[6].thickProf.data[intX / RES + CLOFFSET]);
                                                            else fastLine6.SetNull(intX);
                                                        break;
                                                    case 7:
                                                        fastLine7.Clear();
                                                        for (int intX = oePos; intX <= bePos; intX += RES)
                                                            if (!Single.IsNaN(avgData[7].thickProf.data[intX / RES + CLOFFSET]))
                                                                fastLine7.Add(intX, avgData[7].thickProf.data[intX / RES + CLOFFSET]);
                                                            else fastLine7.SetNull(intX);
                                                        break;
                                                }
                                            }
                                        }
                                        ++index;
                                        if (index == NUMDATA) index = 0;
                                        break;
                                    case (int)FormSort.Profile.TEMPAVG:
                                    case (int)FormSort.Profile.TEMPSINGLE:
                                        PyroDataClass pyroData = RemoteInterfaceClass.XMD.GetPyroData();
                                        avgData[0] = RemoteInterfaceClass.XMD.GetDisplayData();

                                        if (myFormCfg.who.type == (int)FormSort.Profile.TEMPAVG)
                                        {
                                            sumProfile++;
                                            for (int i = 0; i < SIZE.PROF; ++i)
                                            {
                                                avgData[7].tempProf.data[i] += avgData[0].tempProf.data[i];
                                                avgData[0].tempProf.data[i] = avgData[7].tempProf.data[i] / sumProfile;
                                            }
                                            //sumProfile++;
                                        }

                                        fastLine8.Clear();
                                        if (svStat.MEAS)
                                        {
                                            int oePos = ForceRange.FrcInt(-1500, 0, (avgData[0].thickProf.startIndex - CLOFFSET + 1) * RES);
                                            int bePos = ForceRange.FrcInt(0, 1500, (avgData[0].thickProf.stopIndex - CLOFFSET - 1) * RES);
                                            int sumCounter = 0;
                                            for (int intX = oePos; intX <= bePos; intX += RES)
                                            {
                                                sumData[(int)summary.ACT] = pyroData.tempProf.data[intX / RES + CLOFFSET];
                                                fastLine8.Add(intX, sumData[(int)summary.ACT]);
                                                sumData[(int)summary.AVG] += sumData[(int)summary.ACT]; sumCounter++;
                                                if (sumData[(int)summary.MAX] < sumData[(int)summary.ACT]) sumData[(int)summary.MAX] = sumData[(int)summary.ACT];
                                                if (sumData[(int)summary.MIN] > sumData[(int)summary.ACT]) sumData[(int)summary.MIN] = sumData[(int)summary.ACT];

                                            }
                                            if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;
                                        }
                                        break;
                                    case (int)FormSort.Profile.CONTOUR:
                                        avgData[0] = RemoteInterfaceClass.XMD.GetDisplayData();
                                        fastLine8.Clear();
                                        //-- Occasional TeeChart exceptions only update if measuring
                                        if (svStat.MEAS)
                                        {
                                            if (!Single.IsNaN(avgData[0].aCLThickness))
                                            {
                                                for (int i = 0; i < SIZE.SHAPE; ++i)
                                                {
                                                    sumData[(int)summary.ACT] = avgData[0].contour.data[i];
                                                    fastLine8.Add(i, sumData[(int)summary.ACT]);
                                                    sumData[(int)summary.AVG] += sumData[(int)summary.ACT]; sumCounter++;
                                                    if (sumData[(int)summary.MAX] < sumData[(int)summary.ACT]) sumData[(int)summary.MAX] = sumData[(int)summary.ACT];
                                                    if (sumData[(int)summary.MIN] > sumData[(int)summary.ACT]) sumData[(int)summary.MIN] = sumData[(int)summary.ACT];
                                                }
                                            }
                                            if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;
                                        }
                                        break;
                                    case (int)FormSort.Profile.SHAPE:
                                        avgData[0] = RemoteInterfaceClass.XMD.GetDisplayData();
                                        fastLine8.Clear();
                                        //-- Occasional TeeChart exceptions only update if measuring
                                        if (svStat.MEAS)
                                        {
                                            if (!Single.IsNaN(avgData[0].aCLThickness))
                                            {
                                                int oePos = ForceRange.FrcInt(-1500, 0, (avgData[0].shape.startIndex - CLOFFSET + 1) * RES);
                                                int bePos = ForceRange.FrcInt(0, 1500, (avgData[0].shape.stopIndex - CLOFFSET - 1) * RES);
                                                for (int intX = oePos; intX <= bePos; intX += RES)
                                                    if (!Single.IsNaN(avgData[0].shape.data[intX / RES + CLOFFSET]))
                                                    {
                                                        fastLine8.Add(intX, avgData[0].shape.data[intX / RES + CLOFFSET]);
                                                    }
                                                    else fastLine8.SetNull(intX);
                                            }
                                        }
                                        break;
                                }
                            }
                            break;
                        case (int)DataSort.Source.M1COM:
                            {

                            }
                            break;
                    }
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.Publish(exc);
                // ErrorForm.AddException(exc, "Measure Form Data");
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
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
                                SuperStatus svStat = RemoteInterfaceClass.XMD.GetSVStatus();

                                switch (myFormCfg.who.type)
                                {
                                    case (int)FormSort.Profile.SINGLE:
                                        if (svStat.MEAS)
                                        {
                                            textBoxMin.Text = (sumData[(int)summary.MIN] * myFormCfg.unit.factor[(int)UnitCfgClass.type.THICK] + myFormCfg.unit.offset[(int)UnitCfgClass.type.THICK]).ToString("F4");
                                            textBoxMax.Text = (sumData[(int)summary.MAX] * myFormCfg.unit.factor[(int)UnitCfgClass.type.THICK] + myFormCfg.unit.offset[(int)UnitCfgClass.type.THICK]).ToString("F4");
                                            textBoxAvg.Text = (sumData[(int)summary.AVG] * myFormCfg.unit.factor[(int)UnitCfgClass.type.THICK] + myFormCfg.unit.offset[(int)UnitCfgClass.type.THICK]).ToString("F4");
                                        }
                                        else
                                        {
                                            textBoxMin.Text = "000.0";
                                            textBoxMax.Text = "000.0";
                                            textBoxAvg.Text = "000.0";
                                        }
                                        break;
                                    case (int)FormSort.Profile.TEMPSINGLE:
                                        if (svStat.MEAS)
                                        {
                                            textBoxMin.Text = (sumData[(int)summary.MIN] * myFormCfg.unit.factor[(int)UnitCfgClass.type.TEMPERATURE] + myFormCfg.unit.offset[(int)UnitCfgClass.type.TEMPERATURE]).ToString("F4");
                                            textBoxMax.Text = (sumData[(int)summary.MAX] * myFormCfg.unit.factor[(int)UnitCfgClass.type.TEMPERATURE] + myFormCfg.unit.offset[(int)UnitCfgClass.type.TEMPERATURE]).ToString("F4");
                                            textBoxAvg.Text = (sumData[(int)summary.AVG] * myFormCfg.unit.factor[(int)UnitCfgClass.type.TEMPERATURE] + myFormCfg.unit.offset[(int)UnitCfgClass.type.TEMPERATURE]).ToString("F4");
                                        }
                                        else
                                        {
                                            textBoxMin.Text = "000.0";
                                            textBoxMax.Text = "000.0";
                                            textBoxAvg.Text = "000.0";
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;

                        case (int)DataSort.Source.CSVFILE:
                            {
                                int curIndex = RemoteInterfaceClass.XMD.GetUpdateProfileIndex(myFormCfg.who.index);
                                if (((Convert.ToInt16(numericUpDown1.Value.ToString()) - 1) == curIndex) && (myIndex != curIndex))
                                {
                                    DisplayProfile(curIndex);
                                    myIndex = curIndex;
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
                // ErrorForm.AddException(exc, "Measure Form Data");
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked) fastLine8.Visible = true;
            else fastLine8.Visible = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) fastLine1.Visible = true;
            else fastLine1.Visible = false;
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

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked) fastLine4.Visible = true;
            else fastLine4.Visible = false;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked) fastLine5.Visible = true;
            else fastLine5.Visible = false;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked) fastLine6.Visible = true;
            else fastLine6.Visible = false;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked) fastLine7.Visible = true;
            else fastLine7.Visible = false;
        }

        private void tChartProfile_DoubleClick(object sender, EventArgs e)
        {
            ChartForm aChartForm = new ChartForm(tChartProfile, myFormCfg);
            aChartForm.Location = Control.MousePosition;
            aChartForm.Visible = true;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (RemoteInterfaceClass.connected)
                {
                    switch (ConfigDataClass.view.mode)
                    {
                        case (int)DataSort.Source.DEMOFILE:
                            {
                                DisplayProfile(Convert.ToInt16(numericUpDown1.Value.ToString()) - 1);
                            }
                            break;
                        case (int)DataSort.Source.CSVFILE:
                            {
                                RemoteInterfaceClass.XMD.RequestProfIndex(0, Convert.ToInt16(numericUpDown1.Value.ToString()) - 1);
                            }
                            break;
                    }
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.Publish(exc);
                // ErrorForm.AddException(exc, "Measure Form Data");
            }
            //=========================================================================================================

        }
    }
}