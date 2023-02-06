//=================================================================================================
//  Project:    QCS Center
//  Module:     MainForm.cs                                                                         
//  Author:     Jim Zhang
//  Date:       16/12/2019
//  
//  Details:    XMD data library and applications version handler
//                   
//=================================================================================================
using System;
using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
//using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Collections;
using GAUGlib;
using GAUGdata;

namespace GAUGcenter
{
    public partial class MainForm : Form
    {
        //-----------------------------------------------------------------------------------------
        // CLASS VARIABLES
        //-----------------------------------------------------------------------------------------
        //-- Application information class --------------------------------------------------------
        public class APP
        {
            //-- Application enumerator
            public enum INDEX
            {
                LOAD,
                GUI,
                VIEW
            }
            //-- Application name consts
            public const string CENTER = "GAUGcenter";
            public const string LOAD = "GAUGm1com";
            public const string GUI = "GAUGgui";
            public const string VIEW = "GAUGview";
            //-- Application alive counts
            public static int[] aliveCount = new int[10];
            public static int appCount = 0;
            //-- Apps status
            public static bool xmdLoadAlive = false;
            public static bool xmdGuiAlive = false;
            public static bool xmdViewAlive = false;
        }

        //-- Automate READ event to XMD apps
        public static Queue<string> eventQ = new Queue<string>();
        
        
        //-----------------------------------------------------------------------------------------
        // GLOBAL PROCEDURES
        //-----------------------------------------------------------------------------------------
        public MainForm()
        {
            InitializeComponent();
            //-- Update DLL version information
            try
            {
                //-- Start remote server
                serverPortTextBox.Text = ConfigDataClass.xmd.remotePort.ToString();
                if (ConfigDataClass.xmd.autoConnect) StartServer(ConfigDataClass.xmd.remotePort);
                //-- Start XMD applications
                Thread.Sleep(1000);
                if (ConfigDataClass.xmd.startXMDload) StartXMDload();
                if (ConfigDataClass.xmd.startXMDgui) StartXMDgui();
                if (ConfigDataClass.xmd.startXMDview) StartXMDview();
                //-- Start form timers
                eventTimer.Start();
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("Problem retrieving DLL interface data", exc);
            }
        }
        //-- Start XMD-meas measurement algorithms ------------------------------------------------
        public static void AddEvent(string ev)
        {
            eventQ.Enqueue(ev);
        }
        //-----------------------------------------------------------------------------------------
        // LOCAL PROCEDURES
        //-----------------------------------------------------------------------------------------
        //-- Start remote server TCP channel ------------------------------------------------------
        private void StartServer(int portNum)
        {
            try
            {
                //-- Properties of the server channel
                Hashtable props = new Hashtable();
                props.Add("port", portNum.ToString());
                //-- Create server sink providers needed for TCP channel
                BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();
                //-- Set full deserialization level
                serverProvider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
                //-- Create the server channel 
                TcpChannel tcpChannel = new TcpChannel(props, null, serverProvider);
                //-- Register the server channel
                ChannelServices.RegisterChannel(tcpChannel, false);
                //-- Expose the server objects for remote calls
                Type requiredType = typeof(XMDdata);
                //-- Create the server object for clients to connect to for XMDdata
                RemotingConfiguration.RegisterWellKnownServiceType(requiredType, "RemoteInterface", WellKnownObjectMode.SingleCall);
                //-- Create the server object for clients to connect to events/callbacks 
                requiredType = typeof(XMDevent);
                RemotingConfiguration.RegisterWellKnownServiceType(requiredType, "RemoteEvents", WellKnownObjectMode.Singleton);
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("Problem starting remote server interface", exc);
                //ErrorForm.AddException(exc, "Problem starting remote server interface");
            }
        }
        //-- Start XMD-m1com ------------------------------------------------------
        private void StartXMDload()
        {
            try
            {
                Process xmdProcess = new Process();
                xmdProcess.StartInfo.FileName = DIRPATH.ROOT + DIRPATH.APPS + APP.LOAD;
                Process[] Processes = Process.GetProcessesByName(APP.LOAD);
                if (Processes.Length > 1)
                {
                    xmdProcess.Kill();
                    Thread.Sleep(200);
                }
                xmdProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
                Thread.Sleep(200);
                xmdProcess.Start();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "GAUGcenter Problem locating m1com application");
            }
        }
        //-- Start XMD-gui ------------------------------------------------------
        private void StartXMDgui()
        {
            try
            {
                Process xmdProcess = new Process();
                xmdProcess.StartInfo.FileName = DIRPATH.ROOT + DIRPATH.APPS + APP.GUI;
                Process[] Processes = Process.GetProcessesByName(APP.GUI);
                if (Processes.Length > 1)
                {
                    xmdProcess.Kill();
                    Thread.Sleep(200);
                }
                xmdProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                Thread.Sleep(200);
                xmdProcess.Start();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "GAUGcenter Problem locating GUI application");
            }
        }
        //-- Start XMD-view ------------------------------------------------------
        private void StartXMDview()
        {
            try
            {
                Process xmdProcess = new Process();
                xmdProcess.StartInfo.FileName = DIRPATH.ROOT + DIRPATH.APPS + APP.VIEW;
                Process[] Processes = Process.GetProcessesByName(APP.VIEW);
                if (Processes.Length > 1)
                {
                    xmdProcess.Kill();
                    Thread.Sleep(200);
                }
                xmdProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                Thread.Sleep(200);
                xmdProcess.Start();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "GAUGcenter Problem locating VIEW application");
            }
        }
        //-----------------------------------------------------------------------------------------
        // LOCAL EVENTS
        //----------------------------------------------------------------------------------------- 
        //-- Start remote server ------------------------------------------------------------------
        private void serverButton_Click(object sender, EventArgs e)
        {
            StartServer(Int32.Parse(serverPortTextBox.Text));
        }
        //-- Start-up XME-net network interface application ---------------------------------------
        private void startXMDloadButton_Click(object sender, EventArgs e)
        {
            StartXMDload(); 
        }
        //-- Fire a NULL event as a client keepalive ----------------------------------------------
        private void nullEventTimer_Tick(object sender, EventArgs e)
        {
            eventQ.Enqueue(RemoteEvQ.NULL_EV);
        }
        private void XMDInterfaceForm_Load(object sender, EventArgs e)
        {
            //-- Start form timers
            refreshTimer.Start();
        }
        //-- Update form visuals ------------------------------------------------------------------
        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            //-- Update XMD application remote connections
            clientsDGV.Rows.Clear();
            for (int i = 0; i < XMDevent.infoList.Count; i++)
            {
                clientsDGV.Rows.Add(XMDevent.infoList[i].appName, XMDevent.infoList[i].aliveCount);

                switch (XMDevent.infoList[i].appName)
                {
                    case APP.LOAD:
                        {
                            APP.xmdLoadAlive = ((XMDevent.infoList[i].aliveCount > APP.aliveCount[(int)APP.INDEX.LOAD]));
                            APP.aliveCount[(int)APP.INDEX.LOAD] = XMDevent.infoList[i].aliveCount;
                        }
                        break;
                    case APP.GUI:
                        {
                            APP.xmdGuiAlive = ((XMDevent.infoList[i].aliveCount > APP.aliveCount[(int)APP.INDEX.GUI]));
                            APP.aliveCount[(int)APP.INDEX.GUI] = XMDevent.infoList[i].aliveCount;
                        }
                        break;
                    case APP.VIEW:
                        {
                            APP.xmdGuiAlive = ((XMDevent.infoList[i].aliveCount > APP.aliveCount[(int)APP.INDEX.VIEW]));
                            APP.aliveCount[(int)APP.INDEX.GUI] = XMDevent.infoList[i].aliveCount;
                        }
                        break;
                    default:
                        break;
                }
            }
            //-- Report errors on apps that have stopped updating
            //if (XMDdata.statSV.XMDVIEWOK && !APP.xmdViewAlive)ErrorForm.AddRMError("E0090");
            //XMDdata.statSV.XMDVIEWOK = APP.xmdViewAlive;
        }
        //-- Check for events to transmitt --------------------------------------------------------
        private void eventTimer_Tick(object sender, EventArgs e)
        {
            if (eventQ.Count > 0)
                XMDevent.FireNewBroadcastedMessageEvent(eventQ.Dequeue());
        }
        //-- Close all XMD applications running ---------------------------------------------------
        private void closeAllXMDAppsButton_Click(object sender, EventArgs e)
        {
            WarningDialogBox closeAllWarning = new WarningDialogBox("Closing all GAUG suite applications.....\n\nPlease confirm!");
            if (closeAllWarning.ShowDialog() != DialogResult.Cancel)
                 try
                {
                    Process[] myProcesses;
                    myProcesses = Process.GetProcessesByName(APP.LOAD);
                    foreach (Process myProcess in myProcesses)
                    {
                        myProcess.CloseMainWindow();
                        myProcess.Kill();
                    }
                    myProcesses = Process.GetProcessesByName(APP.GUI);
                    foreach (Process myProcess in myProcesses)
                    {
                        myProcess.CloseMainWindow();
                        myProcess.Kill();
                    }
                    myProcesses = Process.GetProcessesByName(APP.VIEW);
                    foreach (Process myProcess in myProcesses)
                    {
                        myProcess.CloseMainWindow();
                        myProcess.Kill();
                    }
                    myProcesses = Process.GetProcessesByName(APP.CENTER);
                    foreach (Process myProcess in myProcesses)
                    {
                        myProcess.CloseMainWindow();
                        myProcess.Kill();
                    }
                }
                catch (Exception exc)
                {
                    ErrorHandlerClass.ReportException("GAUGcenter Exit All Programs", exc);
                }
        }

        private void startXMDguiButton_Click(object sender, EventArgs e)
        {
            StartXMDgui();
        }

        private void startXMDviewButton_Click(object sender, EventArgs e)
        {
            StartXMDview();
        }
        private void closeXMDviewButton_Click(object sender, EventArgs e)
        {
            WarningDialogBox closeAllWarning = new WarningDialogBox("Closing GAUG view application.....\n\nPlease confirm!");
            if (closeAllWarning.ShowDialog() != DialogResult.Cancel)
                try
                {
                    Process[] myProcesses;
                    myProcesses = Process.GetProcessesByName(APP.VIEW);
                    foreach (Process myProcess in myProcesses)
                    {
                        myProcess.CloseMainWindow();
                        myProcess.Kill();
                    }
                }
                catch (Exception exc)
                {
                    ErrorHandlerClass.ReportException("GAUGcenter Exit GAUGview Program", exc);
                }
        }

        private void closeXMDloadButton_Click(object sender, EventArgs e)
        {
            WarningDialogBox closeAllWarning = new WarningDialogBox("Closing GAUG load application.....\n\nPlease confirm!");
            if (closeAllWarning.ShowDialog() != DialogResult.Cancel)
                try
                {
                    Process[] myProcesses;
                    myProcesses = Process.GetProcessesByName(APP.LOAD);
                    foreach (Process myProcess in myProcesses)
                    {
                        myProcess.CloseMainWindow();
                        myProcess.Kill();
                    }
                }
                catch (Exception exc)
                {
                    ErrorHandlerClass.ReportException("GAUGcenter Exit GAUGload Program", exc);
                }
        }
        //-----------------------------------------------------------------------------------------
    }
}

//=================================================================================================