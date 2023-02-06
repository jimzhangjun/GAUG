//=================================================================================================================
//  Project:    RM312/SIPRO SUPERvisor
//  Module:     InfoFormEnquire.cs                                                                         
//  Author:     Andrew Powell
//  Date:       21/03/2006
//  
//  Details:    Display form for visualising measurement data
//  
//=================================================================================================================
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.IO;

using Microsoft.ApplicationBlocks.ExceptionManagement;
using GAUGlib;

namespace GAUGview
{
    public partial class InfoFormEnquire : Form
    {
        //---------------------------------------------------------------------------------------------------------
        // CLASS VARIABLES
        //---------------------------------------------------------------------------------------------------------
        private const int ROWNUM = 7;
        //private const int COLNUM = 6;
        private MeasDataClass avgData = new MeasDataClass();

        private ViewMainForm myOwner = null;
        private FormCfgClass myFormCfg = null;
        private int formIndex = -1;
        private int widthCfg;
        private int heightCfg;
        private int offsetCfg;

        private DataTable aDataTable = new DataTable();

        //private Counter cntUpdate = new Counter();
        //---------------------------------------------------------------------------------------------------------
        // GLOBAL PROCEDURES
        //---------------------------------------------------------------------------------------------------------
        public InfoFormEnquire(Form OwnerForm, int width, int height, int offset, int index)
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

            // Checkbox            
            checkBoxCoilid.Text = FileClass.textItem.GetTextItem(checkBoxCoilid.Tag.ToString(), ConfigDataClass.view.language);
            checkBoxPassnumber.Text = FileClass.textItem.GetTextItem(checkBoxPassnumber.Tag.ToString(), ConfigDataClass.view.language);
            checkBoxLastpass.Text = FileClass.textItem.GetTextItem(checkBoxLastpass.Tag.ToString(), ConfigDataClass.view.language);

            // Radiobutton
            radioButton1.Text = FileClass.textItem.GetTextItem(radioButton1.Tag.ToString(), ConfigDataClass.view.language);
            radioButton2.Text = FileClass.textItem.GetTextItem(radioButton2.Tag.ToString(), ConfigDataClass.view.language);

            // Button
            buttonEnquire.Text = FileClass.textItem.GetTextItem(buttonEnquire.Tag.ToString(), ConfigDataClass.view.language);
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
            //dateTimePicker
            dateTimePickerStart.BackColor = myFormCfg.txtBackColorWrite;
            dateTimePickerStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dateTimePickerStart.ShowUpDown = true;

            dateTimePickerEnd.BackColor = myFormCfg.txtBackColorWrite;
            dateTimePickerEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dateTimePickerEnd.ShowUpDown = true;

            //Text
            textBoxCoilID.BackColor = myFormCfg.txtBackColorWrite;
            textBoxPassnumber.BackColor = myFormCfg.txtBackColorWrite;

