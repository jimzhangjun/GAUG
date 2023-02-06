//=================================================================================================================
//  Project:    RM312/SIPRO SUPERvisor
//  Module:     InfoFormMeasurePos.cs                                                                         
//  Author:     Andrew Powell
//  Date:       21/03/2006
//  
//  Details:    Display form for visualising measurement data
//  
//=================================================================================================================
using System;
using System.Drawing;
using System.Windows.Forms;

using Microsoft.ApplicationBlocks.ExceptionManagement;
using GAUGlib;

namespace GAUGview
{
    public partial class InfoFormMeasure : Form
    {
        //---------------------------------------------------------------------------------------------------------
        // CLASS VARIABLES
        //---------------------------------------------------------------------------------------------------------
 //       private const int ROWNUM = 3;
 //       private const int COLNUM = 6;
        private MeasDataClass avgData = new MeasDataClass();

        private ViewMainForm myOwner = null;
        private TabCfgClass myTabCfg = null;
        private FormCfgClass myFormCfg = null;
        private int tabIndex = -1;
        private int formIndex = -1;
        private int widthCfg;
        private int heightCfg;

        //---------------------------------------------------------------------------------------------------------
        // GLOBAL PROCEDURES
        //---------------------------------------------------------------------------------------------------------
        public InfoFormMeasure(Form OwnerForm, int width, int height, int tindex, int findex)
        {
            tabIndex = tindex;
            formIndex = findex;
            myOwner = OwnerForm as ViewMainForm;
            myTabCfg = ConfigDataClass.view.tabCfg[tabIndex];
            myFormCfg = myTabCfg.formCfg[formIndex];
            widthCfg = width;
            heightCfg = height;

            InitializeComponent();

            //dataGridView1.RowCount = 6;
        }

        //---------------------------------------------------------------------------------------------------------
        // LOCAL PROCEDURES
        //---------------------------------------------------------------------------------------------------------    
        //-- Update Language ----------------------------------------------------------
        private void UpdateLanguage()
        {
            //if (ConfigDataClass.view.language == 0) return;
            // Menu - none

            // Title
            labelText6.Text = FileClass.textItem.GetTextItem(myFormCfg.name, ConfigDataClass.view.language);

            // Label

            // Unit

        }
        //-- Update color and font
        private void UpdateColorAndFont()
        {
            //-- default color and font for lab/unit/text
            setControls(this);

            //-- specific handling

            dataGridView1.BackgroundColor = myFormCfg.formBackColor;
            dataGridView1.BackColor = myFormCfg.txtBackColorRead;
            dataGridView1.ForeColor = myFormCfg.txtFont.color;
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
            labelText6.Left = DISPLAYPARAMS.offsetleft;
            labelText6.Top = DISPLAYPARAMS.offsettop;

            dataGridView1.RowCount = 6;
            dataGridView1.Left = labelText6.Width + DISPLAYPARAMS.offsetleft * 2;
            dataGridView1.Top = DISPLAYPARAMS.offsettop;           
        }
        private void Simulation()
        {
            // Edge position
            for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                dataGridView1.Rows[k].Cells[0].Value = ConfigDataClass.view.edgePos[k];

            // No.
            for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                dataGridView1.Rows[k].Cells[1].Value = k;

            // Drive
            for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                dataGridView1.Rows[k].Cells[2].Value = 2.5 + ConfigDataClass.view.edgePos[k] / 10;

            // CL
            dataGridView1.Rows[0].Cells[3].Value = 2.5;

            // Op
            for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                dataGridView1.Rows[k].Cells[4].Value = 3.1 - ConfigDataClass.view.edgePos[k] / 10;

            // Drive Edge Drop
            for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                dataGridView1.Rows[k].Cells[5].Value = ConfigDataClass.view.edgePos[k] / 10;

            // Op Edge Drop
            for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                dataGridView1.Rows[k].Cells[6].Value = -ConfigDataClass.view.edgePos[k] / 10;

            // Crown
            for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                dataGridView1.Rows[k].Cells[7].Value = ConfigDataClass.view.edgePos[k] / 100;

            // Wedge
            for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                dataGridView1.Rows[k].Cells[8].Value = -ConfigDataClass.view.edgePos[k] / 100;
        }
        //---------------------------------------------------------------------------------------------------------
        // LOCAL EVENTS
        //--------------------------------------------------------------------------------------------------------- 
        private void InfoFormMeasurePos_Load(object sender, EventArgs e)
        {
            UpdateLanguage();
            UpdateColorAndFont();
            UpdateSizeAndLocation();

            timer2.Interval = myFormCfg.timer[1];

            if (myFormCfg.mode==0)
                Simulation();
            else
                timer2.Start();            
        }

