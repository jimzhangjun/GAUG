//=================================================================================================================
//  Project:    RM312/SIPRO SUPERvisor
//  Module:     MeasureFormTrendTime.cs                                                                         
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
using GAUGlib;

namespace GAUGview
{
    public partial class MeasureFormTrendTime : Form
    {
        //---------------------------------------------------------------------------------------------------------
        // CLASS VARIABLES
        //---------------------------------------------------------------------------------------------------------
        private const int CLOFFSET = 300;
        private const int RES = 5;
        private const int NUMDATA = 8;
        private int dummyLength=0;

        private ViewMainForm myOwner = null;
        private TabCfgClass myTabCfg = null;
        private FormCfgClass myFormCfg = null;
        private int widthCfg;
        private int heightCfg;
        private int tabIndex = -1;
        private int formIndex = -1;

        private static string[] FileName = { "ANALOG.csv" };
        private DataTable aDataTable = new DataTable();
        private enum tabCols
        {
            Time,
            CoilID,
            S1Tube,
            S2Tube,
            S1Air,
            S2Air,
            Det,
            DewPt,
            Speed,
            MillPyro,
            Angle,
            AGT,
            NRS1OE,
            NRS1BE,
            NRS2OE,
            NRS2BE,
            NRIndex
        }
        //---------------------------------------------------------------------------------------------------------
        // GLOBAL PROCEDURES
        //---------------------------------------------------------------------------------------------------------
        public MeasureFormTrendTime(Form OwnerForm, int width, int height, int tindex, int findex)
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
            // Title
           // labelText6.Text = FileClass.textItem.GetTextItem(myFormCfg.name, ConfigDataClass.view.language);

            //tChartProfile.Axes.Bottom.Title.Text = FileClass.textItem.GetTextItem("TX_TIME", ConfigDataClass.view.language);
            //this.fastLine1.Title = FileClass.textItem.GetTextItem("TX_THICKNESS", ConfigDataClass.view.language);   
        }

        private void UpdateColorAndFont()
        {
            //-- default color and font for lab/unit/text
            setControls(this);

            // Speical handling
            tChartProfile.BackColor = myFormCfg.chartBackColor;
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

            comboBox1.Left = this.Width - comboBox1.Width - DISPLAYPARAMS.offsetleft;
            comboBox1.Top = DISPLAYPARAMS.offsettop;
            comboBox1.SelectedIndex = myFormCfg.who.type - 100;

            // Tee Chart
            tChartProfile.Top = 0;
            tChartProfile.Left = 0;
            tChartProfile.Width = this.Width - comboBox1.Width - DISPLAYPARAMS.offsetleft * 2;
            tChartProfile.Height = this.Height;
            tChartProfile.Axes.Bottom.AutomaticMaximum = false;
            tChartProfile.Axes.Bottom.Maximum = 100;
        }

