//=================================================================================================
//  Project:    RM312/SIPRO SIPROview
//  Module:     ViewMainForm.cs                                                                         
//  Author:     Andrew Powell
//  Date:       21/04/2010
//  
//  Details:    GUI for SIPRO landscan viewmeter communications interface
//  
//=================================================================================================
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

//using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.ApplicationBlocks.ExceptionManagement;

using GAUGlib;
using GAUGdata;

namespace GAUGview
{
    public partial class ViewMainForm : Form
    {
        //-----------------------------------------------------------------------------------------
        // CLASS VARIABLES
        //-----------------------------------------------------------------------------------------
        private InfoFormProduct[] aInfoFormProduct = new InfoFormProduct[MAXNUM.FORM];
        private InfoFormMeasure[] aInfoFormMearure = new InfoFormMeasure[MAXNUM.FORM];
        private InfoFormEnquire[] aInfoFormEnquire = new InfoFormEnquire[MAXNUM.FORM];

        private MeasureFormProfile [] aMeasureFormProfile = new MeasureFormProfile [MAXNUM.FORM];
        private MeasureFormTrendLen[] aMeasureFormTrendLen = new MeasureFormTrendLen[MAXNUM.FORM];
        private MeasureFormTrendTime[] aMeasureFormTrendTime = new MeasureFormTrendTime[MAXNUM.FORM];
        private MeasureFormMap[] aMeasureFormMap = new MeasureFormMap[MAXNUM.FORM];

        //-- Remote event handling        
        private Thread clientThread;
        private static RemoteEvQ eventQ = new RemoteEvQ();
        public static int aliveCount = 0;
        private static bool connectedToServer = false;

        //-- Error handling
        public string excError = "";
        ErrorMsg viewError = new ErrorMsg();

   //     int waitingCounter = 0;

