namespace GAUGcenter
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
            this.serverPortTextBox = new System.Windows.Forms.TextBox();
            this.serverButton = new System.Windows.Forms.Button();
            this.startXMDloadButton = new System.Windows.Forms.Button();
            this.nullEventTimer = new System.Windows.Forms.Timer(this.components);
            this.clientsDGV = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.eventTimer = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.closeAllXMDAppsButton = new System.Windows.Forms.Button();
            this.startXMDviewButton = new System.Windows.Forms.Button();
            this.closeXMDviewButton = new System.Windows.Forms.Button();
            this.closeXMDloadButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.clientsDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // serverPortTextBox
            // 
            this.serverPortTextBox.Location = new System.Drawing.Point(166, 167);
            this.serverPortTextBox.Name = "serverPortTextBox";
            this.serverPortTextBox.Size = new System.Drawing.Size(35, 20);
            this.serverPortTextBox.TabIndex = 8;
            this.serverPortTextBox.Text = "9001";
            // 
            // serverButton
            // 
            this.serverButton.Location = new System.Drawing.Point(6, 165);
            this.serverButton.Name = "serverButton";
            this.serverButton.Size = new System.Drawing.Size(122, 23);
            this.serverButton.TabIndex = 6;
            this.serverButton.Text = "Start Remote Server";
            this.serverButton.UseVisualStyleBackColor = true;
            this.serverButton.Click += new System.EventHandler(this.serverButton_Click);
            // 
            // startXMDloadButton
            // 
            this.startXMDloadButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.startXMDloadButton.Location = new System.Drawing.Point(207, 165);
            this.startXMDloadButton.Name = "startXMDloadButton";
            this.startXMDloadButton.Size = new System.Drawing.Size(122, 23);
            this.startXMDloadButton.TabIndex = 9;
            this.startXMDloadButton.Text = "Start GAUGload App";
            this.startXMDloadButton.UseVisualStyleBackColor = true;
            this.startXMDloadButton.Click += new System.EventHandler(this.startXMDloadButton_Click);
            // 
            // nullEventTimer
            // 
            this.nullEventTimer.Enabled = true;
            this.nullEventTimer.Interval = 15000;
            this.nullEventTimer.Tick += new System.EventHandler(this.nullEventTimer_Tick);
            // 
            // clientsDGV
            // 
            this.clientsDGV.AllowUserToAddRows = false;
            this.clientsDGV.AllowUserToDeleteRows = false;
            this.clientsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.clientsDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column4});
            this.clientsDGV.Location = new System.Drawing.Point(6, 0);
            this.clientsDGV.Name = "clientsDGV";
            this.clientsDGV.ReadOnly = true;
            this.clientsDGV.RowHeadersVisible = false;
            this.clientsDGV.Size = new System.Drawing.Size(458, 159);
            this.clientsDGV.TabIndex = 62;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Application";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Alive Count";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 1000;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // eventTimer
            // 
            this.eventTimer.Tick += new System.EventHandler(this.eventTimer_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(134, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 79;
            this.label4.Text = "Port";
            // 
            // closeAllXMDAppsButton
            // 
            this.closeAllXMDAppsButton.Location = new System.Drawing.Point(6, 192);
            this.closeAllXMDAppsButton.Name = "closeAllXMDAppsButton";
            this.closeAllXMDAppsButton.Size = new System.Drawing.Size(122, 23);
            this.closeAllXMDAppsButton.TabIndex = 80;
            this.closeAllXMDAppsButton.Text = "Close All GAUG Apps";
            this.closeAllXMDAppsButton.UseVisualStyleBackColor = true;
            this.closeAllXMDAppsButton.Click += new System.EventHandler(this.closeAllXMDAppsButton_Click);
            // 
            // startXMDviewButton
            // 
            this.startXMDviewButton.Location = new System.Drawing.Point(334, 165);
            this.startXMDviewButton.Name = "startXMDviewButton";
            this.startXMDviewButton.Size = new System.Drawing.Size(122, 23);
            this.startXMDviewButton.TabIndex = 9;
            this.startXMDviewButton.Text = "Start GAUGview App";
            this.startXMDviewButton.UseVisualStyleBackColor = true;
            this.startXMDviewButton.Click += new System.EventHandler(this.startXMDviewButton_Click);
            // 
            // closeXMDviewButton
            // 
            this.closeXMDviewButton.Location = new System.Drawing.Point(334, 192);
            this.closeXMDviewButton.Name = "closeXMDviewButton";
            this.closeXMDviewButton.Size = new System.Drawing.Size(122, 23);
            this.closeXMDviewButton.TabIndex = 9;
            this.closeXMDviewButton.Text = "Close GAUGview App";
            this.closeXMDviewButton.UseVisualStyleBackColor = true;
            this.closeXMDviewButton.Click += new System.EventHandler(this.closeXMDviewButton_Click);
            // 
            // closeXMDloadButton
            // 
            this.closeXMDloadButton.Location = new System.Drawing.Point(207, 192);
            this.closeXMDloadButton.Name = "closeXMDloadButton";
            this.closeXMDloadButton.Size = new System.Drawing.Size(122, 23);
            this.closeXMDloadButton.TabIndex = 9;
            this.closeXMDloadButton.Text = "Close GAUGload App";
            this.closeXMDloadButton.UseVisualStyleBackColor = true;
            this.closeXMDloadButton.Click += new System.EventHandler(this.closeXMDloadButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 223);
            this.Controls.Add(this.closeAllXMDAppsButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.clientsDGV);
            this.Controls.Add(this.closeXMDloadButton);
            this.Controls.Add(this.closeXMDviewButton);
            this.Controls.Add(this.startXMDviewButton);
            this.Controls.Add(this.startXMDloadButton);
            this.Controls.Add(this.serverPortTextBox);
            this.Controls.Add(this.serverButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GAUG Main";
            this.Load += new System.EventHandler(this.XMDInterfaceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.clientsDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox serverPortTextBox;
        private System.Windows.Forms.Button serverButton;
        private System.Windows.Forms.Button startXMDloadButton;
        private System.Windows.Forms.Timer nullEventTimer;
        private System.Windows.Forms.DataGridView clientsDGV;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.Timer eventTimer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button closeAllXMDAppsButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Button startXMDviewButton;
        private System.Windows.Forms.Button closeXMDviewButton;
        private System.Windows.Forms.Button closeXMDloadButton;
    }
}