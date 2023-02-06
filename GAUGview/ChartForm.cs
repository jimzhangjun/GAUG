//========================================================================
//  Project:    RM312/SIPRO SUPERvisor
//  Module:     ChartForm.cs                                                                         
//  Author:     Andrew Powell
//  Date:       19/04/2006
//  
//  Details:    Allows runtime setting of useful T-Chart attributes
//  
//========================================================================
using System;
using System.Drawing;
using System.Windows.Forms;
using Steema.TeeChart;
using GAUGlib;

namespace GAUGview
{
    public partial class ChartForm : Form
    {
        //---------------------------------------------------------------------------------------------------------
        // CLASS VARIABLES
        //---------------------------------------------------------------------------------------------------------
        private TChart myChart;
        private FormCfgClass myFormCfg;

        //---------------------------------------------------------------------------------------------------------
        // GLOBAL PROCEDURES
        //---------------------------------------------------------------------------------------------------------
        public ChartForm()
        {
            InitializeComponent();
        }
        public ChartForm(TChart aChart, FormCfgClass aFormCfg)
        {
            myChart = aChart;
            myFormCfg = aFormCfg;
     
            InitializeComponent();
            //this.Text = myChart.Header.Lines[0];
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
            labelUnit1.Text = UnitCfgClass.name[unitType][myFormCfg.unit.style[0]];

            // GroupBox
            groupBox1.Text = FileClass.textItem.GetTextItem(groupBox1.Tag.ToString(), ConfigDataClass.view.language);
            groupBox2.Text = FileClass.textItem.GetTextItem(groupBox2.Tag.ToString(), ConfigDataClass.view.language);
            groupBox3.Text = FileClass.textItem.GetTextItem(groupBox3.Tag.ToString(), ConfigDataClass.view.language);
            groupBox4.Text = FileClass.textItem.GetTextItem(groupBox4.Tag.ToString(), ConfigDataClass.view.language);

            // Button
            buttonOK.Text = FileClass.textItem.GetTextItem(buttonOK.Tag.ToString(), ConfigDataClass.view.language);
            buttonCancel.Text = FileClass.textItem.GetTextItem(buttonCancel.Tag.ToString(), ConfigDataClass.view.language);

            // Label
            labelText1.Text = FileClass.textItem.GetTextItem(labelText1.Tag.ToString(), ConfigDataClass.view.language);
            labelText2.Text = FileClass.textItem.GetTextItem(labelText2.Tag.ToString(), ConfigDataClass.view.language);
            labelText3.Text = FileClass.textItem.GetTextItem(labelText3.Tag.ToString(), ConfigDataClass.view.language);
            labelText4.Text = FileClass.textItem.GetTextItem(labelText4.Tag.ToString(), ConfigDataClass.view.language);
            labelText5.Text = FileClass.textItem.GetTextItem(labelText5.Tag.ToString(), ConfigDataClass.view.language);
            labelText6.Text = FileClass.textItem.GetTextItem(labelText6.Tag.ToString(), ConfigDataClass.view.language);
            labelText7.Text = FileClass.textItem.GetTextItem(labelText7.Tag.ToString(), ConfigDataClass.view.language);
            labelText8.Text = FileClass.textItem.GetTextItem(labelText8.Tag.ToString(), ConfigDataClass.view.language);

            // CheckBox
            checkBoxXMin.Text = FileClass.textItem.GetTextItem(checkBoxXMin.Tag.ToString(), ConfigDataClass.view.language);
            checkBoxXMax.Text = FileClass.textItem.GetTextItem(checkBoxXMax.Tag.ToString(), ConfigDataClass.view.language);
            checkBoxYMin.Text = FileClass.textItem.GetTextItem(checkBoxYMin.Tag.ToString(), ConfigDataClass.view.language);
            checkBoxYMax.Text = FileClass.textItem.GetTextItem(checkBoxYMax.Tag.ToString(), ConfigDataClass.view.language);

            // Label
            radioButtonXAuto.Text = FileClass.textItem.GetTextItem(radioButtonXAuto.Tag.ToString(), ConfigDataClass.view.language);
            radioButtonXManual.Text = FileClass.textItem.GetTextItem(radioButtonXManual.Tag.ToString(), ConfigDataClass.view.language);
            radioButtonYAuto.Text = FileClass.textItem.GetTextItem(radioButtonYAuto.Tag.ToString(), ConfigDataClass.view.language);
            radioButtonYManual.Text = FileClass.textItem.GetTextItem(radioButtonYManual.Tag.ToString(), ConfigDataClass.view.language);
            radioButtonYAbs.Text = FileClass.textItem.GetTextItem(radioButtonYAbs.Tag.ToString(), ConfigDataClass.view.language);
            radioButtonYPerc.Text = FileClass.textItem.GetTextItem(radioButtonYPerc.Tag.ToString(), ConfigDataClass.view.language);
        }
        //-- Update color and font
        private void UpdateColorAndFont()
        {
            //-- default color and font for lab/unit/text
            setControls(this);

            // Speical handling        
            textBoxXManMin.BackColor = myFormCfg.txtBackColorWrite;
            textBoxXManMax.BackColor = myFormCfg.txtBackColorWrite;
            textBoxXInc.BackColor = myFormCfg.txtBackColorWrite;

            textBoxYManMin.BackColor = myFormCfg.txtBackColorWrite;
            textBoxYManMax.BackColor = myFormCfg.txtBackColorWrite;
            textBoxYInc.BackColor = myFormCfg.txtBackColorWrite;

            textBoxAbsMin.BackColor = myFormCfg.txtBackColorWrite;
            textBoxAbsMax.BackColor = myFormCfg.txtBackColorWrite;

            textBoxPercMin.BackColor = myFormCfg.txtBackColorWrite;
            textBoxPercMax.BackColor = myFormCfg.txtBackColorWrite;       
        }

