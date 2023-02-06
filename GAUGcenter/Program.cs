using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace GAUGcenter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //-- Handle all unhandled exceptions
            //Application.ThreadException += new ThreadExceptionEventHandler(ErrorForm.UnhandledExceptionCatcher); 
            //-- Load application configuration
            string rootDir = ConfigurationManager.AppSettings.Get("RootDirKey");
            if (rootDir != null) DIRPATH.ROOT = rootDir;
            //-- Only allow a single instance of the application
            Process Current = Process.GetCurrentProcess();
            Process[] Processes = Process.GetProcessesByName(Current.ProcessName);
            if (Processes.Length > 1)
            {
                WarningDialogBox startupWarning = new WarningDialogBox("Program instance already running!");
                startupWarning.ShowDialog();
            }
            else
                try
                {
                    //-- Start splash screen
                    SplashScreenForm.ShowSplashScreen();

                    Application.EnableVisualStyles(); 
                    Application.SetCompatibleTextRenderingDefault(false);                   

                    //-- Load INI file data    
                    DIRPATH.iniFile = new IniFile(DIRPATH.ROOT + DIRPATH.CFG + DIRPATH.NAME);
                    DIRPATH.iniFile.LoadData();
                    DIRPATH.iniFile.PutData();

                    Application.Run(new MainForm());
                }
                catch (Exception exc)
                {
                    ExceptionManager.Publish(exc);
                    MessageBox.Show(exc.ToString());
                }
        }
    }
}
