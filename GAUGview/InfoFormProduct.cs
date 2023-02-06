//=================================================================================================================
//  Project:    RM312/SIPRO SUPERvisor
//  Module:     InfoFormProduct.cs                                                                         
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
    public partial class InfoFormProduct : Form
    {
        //---------------------------------------------------------------------------------------------------------
        // CLASS VARIABLES
        //---------------------------------------------------------------------------------------------------------
        private const int ROWNUM = 8;
 //       private const int COLNUM = 6;

        private ViewMainForm myOwner = null;
        private FormCfgClass myFormCfg = null;
        private int formIndex = -1;
        private int widthCfg;
        private int heightCfg;
        private int offsetCfg;

        private int myCount = 0;
        //---------------------------------------------------------------------------------------------------------
        // GLOBAL PROCEDURES
        //---------------------------------------------------------------------------------------------------------
        public InfoFormProduct(Form OwnerForm, int width, int height, int offset, int index)
        {
            formIndex = index;
            myOwner = OwnerForm as ViewMainForm;
            myFormCfg = ConfigDataClass.view.formCfg[formIndex];
            widthCfg = width;
            heightCfg = height;
            offsetCfg = offset;

            InitializeComponent();
        }
        //---------------------------------------------------------------------------------------------------------
        // LOCAL PROCEDURES
        //---------------------------------------------------------------------------------------------------------    
        //-- Update Language ----------------------------------------------------------
        private void UpdateLanguage()
        {
            //if (ConfigDataClass.view.language == 0) return;
            this.Text = FileClass.textItem.GetTextItem(myFormCfg.name.ToString(), ConfigDataClass.view.language);

            // Menu - none

            // Label
            labelProd0.Text = FileClass.textItem.GetTextItem(labelProd0.Tag.ToString(), ConfigDataClass.view.language);
            labelProd1.Text = FileClass.textItem.GetTextItem(labelProd1.Tag.ToString(), ConfigDataClass.view.language);
            labelProd2.Text = FileClass.textItem.GetTextItem(labelProd2.Tag.ToString(), ConfigDataClass.view.language);
            labelProd3.Text = FileClass.textItem.GetTextItem(labelProd3.Tag.ToString(), ConfigDataClass.view.language);
            labelProd4.Text = FileClass.textItem.GetTextItem(labelProd4.Tag.ToString(), ConfigDataClass.view.language);
            labelProd5.Text = FileClass.textItem.GetTextItem(labelProd5.Tag.ToString(), ConfigDataClass.view.language);
            labelProd6.Text = FileClass.textItem.GetTextItem(labelProd6.Tag.ToString(), ConfigDataClass.view.language);
            labelProd7.Text = FileClass.textItem.GetTextItem(labelProd7.Tag.ToString(), ConfigDataClass.view.language) + " " + myFormCfg.who.index.ToString();

            // Unit
            labelPUnit0.Text = UnitCfgClass.name[int.Parse(labelPUnit0.Tag.ToString())][myFormCfg.unit.style[int.Parse(labelPUnit0.Tag.ToString())]];
            labelPUnit1.Text = UnitCfgClass.name[int.Parse(labelPUnit1.Tag.ToString())][myFormCfg.unit.style[int.Parse(labelPUnit1.Tag.ToString())]];
            labelPUnit2.Text = UnitCfgClass.name[int.Parse(labelPUnit2.Tag.ToString())][myFormCfg.unit.style[int.Parse(labelPUnit2.Tag.ToString())]];
            labelPUnit3.Text = UnitCfgClass.name[int.Parse(labelPUnit3.Tag.ToString())][myFormCfg.unit.style[int.Parse(labelPUnit3.Tag.ToString())]];
            labelPUnit4.Text = UnitCfgClass.name[int.Parse(labelPUnit4.Tag.ToString())][myFormCfg.unit.style[int.Parse(labelPUnit4.Tag.ToString())]];
            labelPUnit5.Text = UnitCfgClass.name[int.Parse(labelPUnit5.Tag.ToString())][myFormCfg.unit.style[int.Parse(labelPUnit5.Tag.ToString())]];
            labelPUnit6.Text = UnitCfgClass.name[int.Parse(labelPUnit6.Tag.ToString())][myFormCfg.unit.style[int.Parse(labelPUnit6.Tag.ToString())]];
        }

        //-- Update color and font
        private void UpdateColorAndFont()
        {
            //-- default color and font for lab/unit/text
            setControls(this);
        }
        private void setControls(Control con)
        {
            if (con.BackColor != Color.Transparent) con.BackColor = myFormCfg.formBackColor;
            con.Font = new Font(myFormCfg.labFont.name, myFormCfg.labFont.size, myFormCfg.labFont.style);
            con.ForeColor = myFormCfg.labFont.color;

            foreach (Control cons in con.Controls)
                setControls(cons);

            // Speical handling
            textBoxCoilID.BackColor = myFormCfg.txtBackColorRead;
            textBoxNomThick.BackColor = myFormCfg.txtBackColorRead;
            textBoxPTol.BackColor = myFormCfg.txtBackColorRead;
            textBoxMTol.BackColor = myFormCfg.txtBackColorRead;
            textBoxAlloyfactor.BackColor = myFormCfg.txtBackColorRead;           
            textBoxPassnumber.BackColor = myFormCfg.txtBackColorRead;
            textBoxLastpass.BackColor = myFormCfg.txtBackColorRead;
        }

        //-- Update the size and location
        private void UpdateSizeAndLocation()
        {
            this.Left = (int)(myOwner.Width * (myFormCfg.location.left / widthCfg));
            this.Top = (int)((myOwner.Height - DISPLAYPARAMS.heighttab * 3) * (myFormCfg.location.top / heightCfg)) + offsetCfg;
            this.Width = (int)(myOwner.Width * (myFormCfg.location.width / widthCfg));
            this.Height = (int)((myOwner.Height - DISPLAYPARAMS.heighttab * 3) * (myFormCfg.location.height / heightCfg));

            // 1 cols of product 
            // 0: space 1: Label  2: TextBox 3: Unit
            int cols = myFormCfg.widthscale[0] + myFormCfg.widthscale[1] + myFormCfg.widthscale[2];
            int width = this.Width / cols; 
            int height = this.Height / ROWNUM;
            int textheight = height - DISPLAYPARAMS.offsettop * 2;

            //-- Col 1 ---------------------------------------------------------
            // Gauge
            labelProd7.Left = DISPLAYPARAMS.offsetleft;
            labelProd7.Top = DISPLAYPARAMS.offsettop;
            labelProd7.Height = textheight;

            // Coil ID
            labelProd0.Left = DISPLAYPARAMS.offsetleft;
            labelProd0.Top = height + DISPLAYPARAMS.offsettop;
            labelProd0.Height = textheight;

            textBoxCoilID.Left = width * myFormCfg.widthscale[1] + DISPLAYPARAMS.offsetleft;
            textBoxCoilID.Top = height + DISPLAYPARAMS.offsettop;
            textBoxCoilID.Width = width * myFormCfg.widthscale[2] - DISPLAYPARAMS.offsetleft * 2;
            textBoxCoilID.Height = textheight;

            labelPUnit0.Left = width * (myFormCfg.widthscale[1] + myFormCfg.widthscale[2] * 2) + DISPLAYPARAMS.offsetleft;
            labelPUnit0.Top = height + DISPLAYPARAMS.offsettop;
            labelPUnit0.Height = textheight;

            // Nominal Thickness
            labelProd1.Left = DISPLAYPARAMS.offsetleft;
            labelProd1.Top = height * 2 + DISPLAYPARAMS.offsettop;
            labelProd1.Height = textheight;

            textBoxNomThick.Left = width * myFormCfg.widthscale[1] + DISPLAYPARAMS.offsetleft;
            textBoxNomThick.Top = height * 2 + DISPLAYPARAMS.offsettop;
            textBoxNomThick.Width = width * myFormCfg.widthscale[2] - DISPLAYPARAMS.offsetleft * 2;
            textBoxNomThick.Height = textheight;

            labelPUnit1.Left = width * (myFormCfg.widthscale[1] + myFormCfg.widthscale[2]) + DISPLAYPARAMS.offsetleft;
            labelPUnit1.Top = height * 2 + DISPLAYPARAMS.offsettop;
            labelPUnit1.Height = textheight;

            // Thick Limit
            labelProd2.Left = DISPLAYPARAMS.offsetleft;
            labelProd2.Top = height * 3 + DISPLAYPARAMS.offsettop;
            labelProd2.Height = textheight;

            textBoxPTol.Left = width * myFormCfg.widthscale[1] + DISPLAYPARAMS.offsetleft;
            textBoxPTol.Top = height * 3 + DISPLAYPARAMS.offsettop;
            textBoxPTol.Width = width * myFormCfg.widthscale[2] - DISPLAYPARAMS.offsetleft * 2;
            textBoxPTol.Height = textheight;

            labelPUnit2.Left = width * (myFormCfg.widthscale[1] + myFormCfg.widthscale[2]) + DISPLAYPARAMS.offsetleft;
            labelPUnit2.Top = height * 3 + DISPLAYPARAMS.offsettop;
            labelPUnit2.Height = textheight;

            // Thin Limit
            labelProd3.Left = DISPLAYPARAMS.offsetleft;
            labelProd3.Top = height * 4 + DISPLAYPARAMS.offsettop;
            labelProd3.Height = textheight;

            textBoxMTol.Left = width * myFormCfg.widthscale[1] + DISPLAYPARAMS.offsetleft;
            textBoxMTol.Top = height * 4 + DISPLAYPARAMS.offsettop;
            textBoxMTol.Width = width * myFormCfg.widthscale[2] - DISPLAYPARAMS.offsetleft * 2;
            textBoxMTol.Height = textheight;

            labelPUnit3.Left = width * (myFormCfg.widthscale[1] + myFormCfg.widthscale[2]) + DISPLAYPARAMS.offsetleft;
            labelPUnit3.Top = height * 4 + DISPLAYPARAMS.offsettop;
            labelPUnit3.Height = textheight;

            // Alloy factor
            labelProd4.Left = DISPLAYPARAMS.offsetleft;
            labelProd4.Top = height * 5 + DISPLAYPARAMS.offsettop;
            labelProd4.Height = textheight;

            textBoxAlloyfactor.Left = width * myFormCfg.widthscale[1] + DISPLAYPARAMS.offsetleft;
            textBoxAlloyfactor.Top = height * 5 + DISPLAYPARAMS.offsettop;
            textBoxAlloyfactor.Width = width * myFormCfg.widthscale[2] - DISPLAYPARAMS.offsetleft * 2;
            textBoxAlloyfactor.Height = textheight;

            labelPUnit4.Left = width * (myFormCfg.widthscale[1] + myFormCfg.widthscale[2]) + DISPLAYPARAMS.offsetleft;
            labelPUnit4.Top = height * 5 + DISPLAYPARAMS.offsettop;
            labelPUnit4.Height = textheight;

            // Pass number
            labelProd5.Left = DISPLAYPARAMS.offsetleft;
            labelProd5.Top = height * 6 + DISPLAYPARAMS.offsettop;
            labelProd5.Height = textheight;

            textBoxPassnumber.Left = width * myFormCfg.widthscale[1] + DISPLAYPARAMS.offsetleft;
            textBoxPassnumber.Top = height * 6 + DISPLAYPARAMS.offsettop;
            textBoxPassnumber.Width = width * myFormCfg.widthscale[2] - DISPLAYPARAMS.offsetleft * 2;
            textBoxPassnumber.Height = textheight;

            labelPUnit5.Left = width * (myFormCfg.widthscale[1] + myFormCfg.widthscale[2]) + DISPLAYPARAMS.offsetleft;
            labelPUnit5.Top = height * 6 + DISPLAYPARAMS.offsettop;
            labelPUnit5.Height = textheight;

            // Last pass
            labelProd6.Left = DISPLAYPARAMS.offsetleft;
            labelProd6.Top = height * 7 + DISPLAYPARAMS.offsettop;
            labelProd6.Height = textheight;

            textBoxLastpass.Left = width * myFormCfg.widthscale[1] + DISPLAYPARAMS.offsetleft;
            textBoxLastpass.Top = height * 7 + DISPLAYPARAMS.offsettop;
            textBoxLastpass.Width = width * myFormCfg.widthscale[2] - DISPLAYPARAMS.offsetleft * 2;
            textBoxLastpass.Height = textheight;

            labelPUnit6.Left = width * (myFormCfg.widthscale[1] + myFormCfg.widthscale[2]) + DISPLAYPARAMS.offsetleft;
            labelPUnit6.Top = height * 7 + DISPLAYPARAMS.offsettop;
            labelPUnit6.Height = textheight;
        }
        //-- Demo -------------------------------------------------------------------------------------------
        private void Demo()
        {
            textBoxCoilID.Text = "CoilID-1234";
            textBoxNomThick.Text = "2.0";
            textBoxPTol.Text = "0.01";
            textBoxMTol.Text = "0.01";
            textBoxAlloyfactor.Text = "1.0";
            textBoxPassnumber.Text = "2";
            textBoxLastpass.Text = "0";
        }
        //---------------------------------------------------------------------------------------------------------
        // LOCAL EVENTS
        //--------------------------------------------------------------------------------------------------------- 
        private void InfoFormProduct_Load(object sender, EventArgs e)
        {
            UpdateLanguage();
            UpdateColorAndFont();
            UpdateSizeAndLocation();

            timer2.Interval = myFormCfg.timer[1];            

            switch (ConfigDataClass.view.mode)
            {
                case (int)DataSort.Source.REMOTING:
                case (int)DataSort.Source.M1COM:
                case (int)DataSort.Source.CSVFILE:
                    timer2.Start();
                    break;
                default:
                    Demo();
                    break;
            }
        }
        //-- Automated form update --------------------------------------------------------------------------------
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (RemoteInterfaceClass.connected)
                {
                    switch (ConfigDataClass.view.mode)
                    {
                        case (int)DataSort.Source.REMOTING:  //-- for SIPRO via remoting
                            break;
                        case (int)DataSort.Source.M1COM:
                            {
                                UInt16 index = 0;
                                RemoteInterfaceClass.XMD.IsModifiedData(myFormCfg.sourceData[0].type, ref index, myFormCfg.sourceData[0].name, 0, 0, 0.0f);
                                textBoxCoilID.Text = RemoteInterfaceClass.XMD.GetStringData(index).ToString();
                                RemoteInterfaceClass.XMD.IsModifiedData(myFormCfg.sourceData[1].type, ref index, myFormCfg.sourceData[1].name, 0, 0, 0.0f);
                                textBoxNomThick.Text = RemoteInterfaceClass.XMD.GetFloatData(index).ToString("F4");
                                RemoteInterfaceClass.XMD.IsModifiedData(myFormCfg.sourceData[2].type, ref index, myFormCfg.sourceData[2].name, 0, 0, 0.0f);
                                textBoxPTol.Text = RemoteInterfaceClass.XMD.GetFloatData(index).ToString("F4");
                                RemoteInterfaceClass.XMD.IsModifiedData(myFormCfg.sourceData[3].type, ref index, myFormCfg.sourceData[3].name, 0, 0, 0.0f);
                                textBoxMTol.Text = RemoteInterfaceClass.XMD.GetFloatData(index).ToString("F4");
                                RemoteInterfaceClass.XMD.IsModifiedData(myFormCfg.sourceData[4].type, ref index, myFormCfg.sourceData[4].name, 0, 0, 0.0f);
                                textBoxAlloyfactor.Text = RemoteInterfaceClass.XMD.GetFloatArrayData(index, myFormCfg.sourceData[4].aindex).ToString("F4");
                                RemoteInterfaceClass.XMD.IsModifiedData(myFormCfg.sourceData[5].type, ref index, myFormCfg.sourceData[5].name, 0, 0, 0.0f);
                                textBoxPassnumber.Text = RemoteInterfaceClass.XMD.GetIntegerData(index).ToString();
                                RemoteInterfaceClass.XMD.IsModifiedData(myFormCfg.sourceData[6].type, ref index, myFormCfg.sourceData[6].name, 0, 0, 0.0f);
                                textBoxLastpass.Text = RemoteInterfaceClass.XMD.GetIntegerData(index).ToString();
                            }
                            break;
                        case (int)DataSort.Source.CSVFILE:
                            {
                                int curCount = RemoteInterfaceClass.XMD.GetUpdateCoilCount(myFormCfg.who.index);
                                if (myCount != curCount)
                                { 
                                    CoilDataClass coilData = RemoteInterfaceClass.XMD.GetCoilData(myFormCfg.who.index);
                                    textBoxCoilID.Text = coilData.product.coilid;
                                    textBoxNomThick.Text = coilData.product.setpoint[0].ToString("F4");
                                    textBoxPTol.Text = coilData.product.ptol[0].ToString("F4");
                                    textBoxMTol.Text = coilData.product.mtol[0].ToString("F4");
                                    textBoxAlloyfactor.Text = coilData.product.factor[0].ToString("F4");
                                    textBoxPassnumber.Text = coilData.product.passNumber.ToString("F0");
                                    textBoxLastpass.Text = coilData.product.lastPass.ToString("F0");

                                    myCount = curCount;
                                }
                            }
                            break;
                    }
                }
                else
                {
                    textBoxCoilID.Text = " ";
                    textBoxNomThick.Text = "000.0";
                    textBoxPTol.Text = "000.0";
                    textBoxMTol.Text = "000.0";
                    textBoxAlloyfactor.Text = "1.000";
                    textBoxPassnumber.Text = "1";
                    textBoxLastpass.Text = "0";
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.Publish(exc);
            }
        }
        //=========================================================================================================

    }
}