        private void setControls(Control con)
        {
            if (con.BackColor != Color.Transparent) con.BackColor = myFormCfg.formBackColor;
            con.Font = new Font(myFormCfg.labFont.name, myFormCfg.labFont.size, myFormCfg.labFont.style);
            con.ForeColor = myFormCfg.labFont.color;

            foreach (Control cons in con.Controls)
                setControls(cons);
        }

        private void UpdateValue()
        {
            switch (myFormCfg.xScale.mode)
            {
                case SCALEMODE.MANUAL:
                    radioButtonXManual.Checked = true;
                    break;
                default:
                    radioButtonXAuto.Checked = true;
                    break;
            }

            switch (myFormCfg.yScale.mode)
            {
                case SCALEMODE.MANUAL:
                    radioButtonYManual.Checked = true;
                    break;
                case SCALEMODE.ABS:
                    radioButtonYAbs.Checked = true;
                    break;
                case SCALEMODE.PERC:
                    radioButtonYPerc.Checked = true;
                    break;
                default:
                    radioButtonYAuto.Checked = true;
                    break;
            }
        }
        //---------------------------------------------------------------------------------------------------------
        // LOCAL EVENTS
        //--------------------------------------------------------------------------------------------------------- 
        //Accept changes and close dialog
        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonXManual.Checked)
                {
                    myFormCfg.xScale.mode = SCALEMODE.MANUAL;

                    myChart.Axes.Bottom.Minimum = Single.Parse(textBoxXManMin.Text);
                    myChart.Axes.Bottom.Maximum = Single.Parse(textBoxXManMax.Text);
                    myChart.Axes.Bottom.AutomaticMinimum = checkBoxXMin.Checked;
                    myChart.Axes.Bottom.AutomaticMaximum = checkBoxXMax.Checked;
                    myChart.Axes.Bottom.Increment = Single.Parse(textBoxXInc.Text);
                }
                else
                    myFormCfg.xScale.mode = SCALEMODE.AUTO;