        //// -- Delay -----------------------------------------------------------------------------------
        //public static void Delay(int mm)
        //{
        //    DateTime current = DateTime.Now;
        //    while (current.AddMilliseconds(mm) > DateTime.Now)
        //    {
        //        Application.DoEvents();
        //    }
        //    return;
        //}
        //---------------------------------------------------------------------------------------------------------
        // GLOBAL PROCEDURES
        //---------------------------------------------------------------------------------------------------------
        public ViewMainForm()
        {
            InitializeComponent();
        }
        //---------------------------------------------------------------------------------------------------------
        // LOCAL PROCEDURES
        //---------------------------------------------------------------------------------------------------------
        // -- Delay -----------------------------------------------------------------------------------
        private void Delay(int mm)
        {
            DateTime current = DateTime.Now;
            while (current.AddMilliseconds(mm) > DateTime.Now)
            {
                Application.DoEvents();
            }
            return;
        }
        //Dock OP forms to OP tab pages
        private void AssignFormToTabPage(Form aForm, int Tab)
        {
            aForm.TopLevel = false;
            //tabControlMain.TabPages[Tab].Text = HMI.OPTAB[Tab];
            tabControlMain.TabPages[Tab].Controls.Add(aForm);
            aForm.Dock = DockStyle.Fill;
            aForm.Show();
        }
        //-- Initialise class variables based on configurations -----------------------------------
        private void InitClassVariables()
        {
            remoteServerAddressTextBox.Text = ConfigDataClass.remoting.address.ToString();
            remoteServerPortTextBox.Text = ConfigDataClass.remoting.port.ToString();
        }
        //-- Update Language ----------------------------------------------------------
        private void UpdateLanguage()
        {
            // Menu
            loginToolStripButton.Text = FileClass.textItem.GetTextItem(loginToolStripButton.Tag.ToString(), ConfigDataClass.view.language);
            printToolStripButton.Text = FileClass.textItem.GetTextItem(printToolStripButton.Tag.ToString(), ConfigDataClass.view.language);
            printCurrentPageToolStripMenuItem.Text = FileClass.textItem.GetTextItem(printCurrentPageToolStripMenuItem.Tag.ToString(), ConfigDataClass.view.language);
            printAllPagesToolStripMenuItem.Text = FileClass.textItem.GetTextItem(printAllPagesToolStripMenuItem.Tag.ToString(), ConfigDataClass.view.language);
            aboutToolStripButton.Text = FileClass.textItem.GetTextItem(aboutToolStripButton.Tag.ToString(), ConfigDataClass.view.language);

            // Tab


            // Label

            // Unit
        }
        private void UpdateColorAndFont()
        {
            this.BackColor = ConfigDataClass.view.formBackColor;

            //-- default color and font for lab/unit/text
            setControls(this);
        }
        private void setControls(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                if (con.BackColor != Color.Transparent) con.BackColor = ConfigDataClass.view.formBackColor;
                con.Font = new Font(ConfigDataClass.view.comFont.name, ConfigDataClass.view.comFont.size, ConfigDataClass.view.comFont.style);
                con.ForeColor = ConfigDataClass.view.comFont.color;

                if (con.Controls.Count > 0)
                {
                    setControls(con);
                }
            }
        }
        //-- Update the size and location
        private void UpdateSizeAndLocation()
        {

        }
        //-- Display new error message ------------------------------------------------------------
        private void DisplayErrorMsg(ErrorMsg error)
        {
//            errorDGV.Invoke(new EventHandler(delegate
//            {
//                if (error.count == 1)
//                {
//                    errorDGV.Rows.Insert(0, DateTime.Now.ToString(), error.desc, error.count, error.detail);
//                    if (errorDGV.Rows.Count > 20)
//                        errorDGV.Rows.Remove(errorDGV.Rows[errorDGV.Rows.Count - 1]);
//                }
//                else
//                {
//                    errorDGV.Rows[0].Cells[0].Value = DateTime.Now.ToString();
//                    errorDGV.Rows[0].Cells[2].Value = error.count;
//                }
//                errorDGV.ClearSelection();
//            }));
        }
        //-- Return connection status of remote SV - requires exception catch ---------------------
        private bool remoteSVConnected()
        {
            if (!ConfigDataClass.remoting.autoConnect) RemoteInterfaceClass.connected = false;
            else
            try
            {
                SuperStatus sStatus = RemoteInterfaceClass.XMD.GetSVStatus();
                RemoteInterfaceClass.connected = true;
            }
            //-- Remoting data fetch timed out
            catch
            {
                RemoteInterfaceClass.connected = false;
            }
            return (RemoteInterfaceClass.connected);
        }
        //-- Show event to process on screen ------------------------------------------------------
        private void DisplayEvent(string ev)
        {
            //-- Add delegate here
        }
        //-- Process server event -----------------------------------------------------------------
        private void ProcessEvent(string ev)
        {
            if (RemoteInterfaceClass.connected)
            {
                //-- Event processing here               
            }
        }
        //-- Server event handler -----------------------------------------------------------------
        private void NewEvent(object sender, EventArgs e)
        {
            RemoteEvQ eventQ = (RemoteEvQ)sender;
            DisplayEvent(eventQ.eventToProcess);
            ProcessEvent(eventQ.eventToProcess);
        }
        //-- Delegate to append event to queue ----------------------------------------------------
        public static void AddRemoteEvent(string ev)
        {
            eventQ.AddEvent(ev);
        }
        //-- Derived from WKO shared by the two apps exposing methods for server calling backing into client
        public class NotifySink : NotifyCallbackSink
        {
            //-- Events from the server call into here
            protected override void OnNotifyCallback(string s)
            {
                AddRemoteEvent(s);
            }
        }
        //-- Remoting client for handling server events -------------------------------------------
        private void RemotingEventClient()
        {
            try
            {
                //-- Use 0 to receive available client port
                TcpChannel tcpChannel = new TcpChannel(0);
                //-- Register the client channel
                ChannelServices.RegisterChannel(tcpChannel, false);
                //-- Establish remote connection urlString = "tcp://127.0.0.1:9000/RemoteInterface";
                //string urlString = (string)"tcp://" + ConfigDataClass.remoting.address.ToString() + ":" + ConfigDataClass.remoting.port.ToString() + "/";
                string urlString = "tcp://" + remoteServerAddressTextBox.Text + ":" + remoteServerPortTextBox.Text + "/";
                //-- Get type of the server instance to expose
                Type requiredType = typeof(remoteInterface);
                string XMDDataURL = urlString + "RemoteInterface";
                RemoteInterfaceClass.XMD = (remoteInterface)Activator.GetObject(requiredType, XMDDataURL);
                //-- Create the object for calling into the server
                requiredType = typeof(ICallsToServer);
                string ICallsToServerURL = urlString + "RemoteEvents";
                ICallsToServer remoteObject = (ICallsToServer)Activator.GetObject(requiredType, ICallsToServerURL);
                //-- Define sink for keepalive transmissions
                RemotingConfiguration.RegisterWellKnownServiceType(requiredType, "ServerEvents", WellKnownObjectMode.Singleton);
                NotifySink sink = new NotifySink();
                //-- Add event handler for XMD-super events               
                eventQ.NewEvent += new EventHandler(NewEvent);

                while (true)
                {
                    //-- Attempt to make the connection 
                    if (!connectedToServer)
                    {
                        try
                        //-- Assign the callback from the server to here
                        {
                            remoteObject.Notify += new NotifyCallback(sink.FireNotifyCallback);
                            connectedToServer = true;
                            RemoteInterfaceClass.connected = true;
                        }
                        catch (Exception exc)
                        {
                            //-- Assume problem
                            connectedToServer = false;
                            RemoteInterfaceClass.connected = false;
                            excError = exc.Message;
                        }
                        Thread.Sleep(50);
                    }

                    aliveCount = 0;
                    //-- Continually transmit version alive data
                    while (connectedToServer)
                    {
                        try
                        {
                            VersionInfoAlive versInfo = new VersionInfoAlive();
                            versInfo.hostName = Dns.GetHostName();
                            versInfo.appName = this.ProductName;
                            versInfo.appVers = this.ProductVersion;
                            Increment.Integer(ref aliveCount);
                            versInfo.aliveCount = aliveCount;
                            remoteObject.InfoFunction(versInfo);
                            connectedToServer = true;
                            RemoteInterfaceClass.connected = true;
                            Thread.Sleep(50);
                        }
                        catch (Exception exc)
                        {
                            aliveCount--;
                            //-- Assume problem
                            connectedToServer = false;
                            RemoteInterfaceClass.connected = false;
                            ExceptionManager.Publish(exc);
                        }
                        Thread.Sleep(50);
                    }//-- ConnectedToServer
                }//-- Runs Forever.
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("Remote Connection Fault", exc);
            }
        }
        //-----------------------------------------------------------------------------------------
        //-- LOCAL EVENTS
        //-----------------------------------------------------------------------------------------
        //-- Display application properities -------------------------------------------------------
        private void aboutToolStripButton_Click(object sender, EventArgs e)
        {
            AboutBox anAboutBox = new AboutBox();
            anAboutBox.ShowDialog();
        }
        //-- Display viewmeter diagnostic utilities -----------------------------------------------
        private void diagsToolStripButton_Click(object sender, EventArgs e)
        {
        }
        //-- Initiate application start-up --------------------------------------------------------
        private void ViewMainForm_Load(object sender, EventArgs e)
        {
            try
            {
                //-- Show status labels
                startedStatusLabel.Text = "Started:  " + DateTime.Now.ToString();
                loginStatusLabel.Text = "Login:  " + "Engineer";

                //-- Load INI file data        
                FileClass.iniFile.LoadData(IniFile.INI.GAUGVIEW);
                FileClass.iniFile.PutData();
                FileClass.textItem.LoadLanguage();
                FileClass.iniFile.LoadINIData(IniFile.INI.GRAPHICPAGES);
                FileClass.iniFile.LoadINIData(IniFile.INI.PROPERTIES);

                modeToolStripMenuItemOnline.Checked = false;
                //{
                //    modeToolStripMenuItemOnline.Checked = false;
                //    modeToolStripMenuItemOffline.Checked = true;
                //}
                //else
                //{
                //    modeToolStripMenuItemOnline.Checked = false;
                //    modeToolStripMenuItemOffline.Checked = true;
                //}


                //-- Apply configuration to application
                Process current = Process.GetCurrentProcess();
                //-- Assign process to cfged CPU core
                if (Environment.ProcessorCount > 3) current.ProcessorAffinity = (IntPtr)ConfigDataClass.app.affinity;
                //-- Set application priority 
                current.PriorityClass = ConfigDataClass.app.priority;
                //-- Initialise class variables
                InitClassVariables();
                //-- Establish remote SV connection if configured
                if (ConfigDataClass.remoting.autoConnect)
                {
                    //-- Start event handler client thread
                    clientThread = new Thread(new ThreadStart(RemotingEventClient));
                    clientThread.Name = "RemotingEventClient";
                    clientThread.Start();
                    //-- Hide the configuration panel
                    remotingPanel.SendToBack();
                    remotingPanel.Visible = false;
                }
                //-- Establish network connection if configured
                //if (ConfigDataClass.netLink.autoConnect) ConnectNetwork();
                //-- Set-up current viewmeter configuration

                //-- Set-up timers
                timer2.Interval = ConfigDataClass.view.timer[1];
                timer2.Start();
                //-- Set-up component visibility
                //diagsToolStripButton.Visible = false;
                //printToolStripButton.Visible = false;
                //-- Commence thread for poll interfacing
                //readWriteThread = new Thread(new ThreadStart(this.PollNetworkThread));
                //readWriteThread.Name = "SIPROview Read Write Thread";
                //readWriteThread.Start();
                //-- Diagnostics form
                //aDiagsForm = new DiagsForm(this);

                //this.Font = new Font("NotoSans", 8, FontStyle.Regular);
                //this.WindowState = FormWindowState.Maximized;

                UpdateLanguage();
                UpdateColorAndFont();

                // Gauge Common Form
                for (int i = 0; i < ConfigDataClass.view.formnumber; ++i)
                {
                    if (ConfigDataClass.view.formCfg[i].who.sort != (int)FormSort.Category.INFO) continue;
                    switch (ConfigDataClass.view.formCfg[i].who.type)
                    {
                        case (int)FormSort.INFO.PRODUCT:    // product information
                            aInfoFormProduct[ConfigDataClass.view.formCfg[i].who.index] = new InfoFormProduct(this, (int)ConfigDataClass.view.grid.width, (int)ConfigDataClass.view.grid.height, DISPLAYPARAMS.heightmenu, i);
                            aInfoFormProduct[ConfigDataClass.view.formCfg[i].who.index].TopLevel = false;
                            aInfoFormProduct[ConfigDataClass.view.formCfg[i].who.index].Show();
                            this.Controls.Add(aInfoFormProduct[ConfigDataClass.view.formCfg[i].who.index]);                           
                            break;
                        case (int)FormSort.INFO.ENQUIRE:    // product enquire
                            aInfoFormEnquire[ConfigDataClass.view.formCfg[i].who.index] = new InfoFormEnquire(this, (int)ConfigDataClass.view.grid.width, (int)ConfigDataClass.view.grid.height, DISPLAYPARAMS.heightmenu, i);
                            aInfoFormEnquire[ConfigDataClass.view.formCfg[i].who.index].TopLevel = false;
                            aInfoFormEnquire[ConfigDataClass.view.formCfg[i].who.index].Show();
                            this.Controls.Add(aInfoFormEnquire[ConfigDataClass.view.formCfg[i].who.index]);
                            break;
                        case (int)FormSort.INFO.NAVE:
                            break;
                    }
                }

                // TabControlMain
                tabControlMain.Left = (int)(this.Width * (ConfigDataClass.view.tablocation.left / ConfigDataClass.view.grid.width));
                tabControlMain.Top = (int)((this.Height - DISPLAYPARAMS.heighttab * 3) * (ConfigDataClass.view.tablocation.top / ConfigDataClass.view.grid.height)) + DISPLAYPARAMS.heightmenu;
                tabControlMain.Width = (int)((this.Width - DISPLAYPARAMS.offsetleft * 3) * (ConfigDataClass.view.tablocation.width / ConfigDataClass.view.grid.width));
                tabControlMain.Height = (int)((this.Height - DISPLAYPARAMS.heighttab * 3) * (ConfigDataClass.view.tablocation.height / ConfigDataClass.view.grid.height));

                // Gauge Tabs
                for (int i=0; i< ConfigDataClass.view.tabnumber; ++i)
                {
                    ConfigDataClass.view.tabCfg[i].name = FileClass.textItem.GetTextItem(ConfigDataClass.view.tabCfg[i].name, ConfigDataClass.view.language);
                    tabControlMain.TabPages.Add(ConfigDataClass.view.tabCfg[i].name);

                    for (int j = 0; j < ConfigDataClass.view.tabCfg[i].formnumber; ++j)
                    {
                        switch (ConfigDataClass.view.tabCfg[i].formCfg[j].who.sort)
                        {
                            case (int)FormSort.Category.PROFILE:
                                aMeasureFormProfile[ConfigDataClass.view.tabCfg[i].formCfg[j].who.index] = new MeasureFormProfile(this, tabControlMain.Width - DISPLAYPARAMS.offsetleft, tabControlMain.Height - DISPLAYPARAMS.heighttab, i, j);    // height of tab: 50
                                aMeasureFormProfile[ConfigDataClass.view.tabCfg[i].formCfg[j].who.index].TopLevel = false;
                                aMeasureFormProfile[ConfigDataClass.view.tabCfg[i].formCfg[j].who.index].Show();
                                tabControlMain.TabPages[i].Controls.Add(aMeasureFormProfile[ConfigDataClass.view.tabCfg[i].formCfg[j].who.index]);
                                break;
                            case (int)FormSort.Category.TRENDLEN:
                                aMeasureFormTrendLen[ConfigDataClass.view.tabCfg[i].formCfg[j].who.index] = new MeasureFormTrendLen(this, tabControlMain.Width - DISPLAYPARAMS.offsetleft * 2, tabControlMain.Height - DISPLAYPARAMS.heighttab, i, j);    // height of tab: 50
                                aMeasureFormTrendLen[ConfigDataClass.view.tabCfg[i].formCfg[j].who.index].TopLevel = false;
                                aMeasureFormTrendLen[ConfigDataClass.view.tabCfg[i].formCfg[j].who.index].Show();
                                tabControlMain.TabPages[i].Controls.Add(aMeasureFormTrendLen[ConfigDataClass.view.tabCfg[i].formCfg[j].who.index]);
                                break;
                            case (int)FormSort.Category.TREENDTIME:
                                aMeasureFormTrendTime[ConfigDataClass.view.tabCfg[i].formCfg[j].who.index] = new MeasureFormTrendTime(this, tabControlMain.Width - DISPLAYPARAMS.offsetleft, tabControlMain.Height - DISPLAYPARAMS.heighttab, i, j);    // height of tab: 50
                                aMeasureFormTrendTime[ConfigDataClass.view.tabCfg[i].formCfg[j].who.index].TopLevel = false;
                                aMeasureFormTrendTime[ConfigDataClass.view.tabCfg[i].formCfg[j].who.index].Show();
                                tabControlMain.TabPages[i].Controls.Add(aMeasureFormTrendTime[ConfigDataClass.view.tabCfg[i].formCfg[j].who.index]);
                                break;
                            case (int)FormSort.Category.MAP:
                                aMeasureFormMap[ConfigDataClass.view.tabCfg[i].formCfg[j].who.index] = new MeasureFormMap(this, tabControlMain.Width - DISPLAYPARAMS.offsetleft, tabControlMain.Height - DISPLAYPARAMS.heighttab, i, j);    // height of tab: 50
                                aMeasureFormMap[ConfigDataClass.view.tabCfg[i].formCfg[j].who.index].TopLevel = false;
                                aMeasureFormMap[ConfigDataClass.view.tabCfg[i].formCfg[j].who.index].Show();
                                tabControlMain.TabPages[i].Controls.Add(aMeasureFormMap[ConfigDataClass.view.tabCfg[i].formCfg[j].who.index]);
                                break;
                            case (int)FormSort.Category.INFO:                                
                                switch (ConfigDataClass.view.tabCfg[i].formCfg[j].who.type)
                                {
                                    case (int)FormSort.INFO.POS:
                                        aInfoFormMearure[ConfigDataClass.view.tabCfg[i].formCfg[j].who.index] = new InfoFormMeasure(this, tabControlMain.Width - 8, tabControlMain.Height - DISPLAYPARAMS.heighttab, i, j);    // height of tab: 50
                                        aInfoFormMearure[ConfigDataClass.view.tabCfg[i].formCfg[j].who.index].TopLevel = false;
                                        aInfoFormMearure[ConfigDataClass.view.tabCfg[i].formCfg[j].who.index].Show();
                                        tabControlMain.TabPages[i].Controls.Add(aInfoFormMearure[ConfigDataClass.view.tabCfg[i].formCfg[j].who.index]);
                                        break;
                                }                                
                                break;
                        }
                    }
                }

                //aForm.Dock = DockStyle.Fill;
                //-- Report opening event
                ErrorHandlerClass.ReportEvent("Application Started");
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("Form Load Error", exc);
            }
        }
        //-- Update form visuals ------------------------------------------------------------------
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                //-- Check for interface errors
                if (ErrorHandlerClass.CheckError(ref viewError))
                    DisplayErrorMsg(viewError);

