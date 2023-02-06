namespace GAUGview
{
    partial class ViewMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewMainForm));
            this.xmdToolStrip = new System.Windows.Forms.ToolStrip();
            this.loginToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.modeToolStripButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.modeToolStripMenuItemOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.modeToolStripMenuItemOffline = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.printCurrentPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printAllPagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.viewErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.netTimeoutTimer = new System.Windows.Forms.Timer(this.components);
            this.xmdStatusStrip = new System.Windows.Forms.StatusStrip();
            this.loginStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.startedStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.space1StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.space2StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.remoteStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripPanel = new System.Windows.Forms.ToolStripContainer();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.xmdNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.xmdContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.displayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remotingPanel = new System.Windows.Forms.Panel();
            this.remoteConnectButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.remoteServerAddressTextBox = new System.Windows.Forms.TextBox();
            this.remoteServerPortTextBox = new System.Windows.Forms.TextBox();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.xmdToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewErrorProvider)).BeginInit();
            this.xmdStatusStrip.SuspendLayout();
            this.toolStripPanel.TopToolStripPanel.SuspendLayout();
            this.toolStripPanel.SuspendLayout();
            this.xmdContextMenuStrip.SuspendLayout();
            this.remotingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // xmdToolStrip
            // 
            this.xmdToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.xmdToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.xmdToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripButton,
            this.modeToolStripButton,
            this.printToolStripButton,
            this.aboutToolStripButton});
            this.xmdToolStrip.Location = new System.Drawing.Point(3, 0);
            this.xmdToolStrip.Name = "xmdToolStrip";
            this.xmdToolStrip.Size = new System.Drawing.Size(426, 39);
            this.xmdToolStrip.TabIndex = 3;
            // 
            // loginToolStripButton
            // 
            this.loginToolStripButton.AutoSize = false;
            this.loginToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("loginToolStripButton.Image")));
            this.loginToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loginToolStripButton.Name = "loginToolStripButton";
            this.loginToolStripButton.Size = new System.Drawing.Size(100, 22);
            this.loginToolStripButton.Tag = "TX_LOGIN";
            this.loginToolStripButton.Text = "Login";
            // 
            // modeToolStripButton
            // 
            this.modeToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modeToolStripMenuItemOnline,
            this.modeToolStripMenuItemOffline});
            this.modeToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("modeToolStripButton.Image")));
            this.modeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.modeToolStripButton.Name = "modeToolStripButton";
            this.modeToolStripButton.Size = new System.Drawing.Size(83, 36);
            this.modeToolStripButton.Tag = "TX_MODE";
            this.modeToolStripButton.Text = "Mode";
            // 
            // modeToolStripMenuItemOnline
            // 
            this.modeToolStripMenuItemOnline.Name = "modeToolStripMenuItemOnline";
            this.modeToolStripMenuItemOnline.Size = new System.Drawing.Size(180, 22);
            this.modeToolStripMenuItemOnline.Tag = "TX_ONLINE";
            this.modeToolStripMenuItemOnline.Text = "Online";
            this.modeToolStripMenuItemOnline.Click += new System.EventHandler(this.modeToolStripMenuItemOnline_Click);
            // 
            // modeToolStripMenuItemOffline
            // 
            this.modeToolStripMenuItemOffline.Name = "modeToolStripMenuItemOffline";
            this.modeToolStripMenuItemOffline.Size = new System.Drawing.Size(180, 22);
            this.modeToolStripMenuItemOffline.Tag = "TX_OFFLINE";
            this.modeToolStripMenuItemOffline.Text = "Offline";
            this.modeToolStripMenuItemOffline.Click += new System.EventHandler(this.modeToolStripMenuItemOffline_Click);
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.AutoSize = false;
            this.printToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printCurrentPageToolStripMenuItem,
            this.printAllPagesToolStripMenuItem});
            this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(100, 22);
            this.printToolStripButton.Tag = "TX_PRINT";
            this.printToolStripButton.Text = "Print";
            // 
            // printCurrentPageToolStripMenuItem
            // 
            this.printCurrentPageToolStripMenuItem.Name = "printCurrentPageToolStripMenuItem";
            this.printCurrentPageToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.printCurrentPageToolStripMenuItem.Tag = "TX_PRINTCUR";
            this.printCurrentPageToolStripMenuItem.Text = "Print current page";
            this.printCurrentPageToolStripMenuItem.Click += new System.EventHandler(this.printCurrentPageToolStripMenuItem_Click);
            // 
            // printAllPagesToolStripMenuItem
            // 
            this.printAllPagesToolStripMenuItem.Name = "printAllPagesToolStripMenuItem";
            this.printAllPagesToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.printAllPagesToolStripMenuItem.Tag = "TX_PRINTALL";
            this.printAllPagesToolStripMenuItem.Text = "Print all pages";
            this.printAllPagesToolStripMenuItem.Click += new System.EventHandler(this.printAllPagesToolStripMenuItem_Click);
            // 
            // aboutToolStripButton
            // 
            this.aboutToolStripButton.AutoSize = false;
            this.aboutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripButton.Image")));
            this.aboutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.aboutToolStripButton.Name = "aboutToolStripButton";
            this.aboutToolStripButton.Size = new System.Drawing.Size(100, 22);
            this.aboutToolStripButton.Tag = "TX_ABOUT";
            this.aboutToolStripButton.Text = "About";
            this.aboutToolStripButton.Click += new System.EventHandler(this.aboutToolStripButton_Click);
            // 
            // viewErrorProvider
            // 
            this.viewErrorProvider.ContainerControl = this;
            // 
            // xmdStatusStrip
            // 
            this.xmdStatusStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.xmdStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginStatusLabel,
            this.startedStatusLabel,
            this.space1StatusLabel,
            this.space2StatusLabel,
            this.remoteStatusLabel});
            this.xmdStatusStrip.Location = new System.Drawing.Point(0, 554);
            this.xmdStatusStrip.Name = "xmdStatusStrip";
            this.xmdStatusStrip.Size = new System.Drawing.Size(1028, 32);
            this.xmdStatusStrip.TabIndex = 89;
            // 
            // loginStatusLabel
            // 
            this.loginStatusLabel.AutoSize = false;
            this.loginStatusLabel.Image = ((System.Drawing.Image)(resources.GetObject("loginStatusLabel.Image")));
            this.loginStatusLabel.Name = "loginStatusLabel";
            this.loginStatusLabel.Size = new System.Drawing.Size(200, 27);
            this.loginStatusLabel.Text = "Login:";
            // 
            // startedStatusLabel
            // 
            this.startedStatusLabel.AutoSize = false;
            this.startedStatusLabel.Image = ((System.Drawing.Image)(resources.GetObject("startedStatusLabel.Image")));
            this.startedStatusLabel.Name = "startedStatusLabel";
            this.startedStatusLabel.Size = new System.Drawing.Size(200, 27);
            this.startedStatusLabel.Text = "Started:";
            // 
            // space1StatusLabel
            // 
            this.space1StatusLabel.AutoSize = false;
            this.space1StatusLabel.Name = "space1StatusLabel";
            this.space1StatusLabel.Size = new System.Drawing.Size(145, 27);
            // 
            // space2StatusLabel
            // 
            this.space2StatusLabel.AutoSize = false;
            this.space2StatusLabel.Name = "space2StatusLabel";
            this.space2StatusLabel.Size = new System.Drawing.Size(65, 27);
            // 
            // remoteStatusLabel
            // 
            this.remoteStatusLabel.AutoSize = false;
            this.remoteStatusLabel.BackColor = System.Drawing.Color.Red;
            this.remoteStatusLabel.Image = ((System.Drawing.Image)(resources.GetObject("remoteStatusLabel.Image")));
            this.remoteStatusLabel.Name = "remoteStatusLabel";
            this.remoteStatusLabel.Size = new System.Drawing.Size(120, 27);
            this.remoteStatusLabel.Text = "Disconnected";
            this.remoteStatusLabel.Click += new System.EventHandler(this.remoteStatusLabel_Click);
            // 
            // toolStripPanel
            // 
            this.toolStripPanel.BottomToolStripPanelVisible = false;
            // 
            // toolStripPanel.ContentPanel
            // 
            this.toolStripPanel.ContentPanel.Size = new System.Drawing.Size(963, 0);
            this.toolStripPanel.LeftToolStripPanelVisible = false;
            this.toolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.toolStripPanel.Name = "toolStripPanel";
            this.toolStripPanel.RightToolStripPanelVisible = false;
            this.toolStripPanel.Size = new System.Drawing.Size(963, 26);
            this.toolStripPanel.TabIndex = 74;
            this.toolStripPanel.Text = "toolStripContainer1";
            // 
            // toolStripPanel.TopToolStripPanel
            // 
            this.toolStripPanel.TopToolStripPanel.Controls.Add(this.xmdToolStrip);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // xmdNotifyIcon
            // 
            this.xmdNotifyIcon.BalloonTipText = "XMD viewmeter interface";
            this.xmdNotifyIcon.ContextMenuStrip = this.xmdContextMenuStrip;
            this.xmdNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("xmdNotifyIcon.Icon")));
            this.xmdNotifyIcon.Text = "GAUGview";
            this.xmdNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.xmdNotifyIcon_MouseDoubleClick);
            // 
            // xmdContextMenuStrip
            // 
            this.xmdContextMenuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.xmdContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.xmdContextMenuStrip.Name = "detContextMenuStrip";
            this.xmdContextMenuStrip.ShowImageMargin = false;
            this.xmdContextMenuStrip.ShowItemToolTips = false;
            this.xmdContextMenuStrip.Size = new System.Drawing.Size(114, 48);
            this.xmdContextMenuStrip.Text = "XMD Viewmeter";
            // 
            // displayToolStripMenuItem
            // 
            this.displayToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.displayToolStripMenuItem.Name = "displayToolStripMenuItem";
            this.displayToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.displayToolStripMenuItem.Text = "Display HMI";
            this.displayToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.displayToolStripMenuItem.Click += new System.EventHandler(this.displayToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // remotingPanel
            // 
            this.remotingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.remotingPanel.Controls.Add(this.remoteConnectButton);
            this.remotingPanel.Controls.Add(this.label4);
            this.remotingPanel.Controls.Add(this.label8);
            this.remotingPanel.Controls.Add(this.remoteServerAddressTextBox);
            this.remotingPanel.Controls.Add(this.remoteServerPortTextBox);
            this.remotingPanel.Location = new System.Drawing.Point(796, 532);
            this.remotingPanel.Name = "remotingPanel";
            this.remotingPanel.Size = new System.Drawing.Size(140, 165);
            this.remotingPanel.TabIndex = 74;
            this.remotingPanel.Visible = false;
            // 
            // remoteConnectButton
            // 
            this.remoteConnectButton.Location = new System.Drawing.Point(36, 124);
            this.remoteConnectButton.Name = "remoteConnectButton";
            this.remoteConnectButton.Size = new System.Drawing.Size(75, 23);
            this.remoteConnectButton.TabIndex = 13;
            this.remoteConnectButton.Text = "Connect";
            this.remoteConnectButton.UseVisualStyleBackColor = true;
            this.remoteConnectButton.Click += new System.EventHandler(this.remoteConnectButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Remote Port Number";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Remote IP Address";
            // 
            // remoteServerAddressTextBox
            // 
            this.remoteServerAddressTextBox.Location = new System.Drawing.Point(50, 33);
            this.remoteServerAddressTextBox.Name = "remoteServerAddressTextBox";
            this.remoteServerAddressTextBox.Size = new System.Drawing.Size(76, 20);
            this.remoteServerAddressTextBox.TabIndex = 9;
            this.remoteServerAddressTextBox.Text = "127.0.0.1";
            // 
            // remoteServerPortTextBox
            // 
            this.remoteServerPortTextBox.Location = new System.Drawing.Point(50, 82);
            this.remoteServerPortTextBox.Name = "remoteServerPortTextBox";
            this.remoteServerPortTextBox.Size = new System.Drawing.Size(35, 20);
            this.remoteServerPortTextBox.TabIndex = 10;
            this.remoteServerPortTextBox.Text = "9000";
            // 
            // tabControlMain
            // 
            this.tabControlMain.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControlMain.Location = new System.Drawing.Point(9, 198);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.Padding = new System.Drawing.Point(60, 3);
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(899, 388);
            this.tabControlMain.TabIndex = 90;
            this.tabControlMain.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControlMain_DrawItem);
            // 
            // ViewMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1028, 586);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.remotingPanel);
            this.Controls.Add(this.xmdStatusStrip);
            this.Controls.Add(this.toolStripPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ViewMainForm";
            this.Text = "GAUG View";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewMainForm_FormClosing);
            this.Load += new System.EventHandler(this.ViewMainForm_Load);
            this.Resize += new System.EventHandler(this.ViewMainForm_Resize);
            this.xmdToolStrip.ResumeLayout(false);
            this.xmdToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewErrorProvider)).EndInit();
            this.xmdStatusStrip.ResumeLayout(false);
            this.xmdStatusStrip.PerformLayout();
            this.toolStripPanel.TopToolStripPanel.ResumeLayout(false);
            this.toolStripPanel.TopToolStripPanel.PerformLayout();
            this.toolStripPanel.ResumeLayout(false);
            this.toolStripPanel.PerformLayout();
            this.xmdContextMenuStrip.ResumeLayout(false);
            this.remotingPanel.ResumeLayout(false);
            this.remotingPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip xmdToolStrip;
        private System.Windows.Forms.ToolStripButton loginToolStripButton;
        private System.Windows.Forms.ToolStripButton aboutToolStripButton;
        private System.Windows.Forms.ErrorProvider viewErrorProvider;
        private System.Windows.Forms.StatusStrip xmdStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel loginStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel startedStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel space1StatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel space2StatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel remoteStatusLabel;
        private System.Windows.Forms.ToolStripContainer toolStripPanel;
        private System.Windows.Forms.Timer netTimeoutTimer;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.NotifyIcon xmdNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip xmdContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem displayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel remotingPanel;
        private System.Windows.Forms.Button remoteConnectButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox remoteServerAddressTextBox;
        private System.Windows.Forms.TextBox remoteServerPortTextBox;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.ToolStripDropDownButton printToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem printCurrentPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printAllPagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton modeToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItemOnline;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItemOffline;
    }
}

