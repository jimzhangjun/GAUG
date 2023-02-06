namespace GAUGview
{
    partial class MeasureFormTrendTime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MeasureFormTrendTime));
            this.label6 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.tChartProfile = new Steema.TeeChart.TChart();
            this.fastLine1 = new Steema.TeeChart.Styles.FastLine();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(832, 887);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 17);
            this.label6.TabIndex = 22;
            this.label6.Text = "Update Rate";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // tChartProfile
            // 
            // 
            // 
            // 
            this.tChartProfile.Aspect.ElevationFloat = 345D;
            this.tChartProfile.Aspect.RotationFloat = 345D;
            this.tChartProfile.Aspect.View3D = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.Axes.Bottom.Automatic = true;
            // 
            // 
            // 
            this.tChartProfile.Axes.Bottom.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash;
            this.tChartProfile.Axes.Bottom.Grid.ZPosition = 0D;
            this.tChartProfile.Axes.Bottom.Increment = 32D;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.Axes.Bottom.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.tChartProfile.Axes.Bottom.Labels.Font.Shadow.Visible = false;
            this.tChartProfile.Axes.Bottom.Labels.Font.Unit = System.Drawing.GraphicsUnit.World;
            // 
            // 
            // 
            this.tChartProfile.Axes.Bottom.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartProfile.Axes.Bottom.Title.Caption = "Detector Number";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.Axes.Bottom.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.tChartProfile.Axes.Bottom.Title.Font.Shadow.Visible = false;
            this.tChartProfile.Axes.Bottom.Title.Font.Unit = System.Drawing.GraphicsUnit.World;
            this.tChartProfile.Axes.Bottom.Title.Lines = new string[] {
        "Detector Number"};
            // 
            // 
            // 
            this.tChartProfile.Axes.Bottom.Title.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartProfile.Axes.Depth.Automatic = true;
            // 
            // 
            // 
            this.tChartProfile.Axes.Depth.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash;
            this.tChartProfile.Axes.Depth.Grid.ZPosition = 0D;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.Axes.Depth.Labels.Font.Shadow.Visible = false;
            this.tChartProfile.Axes.Depth.Labels.Font.Unit = System.Drawing.GraphicsUnit.World;
            // 
            // 
            // 
            this.tChartProfile.Axes.Depth.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.Axes.Depth.Title.Font.Shadow.Visible = false;
            this.tChartProfile.Axes.Depth.Title.Font.Unit = System.Drawing.GraphicsUnit.World;
            // 
            // 
            // 
            this.tChartProfile.Axes.Depth.Title.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartProfile.Axes.DepthTop.Automatic = true;
            // 
            // 
            // 
            this.tChartProfile.Axes.DepthTop.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash;
            this.tChartProfile.Axes.DepthTop.Grid.ZPosition = 0D;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.Axes.DepthTop.Labels.Font.Shadow.Visible = false;
            this.tChartProfile.Axes.DepthTop.Labels.Font.Unit = System.Drawing.GraphicsUnit.World;
            // 
            // 
            // 
            this.tChartProfile.Axes.DepthTop.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.Axes.DepthTop.Title.Font.Shadow.Visible = false;
            this.tChartProfile.Axes.DepthTop.Title.Font.Unit = System.Drawing.GraphicsUnit.World;
            // 
            // 
            // 
            this.tChartProfile.Axes.DepthTop.Title.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartProfile.Axes.Left.Automatic = true;
            // 
            // 
            // 
            this.tChartProfile.Axes.Left.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash;
            this.tChartProfile.Axes.Left.Grid.ZPosition = 0D;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.Axes.Left.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.tChartProfile.Axes.Left.Labels.Font.Shadow.Visible = false;
            this.tChartProfile.Axes.Left.Labels.Font.Unit = System.Drawing.GraphicsUnit.World;
            // 
            // 
            // 
            this.tChartProfile.Axes.Left.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartProfile.Axes.Left.Title.Caption = "Signal Level";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.Axes.Left.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.tChartProfile.Axes.Left.Title.Font.Shadow.Visible = false;
            this.tChartProfile.Axes.Left.Title.Font.Unit = System.Drawing.GraphicsUnit.World;
            this.tChartProfile.Axes.Left.Title.Lines = new string[] {
        "Signal Level"};
            // 
            // 
            // 
            this.tChartProfile.Axes.Left.Title.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartProfile.Axes.Right.Automatic = true;
            // 
            // 
            // 
            this.tChartProfile.Axes.Right.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash;
            this.tChartProfile.Axes.Right.Grid.ZPosition = 0D;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.Axes.Right.Labels.Font.Shadow.Visible = false;
            this.tChartProfile.Axes.Right.Labels.Font.Unit = System.Drawing.GraphicsUnit.World;
            // 
            // 
            // 
            this.tChartProfile.Axes.Right.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.Axes.Right.Title.Font.Shadow.Visible = false;
            this.tChartProfile.Axes.Right.Title.Font.Unit = System.Drawing.GraphicsUnit.World;
            // 
            // 
            // 
            this.tChartProfile.Axes.Right.Title.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartProfile.Axes.Top.Automatic = true;
            // 
            // 
            // 
            this.tChartProfile.Axes.Top.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash;
            this.tChartProfile.Axes.Top.Grid.ZPosition = 0D;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.Axes.Top.Labels.Font.Shadow.Visible = false;
            this.tChartProfile.Axes.Top.Labels.Font.Unit = System.Drawing.GraphicsUnit.World;
            // 
            // 
            // 
            this.tChartProfile.Axes.Top.Labels.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.Axes.Top.Title.Font.Shadow.Visible = false;
            this.tChartProfile.Axes.Top.Title.Font.Unit = System.Drawing.GraphicsUnit.World;
            // 
            // 
            // 
            this.tChartProfile.Axes.Top.Title.Shadow.Visible = false;
            this.tChartProfile.BackColor = System.Drawing.Color.Transparent;
            this.tChartProfile.Cursor = System.Windows.Forms.Cursors.Default;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.Footer.Font.Shadow.Visible = false;
            this.tChartProfile.Footer.Font.Unit = System.Drawing.GraphicsUnit.World;
            // 
            // 
            // 
            this.tChartProfile.Footer.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.Header.Font.Shadow.Visible = false;
            this.tChartProfile.Header.Font.Unit = System.Drawing.GraphicsUnit.World;
            this.tChartProfile.Header.Lines = new string[] {
        ""};
            // 
            // 
            // 
            this.tChartProfile.Header.Shadow.Visible = false;
            this.tChartProfile.Header.Visible = false;
            // 
            // 
            // 
            this.tChartProfile.Legend.CheckBoxes = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.Legend.Font.Shadow.Visible = false;
            this.tChartProfile.Legend.Font.Unit = System.Drawing.GraphicsUnit.World;
            this.tChartProfile.Legend.LegendStyle = Steema.TeeChart.LegendStyles.Series;
            // 
            // 
            // 
            this.tChartProfile.Legend.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartProfile.Legend.Symbol.Width = 10;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.Legend.Title.Font.Bold = true;
            // 
            // 
            // 
            this.tChartProfile.Legend.Title.Font.Shadow.Visible = false;
            this.tChartProfile.Legend.Title.Font.Unit = System.Drawing.GraphicsUnit.World;
            // 
            // 
            // 
            this.tChartProfile.Legend.Title.Pen.Visible = false;
            // 
            // 
            // 
            this.tChartProfile.Legend.Title.Shadow.Visible = false;
            this.tChartProfile.Location = new System.Drawing.Point(-13, -10);
            this.tChartProfile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tChartProfile.Name = "tChartProfile";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.Panel.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChartProfile.Panel.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tChartProfile.Panel.MarginBottom = 2D;
            this.tChartProfile.Panel.MarginLeft = 2D;
            this.tChartProfile.Panel.MarginRight = 2D;
            this.tChartProfile.Panel.MarginTop = 2D;
            // 
            // 
            // 
            this.tChartProfile.Panel.Shadow.Visible = false;
            this.tChartProfile.Series.Add(this.fastLine1);
            this.tChartProfile.Size = new System.Drawing.Size(1052, 373);
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.SubFooter.Font.Shadow.Visible = false;
            this.tChartProfile.SubFooter.Font.Unit = System.Drawing.GraphicsUnit.World;
            // 
            // 
            // 
            this.tChartProfile.SubFooter.Shadow.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.SubHeader.Font.Shadow.Visible = false;
            this.tChartProfile.SubHeader.Font.Unit = System.Drawing.GraphicsUnit.World;
            // 
            // 
            // 
            this.tChartProfile.SubHeader.Shadow.Visible = false;
            this.tChartProfile.TabIndex = 78;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.Walls.Back.AutoHide = false;
            // 
            // 
            // 
            this.tChartProfile.Walls.Back.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartProfile.Walls.Bottom.AutoHide = false;
            // 
            // 
            // 
            this.tChartProfile.Walls.Bottom.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartProfile.Walls.Left.AutoHide = false;
            // 
            // 
            // 
            this.tChartProfile.Walls.Left.Shadow.Visible = false;
            // 
            // 
            // 
            this.tChartProfile.Walls.Right.AutoHide = false;
            // 
            // 
            // 
            this.tChartProfile.Walls.Right.Shadow.Visible = false;
            // 
            // fastLine1
            // 
            // 
            // 
            // 
            this.fastLine1.LinePen.Color = System.Drawing.Color.Red;
            // 
            // 
            // 
            // 
            // 
            // 
            this.fastLine1.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.fastLine1.Marks.Callout.ArrowHeadSize = 8;
            // 
            // 
            // 
            this.fastLine1.Marks.Callout.Brush.Color = System.Drawing.Color.Black;
            this.fastLine1.Marks.Callout.Distance = 0;
            this.fastLine1.Marks.Callout.Draw3D = false;
            this.fastLine1.Marks.Callout.Length = 10;
            this.fastLine1.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            // 
            // 
            // 
            // 
            // 
            // 
            this.fastLine1.Marks.Font.Shadow.Visible = false;
            this.fastLine1.Marks.Font.Unit = System.Drawing.GraphicsUnit.World;
            this.fastLine1.Title = "fastLine1";
            // 
            // 
            // 
            this.fastLine1.XValues.DataMember = "X";
            this.fastLine1.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.fastLine1.YValues.DataMember = "Y";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "StripSpeed",
            "AGT",
            "DetTemp",
            "DewPoint",
            "S1TubeTemp",
            "S2TubeTemp",
            "S1AirTemp",
            "S2AirTemp",
            "S1Kvs",
            "S1Mas",
            "S2Kvs",
            "S2Mas",
            "MillPyroTemp",
            "MillStripAngle",
            "AISPare3",
            "AISpare4"});
            this.comboBox1.Location = new System.Drawing.Point(1033, 14);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(183, 24);
            this.comboBox1.TabIndex = 79;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // MeasureFormTrendTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1736, 634);
            this.ControlBox = false;
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.tChartProfile);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MeasureFormTrendTime";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "   Measure Trend Length Data";
            this.Load += new System.EventHandler(this.MeasureFormTrendTime_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private Steema.TeeChart.TChart tChartProfile;
        private Steema.TeeChart.Styles.FastLine fastLine1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}