                //-- Display SV remote interface status
                if (remoteSVConnected())
                {
                    remoteStatusLabel.BackColor = Color.GreenYellow;
                    remoteStatusLabel.Text = "Connected";
                }
                else
                {
                    remoteStatusLabel.BackColor = Color.Red;
                    remoteStatusLabel.Text = "Disconnected";
                }
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("Screen Update Error", exc);
            }
        }
        //-- Clear up system tray -----------------------------------------------------------------
        private void ViewMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            WarningDialogBox closeViewWarning = new WarningDialogBox("Closing GAUGview application.....\n\nPlease confirm!");
            if (closeViewWarning.ShowDialog() == DialogResult.Cancel)
                e.Cancel = true;
            else try
                {
                    ErrorHandlerClass.ReportEvent("Application Closed");
                    Thread.Sleep(1500);
                    xmdNotifyIcon.Dispose();
                    Process[] myProcesses;
                    myProcesses = Process.GetProcessesByName("GAUGview");
                    foreach (Process myProcess in myProcesses)
                    {
                        myProcess.CloseMainWindow();
                        myProcess.Kill();
                    }
                    //if (readWriteThread.IsAlive) readWriteThread.Abort();
                }
                catch (Exception exc)
                {
                    ExceptionManager.Publish(exc);
                    MessageBox.Show(exc.Message, "GAUGview");
                }
        }
        //-- On minimizing form place icon in system tray -----------------------------------------
        private void ViewMainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                xmdNotifyIcon.Visible = true;
                xmdNotifyIcon.ShowBalloonTip(10000); //-- 10 seconds minimum
                this.Hide();
            }
        }
        //-- Restore application from system tray -------------------------------------------------
        private void xmdNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }
        //-- On clicking icon in system tray maximize form ----------------------------------------
        private void displayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
        //-- On clicking icon in system tray close form -------------------------------------------
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //-- View XMD-super remote interface configuration ----------------------------------------
        private void remoteStatusLabel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!remotingPanel.Visible)
                {
                    remoteServerAddressTextBox.Text = ConfigDataClass.remoting.address.ToString();
                    remoteServerPortTextBox.Text = ConfigDataClass.remoting.port.ToString();
                    if (RemoteInterfaceClass.connected) remoteConnectButton.Enabled = false;
                    remotingPanel.BringToFront();
                    remotingPanel.Visible = true;
                }
                else
                {
                    remotingPanel.SendToBack();
                    remotingPanel.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("Remote Connection Fault", exc);
            }
        }
        //-- Establish remote connection to XMD-super ---------------------------------------------
        private void remoteConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                //-- Start event handler client thread
                clientThread = new Thread(new ThreadStart(RemotingEventClient));
                clientThread.Name = "RemotingEventClient";
                clientThread.Start();
                //-- Hide the configuration panel
                remotingPanel.SendToBack();
                remotingPanel.Visible = false;
                //-- Set-up auto updating
                //StartAutoDetPolling();
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("Remote Connection Fault", exc);
            }
        }        
        //-- Save a screenshot of the applications window -----------------------------------------
        private void printCurrentPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string fName = FileClass.Diagnostic() + ConfigDataClass.app.gaugeId + DateTime.Now.ToString("_yyyyMMdd_HHmmss");
                //Rectangle appBounds = new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                //Rectangle appBounds = new Rectangle(0, 0, 1388, 768);
                Rectangle appBounds = this.Bounds;

                using (Bitmap bitmap = new Bitmap(appBounds.Width, appBounds.Height))                
                {
                    using (Graphics gfx = Graphics.FromImage(bitmap))
                    {
                        gfx.CopyFromScreen(new Point(appBounds.Left, appBounds.Top), Point.Empty, appBounds.Size);
                    }
                    bitmap.Save(fName + "_Tab" + tabControlMain.SelectedIndex.ToString() + "_GAUGview.jpg", ImageFormat.Jpeg);

                    //iTextSharp.text.Document document = new iTextSharp.text.Document();
                    //PdfWriter.GetInstance(document, new FileStream(fName + ".pdf", FileMode.Create));
                    //document.Open();
                    //iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(Image.FromHbitmap(bitmap.GetHbitmap()), ImageFormat.Jpeg);
                    //image.ScalePercent(40);
                    //document.Add(image);
                    //document.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Screenshot capture error: " + exc.Message, "GAUGview");
            }
        }

        private void printAllPagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string fName = FileClass.Diagnostic() + ConfigDataClass.app.gaugeId
                               + DateTime.Now.ToString("_yyyyMMdd_HHmmss");
                int savedIndex = tabControlMain.SelectedIndex;

                iTextSharp.text.Document document = new iTextSharp.text.Document();
                PdfWriter.GetInstance(document, new FileStream(fName + ".pdf", FileMode.Create));
                document.Open();

                for (int i = 0; i < tabControlMain.TabCount; ++i)
                {
                    tabControlMain.SelectedIndex = i;
                    Delay(ConfigDataClass.view.hardcopywaittime);
                    { 
                        Rectangle appBounds = this.Bounds;
                        using (Bitmap bitmap = new Bitmap(appBounds.Width, appBounds.Height))
                        {
                            using (Graphics gfx = Graphics.FromImage(bitmap))
                            {
                                gfx.CopyFromScreen(new Point(appBounds.Left, appBounds.Top), Point.Empty, appBounds.Size);
                            }
                            bitmap.Save(fName + "_Tab" + tabControlMain.SelectedIndex.ToString() + "_GAUGview.jpg", ImageFormat.Jpeg);

                            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(Image.FromHbitmap(bitmap.GetHbitmap()), ImageFormat.Jpeg);
                            image.ScalePercent(ConfigDataClass.view.pdfScale);
                            document.Add(image);
                        }
                    }
                }

                document.Close();

                tabControlMain.SelectedIndex = savedIndex;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Screenshot capture error: " + exc.Message, "GAUGview");
            }
        }

        private void tabControlMain_DrawItem(object sender, DrawItemEventArgs e)
        {
            SolidBrush brushBackColor = new SolidBrush(ConfigDataClass.view.formBackColor);
            SolidBrush brushActivedColor = new SolidBrush(ConfigDataClass.view.tabBackColorActived);
            SolidBrush brushForeColor = new SolidBrush(ConfigDataClass.view.comFont.color);
            StringFormat strFormat = new StringFormat();
            Rectangle Rec;

            strFormat.Alignment = StringAlignment.Center;
            for (int i = 0; i < tabControlMain.TabCount; ++i)
            {
                Rec = tabControlMain.GetTabRect(i);
                if (i == tabControlMain.SelectedIndex) e.Graphics.FillRectangle(brushActivedColor, Rec);
                else e.Graphics.FillRectangle(brushBackColor, Rec);
                e.Graphics.DrawString(tabControlMain.TabPages[i].Text, new Font(ConfigDataClass.view.comFont.name, ConfigDataClass.view.comFont.size), brushForeColor, Rec, strFormat);
            }
        }

        private void modeToolStripMenuItemOnline_Click(object sender, EventArgs e)
        {
            modeToolStripMenuItemOnline.Checked = true;
            modeToolStripMenuItemOffline.Checked = false;
        }

        private void modeToolStripMenuItemOffline_Click(object sender, EventArgs e)
        {
            modeToolStripMenuItemOnline.Checked = false;
            modeToolStripMenuItemOffline.Checked = true;
        }
    }
}
//=================================================================================================