                if (radioButtonYManual.Checked)
                {
                    myFormCfg.yScale.mode = SCALEMODE.MANUAL;

                    myChart.Axes.Left.Minimum = Single.Parse(textBoxYManMin.Text);
                    myChart.Axes.Left.Maximum = Single.Parse(textBoxYManMax.Text);
                    myChart.Axes.Left.AutomaticMinimum = checkBoxYMin.Checked;
                    myChart.Axes.Left.AutomaticMaximum = checkBoxYMax.Checked;
                    myChart.Axes.Left.Increment = Single.Parse(textBoxYInc.Text);
                }
                else if (radioButtonYAbs.Checked)
                {
                    myFormCfg.yScale.mode = SCALEMODE.ABS;

                    myFormCfg.yScale.lowerlimit = Single.Parse(textBoxAbsMin.Text);
                    myFormCfg.yScale.upperlimit = Single.Parse(textBoxAbsMax.Text);

                    Single setpoint = 0;
                    if (RemoteInterfaceClass.connected)
                    {
                        SetupData setCurrent = RemoteInterfaceClass.XMD.GetCurrentSetup();
                        setpoint = setCurrent.sNomThick;
                    }

                    myChart.Axes.Left.Minimum = setpoint - myFormCfg.yScale.lowerlimit;
                    myChart.Axes.Left.Maximum = setpoint + myFormCfg.yScale.upperlimit;                    
                }
                else if (radioButtonYPerc.Checked)
                {
                    myFormCfg.yScale.mode = SCALEMODE.PERC;

                    myFormCfg.yScale.lowerlimit = Single.Parse(textBoxPercMin.Text);
                    myFormCfg.yScale.upperlimit = Single.Parse(textBoxPercMax.Text);

                    Single setpoint = 0;
                    if (RemoteInterfaceClass.connected)
                    {
                        SetupData setCurrent = RemoteInterfaceClass.XMD.GetCurrentSetup();
                        setpoint = setCurrent.sNomThick;
                    }

                    myChart.Axes.Left.Minimum = setpoint * (1 - myFormCfg.yScale.lowerlimit / 100);
                    myChart.Axes.Left.Maximum = setpoint * (1 + myFormCfg.yScale.upperlimit / 100);
                }
                else
                    myFormCfg.yScale.mode = SCALEMODE.AUTO;
            }

            catch (Exception)
            {
                MessageBox.Show("Data rejected - incorrectly formatted", "Data entry error");
                //return;
            }

            this.Close();
        }

        //Cancel changes and close dialog
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Deselect auto axis scaling on manual change
        private void textBoxXManMin_TextChanged(object sender, EventArgs e)
        {
            checkBoxXMin.Checked = false;
        }
        private void textBoxXManMax_TextChanged(object sender, EventArgs e)
        {
            checkBoxXMax.Checked = false;
        }
        private void textBoxYManMin_TextChanged(object sender, EventArgs e)
        {
            checkBoxYMin.Checked = false;
        }
        private void textBoxYManMax_TextChanged(object sender, EventArgs e)
        {
            checkBoxYMax.Checked = false;
        }

        //Check and prompt for numeric input
        private void textBoxXManMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar) >= 65 && (e.KeyChar) <= 90) || ((e.KeyChar) >= 97 && (e.KeyChar) <= 122))
            {
                MessageBox.Show("Enter numeric values only", "Data entry error"); textBoxXInc.Focus();
            }
        }
        private void textBoxXManMax_KeyPress(object sender, KeyPressEventArgs e)
        {
          if (((e.KeyChar) >= 65 && (e.KeyChar) <= 90) || ((e.KeyChar) >= 97 && (e.KeyChar) <= 122))
            {
                MessageBox.Show("Enter numeric values only", "Data entry error"); textBoxXInc.Focus();
            }
        }
        private void textBoxXInc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar) >= 65 && (e.KeyChar) <= 90) || ((e.KeyChar) >= 97 && (e.KeyChar) <= 122))
            {
                MessageBox.Show("Enter numeric values only", "Data entry error"); textBoxXInc.Focus();
            }
        }
        private void textBoxYManMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar) >= 65 && (e.KeyChar) <= 90) || ((e.KeyChar) >= 97 && (e.KeyChar) <= 122))
            {
                MessageBox.Show("Enter numeric values only", "Data entry error"); textBoxYInc.Focus();
            }
        }
        private void textBoxYManMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar) >= 65 && (e.KeyChar) <= 90) || ((e.KeyChar) >= 97 && (e.KeyChar) <= 122))
            {
                MessageBox.Show("Enter numeric values only", "Data entry error"); textBoxYInc.Focus();
            }
        }
        private void textBoxYInc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar) >= 65 && (e.KeyChar) <= 90) || ((e.KeyChar) >= 97 && (e.KeyChar) <= 122))
            {
                MessageBox.Show("Enter numeric values only", "Data entry error"); textBoxYInc.Focus();
            }
        }

        private void ChartForm_Load(object sender, EventArgs e)
        {
            UpdateLanguage();
            UpdateColorAndFont();
            UpdateValue();
        }

        private void radioButtonXAuto_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
        }

        private void radioButtonXManual_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;

            textBoxXManMin.Text = myChart.Axes.Bottom.Minimum.ToString("F2");
            textBoxXManMax.Text = myChart.Axes.Bottom.Maximum.ToString("F2");
            textBoxXInc.Text = myChart.Axes.Bottom.Increment.ToString("F2");
            checkBoxXMin.Checked = myChart.Axes.Bottom.AutomaticMinimum;
            checkBoxXMax.Checked = myChart.Axes.Bottom.AutomaticMaximum;
        }

        private void radioButtonYAuto_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Enabled = false;
            textBoxAbsMin.Enabled = false;
            textBoxAbsMax.Enabled = false;
            textBoxPercMin.Enabled = false;
            textBoxPercMax.Enabled = false;
        }

        private void radioButtonYManual_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Enabled = true;
            textBoxAbsMin.Enabled = false;
            textBoxAbsMax.Enabled = false;
            textBoxPercMin.Enabled = false;
            textBoxPercMax.Enabled = false;        

            textBoxYManMin.Text = myChart.Axes.Left.Minimum.ToString("F3");
            textBoxYManMax.Text = myChart.Axes.Left.Maximum.ToString("F3");
            textBoxYInc.Text = myChart.Axes.Left.Increment.ToString("F3");
            checkBoxYMin.Checked = myChart.Axes.Left.AutomaticMinimum;
            checkBoxYMax.Checked = myChart.Axes.Left.AutomaticMaximum;
        }

        private void radioButtonYAbs_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Enabled = false;
            textBoxAbsMin.Enabled = true;
            textBoxAbsMax.Enabled = true;
            textBoxPercMin.Enabled = false;
            textBoxPercMax.Enabled = false;

            textBoxAbsMin.Text = myFormCfg.yScale.lowerlimit.ToString("F4");
            textBoxAbsMax.Text = myFormCfg.yScale.upperlimit.ToString("F4");
        }

        private void radioButtonYPerc_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Enabled = false;
            textBoxAbsMin.Enabled = false;
            textBoxAbsMax.Enabled = false;
            textBoxPercMin.Enabled = true;
            textBoxPercMax.Enabled = true;

            textBoxPercMin.Text = myFormCfg.yScale.lowerlimit.ToString("F1");
            textBoxPercMax.Text = myFormCfg.yScale.upperlimit.ToString("F1");
        }
    }
}