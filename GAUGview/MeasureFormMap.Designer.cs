namespace GAUGview
{
    partial class MeasureFormMap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MeasureFormMap));
            this.label6 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxAvg = new System.Windows.Forms.TextBox();
            this.textBoxMax = new System.Windows.Forms.TextBox();
            this.textBoxMin = new System.Windows.Forms.TextBox();
            this.labelText5 = new System.Windows.Forms.Label();
            this.labelText4 = new System.Windows.Forms.Label();
            this.labelUnit3 = new System.Windows.Forms.Label();
            this.labelUnit2 = new System.Windows.Forms.Label();
            this.labelUnit1 = new System.Windows.Forms.Label();
            this.labelText3 = new System.Windows.Forms.Label();
            this.tChartProfile = new Steema.TeeChart.TChart();
            this.colorGrid1 = new Steema.TeeChart.Styles.ColorGrid();
            this.fastLine1 = new Steema.TeeChart.Styles.FastLine();
            this.fastLine3 = new Steema.TeeChart.Styles.FastLine();
            this.fastLine4 = new Steema.TeeChart.Styles.FastLine();
            this.fastLine5 = new Steema.TeeChart.Styles.FastLine();
            this.fastLine6 = new Steema.TeeChart.Styles.FastLine();
            this.fastLine7 = new Steema.TeeChart.Styles.FastLine();
            this.fastLine8 = new Steema.TeeChart.Styles.FastLine();
            this.labelText6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(624, 721);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Update Rate";
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox1.Controls.Add(this.textBoxAvg);
            this.groupBox1.Controls.Add(this.textBoxMax);
            this.groupBox1.Controls.Add(this.textBoxMin);
            this.groupBox1.Controls.Add(this.labelText5);
            this.groupBox1.Controls.Add(this.labelText4);
            this.groupBox1.Controls.Add(this.labelUnit3);
            this.groupBox1.Controls.Add(this.labelUnit2);
            this.groupBox1.Controls.Add(this.labelUnit1);
            this.groupBox1.Controls.Add(this.labelText3);
            this.groupBox1.Location = new System.Drawing.Point(890, 130);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(138, 96);
            this.groupBox1.TabIndex = 77;
            this.groupBox1.TabStop = false;
            // 
            // textBoxAvg
            // 
            this.textBoxAvg.Location = new System.Drawing.Point(54, 64);
            this.textBoxAvg.Name = "textBoxAvg";
            this.textBoxAvg.Size = new System.Drawing.Size(52, 20);
            this.textBoxAvg.TabIndex = 1;
            // 
            // textBoxMax
            // 
            this.textBoxMax.Location = new System.Drawing.Point(54, 38);
            this.textBoxMax.Name = "textBoxMax";
            this.textBoxMax.Size = new System.Drawing.Size(52, 20);
            this.textBoxMax.TabIndex = 1;
            // 
            // textBoxMin
            // 
            this.textBoxMin.Location = new System.Drawing.Point(54, 13);
            this.textBoxMin.Name = "textBoxMin";
            this.textBoxMin.Size = new System.Drawing.Size(52, 20);
            this.textBoxMin.TabIndex = 1;
            // 
            // labelText5
            // 
            this.labelText5.AutoSize = true;
            this.labelText5.Location = new System.Drawing.Point(6, 67);
            this.labelText5.Name = "labelText5";
            this.labelText5.Size = new System.Drawing.Size(35, 13);
            this.labelText5.TabIndex = 0;
            this.labelText5.Tag = "TX_AVG";
            this.labelText5.Text = "label1";
            // 
            // labelText4
            // 
            this.labelText4.AutoSize = true;
            this.labelText4.Location = new System.Drawing.Point(6, 41);
            this.labelText4.Name = "labelText4";
            this.labelText4.Size = new System.Drawing.Size(35, 13);
            this.labelText4.TabIndex = 0;
            this.labelText4.Tag = "TX_MAX";
            this.labelText4.Text = "label1";
            // 
            // labelUnit3
            // 
            this.labelUnit3.AutoSize = true;
            this.labelUnit3.Location = new System.Drawing.Point(112, 67);
            this.labelUnit3.Name = "labelUnit3";
            this.labelUnit3.Size = new System.Drawing.Size(35, 13);
            this.labelUnit3.TabIndex = 0;
            this.labelUnit3.Tag = "8";
            this.labelUnit3.Text = "label1";
            // 
            // labelUnit2
            // 
            this.labelUnit2.AutoSize = true;
            this.labelUnit2.Location = new System.Drawing.Point(112, 41);
            this.labelUnit2.Name = "labelUnit2";
            this.labelUnit2.Size = new System.Drawing.Size(35, 13);
            this.labelUnit2.TabIndex = 0;
            this.labelUnit2.Tag = "8";
            this.labelUnit2.Text = "label1";
            // 
            // labelUnit1
            // 
            this.labelUnit1.AutoSize = true;
            this.labelUnit1.Location = new System.Drawing.Point(112, 16);
            this.labelUnit1.Name = "labelUnit1";
            this.labelUnit1.Size = new System.Drawing.Size(35, 13);
            this.labelUnit1.TabIndex = 0;
            this.labelUnit1.Tag = "8";
            this.labelUnit1.Text = "label1";
            // 
            // labelText3
            // 
            this.labelText3.AutoSize = true;
            this.labelText3.Location = new System.Drawing.Point(6, 16);
            this.labelText3.Name = "labelText3";
            this.labelText3.Size = new System.Drawing.Size(35, 13);
            this.labelText3.TabIndex = 0;
            this.labelText3.Tag = "TX_MIN";
            this.labelText3.Text = "label1";
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
            this.tChartProfile.Axes.Bottom.Title.Caption = "  ";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.Axes.Bottom.Title.Font.Brush.Color = System.Drawing.Color.Red;
            // 
            // 
            // 
            this.tChartProfile.Axes.Bottom.Title.Font.Shadow.Visible = false;
            this.tChartProfile.Axes.Bottom.Title.Font.Unit = System.Drawing.GraphicsUnit.World;
            this.tChartProfile.Axes.Bottom.Title.Lines = new string[] {
        "  "};
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
            this.tChartProfile.Axes.Left.Title.Caption = "  ";
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
        "  "};
            // 
            // 
            // 
            this.tChartProfile.Axes.Left.Title.Shadow.Visible = false;
            this.tChartProfile.Axes.Left.Title.Visible = false;
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
            this.tChartProfile.Axes.Right.Visible = false;
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
            this.tChartProfile.Footer.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
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
            this.tChartProfile.Header.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
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
            this.tChartProfile.Legend.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
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
            this.tChartProfile.Legend.Visible = false;
            this.tChartProfile.Location = new System.Drawing.Point(-10, -8);
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
            this.tChartProfile.Series.Add(this.colorGrid1);
            this.tChartProfile.Size = new System.Drawing.Size(789, 303);
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartProfile.SubFooter.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
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
            this.tChartProfile.SubHeader.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
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
            this.tChartProfile.DoubleClick += new System.EventHandler(this.tChartProfile_DoubleClick);
            // 
            // colorGrid1
            // 
            // 
            // 
            // 
            this.colorGrid1.Brush.Color = System.Drawing.Color.Red;
            // 
            // 
            // 
            this.colorGrid1.Marks.ArrowLength = 0;
            // 
            // 
            // 
            this.colorGrid1.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.colorGrid1.Marks.Callout.ArrowHeadSize = 8;
            // 
            // 
            // 
            this.colorGrid1.Marks.Callout.Brush.Color = System.Drawing.Color.Black;
            this.colorGrid1.Marks.Callout.Distance = 0;
            this.colorGrid1.Marks.Callout.Draw3D = false;
            this.colorGrid1.Marks.Callout.Length = 0;
            this.colorGrid1.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            // 
            // 
            // 
            // 
            // 
            // 
            this.colorGrid1.Marks.Font.Shadow.Visible = false;
            this.colorGrid1.Marks.Font.Unit = System.Drawing.GraphicsUnit.World;
            this.colorGrid1.NumXValues = 600;
            this.colorGrid1.NumZValues = 600;
            this.colorGrid1.PaletteMin = 0D;
            this.colorGrid1.PaletteStep = 0D;
            // 
            // 
            // 
            this.colorGrid1.Pen.Visible = false;
            this.colorGrid1.Title = "colorGrid1";
            this.colorGrid1.XStep = 1;
            // 
            // 
            // 
            this.colorGrid1.XValues.DataMember = "X";
            // 
            // 
            // 
            this.colorGrid1.YValues.DataMember = "Y";
            this.colorGrid1.ZStep = 1;
            // 
            // 
            // 
            this.colorGrid1.ZValues.DataMember = "Z";
            // 
            // fastLine1
            // 
            this.fastLine1.DataSource = null;
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
            // fastLine3
            // 
            this.fastLine3.DataSource = null;
            // 
            // 
            // 
            this.fastLine3.LinePen.Color = System.Drawing.Color.Yellow;
            // 
            // 
            // 
            // 
            // 
            // 
            this.fastLine3.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.fastLine3.Marks.Callout.ArrowHeadSize = 8;
            // 
            // 
            // 
            this.fastLine3.Marks.Callout.Brush.Color = System.Drawing.Color.Black;
            this.fastLine3.Marks.Callout.Distance = 0;
            this.fastLine3.Marks.Callout.Draw3D = false;
            this.fastLine3.Marks.Callout.Length = 10;
            this.fastLine3.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            // 
            // 
            // 
            // 
            // 
            // 
            this.fastLine3.Marks.Font.Shadow.Visible = false;
            this.fastLine3.Marks.Font.Unit = System.Drawing.GraphicsUnit.World;
            this.fastLine3.Title = "fastLine3";
            // 
            // 
            // 
            this.fastLine3.XValues.DataMember = "X";
            this.fastLine3.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.fastLine3.YValues.DataMember = "Y";
            // 
            // fastLine4
            // 
            this.fastLine4.DataSource = null;
            // 
            // 
            // 
            this.fastLine4.LinePen.Color = System.Drawing.Color.Blue;
            // 
            // 
            // 
            // 
            // 
            // 
            this.fastLine4.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.fastLine4.Marks.Callout.ArrowHeadSize = 8;
            // 
            // 
            // 
            this.fastLine4.Marks.Callout.Brush.Color = System.Drawing.Color.Black;
            this.fastLine4.Marks.Callout.Distance = 0;
            this.fastLine4.Marks.Callout.Draw3D = false;
            this.fastLine4.Marks.Callout.Length = 10;
            this.fastLine4.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            // 
            // 
            // 
            // 
            // 
            // 
            this.fastLine4.Marks.Font.Shadow.Visible = false;
            this.fastLine4.Marks.Font.Unit = System.Drawing.GraphicsUnit.World;
            this.fastLine4.Title = "fastLine4";
            // 
            // 
            // 
            this.fastLine4.XValues.DataMember = "X";
            this.fastLine4.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.fastLine4.YValues.DataMember = "Y";
            // 
            // fastLine5
            // 
            this.fastLine5.DataSource = null;
            // 
            // 
            // 
            this.fastLine5.LinePen.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            // 
            // 
            // 
            this.fastLine5.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.fastLine5.Marks.Callout.ArrowHeadSize = 8;
            // 
            // 
            // 
            this.fastLine5.Marks.Callout.Brush.Color = System.Drawing.Color.Black;
            this.fastLine5.Marks.Callout.Distance = 0;
            this.fastLine5.Marks.Callout.Draw3D = false;
            this.fastLine5.Marks.Callout.Length = 10;
            this.fastLine5.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            // 
            // 
            // 
            // 
            // 
            // 
            this.fastLine5.Marks.Font.Shadow.Visible = false;
            this.fastLine5.Marks.Font.Unit = System.Drawing.GraphicsUnit.World;
            this.fastLine5.Title = "fastLine5";
            // 
            // 
            // 
            this.fastLine5.XValues.DataMember = "X";
            this.fastLine5.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.fastLine5.YValues.DataMember = "Y";
            // 
            // fastLine6
            // 
            this.fastLine6.DataSource = null;
            // 
            // 
            // 
            this.fastLine6.LinePen.Color = System.Drawing.Color.Gray;
            // 
            // 
            // 
            // 
            // 
            // 
            this.fastLine6.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.fastLine6.Marks.Callout.ArrowHeadSize = 8;
            // 
            // 
            // 
            this.fastLine6.Marks.Callout.Brush.Color = System.Drawing.Color.Black;
            this.fastLine6.Marks.Callout.Distance = 0;
            this.fastLine6.Marks.Callout.Draw3D = false;
            this.fastLine6.Marks.Callout.Length = 10;
            this.fastLine6.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            // 
            // 
            // 
            // 
            // 
            // 
            this.fastLine6.Marks.Font.Shadow.Visible = false;
            this.fastLine6.Marks.Font.Unit = System.Drawing.GraphicsUnit.World;
            this.fastLine6.Title = "fastLine6";
            // 
            // 
            // 
            this.fastLine6.XValues.DataMember = "X";
            this.fastLine6.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.fastLine6.YValues.DataMember = "Y";
            // 
            // fastLine7
            // 
            this.fastLine7.DataSource = null;
            // 
            // 
            // 
            this.fastLine7.LinePen.Color = System.Drawing.Color.Fuchsia;
            // 
            // 
            // 
            // 
            // 
            // 
            this.fastLine7.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.fastLine7.Marks.Callout.ArrowHeadSize = 8;
            // 
            // 
            // 
            this.fastLine7.Marks.Callout.Brush.Color = System.Drawing.Color.Black;
            this.fastLine7.Marks.Callout.Distance = 0;
            this.fastLine7.Marks.Callout.Draw3D = false;
            this.fastLine7.Marks.Callout.Length = 10;
            this.fastLine7.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            // 
            // 
            // 
            // 
            // 
            // 
            this.fastLine7.Marks.Font.Shadow.Visible = false;
            this.fastLine7.Marks.Font.Unit = System.Drawing.GraphicsUnit.World;
            this.fastLine7.Title = "fastLine7";
            // 
            // 
            // 
            this.fastLine7.XValues.DataMember = "X";
            this.fastLine7.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.fastLine7.YValues.DataMember = "Y";
            // 
            // fastLine8
            // 
            this.fastLine8.DataSource = null;
            // 
            // 
            // 
            this.fastLine8.LinePen.Color = System.Drawing.Color.Teal;
            // 
            // 
            // 
            // 
            // 
            // 
            this.fastLine8.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.fastLine8.Marks.Callout.ArrowHeadSize = 8;
            // 
            // 
            // 
            this.fastLine8.Marks.Callout.Brush.Color = System.Drawing.Color.Black;
            this.fastLine8.Marks.Callout.Distance = 0;
            this.fastLine8.Marks.Callout.Draw3D = false;
            this.fastLine8.Marks.Callout.Length = 10;
            this.fastLine8.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            // 
            // 
            // 
            // 
            // 
            // 
            this.fastLine8.Marks.Font.Shadow.Visible = false;
            this.fastLine8.Marks.Font.Unit = System.Drawing.GraphicsUnit.World;
            this.fastLine8.Title = "fastLine8";
            // 
            // 
            // 
            this.fastLine8.XValues.DataMember = "X";
            this.fastLine8.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.fastLine8.YValues.DataMember = "Y";
            // 
            // labelText6
            // 
            this.labelText6.AutoSize = true;
            this.labelText6.BackColor = System.Drawing.Color.Transparent;
            this.labelText6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelText6.Location = new System.Drawing.Point(819, 9);
            this.labelText6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelText6.Name = "labelText6";
            this.labelText6.Size = new System.Drawing.Size(102, 15);
            this.labelText6.TabIndex = 79;
            this.labelText6.Tag = "";
            this.labelText6.Text = "Cframe Back Arm";
            // 
            // MeasureFormMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1302, 515);
            this.ControlBox = false;
            this.Controls.Add(this.labelText6);
            this.Controls.Add(this.tChartProfile);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MeasureFormMap";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "   Measure Trend Length Data";
            this.Load += new System.EventHandler(this.MeasureFormMap_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelText5;
        private System.Windows.Forms.Label labelText4;
        private System.Windows.Forms.Label labelText3;
        private System.Windows.Forms.TextBox textBoxAvg;
        private System.Windows.Forms.TextBox textBoxMax;
        private System.Windows.Forms.TextBox textBoxMin;
        private System.Windows.Forms.Label labelUnit1;
        private System.Windows.Forms.Label labelUnit3;
        private System.Windows.Forms.Label labelUnit2;
        private Steema.TeeChart.TChart tChartProfile;
        private Steema.TeeChart.Styles.FastLine fastLine1;
        private Steema.TeeChart.Styles.FastLine fastLine3;
        private Steema.TeeChart.Styles.FastLine fastLine4;
        private Steema.TeeChart.Styles.FastLine fastLine5;
        private Steema.TeeChart.Styles.FastLine fastLine6;
        private Steema.TeeChart.Styles.FastLine fastLine7;
        private Steema.TeeChart.Styles.FastLine fastLine8;
        private Steema.TeeChart.Styles.ColorGrid colorGrid1;
        private System.Windows.Forms.Label labelText6;
    }
}