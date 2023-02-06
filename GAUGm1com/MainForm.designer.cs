namespace GAUGm1com
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.xmdToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.remotingPanel = new System.Windows.Forms.Panel();
            this.remoteConnectButton = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.remoteServerAddressTextBox = new System.Windows.Forms.TextBox();
            this.remoteServerPortTextBox = new System.Windows.Forms.TextBox();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.consoleTextBox = new System.Windows.Forms.TextBox();
            this.xmdStatusStrip = new System.Windows.Forms.StatusStrip();
            this.dateStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.remoteStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.xmdToolStrip = new System.Windows.Forms.ToolStrip();
            this.aboutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.errorsComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.pvarUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.xmdNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.xmdToolStripContainer.ContentPanel.SuspendLayout();
            this.xmdToolStripContainer.TopToolStripPanel.SuspendLayout();
            this.xmdToolStripContainer.SuspendLayout();
            this.remotingPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.xmdStatusStrip.SuspendLayout();
            this.xmdToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // xmdToolStripContainer
            // 
            this.xmdToolStripContainer.BottomToolStripPanelVisible = false;
            // 
            // xmdToolStripContainer.ContentPanel
            // 
            this.xmdToolStripContainer.ContentPanel.Controls.Add(this.remotingPanel);
            this.xmdToolStripContainer.ContentPanel.Controls.Add(this.mainPanel);
            this.xmdToolStripContainer.ContentPanel.Controls.Add(this.xmdStatusStrip);
            this.xmdToolStripContainer.ContentPanel.Size = new System.Drawing.Size(1263, 561);
            this.xmdToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmdToolStripContainer.LeftToolStripPanelVisible = false;
            this.xmdToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.xmdToolStripContainer.Name = "xmdToolStripContainer";
            this.xmdToolStripContainer.RightToolStripPanelVisible = false;
            this.xmdToolStripContainer.Size = new System.Drawing.Size(1263, 586);
            this.xmdToolStripContainer.TabIndex = 8;
            this.xmdToolStripContainer.Text = "toolStripContainer1";
            // 
            // xmdToolStripContainer.TopToolStripPanel
            // 
            this.xmdToolStripContainer.TopToolStripPanel.Controls.Add(this.xmdToolStrip);
            // 
            // remotingPanel
            // 
            this.remotingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.remotingPanel.Controls.Add(this.remoteConnectButton);
            this.remotingPanel.Controls.Add(this.label19);
            this.remotingPanel.Controls.Add(this.label20);
            this.remotingPanel.Controls.Add(this.remoteServerAddressTextBox);
            this.remotingPanel.Controls.Add(this.remoteServerPortTextBox);
            this.remotingPanel.Location = new System.Drawing.Point(1075, 4);
            this.remotingPanel.Name = "remotingPanel";
            this.remotingPanel.Size = new System.Drawing.Size(185, 116);
            this.remotingPanel.TabIndex = 75;
            this.remotingPanel.Visible = false;
            // 
            // remoteConnectButton
            // 
            this.remoteConnectButton.Location = new System.Drawing.Point(6, 62);
            this.remoteConnectButton.Name = "remoteConnectButton";
            this.remoteConnectButton.Size = new System.Drawing.Size(75, 23);
            this.remoteConnectButton.TabIndex = 13;
            this.remoteConnectButton.Text = "Connect";
            this.remoteConnectButton.UseVisualStyleBackColor = true;
            this.remoteConnectButton.Click += new System.EventHandler(this.remoteConnectButton_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(3, 37);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(106, 13);
            this.label19.TabIndex = 12;
            this.label19.Text = "Remote Port Number";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(3, 13);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(98, 13);
            this.label20.TabIndex = 11;
            this.label20.Text = "Remote IP Address";
            // 
            // remoteServerAddressTextBox
            // 
            this.remoteServerAddressTextBox.Location = new System.Drawing.Point(115, 10);
            this.remoteServerAddressTextBox.Name = "remoteServerAddressTextBox";
            this.remoteServerAddressTextBox.Size = new System.Drawing.Size(61, 20);
            this.remoteServerAddressTextBox.TabIndex = 9;
            this.remoteServerAddressTextBox.Text = "127.0.0.1";
            // 
            // remoteServerPortTextBox
            // 
            this.remoteServerPortTextBox.Location = new System.Drawing.Point(115, 34);
            this.remoteServerPortTextBox.Name = "remoteServerPortTextBox";
            this.remoteServerPortTextBox.Size = new System.Drawing.Size(35, 20);
            this.remoteServerPortTextBox.TabIndex = 10;
            this.remoteServerPortTextBox.Text = "9001";
            // 
            // mainPanel
            // 
            this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mainPanel.Controls.Add(this.consoleTextBox);
            this.mainPanel.Location = new System.Drawing.Point(5, 4);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1068, 532);
            this.mainPanel.TabIndex = 18;
            // 
            // consoleTextBox
            // 
            this.consoleTextBox.Location = new System.Drawing.Point(-2, -2);
            this.consoleTextBox.Multiline = true;
            this.consoleTextBox.Name = "consoleTextBox";
            this.consoleTextBox.ReadOnly = true;
            this.consoleTextBox.Size = new System.Drawing.Size(1068, 527);
            this.consoleTextBox.TabIndex = 0;
            // 
            // xmdStatusStrip
            // 
            this.xmdStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateStatusLabel,
            this.remoteStatusLabel});
            this.xmdStatusStrip.Location = new System.Drawing.Point(0, 539);
            this.xmdStatusStrip.Name = "xmdStatusStrip";
            this.xmdStatusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.xmdStatusStrip.Size = new System.Drawing.Size(1263, 22);
            this.xmdStatusStrip.TabIndex = 8;
            this.xmdStatusStrip.Text = "statusStrip1";
            // 
            // dateStatusLabel
            // 
            this.dateStatusLabel.AutoSize = false;
            this.dateStatusLabel.Name = "dateStatusLabel";
            this.dateStatusLabel.Size = new System.Drawing.Size(240, 17);
            this.dateStatusLabel.Text = "03/09/2008 10:23";
            this.dateStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // remoteStatusLabel
            // 
            this.remoteStatusLabel.AutoSize = false;
            this.remoteStatusLabel.BackColor = System.Drawing.Color.Red;
            this.remoteStatusLabel.Image = ((System.Drawing.Image)(resources.GetObject("remoteStatusLabel.Image")));
            this.remoteStatusLabel.LinkColor = System.Drawing.Color.Red;
            this.remoteStatusLabel.Name = "remoteStatusLabel";
            this.remoteStatusLabel.Size = new System.Drawing.Size(120, 17);
            this.remoteStatusLabel.Text = "Disconnected";
            this.remoteStatusLabel.Click += new System.EventHandler(this.remoteStatusLabel_Click);
            // 
            // xmdToolStrip
            // 
            this.xmdToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.xmdToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripButton});
            this.xmdToolStrip.Location = new System.Drawing.Point(3, 0);
            this.xmdToolStrip.Name = "xmdToolStrip";
            this.xmdToolStrip.Size = new System.Drawing.Size(112, 25);
            this.xmdToolStrip.TabIndex = 0;
            // 
            // aboutToolStripButton
            // 
            this.aboutToolStripButton.AutoSize = false;
            this.aboutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripButton.Image")));
            this.aboutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.aboutToolStripButton.Name = "aboutToolStripButton";
            this.aboutToolStripButton.Size = new System.Drawing.Size(100, 22);
            this.aboutToolStripButton.Text = "About";
            this.aboutToolStripButton.Click += new System.EventHandler(this.aboutToolStripButton_Click);
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 40;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // errorsComboBox
            // 
            this.errorsComboBox.DropDownHeight = 140;
            this.errorsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorsComboBox.DropDownWidth = 200;
            this.errorsComboBox.IntegralHeight = false;
            this.errorsComboBox.Items.AddRange(new object[] {
            "17:20 12/09/2008 Open beam changed too much",
            "17:00 12/09/2008 HMD during calibration"});
            this.errorsComboBox.MaxDropDownItems = 10;
            this.errorsComboBox.Name = "errorsComboBox";
            this.errorsComboBox.Size = new System.Drawing.Size(200, 23);
            // 
            // pvarUpdateTimer
            // 
            this.pvarUpdateTimer.Interval = 50;
            this.pvarUpdateTimer.Tick += new System.EventHandler(this.pvarUpdateTimer_Tick);
            // 
            // xmdNotifyIcon
            // 
            this.xmdNotifyIcon.Text = "notifyIcon1";
            this.xmdNotifyIcon.Visible = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 586);
            this.Controls.Add(this.xmdToolStripContainer);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " TITLE";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.xmdToolStripContainer.ContentPanel.ResumeLayout(false);
            this.xmdToolStripContainer.ContentPanel.PerformLayout();
            this.xmdToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.xmdToolStripContainer.TopToolStripPanel.PerformLayout();
            this.xmdToolStripContainer.ResumeLayout(false);
            this.xmdToolStripContainer.PerformLayout();
            this.remotingPanel.ResumeLayout(false);
            this.remotingPanel.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.xmdStatusStrip.ResumeLayout(false);
            this.xmdStatusStrip.PerformLayout();
            this.xmdToolStrip.ResumeLayout(false);
            this.xmdToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStripContainer xmdToolStripContainer;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.ToolStripComboBox errorsComboBox;
        private System.Windows.Forms.Panel remotingPanel;
        private System.Windows.Forms.Button remoteConnectButton;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox remoteServerAddressTextBox;
        private System.Windows.Forms.TextBox remoteServerPortTextBox;
        private System.Windows.Forms.StatusStrip xmdStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel dateStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel remoteStatusLabel;
        private System.Windows.Forms.ToolStrip xmdToolStrip;
        private System.Windows.Forms.ToolStripButton aboutToolStripButton;
        private System.Windows.Forms.Timer pvarUpdateTimer;
        private System.Windows.Forms.NotifyIcon xmdNotifyIcon;
        private System.Windows.Forms.TextBox consoleTextBox;
    }
}

