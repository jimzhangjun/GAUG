//=================================================================================================
//  Project:    RM312/SiPRO SiPROView Remote HMI Application
//  Module:     RemoteApplicationForm.cs                                                                         
//  Author:     Andrew Powell
//  Date:       15/08/2008
//  
//  Details:    Remote application operator HMI to SUPERvisor data
//  
//=================================================================================================
using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
using System.Drawing;
//using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
//using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Net;
//using System.Net.Sockets;
//using System.Windows.Forms;
using Microsoft.ApplicationBlocks.ExceptionManagement;
//using M1ComNET;
//using M1ComNET.M1;
using GAUGlib;
using GAUGdata;

namespace GAUGm1com
{
    public partial class MainForm : Form
    {
        //-----------------------------------------------------------------------------------------
        // CLASS VARIABLES
        //-----------------------------------------------------------------------------------------
        //-- Remote event handling        
        private Thread clientThread;        
        private static RemoteEvQ eventQ = new RemoteEvQ();
        public static int aliveCount = 0;
        private static bool connectedToServer = false;

        //-- Gash looping parameters here for now
 //       private int loopCount = 0;

        // m1com net interface
        private Thread m1comThread;
        M1ComInterface m1ComInterface = null;

        //-----------------------------------------------------------------------------------------
        // GLOBAL PROCEDURES
        //-----------------------------------------------------------------------------------------
        public MainForm()
        {
            InitializeComponent();
            this.IsMdiContainer = true;

      //      this.Text = ConfigDataClass.app.title;

            INFORMATION.logItem = "";
        }

        //-----------------------------------------------------------------------------------------
        // LOCAL PROCEDURES
        //-----------------------------------------------------------------------------------------
        //-- Return connection status of remote SV - requires exception catch ---------------------
        private bool remoteSVConnected()
        {
            try
            {
                UInt16 i=0;
                bool newData = RemoteInterfaceClass.XMD.IsModifiedData(0, ref i, "", 0, 0, 0);
                RemoteInterfaceClass.connected = true;
            }
            catch
            {
                RemoteInterfaceClass.connected = false;
            }
            return (RemoteInterfaceClass.connected);
        }
        //-- Show event to process on screen ------------------------------------------------------
        private void DisplayEvent(string ev)
        {
            //-- Display event if required
        }
        //-- Process server event -----------------------------------------------------------------
        private void ProcessEvent(string ev)
        {
            if (RemoteInterfaceClass.connected)
            {
                if (ev == RemoteEvQ.SOS_EV)
                {
                }
                else if (ev == RemoteEvQ.EOS_EV)
                {
                }
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
                //-- Define sink for events
                RemotingConfiguration.RegisterWellKnownServiceType(requiredType, "ServerEvents", WellKnownObjectMode.Singleton);
                NotifySink sink = new NotifySink();
                //-- Add event handler                
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
                            ////-- Start-up measurement thread
                            //StartMeasurement();
                        }
                        catch (Exception)
                        {
                            //-- Assume problem
                            connectedToServer = false;
                            RemoteInterfaceClass.connected = false;
                        }
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
                            versInfo.aliveCount = aliveCount++;
                            remoteObject.InfoFunction(versInfo);
                            connectedToServer = true;
                            RemoteInterfaceClass.connected = true;
                            //System.Threading.
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
        // LOCAL EVENTS
        //-----------------------------------------------------------------------------------------
        //-- Update screen visual components ------------------------------------------------------
        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            try
            {
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

                if(INFORMATION.logItem.Length>0)
                {
                    //string Time = Convert.ToString(DateTime.Now);
                    consoleTextBox.AppendText(INFORMATION.logItem);
                    INFORMATION.logItem = "";
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("refreshTimer_Tick", exc.Message);
            }
        }
        //-- Update screen measurement data components --------------------------------------------
        private void pvarUpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (RemotePVarQ.Ready)
                {
                    foreach (InfoPVar pvarInfo in RemotePVarQ.pvarList)
                    {
                        if (m1ComInterface != null) m1ComInterface.update(pvarInfo);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("pvarUpdateTimer_Tick", exc.Message);
            }
        }
        //-- Display about box application properties ---------------------------------------------
        private void aboutToolStripButton_Click(object sender, EventArgs e)
        {
            AboutBox anAboutBox = new AboutBox();
            anAboutBox.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                //-- Load INI file data    
                FileClass.iniFile = new IniFile(FileClass.rootDir + FileClass.filePath + FileClass.fileName, FileClass.rootDir + FileClass.filePath + FileClass.pidName);
                FileClass.iniFile.LoadData();
                FileClass.iniFile.PutData();
                FileClass.iniFile.LoadPID();
                FileClass.iniFile.PutPID();

                string strColor = Color.Black.ToString();
                int intColor = Color.Black.ToArgb();
                //Color myColor;
                //System.Drawing.Color.FromName
                remoteServerAddressTextBox.Text = ConfigDataClass.remoting.address.ToString();
                remoteServerPortTextBox.Text = ConfigDataClass.remoting.port.ToString();

                //-- Establish remote SV connection if configured
                if (ConfigDataClass.remoting.autoConnect)
                {
                    //-- Start event handler client thread
                    clientThread = new Thread(new ThreadStart(RemotingEventClient));
                    clientThread.Name = "RemotingEventClient";
                    clientThread.Start();
                }

                if (ConfigDataClass.m1com.autoConnect)
                {
                    m1comThread = new Thread(new ThreadStart(M1ComLoad));
                    this.Closed += new System.EventHandler(M1ComClosed);
                    m1comThread.Name = "M1ComLoad";
                    m1comThread.Start();
                }

                refreshTimer.Enabled = true;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "MainForm_Load Error");
            }
        }

        //-- Close all resources gracefully -------------------------------------------------------
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            WarningDialogBox closeArchiveWarning = new WarningDialogBox("Closing GAUGm1com application.....\n\nPlease confirm!");
            if (closeArchiveWarning.ShowDialog() == DialogResult.Cancel)
                e.Cancel = true;
            else try
                {
                    ErrorHandlerClass.ReportEvent("Application Closed");
                    Thread.Sleep(1500);
                    xmdNotifyIcon.Dispose();
                    Process[] myProcesses;
                    myProcesses = Process.GetProcessesByName("GAUGload");
                    foreach (Process myProcess in myProcesses)
                    {
                        myProcess.CloseMainWindow();
                        myProcess.Kill();
                    }
                    //if (clientThread.IsAlive) clientThread.Abort();                   
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "MainForm_FormClosing Error");
                }
        }
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
        //-- Initiate remote client thread --------------------------------------------------------
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
                refreshTimer.Start();
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("Remote Connection Fault", exc);
            }
        }
        //-----------------------------------------------------------------------------------------
        //--------M1Com.NET Load ---------------------------------------------------------------------
        private void M1ComLoad()
        {
            try
            {
                if (m1ComInterface == null)
                {
                    m1ComInterface = new M1ComInterface();
                    m1ComInterface.start();
                }
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("M1ComLoad Fault", exc);
            }
        }
        private void M1ComClosed(object sender, EventArgs e)
        {
            try
            {
                if(m1ComInterface != null)
                {
                    m1ComInterface.stop();
                }                
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("M1ComClosed Fault", exc);
            }
        }
    }

}
//=================================================================================================