        private void RescaleTimeAxis(Steema.TeeChart.Axis Axis, bool Auto)
        {
            Axis.AutomaticMinimum = Auto;
            Axis.AutomaticMaximum = Auto;
            Axis.Minimum = dummyLength - TIME.FiveMins;
            Axis.Maximum = dummyLength + TIME.OneMin;
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
                    if (NumOfData > MAXNUM.ANALOG) NumOfData = MAXNUM.ANALOG;

                    fastLine1.Clear();
                    switch (myFormCfg.who.type)
                    {
                        case (int)FormSort.TrendTime.Ai.SPEED:                            

                            for (int i = 0; i < NumOfData; ++i)
                            {
                                float data = Convert.ToSingle(aDataTable.Rows[i][(int)tabCols.Speed]);

                                fastLine1.Add(i, data);
                            }
                            break;
                        case (int)FormSort.TrendTime.Ai.TUBETEMP1:
                            for (int i = 0; i < NumOfData; ++i)
                            {
                                float data = Convert.ToSingle(aDataTable.Rows[i][(int)tabCols.S1Tube]);

                                fastLine1.Add(i, data);
                            }
                            break;
                        case (int)FormSort.TrendTime.Ai.TUBETEMP2:
                            for (int i = 0; i < NumOfData; ++i)
                            {
                                float data = Convert.ToSingle(aDataTable.Rows[i][(int)tabCols.S2Tube]);

                                fastLine1.Add(i, data);
                            }
                            break;
                        case (int)FormSort.TrendTime.Ai.TOPARMTEMP1:
                            for (int i = 0; i < NumOfData; ++i)
                            {
                                float data = Convert.ToSingle(aDataTable.Rows[i][(int)tabCols.S1Air]);

                                fastLine1.Add(i, data);
                            }
                            break;
                        case (int)FormSort.TrendTime.Ai.TOPARMTEMP2:
                            for (int i = 0; i < NumOfData; ++i)
                            {
                                float data = Convert.ToSingle(aDataTable.Rows[i][(int)tabCols.S2Air]);

                                fastLine1.Add(i, data);
                            }
                            break;
                        case (int)FormSort.TrendTime.Ai.MILLSTRIPANGLE:
                            for (int i = 0; i < NumOfData; ++i)
                            {
                                float data = Convert.ToSingle(aDataTable.Rows[i][(int)tabCols.Angle]);

                                fastLine1.Add(i, data);
                            }
                            break;
                        case (int)FormSort.TrendTime.Ai.DEWPOINT:
                            for (int i = 0; i < NumOfData; ++i)
                            {
                                float data = Convert.ToSingle(aDataTable.Rows[i][(int)tabCols.DewPt]);

                                fastLine1.Add(i, data);
                            }
                            break;
                        case (int)FormSort.TrendTime.Ai.MILLPYROTEMP:
                            for (int i = 0; i < NumOfData; ++i)
                            {
                                float data = Convert.ToSingle(aDataTable.Rows[i][(int)tabCols.MillPyro]);

                                fastLine1.Add(i, data);
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
        
        //---------------------------------------------------------------------------------------------------------
        // LOCAL EVENTS
        //--------------------------------------------------------------------------------------------------------- 
        private void MeasureFormTrendTime_Load(object sender, EventArgs e)
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
                //SuperStatus svStat = RemoteInterfaceClass.XMD.GetSVStatus();
                AnalogInputClass aInputs = RemoteInterfaceClass.XMD.GetAnalogInputs();
                //StripLength svLength = RemoteInterfaceClass.XMD.GetStripLength();

                float data = 0;
                switch (myFormCfg.who.type)
                {
                    case (int)FormSort.TrendTime.Ai.SPEED:
                        data = aInputs.aStripSpeed;
                        break;
                    case (int)FormSort.TrendTime.Ai.AGT:
                        data = aInputs.aAGT;
                        break;
                    case (int)FormSort.TrendTime.Ai.DETTEMP:
                        data = aInputs.aDetTemp;
                        break;
                    case (int)FormSort.TrendTime.Ai.DEWPOINT:
                        data = aInputs.aDewPoint;
                        break;
                    case (int)FormSort.TrendTime.Ai.TUBETEMP1:
                        data = aInputs.aTubeTemp1;
                        break;
                    case (int)FormSort.TrendTime.Ai.TUBETEMP2:
                        data = aInputs.aTubeTemp2;
                        break;
                    case (int)FormSort.TrendTime.Ai.TOPARMTEMP1:
                        data = aInputs.aToparmTemp1;
                        break;
                    case (int)FormSort.TrendTime.Ai.TOPARMTEMP2:
                        data = aInputs.aToparmTemp2;
                        break;
                    case (int)FormSort.TrendTime.Ai.ACTS1KVS:
                       // summaryData[(int)summary.ACT] = SourceDataClass.Act.S1kvs;
                        break;
                    case (int)FormSort.TrendTime.Ai.ACTS1MAS:
                      //  summaryData[(int)summary.ACT] = aInputs.aTubeTemp1;
                        break;
                    case (int)FormSort.TrendTime.Ai.ACTS2KVS:
                      //  summaryData[(int)summary.ACT] = aInputs.aTubeTemp1;
                        break;
                    case (int)FormSort.TrendTime.Ai.ACTS2MAS:
                      //  summaryData[(int)summary.ACT] = aInputs.aTubeTemp1;
                        break;
                    case (int)FormSort.TrendTime.Ai.MILLPYROTEMP:
                        data = aInputs.aMillPyroTemp;
                        break;
                    case (int)FormSort.TrendTime.Ai.MILLSTRIPANGLE:
                        data = aInputs.aMillStripAngle;
                        break;
                    case (int)FormSort.TrendTime.Ai.MILLAI3:
                        data = aInputs.aMillAI3;
                        break;
                    case (int)FormSort.TrendTime.Ai.MILLAI4:
                        data = aInputs.aMillAI4;
                        break;
                }
                fastLine1.Add(dummyLength, data);
                dummyLength += 1;

                if ((dummyLength >= TIME.FiveMins) && (dummyLength % TIME.OneMin == 0))
                {
                    RescaleTimeAxis(tChartProfile.Axes.Bottom, false);
                }

                if (dummyLength >= TIME.OneHour)
                {
                    dummyLength = 0;
                    tChartProfile.Clear();
                    RescaleTimeAxis(tChartProfile.Axes.Bottom, true);
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.Publish(exc);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            // data updates
            try
            {
                if (!RemoteInterfaceClass.connected) return;
                
                AnalogInputClass aInputs = RemoteInterfaceClass.XMD.GetAnalogInputs();
 
            }
            catch (Exception exc)
            {
                ExceptionManager.Publish(exc);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            myFormCfg.who.type = comboBox1.SelectedIndex + 100;
            if (myFormCfg.mode==0)
                Simulation();
            else
            {
                timer1.Stop();
                fastLine1.Clear();
                timer1.Start();
            }
        }
        //=========================================================================================================

    }
}