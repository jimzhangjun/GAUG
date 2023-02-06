namespace GAUGview
{
    partial class ChartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelText6 = new System.Windows.Forms.Label();
            this.textBoxYInc = new System.Windows.Forms.TextBox();
            this.labelText5 = new System.Windows.Forms.Label();
            this.textBoxYManMax = new System.Windows.Forms.TextBox();
            this.checkBoxYMax = new System.Windows.Forms.CheckBox();
            this.labelText4 = new System.Windows.Forms.Label();
            this.textBoxYManMin = new System.Windows.Forms.TextBox();
            this.checkBoxYMin = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelText3 = new System.Windows.Forms.Label();
            this.textBoxXInc = new System.Windows.Forms.TextBox();
            this.labelText2 = new System.Windows.Forms.Label();
            this.textBoxXManMax = new System.Windows.Forms.TextBox();
            this.checkBoxXMax = new System.Windows.Forms.CheckBox();
            this.labelText1 = new System.Windows.Forms.Label();
            this.textBoxXManMin = new System.Windows.Forms.TextBox();
            this.checkBoxXMin = new System.Windows.Forms.CheckBox();
            this.BackcolorDialog = new System.Windows.Forms.ColorDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonXManual = new System.Windows.Forms.RadioButton();
            this.radioButtonXAuto = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.labelText8 = new System.Windows.Forms.Label();
            this.labelText7 = new System.Windows.Forms.Label();
            this.textBoxAbsMin = new System.Windows.Forms.TextBox();
            this.textBoxPercMin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelUnit1 = new System.Windows.Forms.Label();
            this.textBoxPercMax = new System.Windows.Forms.TextBox();
            this.textBoxAbsMax = new System.Windows.Forms.TextBox();
            this.radioButtonYManual = new System.Windows.Forms.RadioButton();
            this.radioButtonYAuto = new System.Windows.Forms.RadioButton();
            this.radioButtonYPerc = new System.Windows.Forms.RadioButton();
            this.radioButtonYAbs = new System.Windows.Forms.RadioButton();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(343, 26);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(2);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(93, 27);
            this.buttonOK.TabIndex = 14;
            this.buttonOK.Tag = "TX_OK";
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(343, 72);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(93, 27);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Tag = "TX_CANCEL";
            this.buttonCancel.Text = "CANCEL";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelText6);
            this.groupBox2.Controls.Add(this.textBoxYInc);
            this.groupBox2.Controls.Add(this.labelText5);
            this.groupBox2.Controls.Add(this.textBoxYManMax);
            this.groupBox2.Controls.Add(this.checkBoxYMax);
            this.groupBox2.Controls.Add(this.labelText4);
            this.groupBox2.Controls.Add(this.textBoxYManMin);
            this.groupBox2.Controls.Add(this.checkBoxYMin);
            this.groupBox2.Location = new System.Drawing.Point(223, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(202, 100);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Tag = "TX_MANUAL";
            this.groupBox2.Text = "Manual";
            // 
            // labelText6
            // 
            this.labelText6.AutoSize = true;
            this.labelText6.Location = new System.Drawing.Point(16, 71);
            this.labelText6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelText6.Name = "labelText6";
            this.labelText6.Size = new System.Drawing.Size(54, 13);
            this.labelText6.TabIndex = 27;
            this.labelText6.Tag = "TX_INCREMENT";
            this.labelText6.Text = "Increment";
            // 
            // textBoxYInc
            // 
            this.textBoxYInc.Location = new System.Drawing.Point(80, 68);
            this.textBoxYInc.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxYInc.Name = "textBoxYInc";
            this.textBoxYInc.Size = new System.Drawing.Size(62, 20);
            this.textBoxYInc.TabIndex = 26;
            this.textBoxYInc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxYInc_KeyPress);
            // 
            // labelText5
            // 
            this.labelText5.AutoSize = true;
            this.labelText5.Location = new System.Drawing.Point(16, 47);
            this.labelText5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelText5.Name = "labelText5";
            this.labelText5.Size = new System.Drawing.Size(57, 13);
            this.labelText5.TabIndex = 25;
            this.labelText5.Tag = "TX_UPPER";
            this.labelText5.Text = "Max Value";
            // 
            // textBoxYManMax
            // 
            this.textBoxYManMax.Location = new System.Drawing.Point(80, 44);
            this.textBoxYManMax.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxYManMax.Name = "textBoxYManMax";
            this.textBoxYManMax.Size = new System.Drawing.Size(62, 20);
            this.textBoxYManMax.TabIndex = 24;
            this.textBoxYManMax.TextChanged += new System.EventHandler(this.textBoxYManMax_TextChanged);
            this.textBoxYManMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxYManMax_KeyPress);
            // 
            // checkBoxYMax
            // 
            this.checkBoxYMax.AutoSize = true;
            this.checkBoxYMax.Location = new System.Drawing.Point(153, 46);
            this.checkBoxYMax.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxYMax.Name = "checkBoxYMax";
            this.checkBoxYMax.Size = new System.Drawing.Size(48, 17);
            this.checkBoxYMax.TabIndex = 23;
            this.checkBoxYMax.Tag = "TX_AUTO";
            this.checkBoxYMax.Text = "Auto";
            this.checkBoxYMax.UseVisualStyleBackColor = true;
            // 
            // labelText4
            // 
            this.labelText4.AutoSize = true;
            this.labelText4.Location = new System.Drawing.Point(16, 23);
            this.labelText4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelText4.Name = "labelText4";
            this.labelText4.Size = new System.Drawing.Size(54, 13);
            this.labelText4.TabIndex = 22;
            this.labelText4.Tag = "TX_LOWER";
            this.labelText4.Text = "Min Value";
            // 
            // textBoxYManMin
            // 
            this.textBoxYManMin.Location = new System.Drawing.Point(80, 20);
            this.textBoxYManMin.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxYManMin.Name = "textBoxYManMin";
            this.textBoxYManMin.Size = new System.Drawing.Size(62, 20);
            this.textBoxYManMin.TabIndex = 21;
            this.textBoxYManMin.TextChanged += new System.EventHandler(this.textBoxYManMin_TextChanged);
            this.textBoxYManMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxYManMin_KeyPress);
            // 
            // checkBoxYMin
            // 
            this.checkBoxYMin.AutoSize = true;
            this.checkBoxYMin.Location = new System.Drawing.Point(153, 22);
            this.checkBoxYMin.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxYMin.Name = "checkBoxYMin";
            this.checkBoxYMin.Size = new System.Drawing.Size(48, 17);
            this.checkBoxYMin.TabIndex = 20;
            this.checkBoxYMin.Tag = "TX_AUTO";
            this.checkBoxYMin.Text = "Auto";
            this.checkBoxYMin.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelText3);
            this.groupBox1.Controls.Add(this.textBoxXInc);
            this.groupBox1.Controls.Add(this.labelText2);
            this.groupBox1.Controls.Add(this.textBoxXManMax);
            this.groupBox1.Controls.Add(this.checkBoxXMax);
            this.groupBox1.Controls.Add(this.labelText1);
            this.groupBox1.Controls.Add(this.textBoxXManMin);
            this.groupBox1.Controls.Add(this.checkBoxXMin);
            this.groupBox1.Location = new System.Drawing.Point(87, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 100);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "TX_MANUAL";
            this.groupBox1.Text = "Manual";
            // 
            // labelText3
            // 
            this.labelText3.AutoSize = true;
            this.labelText3.Location = new System.Drawing.Point(16, 71);
            this.labelText3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelText3.Name = "labelText3";
            this.labelText3.Size = new System.Drawing.Size(54, 13);
            this.labelText3.TabIndex = 26;
            this.labelText3.Tag = "TX_INCREMENT";
            this.labelText3.Text = "Increment";
            // 
            // textBoxXInc
            // 
            this.textBoxXInc.Location = new System.Drawing.Point(80, 68);
            this.textBoxXInc.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxXInc.Name = "textBoxXInc";
            this.textBoxXInc.Size = new System.Drawing.Size(62, 20);
            this.textBoxXInc.TabIndex = 25;
            this.textBoxXInc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxXInc_KeyPress);
            // 
            // labelText2
            // 
            this.labelText2.AutoSize = true;
            this.labelText2.Location = new System.Drawing.Point(16, 47);
            this.labelText2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelText2.Name = "labelText2";
            this.labelText2.Size = new System.Drawing.Size(57, 13);
            this.labelText2.TabIndex = 24;
            this.labelText2.Tag = "TX_UPPER";
            this.labelText2.Text = "Max Value";
            // 
            // textBoxXManMax
            // 
            this.textBoxXManMax.Location = new System.Drawing.Point(80, 44);
            this.textBoxXManMax.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxXManMax.Name = "textBoxXManMax";
            this.textBoxXManMax.Size = new System.Drawing.Size(62, 20);
            this.textBoxXManMax.TabIndex = 23;
            this.textBoxXManMax.TextChanged += new System.EventHandler(this.textBoxXManMax_TextChanged);
            this.textBoxXManMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxXManMax_KeyPress);
            // 
            // checkBoxXMax
            // 
            this.checkBoxXMax.AutoSize = true;
            this.checkBoxXMax.Location = new System.Drawing.Point(153, 46);
            this.checkBoxXMax.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxXMax.Name = "checkBoxXMax";
            this.checkBoxXMax.Size = new System.Drawing.Size(48, 17);
            this.checkBoxXMax.TabIndex = 22;
            this.checkBoxXMax.Tag = "TX_AUTO";
            this.checkBoxXMax.Text = "Auto";
            this.checkBoxXMax.UseVisualStyleBackColor = true;
            // 
            // labelText1
            // 
            this.labelText1.AutoSize = true;
            this.labelText1.Location = new System.Drawing.Point(16, 23);
            this.labelText1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelText1.Name = "labelText1";
            this.labelText1.Size = new System.Drawing.Size(54, 13);
            this.labelText1.TabIndex = 21;
            this.labelText1.Tag = "TX_LOWER";
            this.labelText1.Text = "Min Value";
            // 
            // textBoxXManMin
            // 
            this.textBoxXManMin.Location = new System.Drawing.Point(80, 20);
            this.textBoxXManMin.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxXManMin.Name = "textBoxXManMin";
            this.textBoxXManMin.Size = new System.Drawing.Size(62, 20);
            this.textBoxXManMin.TabIndex = 20;
            this.textBoxXManMin.TextChanged += new System.EventHandler(this.textBoxXManMin_TextChanged);
            this.textBoxXManMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxXManMin_KeyPress);
            // 
            // checkBoxXMin
            // 
            this.checkBoxXMin.AutoSize = true;
            this.checkBoxXMin.Location = new System.Drawing.Point(153, 22);
            this.checkBoxXMin.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxXMin.Name = "checkBoxXMin";
            this.checkBoxXMin.Size = new System.Drawing.Size(48, 17);
            this.checkBoxXMin.TabIndex = 19;
            this.checkBoxXMin.Tag = "TX_AUTO";
            this.checkBoxXMin.Text = "Auto";
            this.checkBoxXMin.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButtonXManual);
            this.groupBox3.Controls.Add(this.radioButtonXAuto);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(312, 127);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Tag = "TX_XAXIS";
            this.groupBox3.Text = " X-Axis ";
            // 
            // radioButtonXManual
            // 
            this.radioButtonXManual.AutoSize = true;
            this.radioButtonXManual.Location = new System.Drawing.Point(6, 48);
            this.radioButtonXManual.Name = "radioButtonXManual";
            this.radioButtonXManual.Size = new System.Drawing.Size(60, 17);
            this.radioButtonXManual.TabIndex = 0;
            this.radioButtonXManual.TabStop = true;
            this.radioButtonXManual.Tag = "TX_MANUAL";
            this.radioButtonXManual.Text = "Manual";
            this.radioButtonXManual.UseVisualStyleBackColor = true;
            this.radioButtonXManual.CheckedChanged += new System.EventHandler(this.radioButtonXManual_CheckedChanged);
            // 
            // radioButtonXAuto
            // 
            this.radioButtonXAuto.AutoSize = true;
            this.radioButtonXAuto.Location = new System.Drawing.Point(6, 24);
            this.radioButtonXAuto.Name = "radioButtonXAuto";
            this.radioButtonXAuto.Size = new System.Drawing.Size(47, 17);
            this.radioButtonXAuto.TabIndex = 0;
            this.radioButtonXAuto.TabStop = true;
            this.radioButtonXAuto.Tag = "TX_AUTO";
            this.radioButtonXAuto.Text = "Auto";
            this.radioButtonXAuto.UseVisualStyleBackColor = true;
            this.radioButtonXAuto.CheckedChanged += new System.EventHandler(this.radioButtonXAuto_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.labelText8);
            this.groupBox4.Controls.Add(this.labelText7);
            this.groupBox4.Controls.Add(this.textBoxAbsMin);
            this.groupBox4.Controls.Add(this.textBoxPercMin);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.labelUnit1);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Controls.Add(this.textBoxPercMax);
            this.groupBox4.Controls.Add(this.textBoxAbsMax);
            this.groupBox4.Controls.Add(this.radioButtonYManual);
            this.groupBox4.Controls.Add(this.radioButtonYAuto);
            this.groupBox4.Controls.Add(this.radioButtonYPerc);
            this.groupBox4.Controls.Add(this.radioButtonYAbs);
            this.groupBox4.Location = new System.Drawing.Point(12, 145);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(432, 122);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Tag = "TX_YAXIS";
            this.groupBox4.Text = " Y-Axis ";
            // 
            // labelText8
            // 
            this.labelText8.AutoSize = true;
            this.labelText8.Location = new System.Drawing.Point(129, 48);
            this.labelText8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelText8.Name = "labelText8";
            this.labelText8.Size = new System.Drawing.Size(57, 13);
            this.labelText8.TabIndex = 27;
            this.labelText8.Tag = "TX_UPPER";
            this.labelText8.Text = "Max Value";
            // 
            // labelText7
            // 
            this.labelText7.AutoSize = true;
            this.labelText7.Location = new System.Drawing.Point(66, 48);
            this.labelText7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelText7.Name = "labelText7";
            this.labelText7.Size = new System.Drawing.Size(54, 13);
            this.labelText7.TabIndex = 26;
            this.labelText7.Tag = "TX_LOWER";
            this.labelText7.Text = "Min Value";
            // 
            // textBoxAbsMin
            // 
            this.textBoxAbsMin.Location = new System.Drawing.Point(60, 67);
            this.textBoxAbsMin.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxAbsMin.Name = "textBoxAbsMin";
            this.textBoxAbsMin.Size = new System.Drawing.Size(60, 20);
            this.textBoxAbsMin.TabIndex = 25;
            // 
            // textBoxPercMin
            // 
            this.textBoxPercMin.Location = new System.Drawing.Point(60, 94);
            this.textBoxPercMin.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPercMin.Name = "textBoxPercMin";
            this.textBoxPercMin.Size = new System.Drawing.Size(60, 20);
            this.textBoxPercMin.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(194, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "%";
            // 
            // labelUnit1
            // 
            this.labelUnit1.AutoSize = true;
            this.labelUnit1.Location = new System.Drawing.Point(194, 70);
            this.labelUnit1.Name = "labelUnit1";
            this.labelUnit1.Size = new System.Drawing.Size(23, 13);
            this.labelUnit1.TabIndex = 23;
            this.labelUnit1.Tag = "0";
            this.labelUnit1.Text = "mm";
            // 
            // textBoxPercMax
            // 
            this.textBoxPercMax.Location = new System.Drawing.Point(126, 94);
            this.textBoxPercMax.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPercMax.Name = "textBoxPercMax";
            this.textBoxPercMax.Size = new System.Drawing.Size(60, 20);
            this.textBoxPercMax.TabIndex = 22;
            // 
            // textBoxAbsMax
            // 
            this.textBoxAbsMax.Location = new System.Drawing.Point(126, 67);
            this.textBoxAbsMax.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxAbsMax.Name = "textBoxAbsMax";
            this.textBoxAbsMax.Size = new System.Drawing.Size(60, 20);
            this.textBoxAbsMax.TabIndex = 21;
            // 
            // radioButtonYManual
            // 
            this.radioButtonYManual.AutoSize = true;
            this.radioButtonYManual.Location = new System.Drawing.Point(74, 24);
            this.radioButtonYManual.Name = "radioButtonYManual";
            this.radioButtonYManual.Size = new System.Drawing.Size(60, 17);
            this.radioButtonYManual.TabIndex = 0;
            this.radioButtonYManual.TabStop = true;
            this.radioButtonYManual.Tag = "TX_MANUAL";
            this.radioButtonYManual.Text = "Manual";
            this.radioButtonYManual.UseVisualStyleBackColor = true;
            this.radioButtonYManual.CheckedChanged += new System.EventHandler(this.radioButtonYManual_CheckedChanged);
            // 
            // radioButtonYAuto
            // 
            this.radioButtonYAuto.AutoSize = true;
            this.radioButtonYAuto.Location = new System.Drawing.Point(6, 24);
            this.radioButtonYAuto.Name = "radioButtonYAuto";
            this.radioButtonYAuto.Size = new System.Drawing.Size(47, 17);
            this.radioButtonYAuto.TabIndex = 0;
            this.radioButtonYAuto.TabStop = true;
            this.radioButtonYAuto.Tag = "TX_AUTO";
            this.radioButtonYAuto.Text = "Auto";
            this.radioButtonYAuto.UseVisualStyleBackColor = true;
            this.radioButtonYAuto.CheckedChanged += new System.EventHandler(this.radioButtonYAuto_CheckedChanged);
            // 
            // radioButtonYPerc
            // 
            this.radioButtonYPerc.AutoSize = true;
            this.radioButtonYPerc.Location = new System.Drawing.Point(6, 95);
            this.radioButtonYPerc.Name = "radioButtonYPerc";
            this.radioButtonYPerc.Size = new System.Drawing.Size(47, 17);
            this.radioButtonYPerc.TabIndex = 0;
            this.radioButtonYPerc.TabStop = true;
            this.radioButtonYPerc.Tag = "TX_PERC";
            this.radioButtonYPerc.Text = "Perc";
            this.radioButtonYPerc.UseVisualStyleBackColor = true;
            this.radioButtonYPerc.CheckedChanged += new System.EventHandler(this.radioButtonYPerc_CheckedChanged);
            // 
            // radioButtonYAbs
            // 
            this.radioButtonYAbs.AutoSize = true;
            this.radioButtonYAbs.Location = new System.Drawing.Point(6, 68);
            this.radioButtonYAbs.Name = "radioButtonYAbs";
            this.radioButtonYAbs.Size = new System.Drawing.Size(43, 17);
            this.radioButtonYAbs.TabIndex = 0;
            this.radioButtonYAbs.TabStop = true;
            this.radioButtonYAbs.Tag = "TX_ABS";
            this.radioButtonYAbs.Text = "Abs";
            this.radioButtonYAbs.UseVisualStyleBackColor = true;
            this.radioButtonYAbs.CheckedChanged += new System.EventHandler(this.radioButtonYAbs_CheckedChanged);
            // 
            // ChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 276);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ChartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Configure Chart";
            this.Load += new System.EventHandler(this.ChartForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelText6;
        private System.Windows.Forms.TextBox textBoxYInc;
        private System.Windows.Forms.Label labelText5;
        private System.Windows.Forms.TextBox textBoxYManMax;
        private System.Windows.Forms.CheckBox checkBoxYMax;
        private System.Windows.Forms.Label labelText4;
        private System.Windows.Forms.TextBox textBoxYManMin;
        private System.Windows.Forms.CheckBox checkBoxYMin;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelText3;
        private System.Windows.Forms.TextBox textBoxXInc;
        private System.Windows.Forms.Label labelText2;
        private System.Windows.Forms.TextBox textBoxXManMax;
        private System.Windows.Forms.CheckBox checkBoxXMax;
        private System.Windows.Forms.Label labelText1;
        private System.Windows.Forms.TextBox textBoxXManMin;
        private System.Windows.Forms.CheckBox checkBoxXMin;
        private System.Windows.Forms.ColorDialog BackcolorDialog;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButtonXManual;
        private System.Windows.Forms.RadioButton radioButtonXAuto;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButtonYManual;
        private System.Windows.Forms.RadioButton radioButtonYAuto;
        private System.Windows.Forms.RadioButton radioButtonYPerc;
        private System.Windows.Forms.RadioButton radioButtonYAbs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelUnit1;
        private System.Windows.Forms.TextBox textBoxPercMax;
        private System.Windows.Forms.TextBox textBoxAbsMax;
        private System.Windows.Forms.TextBox textBoxPercMin;
        private System.Windows.Forms.Label labelText8;
        private System.Windows.Forms.Label labelText7;
        private System.Windows.Forms.TextBox textBoxAbsMin;
    }
}