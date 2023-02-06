namespace GAUGcenter
{
    partial class SplashScreenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreenForm));
            this.Fadetimer = new System.Windows.Forms.Timer(this.components);
            this.Loadtimer = new System.Windows.Forms.Timer(this.components);
            this.loadProgressBar = new System.Windows.Forms.ProgressBar();
            this.versionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Fadetimer
            // 
            this.Fadetimer.Interval = 10;
            this.Fadetimer.Tick += new System.EventHandler(this.Fadetimer_Tick);
            // 
            // Loadtimer
            // 
            this.Loadtimer.Tick += new System.EventHandler(this.Loadtimer_Tick);
            // 
            // loadProgressBar
            // 
            this.loadProgressBar.BackColor = System.Drawing.Color.Black;
            this.loadProgressBar.ForeColor = System.Drawing.Color.Yellow;
            this.loadProgressBar.Location = new System.Drawing.Point(51, 251);
            this.loadProgressBar.MarqueeAnimationSpeed = 150;
            this.loadProgressBar.Maximum = 10;
            this.loadProgressBar.Name = "loadProgressBar";
            this.loadProgressBar.Size = new System.Drawing.Size(219, 22);
            this.loadProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.loadProgressBar.TabIndex = 1;
            this.loadProgressBar.UseWaitCursor = true;
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.BackColor = System.Drawing.Color.Black;
            this.versionLabel.ForeColor = System.Drawing.Color.White;
            this.versionLabel.Location = new System.Drawing.Point(230, 9);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(40, 13);
            this.versionLabel.TabIndex = 3;
            this.versionLabel.Text = "0.0.0.0";
            this.versionLabel.UseWaitCursor = true;
            // 
            // SplashScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(329, 327);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.loadProgressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SplashScreenForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LOADING";
            this.TopMost = true;
            this.UseWaitCursor = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer Fadetimer;
        private System.Windows.Forms.Timer Loadtimer;
        private System.Windows.Forms.ProgressBar loadProgressBar;
        private System.Windows.Forms.Label versionLabel;
    }
}