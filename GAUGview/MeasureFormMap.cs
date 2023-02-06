//=================================================================================================================
//  Project:    RM312/SIPRO SUPERvisor
//  Module:     MeasureFormMap.cs                                                                         
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
    public partial class MeasureFormMap : Form
    {
        //---------------------------------------------------------------------------------------------------------
        // CLASS VARIABLES
        //---------------------------------------------------------------------------------------------------------
        private const int CLOFFSET = 300;
        private const int RES = 5;
        private const int NUMDATA = 8;
        private MeasDataClass avgData = new MeasDataClass();
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
        private bool lastMeasStatus = false;

        private ViewMainForm myOwner = null;
        private TabCfgClass myTabCfg = null;
        private FormCfgClass myFormCfg = null;
        private int widthCfg;
        private int heightCfg;
        private int tabIndex = -1;
        private int formIndex = -1;

        private static string[] FileName = { "MDIAGS.csv" };
        private DataTable aDataTable = new DataTable();
        private enum tabCols
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
        //---------------------------------------------------------------------------------------------------------
        // GLOBAL PROCEDURES
        //---------------------------------------------------------------------------------------------------------
        public MeasureFormMap(Form OwnerForm, int width, int height, int tindex, int findex)
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
            int unitType = 0;
            switch (myFormCfg.who.type)
            {
                case (int)FormSort.Map.THICK:
                    unitType = 1;
                    break;
                case (int)FormSort.Map.HEIGHT:
                    unitType = 2;
                    break;
                case (int)FormSort.Map.TEMP:
                    unitType = 5;
                    break;
                default:
                    break;
            }

            labelUnit1.Text = UnitCfgClass.name[unitType][myFormCfg.unit.style[unitType]];
            labelUnit2.Text = UnitCfgClass.name[unitType][myFormCfg.unit.style[unitType]];
            labelUnit3.Text = UnitCfgClass.name[unitType][myFormCfg.unit.style[unitType]];

            // Title
            labelText6.Text = FileClass.textItem.GetTextItem(myFormCfg.name, ConfigDataClass.view.language);
            //tChartProfile.Axes.Bottom.Title.Text = FileClass.textItem.GetTextItem(myFormCfg.name, ConfigDataClass.view.language);
            //tChartProfile.Axes.Bottom.Title.Color = myFormCfg.labFont.color;

            // Label
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

            // Summary
            groupBox1.Left = labelText6.Left;
            groupBox1.Top = DISPLAYPARAMS.offsettop * 2 + labelText6.Height;

            // Tee Chart
            tChartProfile.Top = DISPLAYPARAMS.offsettop;
            tChartProfile.Left = DISPLAYPARAMS.offsetleft;
            tChartProfile.Width = this.Width - groupBox1.Width - DISPLAYPARAMS.offsetleft * 3;
            tChartProfile.Height = this.Height - DISPLAYPARAMS.offsettop * 2;

            tChartProfile.Chart.Panel.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;

            //this.FormBorderStyle = FormBorderStyle.None;
            //this.Dock = DockStyle.Fill;
            //myMapColorGrid = new Steema.TeeChart.Styles.ColorGrid(tChartProfile.Chart);
            colorGrid1.Pen.Visible = false;
            colorGrid1.IrregularGrid = false;

            //tChartProfile.Header.Font.Size = 12;
            //tChartProfile.Header.Color = Color.White;
            //tChartProfile.Header.Transparent = false;
            //tChartProfile.Header.Text = mapTitle;

            //tChartProfile.Aspect.View3D = false;
            //tChartProfile.Legend.Visible = false;

            //tChartPro.file.Chart.Axes.Bottom.Increment = 1.0;

            //tChartProfile.Chart.Axes.Bottom.MinAxisIncrement = 1.0;
            tChartProfile.Aspect.View3D = false;
            tChartProfile.Aspect.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            tChartProfile.Legend.Visible = false;
            //tChartProfile.Axes.Bottom.Title.Text = "R";
            tChartProfile.Axes.Bottom.SetMinMax(0, 600);
            tChartProfile.Axes.Bottom.Increment = 20;
            //tChartProfile.Axes.Left.Title.Text = "D";
            tChartProfile.Axes.Left.SetMinMax(290, 310);
        }

        private void Reset()
        {
            sumData[(int)summary.AVG] = 0;
            sumData[(int)summary.MAX] = -9999.0f;
            sumData[(int)summary.MIN] = 9999.0f;

            colorGrid1.Clear();
            sumCounter = 0;
        }

        private void Simulation()
        {
            try
            {
                string filePath = FileClass.rootDir + FileClass.diagPath + FileName[0];
                FileInfo aFile = new FileInfo(filePath);
                if (aFile.Exists)
                {
                    StreamReader sReader = new StreamReader(aFile.OpenRead());
                    CsvParserClass aCsvParser = new CsvParserClass();
                    aDataTable = CsvParserClass.Parse(sReader, true);

                    int NumOfData = aDataTable.Rows.Count;
                    if (NumOfData > SIZE.RAW) NumOfData = SIZE.RAW;

                    float sNomimal=0;
                    switch (myFormCfg.who.type)
                    {
                        case (int)FormSort.Map.THICK:
                            sNomimal = Convert.ToSingle(aDataTable.Rows[18][(int)tabCols.Value]);
                            break;
                        case (int)FormSort.Map.TEMP:
                            sNomimal = 900;
                            break;
                        case (int)FormSort.Map.SHAPE:
                            sNomimal = 0;
                            break;
                    }
                    float sThinLim = sNomimal * 0.99f;
                    float sVThinLim = sNomimal * 0.98f;
                    float sThickLim = sNomimal * 1.01f;
                    float sVThickLim = sNomimal * 1.02f;

                    avgData.thickProf.startIndex = -1;
                    avgData.thickProf.stopIndex = -1;
                    for (int i = 0; i < NumOfData; ++i)
                    {
                        switch (myFormCfg.who.type)
                        {
                            case (int)FormSort.Map.THICK:
                                avgData.thickProf.data[i] = Convert.ToSingle(aDataTable.Rows[i][(int)tabCols.Composite]);
                                break;
                            case (int)FormSort.Map.SHAPE:
                                avgData.thickProf.data[i] = Convert.ToSingle(aDataTable.Rows[i][(int)tabCols.Composite]);
                                avgData.tempProf.data[i] = Convert.ToSingle(aDataTable.Rows[i][(int)tabCols.Shape]);                                
                                break;
                            case (int)FormSort.Map.TEMP:
                                avgData.thickProf.data[i] = Convert.ToSingle(aDataTable.Rows[i][(int)tabCols.Composite]);
                                avgData.tempProf.data[i] = Convert.ToSingle(aDataTable.Rows[i][(int)tabCols.Temperature]);                                
                                break;
                        }

                        if (avgData.thickProf.startIndex < 0 && avgData.thickProf.data[i] > 0.5)
                            avgData.thickProf.startIndex = i;
                        else if (avgData.thickProf.startIndex > avgData.thickProf.stopIndex && avgData.thickProf.data[i] < 0.5)
                            avgData.thickProf.stopIndex = i;
                    }

                    // Display
                    Reset();
                    switch (myFormCfg.who.type)
                    {
                        case (int)FormSort.Map.THICK:
                            for (int i = 0; i < MAXNUM.STRIP; ++i)
                            {
                                int oePos = avgData.thickProf.startIndex - 5;
                                int bePos = avgData.thickProf.stopIndex + 5;
                                if ((oePos >= 0) && (oePos < tChartProfile.Axes.Left.Minimum)) tChartProfile.Axes.Left.Minimum = oePos;
                                if ((bePos <= 600) && (bePos > tChartProfile.Axes.Left.Maximum)) tChartProfile.Axes.Left.Maximum = bePos;

                                for (int intX = oePos; intX <= bePos; ++intX)
                                {
                                    Color myColor = Color.Green;
                                    if (!Single.IsNaN(avgData.thickProf.data[intX]) && (avgData.thickProf.data[intX] > 0.25))
                                    {
                                        sumData[(int)summary.ACT] = avgData.thickProf.data[intX];
                                        sumData[(int)summary.AVG] += sumData[(int)summary.ACT]; sumCounter++;
                                        if (sumData[(int)summary.MAX] < sumData[(int)summary.ACT]) sumData[(int)summary.MAX] = sumData[(int)summary.ACT];
                                        if (sumData[(int)summary.MIN] > sumData[(int)summary.ACT]) sumData[(int)summary.MIN] = sumData[(int)summary.ACT];

                                        if ((sumData[(int)summary.ACT] < sVThinLim) || (sumData[(int)summary.ACT] > sVThickLim))
                                        {
                                            myColor = Color.DarkRed;
                                        }
                                        else if ((sumData[(int)summary.ACT] < sThinLim) || (sumData[(int)summary.ACT] > sThickLim))
                                        {
                                            myColor = Color.Red;
                                        }
                                        colorGrid1.Add(i, 1, intX, myColor);
                                    }
                                }                                
                            }
                            if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;

                            textBoxMin.Text = sumData[(int)summary.MIN].ToString("F4");
                            textBoxMax.Text = sumData[(int)summary.MAX].ToString("F4");
                            textBoxAvg.Text = sumData[(int)summary.AVG].ToString("F4");

                            break;
                        case (int)FormSort.Map.TEMP:
                            for (int i = 0; i < MAXNUM.STRIP; ++i)
                            {
                                int oePos = avgData.thickProf.startIndex - 5;
                                int bePos = avgData.thickProf.stopIndex + 5;
                                if ((oePos >= 0) && (oePos < tChartProfile.Axes.Left.Minimum)) tChartProfile.Axes.Left.Minimum = oePos;
                                if ((bePos <= 600) && (bePos > tChartProfile.Axes.Left.Maximum)) tChartProfile.Axes.Left.Maximum = bePos;

                                for (int intX = oePos; intX <= bePos; ++intX)
                                {
                                    Color myColor = Color.Green;
                                    if (!Single.IsNaN(avgData.tempProf.data[intX]))
                                    {
                                        sumData[(int)summary.ACT] = avgData.tempProf.data[intX];
                                        sumData[(int)summary.AVG] += sumData[(int)summary.ACT]; sumCounter++;
                                        if (sumData[(int)summary.MAX] < sumData[(int)summary.ACT]) sumData[(int)summary.MAX] = sumData[(int)summary.ACT];
                                        if (sumData[(int)summary.MIN] > sumData[(int)summary.ACT]) sumData[(int)summary.MIN] = sumData[(int)summary.ACT];

                                        if ((sumData[(int)summary.ACT] < sVThinLim) || (sumData[(int)summary.ACT] > sVThickLim))
                                        {
                                            myColor = Color.DarkRed;
                                        }
                                        else if ((sumData[(int)summary.ACT] < sThinLim) || (sumData[(int)summary.ACT] > sThickLim))
                                        {
                                            myColor = Color.Red;
                                        }
                                        colorGrid1.Add(i, 1, intX, myColor);
                                    }
                                }
                            }
                            if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;

                            textBoxMin.Text = sumData[(int)summary.MIN].ToString("F4");
                            textBoxMax.Text = sumData[(int)summary.MAX].ToString("F4");
                            textBoxAvg.Text = sumData[(int)summary.AVG].ToString("F4");
                            break;
                        case (int)FormSort.Map.SHAPE:
                            for (int i = 0; i < MAXNUM.STRIP; ++i)
                            {
                                int oePos = avgData.thickProf.startIndex - 5;
                                int bePos = avgData.thickProf.stopIndex + 5;
                                if ((oePos >= 0) && (oePos < tChartProfile.Axes.Left.Minimum)) tChartProfile.Axes.Left.Minimum = oePos;
                                if ((bePos <= 600) && (bePos > tChartProfile.Axes.Left.Maximum)) tChartProfile.Axes.Left.Maximum = bePos;

                                for (int intX = oePos; intX <= bePos; ++intX)
                                {
                                    Color myColor = Color.Green;
                                    if (!Single.IsNaN(avgData.tempProf.data[intX]))
                                    {
                                        sumData[(int)summary.ACT] = avgData.tempProf.data[intX];
                                        sumData[(int)summary.AVG] += sumData[(int)summary.ACT]; sumCounter++;
                                        if (sumData[(int)summary.MAX] < sumData[(int)summary.ACT]) sumData[(int)summary.MAX] = sumData[(int)summary.ACT];
                                        if (sumData[(int)summary.MIN] > sumData[(int)summary.ACT]) sumData[(int)summary.MIN] = sumData[(int)summary.ACT];

                                        if ((sumData[(int)summary.ACT] < sVThinLim) || (sumData[(int)summary.ACT] > sVThickLim))
                                        {
                                            myColor = Color.DarkRed;
                                        }
                                        else if ((sumData[(int)summary.ACT] < sThinLim) || (sumData[(int)summary.ACT] > sThickLim))
                                        {
                                            myColor = Color.Red;
                                        }
                                        colorGrid1.Add(i, 1, intX, myColor);
                                    }
                                }
                            }
                            if (sumCounter > 0) sumData[(int)summary.AVG] /= sumCounter;

                            textBoxMin.Text = sumData[(int)summary.MIN].ToString("F4");
                            textBoxMax.Text = sumData[(int)summary.MAX].ToString("F4");
                            textBoxAvg.Text = sumData[(int)summary.AVG].ToString("F4");
                            break;
                    }
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.Publish(exc);
            }
        }
        //---------------------------------------------------------------------------------------------------------
        // LOCAL EVENTS
        //--------------------------------------------------------------------------------------------------------- 
        private void MeasureFormMap_Load(object sender, EventArgs e)
        {
            UpdateLanguage();
            UpdateColorAndFont();
            UpdateSizeAndLocation();

            timer1.Interval = myFormCfg.timer[0];
            timer2.Interval = myFormCfg.timer[1];

            if (myFormCfg.mode==0)
                Simulation();
            else
            {
                timer1.Start();
                timer2.Start();
            }
        }

        //-- Automated form update --------------------------------------------------------------------------------
        private void timer1_Tick(object sender, EventArgs e)
        {
            // tee chart updates
            try
            {
                if (!RemoteInterfaceClass.connected) return;
                SuperStatus svStat = RemoteInterfaceClass.XMD.GetSVStatus();
                StripLength svLength = RemoteInterfaceClass.XMD.GetStripLength();
                SetupData setCurrent = RemoteInterfaceClass.XMD.GetCurrentSetup();

                if (svStat.MEAS != lastMeasStatus)
                {
                    if (svStat.MEAS) // reset
                        Reset();
                    lastMeasStatus = svStat.MEAS;
                }


                // tChartProfile.Clear();
                switch (myFormCfg.who.type)
                {
                    case (int)FormSort.Map.THICK:
                        avgData = RemoteInterfaceClass.XMD.GetDisplayData();

                        if (svStat.MEAS)
                        {
                            //if (!Single.IsNaN(avgData.aCLThickness))
                            {
                                int oePos = avgData.thickProf.startIndex - 5;
                                int bePos = avgData.thickProf.stopIndex + 5;
                                if ((oePos>=0) && (oePos < tChartProfile.Axes.Left.Minimum)) tChartProfile.Axes.Left.Minimum = oePos;
                                if ((bePos<=600) && (bePos > tChartProfile.Axes.Left.Maximum)) tChartProfile.Axes.Left.Maximum = bePos;
                               
                                if (svLength.length[(int)svLength.lengthSource] > tChartProfile.Axes.Bottom.Maximum * myFormCfg.rate4ext)
                                {
                                    tChartProfile.Axes.Bottom.Maximum *= myFormCfg.rate2ext;
                                }

                                for (int intX = oePos; intX <= bePos; ++intX)
                                {
                                    Color myColor = Color.Green;
                                    if (!Single.IsNaN(avgData.thickProf.data[intX]) && (avgData.thickProf.data[intX] > 0.25))
                                    {
                                        sumData[(int)summary.ACT] = avgData.thickProf.data[intX];
                                        sumData[(int)summary.AVG] += sumData[(int)summary.ACT]; sumCounter++;
                                        if (sumData[(int)summary.MAX] < sumData[(int)summary.ACT]) sumData[(int)summary.MAX] = sumData[(int)summary.ACT];
                                        if (sumData[(int)summary.MIN] > sumData[(int)summary.ACT]) sumData[(int)summary.MIN] = sumData[(int)summary.ACT];

                                        if ((avgData.thickProf.data[intX] < (setCurrent.sNomThick - setCurrent.sVThinLim)) ||
                                            (avgData.thickProf.data[intX] > (setCurrent.sNomThick + setCurrent.sVThickLim)))
                                        {
                                            myColor = Color.DarkRed;
                                        }
                                        else if ((avgData.thickProf.data[intX] < (setCurrent.sNomThick - setCurrent.sThinLim)) ||
                                            (avgData.thickProf.data[intX] > (setCurrent.sNomThick + setCurrent.sThickLim)))
                                        {
                                            myColor = Color.Red;
                                        }
                                        colorGrid1.Add(svLength.length[(int)svLength.lengthSource], 1, intX, myColor);
                                    }                                    
                                }
                            }
                        }
                        break;
                    case (int)FormSort.Map.TEMP:
                        avgData = RemoteInterfaceClass.XMD.GetDisplayData();
                        if (svStat.MEAS)
                        {
                            //if (!Single.IsNaN(avgData.aCLThickness))
                            {
                                int oePos = avgData.thickProf.startIndex - 5;
                                int bePos = avgData.thickProf.stopIndex + 5;
                                if ((oePos >= 0) && (oePos < tChartProfile.Axes.Left.Minimum)) tChartProfile.Axes.Left.Minimum = oePos;
                                if ((bePos <= 600) && (bePos > tChartProfile.Axes.Left.Maximum)) tChartProfile.Axes.Left.Maximum = bePos;

                                if (svLength.length[(int)svLength.lengthSource] > tChartProfile.Axes.Bottom.Maximum * myFormCfg.rate4ext)
                                {
                                    tChartProfile.Axes.Bottom.Maximum *= myFormCfg.rate2ext;
                                }

                                for (int intX = oePos; intX <= bePos; ++intX)
                                {
                                    Color myColor = Color.Green;
                                    if (!Single.IsNaN(avgData.thickProf.data[intX]) && (avgData.thickProf.data[intX] > 0.25))
                                    {
                                        sumData[(int)summary.ACT] = avgData.tempProf.data[intX];
                                        sumData[(int)summary.AVG] += sumData[(int)summary.ACT]; sumCounter++;
                                        if (sumData[(int)summary.MAX] < sumData[(int)summary.ACT]) sumData[(int)summary.MAX] = sumData[(int)summary.ACT];
                                        if (sumData[(int)summary.MIN] > sumData[(int)summary.ACT]) sumData[(int)summary.MIN] = sumData[(int)summary.ACT];

                                        // Black:   0,0,0
                                        // Blue:    0,0,255
                                        // Green:   0,255,0
                                        // Yellow:  255,255,0
                                        // Red:     255,0,0

                                        float tempDiff = avgData.tempProf.data[intX] - setCurrent.sStripTemp;

                                        if (tempDiff < -510) myColor = Color.FromArgb(0, 0, 0);
                                        else if (tempDiff < -255) myColor = Color.FromArgb(0, 0, (int)(510 + tempDiff));
                                        else if (tempDiff < 0) myColor = Color.FromArgb(0, (int)(255 + tempDiff), (int)(-tempDiff));
                                        else if (tempDiff < 255) myColor = Color.FromArgb((int)tempDiff, 255, 0);
                                        else if (tempDiff < 510) myColor = Color.FromArgb(255, (int)(510-tempDiff), 0);
                                        else myColor = Color.FromArgb(255, 0, 0);

                                        colorGrid1.Add(svLength.length[(int)svLength.lengthSource], 1, intX, myColor);
                                    }
                                }
                            }
                        }
                        break;
                    case (int)FormSort.Map.SHAPE:
                        avgData = RemoteInterfaceClass.XMD.GetDisplayData();
                        if (svStat.MEAS)
                        {
                            //if (!Single.IsNaN(avgData.aCLThickness))
                            {
                                int oePos = avgData.shape.startIndex - 5;
                                int bePos = avgData.shape.stopIndex + 5;
                                if ((oePos >= 0) && (oePos < tChartProfile.Axes.Left.Minimum)) tChartProfile.Axes.Left.Minimum = oePos;
                                if ((bePos <= 600) && (bePos > tChartProfile.Axes.Left.Maximum)) tChartProfile.Axes.Left.Maximum = bePos;

                                if (svLength.length[(int)svLength.lengthSource] > tChartProfile.Axes.Bottom.Maximum * myFormCfg.rate4ext)
                                {
                                    tChartProfile.Axes.Bottom.Maximum *= myFormCfg.rate2ext;
                                }

                                for (int intX = oePos; intX <= bePos; ++intX)
                                {
                                    Color myColor = Color.Green;
                                    if (!Single.IsNaN(avgData.shape.data[intX]))
                                    {
                                        sumData[(int)summary.ACT] = avgData.shape.data[intX];
                                        sumData[(int)summary.AVG] += sumData[(int)summary.ACT]; sumCounter++;
                                        if (sumData[(int)summary.MAX] < sumData[(int)summary.ACT]) sumData[(int)summary.MAX] = sumData[(int)summary.ACT];
                                        if (sumData[(int)summary.MIN] > sumData[(int)summary.ACT]) sumData[(int)summary.MIN] = sumData[(int)summary.ACT];

                                        // Blue:    0,0,255
                                        // Cyan:    0,255,255
                                        // Green:   0,255,0
                                        // Yellow:  255,255,0
                                        // Red:     255,0,0

                                        float tempDiff = avgData.tempProf.data[intX] - setCurrent.sStripTemp;

                                        if (tempDiff < -510) myColor = Color.FromArgb(0, 0, 255);
                                        else if (tempDiff < -255) myColor = Color.FromArgb(0, (int)(510 + tempDiff), 255);
                                        else if (tempDiff < 0) myColor = Color.FromArgb(0, 255, (int)(-tempDiff));
                                        else if (tempDiff < 255) myColor = Color.FromArgb((int)tempDiff, 255, 0);
                                        else if (tempDiff < 510) myColor = Color.FromArgb(255, (int)(510 - tempDiff), 0);
                                        else myColor = Color.FromArgb(255, 0, 0);

                                        colorGrid1.Add(svLength.length[(int)svLength.lengthSource], 1, intX, myColor);
                                    }
                                }
                            }
                        }
                        break;
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
                if (!RemoteInterfaceClass.connected) return;
                SuperStatus svStat = RemoteInterfaceClass.XMD.GetSVStatus();
                SetupData setCurrent = RemoteInterfaceClass.XMD.GetCurrentSetup();

                switch (myFormCfg.who.type)
                {
                    case (int)FormSort.Map.THICK:
                        avgData = (MeasDataClass)XMDdata.measDisplayAvg.Clone();
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
                    case (int)FormSort.Map.TEMP:
                        avgData = (MeasDataClass)XMDdata.measDisplayAvg.Clone();
                        if (svStat.MEAS)
                        {
                            textBoxMin.Text = (sumData[(int)summary.MIN] * myFormCfg.unit.factor[(int)UnitCfgClass.type.TEMPERATURE] + myFormCfg.unit.offset[(int)UnitCfgClass.type.TEMPERATURE]).ToString("F1");
                            textBoxMax.Text = (sumData[(int)summary.MAX] * myFormCfg.unit.factor[(int)UnitCfgClass.type.TEMPERATURE] + myFormCfg.unit.offset[(int)UnitCfgClass.type.TEMPERATURE]).ToString("F1");
                            textBoxAvg.Text = (sumData[(int)summary.AVG] * myFormCfg.unit.factor[(int)UnitCfgClass.type.TEMPERATURE] + myFormCfg.unit.offset[(int)UnitCfgClass.type.TEMPERATURE]).ToString("F1");
                        }
                        else
                        {
                            textBoxMin.Text = "000.0";
                            textBoxMax.Text = "000.0";
                            textBoxAvg.Text = "000.0";

                        }
                        break;
                    case (int)FormSort.Map.SHAPE:
                        avgData = (MeasDataClass)XMDdata.measDisplayAvg.Clone();
                        if (svStat.MEAS)
                        {
                            textBoxMin.Text = (sumData[(int)summary.MIN] * myFormCfg.unit.factor[(int)UnitCfgClass.type.FLATNESS] + myFormCfg.unit.offset[(int)UnitCfgClass.type.FLATNESS]).ToString("F4");
                            textBoxMax.Text = (sumData[(int)summary.MAX] * myFormCfg.unit.factor[(int)UnitCfgClass.type.FLATNESS] + myFormCfg.unit.offset[(int)UnitCfgClass.type.FLATNESS]).ToString("F4");
                            textBoxAvg.Text = (sumData[(int)summary.AVG] * myFormCfg.unit.factor[(int)UnitCfgClass.type.FLATNESS] + myFormCfg.unit.offset[(int)UnitCfgClass.type.FLATNESS]).ToString("F4");
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
            catch (Exception exc)
            {
                ExceptionManager.Publish(exc);
                // ErrorForm.AddException(exc, "Measure Form Data");
            }
        }

        private void tChartProfile_DoubleClick(object sender, EventArgs e)
        {
            ChartForm aChartForm = new ChartForm(tChartProfile, myFormCfg);
            aChartForm.Location = Control.MousePosition;
            aChartForm.Visible = true;
        }
        //=========================================================================================================

    }
}