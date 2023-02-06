//=================================================================================================
//  Project:    RM312/SIPRO SIPROview
//  Module:     Program.cs                                                                         
//  Author:     Andrew Powell
//  Date:       21/04/2010
//  
//  Details:    SIPRO viewmeter interface - application
//  
//=================================================================================================
using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;

using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace GAUGview
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //-- Load application configuration
            string rootDir = ConfigurationManager.AppSettings.Get("RootDirKey");
            if (rootDir != null) FileClass.rootDir = rootDir;
            //-- Only allow a single instance of the application
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            if (processes.Length > 1)
            {
                WarningDialogBox startupWarning = new WarningDialogBox("Program instance already running!");
                startupWarning.ShowDialog();
            }
            else
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new ViewMainForm());
                }
                catch (Exception exc)
                {
                    ExceptionManager.Publish(exc);
                    MessageBox.Show(exc.ToString());
                }
        }
    }
}
//=================================================================================================