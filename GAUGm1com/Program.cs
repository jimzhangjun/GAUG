using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.ApplicationBlocks.ExceptionManagement;
//using QCSlib;

namespace GAUGm1com
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                //-- Load application configuration
                string rootDir = ConfigurationManager.AppSettings.Get("RootDirKey");
                if (rootDir != null) FileClass.rootDir = rootDir;
                //-- Display start-up splash screen
                //SplashScreenForm.ShowSplashScreen();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch(Exception exc)
            {
                ExceptionManager.Publish(exc);
            }
        }
    }
}