            //Listbox??
        }
        //-- Update the size and location
        private void UpdateSizeAndLocation()
        {
            this.Left = (int)(myOwner.Width * (myFormCfg.location.left / widthCfg));
            this.Top = (int)((myOwner.Height - DISPLAYPARAMS.heighttab * 3) * (myFormCfg.location.top / heightCfg)) + offsetCfg;
            this.Width = (int)(myOwner.Width * (myFormCfg.location.width / widthCfg));
            this.Height = (int)((myOwner.Height - DISPLAYPARAMS.heighttab * 3) * (myFormCfg.location.height / heightCfg));

            // 1 col of label, 1 col of dateTimePicker, 1 col of listBox, 2 cols of space
            // 0: space 1: Label    2: TimePicker   3: TextBox  4: ListBox
            int cols = myFormCfg.widthscale[1] + myFormCfg.widthscale[2] + myFormCfg.widthscale[4] + myFormCfg.widthscale[0] * 2;
            int width = this.Width / cols;
            int height = this.Height / ROWNUM;
            int textheight = height - DISPLAYPARAMS.offsettop * 2;

            //-- Col 1 ---------------------------------------------------------
            // label - DateTime
            labelProd2.Left = DISPLAYPARAMS.offsetleft;
            labelProd2.Top = DISPLAYPARAMS.offsettop;
            labelProd2.Height = textheight;

            // dateTimePicker - Start
            labelProd0.Left = DISPLAYPARAMS.offsetleft;
            labelProd0.Top = height + DISPLAYPARAMS.offsettop;
            labelProd0.Height = textheight;

            dateTimePickerStart.Left = labelProd0.Left + labelProd0.Width + DISPLAYPARAMS.offsetleft;
            dateTimePickerStart.Top = height + DISPLAYPARAMS.offsettop;
            dateTimePickerStart.Width = width * myFormCfg.widthscale[2] - DISPLAYPARAMS.offsetleft;
            dateTimePickerStart.Height = textheight;

            // dateTimePicker - End
            labelProd1.Left = DISPLAYPARAMS.offsetleft;
            labelProd1.Top = height * 2 + DISPLAYPARAMS.offsettop;
            labelProd1.Height = textheight;

            dateTimePickerEnd.Left = dateTimePickerStart.Left;
            dateTimePickerEnd.Top = height * 2 + DISPLAYPARAMS.offsettop;
            dateTimePickerEnd.Width = width * myFormCfg.widthscale[2] - DISPLAYPARAMS.offsetleft;
            dateTimePickerEnd.Height = textheight;

            // checkBox - Coil ID
            checkBoxCoilid.Left = DISPLAYPARAMS.offsetleft;
            checkBoxCoilid.Top = height * 3 + DISPLAYPARAMS.offsettop;
            checkBoxCoilid.Height = textheight;

            // textBox - Coil ID
            textBoxCoilID.Left = DISPLAYPARAMS.offsetleft;
            textBoxCoilID.Top = height * 4 + DISPLAYPARAMS.offsettop;
            textBoxCoilID.Width = width * myFormCfg.widthscale[3] - DISPLAYPARAMS.offsetleft;
            textBoxCoilID.Height = textheight;

            // checkBox - Pass number
            checkBoxPassnumber.Left = DISPLAYPARAMS.offsetleft;
            checkBoxPassnumber.Top = height * 5 + DISPLAYPARAMS.offsettop;
            checkBoxPassnumber.Height = textheight;

            // textBox - Pass number
            textBoxPassnumber.Left = myFormCfg.widthscale[1] * width + DISPLAYPARAMS.offsetleft;
            textBoxPassnumber.Top = height * 5 + DISPLAYPARAMS.offsettop;
            textBoxPassnumber.Width = width * (myFormCfg.widthscale[3] - myFormCfg.widthscale[1] )- DISPLAYPARAMS.offsetleft;
            textBoxPassnumber.Height = textheight;

            // checkBox - Last pass
            checkBoxLastpass.Left = DISPLAYPARAMS.offsetleft;
            checkBoxLastpass.Top = height * 6 + DISPLAYPARAMS.offsettop;
            checkBoxLastpass.Height = textheight;

            // Radiobutton
            radioButton1.Left = dateTimePickerStart.Left + dateTimePickerStart.Width + DISPLAYPARAMS.offsetleft;
            radioButton1.Top = height * 1 + DISPLAYPARAMS.offsettop;
            radioButton1.Height = textheight;

            radioButton2.Left = dateTimePickerStart.Left + dateTimePickerStart.Width + DISPLAYPARAMS.offsetleft;
            radioButton2.Top = height * 2 + DISPLAYPARAMS.offsettop;
            radioButton2.Height = textheight;

            radioButton3.Visible = false;       // total is not used

            // Button
            buttonEnquire.Left = width * myFormCfg.widthscale[3] + DISPLAYPARAMS.offsetleft;
            buttonEnquire.Top = height * 3 + DISPLAYPARAMS.offsettop * 3;
            buttonEnquire.Width = radioButton1.Left + radioButton1.Width - textBoxCoilID.Width - DISPLAYPARAMS.offsetleft;
            buttonEnquire.Height = textheight * 4;

            // Listbox1
            listView1.Left = buttonEnquire.Left + buttonEnquire.Width + DISPLAYPARAMS.offsetleft;
            listView1.Top = DISPLAYPARAMS.offsettop;
            listView1.Width = this.Width - listView1.Left - DISPLAYPARAMS.offsetleft * 3;
            listView1.Height = height * 7;
        }
        //-- Demo -------------------------------------------------------------------------------------------
        private void Demo()
        {
            textBoxCoilID.Text = "CoilID-1234";
            textBoxPassnumber.Text = "1";
        }
        private void doFileSearch()
        {
            try
            {
                //dateTimePickerStart.Value = DateTime.Parse(dateTimePickerStart.Value.Year.ToString() + "/" + dateTimePickerStart.Value.Month.ToString().PadLeft(2, '0') + "/" + dateTimePickerStart.Value.Day.ToString().PadLeft(2, '0'));
                //dateTimePickerEnd.Value = DateTime.Parse(dateTimePickerEnd.Value.Year.ToString() + "/" + dateTimePickerEnd.Value.Month.ToString().PadLeft(2, '0') + "/" + dateTimePickerEnd.Value.Day.ToString().PadLeft(2, '0') + " 23:59:59");

                // check the date for searching - the start/end of the month
                int totalmonth = 1 + dateTimePickerEnd.Value.Month - dateTimePickerStart.Value.Month + (dateTimePickerEnd.Value.Year - dateTimePickerStart.Value.Year) * 12;
                if (totalmonth <= 0) return;

                listView1.Items.Clear();

                int iyear, imonth;
                string sCoilFilePath;

                // for each month, to open one coil report file
                // put all valid files into the list
                for (int i = 0; i < totalmonth; i++)
                {
                    imonth = dateTimePickerStart.Value.Month + i;
                    iyear = (imonth - 1) / 12;
                    imonth -= (12 * iyear);
                    iyear += dateTimePickerStart.Value.Year;

                    ArchiveCfgClass archive = ConfigDataClass.archive;
                    if (archive.pathformat == 1)
                        sCoilFilePath = archive.database + iyear.ToString() + "\\" + imonth.ToString().PadLeft(2, '0') + "\\";
                    else
                        sCoilFilePath = archive.database + iyear.ToString() + imonth.ToString().PadLeft(2, '0') + "\\";
                    loadFileSystem(sCoilFilePath);
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.Publish(exc);
                // ErrorForm.AddException(exc, "Measure Form Data");
            }
        }
        private void loadFileSystem(string path)
        {
            try
            {
                string[] files = System.IO.Directory.GetFileSystemEntries(path, "*.csv");   // coildid_Profile_cold_top_yyyymmdd_hhmm.csv
                DateTime filetime;
                for (int i = 0; i < files.Length; i++)
                {
                    filetime = GetTimeFromFile(files[i]);

                    string imgName = files[i].Replace(path, "");
                    string passnumber = "_" + textBoxPassnumber.Text.Trim() + "_";
                    string lastPass = "1>";

                    string gaugeName = "_Gauge 1";
                    if (radioButton2.Checked) gaugeName = "_Gauge 2";

                    if (imgName.ToString().Contains(gaugeName))
                        imgName = imgName.Replace(gaugeName, "");
                    else continue;

                    imgName = imgName.Replace(".csv", "");
                    string imgName1 = imgName.Substring(imgName.Length - 13);   // date and time: keep "yyyymmdd_hhmm"
                    string imgName2 = imgName.Remove(imgName.Length - 14);      // coil id: remove "_yyyymmdd_hhmm"
                    imgName = imgName1 + "_" + imgName2 + ">";                  // yyyymmdd_hhmm_coilid>

                    ListViewItem item1 = new ListViewItem(imgName);                    
                    item1.Name = path;
                    item1.Tag = imgName;

                    if (filetime.Ticks > dateTimePickerStart.Value.Ticks && filetime.Ticks < dateTimePickerEnd.Value.Ticks &&
                        (checkBoxCoilid.Checked == false || item1.Tag.ToString().Contains(textBoxCoilID.Text.Trim())))
                    {
                        if(checkBoxLastpass.Checked)     // is last pass, any pass number
                        {
                            if (item1.Tag.ToString().Contains(lastPass)) listView1.Items.Add(item1);
                            else continue;
                        }
                        else if (checkBoxPassnumber.Checked == false || item1.Tag.ToString().Contains(passnumber))
                        {
                            listView1.Items.Add(item1);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("EXCEPTION in loadFileSystem", exc);
            }
        }
        //-----------------------------------------------------------------------------------------
        //-- Display about box application properties ---------------------------------------------
        private bool ReadReport(string filenameG1, string filenameG2, int fileType)
        {
            try
            {
                FileInfo aFile;
                if (radioButton2.Checked)
                {
                    aFile = new FileInfo(filenameG2);
                }
                else
                {
                    aFile = new FileInfo(filenameG1);
                }                

                if (aFile.Exists)
                {
                    StreamReader sReader = new StreamReader(aFile.OpenRead());
                    CsvParserClass aCsvParser = new CsvParserClass();
                    if (fileType == 1) aDataTable = CsvParserClass.Parse(sReader, true);
                }

                return true;
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("EXCEPTION in ReadReport", exc);
                return false;
            }
        }
        private DateTime GetTimeFromFile(string file)
        {
            int length = file.Length;

            // coildid_passnumber_lastpass_Gauge 1/2_yyyymmdd_hhmm.csv
            //
            // 20160919_1238
            // 17  1311 8 6
            DateTime time = new DateTime(Int32.Parse(file.Substring(length - 17, 4).ToString()), Int32.Parse(file.Substring(length - 13, 2).ToString()), Int32.Parse(file.Substring(length - 11, 2).ToString()))
                          + new TimeSpan(0, Int32.Parse(file.Substring(length - 8, 2).ToString()), Int32.Parse(file.Substring(length - 6, 2).ToString()), 0);

            return time;

        }
        //---------------------------------------------------------------------------------------------------------
        // LOCAL EVENTS
        //--------------------------------------------------------------------------------------------------------- 
        private void InfoFormEnquire_Load(object sender, EventArgs e)
        {
            UpdateLanguage();
            UpdateColorAndFont();
            UpdateSizeAndLocation();

            timer1.Interval = myFormCfg.timer[0];
            timer2.Interval = myFormCfg.timer[1];

            switch (ConfigDataClass.view.mode)
            {
                case (int)DataSort.Source.DEMOFILE:
                    Demo();
                    break;
            }
        }
        private void buttonEnquire_Click(object sender, EventArgs e)
        {
            switch (ConfigDataClass.view.mode)
            {
                case (int)DataSort.Source.CSVFILE:
                    doFileSearch();
                    break;
            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (ConfigDataClass.view.mode)
                {
                    case (int)DataSort.Source.CSVFILE:
                        {
                            labelFileNameG1.Text = "";
                            labelFileNameG2.Text = "";

                            int whichGauge = 1;
                            if (radioButton2.Checked) whichGauge = 2;

                            ListViewItem item1;
                            string[] sArray;
                            if (listView1.SelectedItems.Count > 0)
                            {
                                item1 = listView1.SelectedItems[0];
                                sArray = item1.Tag.ToString().Split('_');
                            }
                            else return;

                            string[] files = System.IO.Directory.GetFileSystemEntries(item1.Name, "*.csv");
                            for (int i = 0; i < files.Length; i++)
                            {
                                switch (whichGauge)
                                {
                                    case 1:
                                        if (files[i].Contains(sArray[0].ToString()) && files[i].Contains(sArray[1].ToString()) && files[i].Contains(sArray[2].ToString())
                                            && files[i].Contains("_Gauge 1_"))
                                        {
                                            labelFileNameG1.Text = files[i].ToString();
                                        }
                                        break;
                                    case 2:
                                        if (files[i].Contains(sArray[0].ToString()) && files[i].Contains(sArray[1].ToString()) && files[i].Contains(sArray[2].ToString())
                                            && files[i].Contains("_Gauge 2_"))
                                        {
                                            labelFileNameG2.Text = files[i].ToString();
                                        }
                                        break;
                                }
                            }

                            // put data from file to table
                            ReadReport(labelFileNameG1.Text.ToString(), labelFileNameG2.Text.ToString(), ConfigDataClass.archive.fileType);

                            if (RemoteInterfaceClass.connected)
                            {
                                switch (ConfigDataClass.archive.fileType)
                                {
                                    case 1: // Coil report
                                        {
                                            CoilDataClass aGauge = RemoteInterfaceClass.XMD.GetCoilData(whichGauge);

                                            // Product
                                            aGauge.product.coilid = aDataTable.Rows[(int)CoilTable.row.Product][(int)CoilTable.colProduct.Coilid].ToString();
                                            aGauge.product.setpoint[0] = Convert.ToSingle(aDataTable.Rows[(int)CoilTable.row.Product][(int)CoilTable.colProduct.Setpoint].ToString());
                                            aGauge.product.ptol[0] = Convert.ToSingle(aDataTable.Rows[(int)CoilTable.row.Product][(int)CoilTable.colProduct.PTol].ToString());
                                            aGauge.product.mtol[0] = Convert.ToSingle(aDataTable.Rows[(int)CoilTable.row.Product][(int)CoilTable.colProduct.MTol].ToString());
                                            aGauge.product.factor[0] = Convert.ToSingle(aDataTable.Rows[(int)CoilTable.row.Product][(int)CoilTable.colProduct.Alloyfactor].ToString());
                                            aGauge.product.offset[0] = Convert.ToSingle(aDataTable.Rows[(int)CoilTable.row.Product][(int)CoilTable.colProduct.Offset].ToString());
                                            aGauge.product.passNumber = Convert.ToInt16(aDataTable.Rows[(int)CoilTable.row.Product][(int)CoilTable.colProduct.Passnumber].ToString());
                                            aGauge.product.lastPass = Convert.ToInt16(aDataTable.Rows[(int)CoilTable.row.Product][(int)CoilTable.colProduct.Lastpass].ToString());

                                            // Data
                                            //int numElem = Convert.ToInt16(aDataTable.Rows[(int)CoilTable.row.Product][(int)CoilTable.colProduct.Elemnumber].ToString());
                                            //aGauge.product.dataNumber = numElem;
                                            int i = 0;
                                            try
                                            {
                                                while (true)
                                                {
                                                    if (aDataTable.Rows[(int)CoilTable.row.Data+i][(int)CoilTable.colData.MeasStatus].ToString() == "") break;
                                                    aGauge.data[i].length = Convert.ToSingle(aDataTable.Rows[(int)CoilTable.row.Data + i][(int)CoilTable.colData.Length].ToString());
                                                    aGauge.data[i].measThick = Convert.ToSingle(aDataTable.Rows[(int)CoilTable.row.Data + i][(int)CoilTable.colData.MeasThick].ToString());
                                                    i++;
                                                }
                                            }
                                            catch (Exception exc)
                                            {
                                                ErrorHandlerClass.ReportException("Counting the data until " + i.ToString(), exc);
                                            }
                                            aGauge.product.dataNumber = i;

                                            // Summary

                                            //--
                                            RemoteInterfaceClass.XMD.SetCoilData(ref aGauge, whichGauge);
                                            RemoteInterfaceClass.XMD.UpdateCoilCount(whichGauge);
                                        }
                                        break;

                                    case 2: // Profile report - only take the product and coil here, the profile data will be taken in the timer 2 one by one
                                        {
                                            CoilDataClass aGaugeCoil = RemoteInterfaceClass.XMD.GetCoilData(whichGauge);

                                            // Product
                                            aGaugeCoil.product.coilid = aDataTable.Rows[(int)ProfileTable.row.Product][(int)ProfileTable.colProduct.Coilid].ToString();
                                            aGaugeCoil.product.measmode = Convert.ToInt16(aDataTable.Rows[(int)ProfileTable.row.Product][(int)ProfileTable.colProduct.MeasMode].ToString());                                            
                                            aGaugeCoil.product.setpoint[0] = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Product][(int)ProfileTable.colProduct.Setpoint1].ToString());
                                            aGaugeCoil.product.ptol[0] = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Product][(int)ProfileTable.colProduct.PTol1].ToString());
                                            aGaugeCoil.product.mtol[0] = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Product][(int)ProfileTable.colProduct.MTol1].ToString());
                                            aGaugeCoil.product.factor[0] = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Product][(int)ProfileTable.colProduct.Alloyfactor1].ToString());
                                            aGaugeCoil.product.offset[0] = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Product][(int)ProfileTable.colProduct.Offset1].ToString());
                                            if(aGaugeCoil.product.measmode == 1)
                                            {
                                                aGaugeCoil.product.setpoint[1] = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Product][(int)ProfileTable.colProduct.Setpoint2].ToString());
                                                aGaugeCoil.product.ptol[1] = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Product][(int)ProfileTable.colProduct.PTol2].ToString());
                                                aGaugeCoil.product.mtol[1] = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Product][(int)ProfileTable.colProduct.MTol2].ToString());
                                                aGaugeCoil.product.factor[1] = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Product][(int)ProfileTable.colProduct.Alloyfactor2].ToString());
                                                aGaugeCoil.product.offset[1] = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Product][(int)ProfileTable.colProduct.Offset2].ToString());
                                            }

                                            // Data
                                            CoilProfClass aGaugeProf = RemoteInterfaceClass.XMD.GetProfData(whichGauge);
                                            aGaugeProf.datatime = aDataTable.Rows[(int)ProfileTable.row.Data][(int)ProfileTable.colData.Date].ToString() + aDataTable.Rows[(int)ProfileTable.row.Data][(int)ProfileTable.colData.Time].ToString();
                                            aGaugeProf.measmode = Convert.ToInt16(aDataTable.Rows[(int)ProfileTable.row.Data][(int)ProfileTable.colData.Measmode].ToString());
                                            aGaugeProf.length = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Data][(int)ProfileTable.colData.Length].ToString());
                                            aGaugeProf.min[0] = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Data][(int)ProfileTable.colData.Min].ToString());
                                            aGaugeProf.max[0] = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Data][(int)ProfileTable.colData.Max].ToString());
                                            aGaugeProf.avg[0] = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Data][(int)ProfileTable.colData.Avg].ToString());
                                            aGaugeProf.sigma[0] = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Data][(int)ProfileTable.colData.Sigma].ToString());
                                            for (int i=0; i<SIZE.TRIPLE; ++i)
                                            {
                                                aGaugeProf.triple[0][i] = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Data][(int)ProfileTable.colData.Triple+i].ToString());
                                            }
                                            for (int i = 0; i < SIZE.PROF; ++i)
                                            {
                                                aGaugeProf.profile[0][i] = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Data][(int)ProfileTable.colData.Zone+i].ToString());
                                            }
                                            if (aGaugeCoil.product.measmode == 1)
                                            {
                                                aGaugeProf.min[1] = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Data+1][(int)ProfileTable.colData.Min].ToString());
                                                aGaugeProf.max[1] = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Data+1][(int)ProfileTable.colData.Max].ToString());
                                                aGaugeProf.avg[1] = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Data+1][(int)ProfileTable.colData.Avg].ToString());
                                                aGaugeProf.sigma[1] = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Data+1][(int)ProfileTable.colData.Sigma].ToString());
                                                for (int i = 0; i < SIZE.TRIPLE; ++i)
                                                {
                                                    aGaugeProf.triple[1][i] = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Data+1][(int)ProfileTable.colData.Triple + i].ToString());
                                                }
                                                for (int i = 0; i < SIZE.PROF; ++i)
                                                {
                                                    aGaugeProf.profile[1][i] = Convert.ToSingle(aDataTable.Rows[(int)ProfileTable.row.Data+1][(int)ProfileTable.colData.Zone + i].ToString());
                                                }
                                            }
                                            aGaugeProf.reqIndex = 0; aGaugeProf.updateIndex = 0;

                                            // Counter
                                            int counter = 0;
                                            try
                                            { 
                                                while(true)
                                                {
                                                    if (aDataTable.Rows[(int)ProfileTable.row.Data+counter][(int)ProfileTable.colData.Date].ToString() == "") break;
                                                    counter++;
                                                }
                                            }
                                            catch (Exception exc)
                                            {
                                                ErrorHandlerClass.ReportException("Counting the data until " + counter.ToString(), exc);
                                            }
                                            if (aGaugeCoil.product.measmode == 1) aGaugeProf.totalIndex = counter/2;
                                            else aGaugeProf.totalIndex = counter;

                                            // Summary

                                            //--
                                            RemoteInterfaceClass.XMD.SetCoilData(ref aGaugeCoil, whichGauge);
                                            RemoteInterfaceClass.XMD.UpdateCoilCount(whichGauge);
                                            RemoteInterfaceClass.XMD.SetProfData(ref aGaugeProf, whichGauge);
                                            if(aGaugeProf.totalIndex > 0) timer1.Start();
                                        }
                                        break;
                                }
                            }
                        }
                        break;
                    case (int)DataSort.Source.SQLSERVER:
                        break;
                }
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("EXCEPTION in listView1_SelectedIndexChanged", exc);
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            switch (ConfigDataClass.view.mode)
            {
                case (int)DataSort.Source.CSVFILE:
                    doFileSearch();
                    break;
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            switch (ConfigDataClass.view.mode)
            {
                case (int)DataSort.Source.CSVFILE:
                    doFileSearch();
                    break;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (RemoteInterfaceClass.connected)
            {
                switch (ConfigDataClass.archive.fileType)
                {
                    case 2: // Profile report
                        {
                            int whichGauge = 1;
                            if (radioButton2.Checked) whichGauge = 2;
                            CoilProfClass aGaugeProf = RemoteInterfaceClass.XMD.GetProfData(whichGauge);

                            if ((aGaugeProf.reqIndex != aGaugeProf.updateIndex) && (aGaugeProf.reqIndex < aGaugeProf.totalIndex))
                            {
                                int index = aGaugeProf.reqIndex;
                                if (aGaugeProf.measmode == 1) index = (int)ProfileTable.row.Data + index * 2;
                                else index = (int)ProfileTable.row.Data + index;

                                aGaugeProf.datatime = aDataTable.Rows[index][(int)ProfileTable.colData.Date].ToString() + aDataTable.Rows[index][(int)ProfileTable.colData.Time].ToString();
                                aGaugeProf.length = Convert.ToSingle(aDataTable.Rows[index][(int)ProfileTable.colData.Length].ToString());
                                aGaugeProf.min[0] = Convert.ToSingle(aDataTable.Rows[index][(int)ProfileTable.colData.Min].ToString());
                                aGaugeProf.max[0] = Convert.ToSingle(aDataTable.Rows[index][(int)ProfileTable.colData.Max].ToString());
                                aGaugeProf.avg[0] = Convert.ToSingle(aDataTable.Rows[index][(int)ProfileTable.colData.Avg].ToString());
                                aGaugeProf.sigma[0] = Convert.ToSingle(aDataTable.Rows[index][(int)ProfileTable.colData.Sigma].ToString());
                                for (int i = 0; i < SIZE.TRIPLE; ++i)
                                {
                                    aGaugeProf.triple[0][i] = Convert.ToSingle(aDataTable.Rows[index][(int)ProfileTable.colData.Triple + i].ToString());
                                }
                                for (int i = 0; i < SIZE.PROF; ++i)
                                {
                                    aGaugeProf.profile[0][i] = Convert.ToSingle(aDataTable.Rows[index][(int)ProfileTable.colData.Zone + i].ToString());
                                }
                                if (aGaugeProf.measmode == 1)
                                {
                                    aGaugeProf.min[1] = Convert.ToSingle(aDataTable.Rows[index + 1][(int)ProfileTable.colData.Min].ToString());
                                    aGaugeProf.max[1] = Convert.ToSingle(aDataTable.Rows[index + 1][(int)ProfileTable.colData.Max].ToString());
                                    aGaugeProf.avg[1] = Convert.ToSingle(aDataTable.Rows[index + 1][(int)ProfileTable.colData.Avg].ToString());
                                    aGaugeProf.sigma[1] = Convert.ToSingle(aDataTable.Rows[index + 1][(int)ProfileTable.colData.Sigma].ToString());
                                    for (int i = 0; i < SIZE.TRIPLE; ++i)
                                    {
                                        aGaugeProf.triple[1][i] = Convert.ToSingle(aDataTable.Rows[index + 1][(int)ProfileTable.colData.Triple + i].ToString());
                                    }
                                    for (int i = 0; i < SIZE.PROF; ++i)
                                    {
                                        aGaugeProf.profile[1][i] = Convert.ToSingle(aDataTable.Rows[index + 1][(int)ProfileTable.colData.Zone + i].ToString());
                                    }
                                }
                                aGaugeProf.updateIndex = aGaugeProf.reqIndex;
                            }
                        }
                        break;
                }
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (RemoteInterfaceClass.connected)
                {
                }
                else
                {
                    textBoxCoilID.Text = "No remoting";
                    textBoxPassnumber.Text = "1";
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