        //-- Automated form update --------------------------------------------------------------------------------
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (RemoteInterfaceClass.connected)
                {
                    avgData = RemoteInterfaceClass.XMD.GetDisplayData();
                    SuperStatus svStat = RemoteInterfaceClass.XMD.GetSVStatus();
             
                    if (svStat.MEAS && !Single.IsNaN(avgData.aCLThickness))
                    {
                        // Edge position
                        for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                            dataGridView1.Rows[0].Cells[0].Value = ConfigDataClass.view.edgePos[k];

                        // No.
                        for (int k=0; k<MAXNUM.EDGEPOSITION; ++k)
                            dataGridView1.Rows[0].Cells[1].Value = k;

                        // Drive
                        if (myFormCfg.inverted)
                            for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                                dataGridView1.Rows[0].Cells[2].Value = avgData.aOEThickness[k] * myFormCfg.unit.factor[(int)UnitCfgClass.type.THICK] + myFormCfg.unit.offset[(int)UnitCfgClass.type.THICK];
                        else
                            for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                                dataGridView1.Rows[0].Cells[2].Value = avgData.aBEThickness[k] * myFormCfg.unit.factor[(int)UnitCfgClass.type.THICK] + myFormCfg.unit.offset[(int)UnitCfgClass.type.THICK];

                        // CL
                        dataGridView1.Rows[0].Cells[3].Value = avgData.aCLThickness * myFormCfg.unit.factor[(int)UnitCfgClass.type.THICK] + myFormCfg.unit.offset[(int)UnitCfgClass.type.THICK];

                        // Op
                        if (myFormCfg.inverted)
                            for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                                dataGridView1.Rows[0].Cells[4].Value = avgData.aBEThickness[k] * myFormCfg.unit.factor[(int)UnitCfgClass.type.THICK] + myFormCfg.unit.offset[(int)UnitCfgClass.type.THICK];
                        else
                            for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                                dataGridView1.Rows[0].Cells[4].Value = avgData.aOEThickness[k] * myFormCfg.unit.factor[(int)UnitCfgClass.type.THICK] + myFormCfg.unit.offset[(int)UnitCfgClass.type.THICK];

                        // Drive Edge Drop
                        if (myFormCfg.inverted)
                            for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                                dataGridView1.Rows[0].Cells[5].Value = (avgData.aOEThickness[k] - avgData.aOEThickness[MAXNUM.EDGEPOSITION - 1]) * myFormCfg.unit.factor[(int)UnitCfgClass.type.THICK] + myFormCfg.unit.offset[(int)UnitCfgClass.type.THICK];
                        else
                            for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                                dataGridView1.Rows[0].Cells[5].Value = (avgData.aBEThickness[k] - avgData.aBEThickness[MAXNUM.EDGEPOSITION - 1]) *myFormCfg.unit.factor[(int)UnitCfgClass.type.THICK] + myFormCfg.unit.offset[(int)UnitCfgClass.type.THICK];

                        // Op Edge Drop
                        if (myFormCfg.inverted)
                            for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                                dataGridView1.Rows[0].Cells[6].Value = (avgData.aBEThickness[k] - avgData.aBEThickness[MAXNUM.EDGEPOSITION - 1]) * myFormCfg.unit.factor[(int)UnitCfgClass.type.THICK] + myFormCfg.unit.offset[(int)UnitCfgClass.type.THICK];
                        else
                            for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                                dataGridView1.Rows[0].Cells[6].Value = (avgData.aOEThickness[k] - avgData.aOEThickness[MAXNUM.EDGEPOSITION - 1]) * myFormCfg.unit.factor[(int)UnitCfgClass.type.THICK] + myFormCfg.unit.offset[(int)UnitCfgClass.type.THICK];

                        // Crown
                        for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                            dataGridView1.Rows[0].Cells[7].Value = avgData.aCrown[k] * myFormCfg.unit.factor[(int)UnitCfgClass.type.THICK] + myFormCfg.unit.offset[(int)UnitCfgClass.type.THICK];

                        // Wedge - no need inverted here
                        for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                            dataGridView1.Rows[0].Cells[8].Value = avgData.aWedge[k] * myFormCfg.unit.factor[(int)UnitCfgClass.type.THICK] + myFormCfg.unit.offset[(int)UnitCfgClass.type.THICK];
                    }
                    else
                    {
                        // Edge position
                        for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                            dataGridView1.Rows[0].Cells[0].Value = ConfigDataClass.view.edgePos[k];

                        // No.
                        for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                            dataGridView1.Rows[0].Cells[1].Value = k;

                        // Drive
                        for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                            dataGridView1.Rows[0].Cells[2].Value = "000.0";

                        // CL
                        dataGridView1.Rows[0].Cells[3].Value = "000.0";

                        // Op
                        for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                            dataGridView1.Rows[0].Cells[4].Value = "000.0";

                        // Drive Edge Drop
                        for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                            dataGridView1.Rows[0].Cells[4].Value = "000.0";

                        // Op Edge Drop
                        for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                            dataGridView1.Rows[0].Cells[4].Value = "000.0";

                        // Crown
                        for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                            dataGridView1.Rows[0].Cells[4].Value = "000.0";

                        // Wedge
                        for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                            dataGridView1.Rows[0].Cells[4].Value = "000.0";
                    }
                }
                else
                {
                    // Edge position
                    for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                        dataGridView1.Rows[0].Cells[0].Value = ConfigDataClass.view.edgePos[k];

                    // No.
                    for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                        dataGridView1.Rows[0].Cells[1].Value = k;

                    // Drive
                    for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                        dataGridView1.Rows[0].Cells[2].Value = "000.0";

                    // CL
                    dataGridView1.Rows[0].Cells[3].Value = "000.0";

                    // Op
                    for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                        dataGridView1.Rows[0].Cells[4].Value = "000.0";

                    // Drive Edge Drop
                    for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                        dataGridView1.Rows[0].Cells[4].Value = "000.0";

                    // Op Edge Drop
                    for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                        dataGridView1.Rows[0].Cells[4].Value = "000.0";

                    // Crown
                    for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                        dataGridView1.Rows[0].Cells[4].Value = "000.0";

                    // Wedge
                    for (int k = 0; k < MAXNUM.EDGEPOSITION; ++k)
                        dataGridView1.Rows[0].Cells[4].Value = "000.0";
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.Publish(exc);
                // ErrorForm.AddException(exc, "Measure Form Data");
            }
        }
        //=========================================================================================================
      
    }
}