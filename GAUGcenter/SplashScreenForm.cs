//=================================================================================================================
//  Project:    GAUG Center
//  Module:     SplashScreenForm.cs                                                                         
//  Author:     Jim Zhang
//  Date:       16/12/2019
//  
//  Details:    Loading splash screen to prevent user input at start-up
//  
//=================================================================================================================
using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

namespace GAUGcenter
{
    public partial class SplashScreenForm : Form
    {
        //---------------------------------------------------------------------------------------------------------
        // CLASS VARIABLES
        //---------------------------------------------------------------------------------------------------------
        static SplashScreenForm ms_frmSplash = null;
        static Thread ms_oThread = null;

        private double m_dblOpacityIncrement = .05;
        private double m_dblOpacityDecrement = .1;
        private const int TIMER_INTERVAL = 10;
        private int LoadCount = 0;
        //---------------------------------------------------------------------------------------------------------
        // GLOBAL PROCEDURES
        //---------------------------------------------------------------------------------------------------------
        public SplashScreenForm()
        {
            InitializeComponent();
            //this.ClientSize = this.BackgroundImage.Size;           
            this.Opacity = .0;
            Fadetimer.Interval = TIMER_INTERVAL;
            Fadetimer.Start();
            Loadtimer.Start();
            versionLabel.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
        //-- Checks if splash screen is still visible -------------------------------------------------------------
        static public bool FormActive()
        {
            return (ms_frmSplash != null);
        }
        //-- Close the SplashScreen -------------------------------------------------------------------------------
        static public void CloseForm()
        {
            if (ms_frmSplash != null)
            {
                // Make it start going away.
                ms_frmSplash.m_dblOpacityIncrement = -ms_frmSplash.m_dblOpacityDecrement;
            }
            ms_oThread = null;  // we do not need these any more.
            ms_frmSplash = null;
        }
        //-- Display splash screen --------------------------------------------------------------------------------
        static public void ShowSplashScreen()
        {
            // Make sure it is only launched once.
            if (ms_frmSplash != null)
                return;
            ms_oThread = new Thread(new ThreadStart(SplashScreenForm.ShowForm));
            ms_oThread.IsBackground = true;
            //ms_oThread.SetApartmentState = ApartmentState.STA;
            ms_oThread.Start();
        }
        //---------------------------------------------------------------------------------------------------------
        // LOCAL PROCEDURES
        //---------------------------------------------------------------------------------------------------------
        //-- Launch splash screen ---------------------------------------------------------------------------------
        static private void ShowForm()
        {
            ms_frmSplash = new SplashScreenForm();
            Application.Run(ms_frmSplash);
        }
        //-- Fade splash screen away when closing -----------------------------------------------------------------
        private void Fadetimer_Tick(object sender, EventArgs e)
        {
            if (m_dblOpacityIncrement > 0)
            {
                if (this.Opacity < 1)
                    this.Opacity += m_dblOpacityIncrement;
            }
            else
            {
                if (this.Opacity > 0)
                    this.Opacity += m_dblOpacityIncrement;
                else
                    this.Close();
            }
            this.BringToFront();
        }
        //---------------------------------------------------------------------------------------------------------
        // LOCAL EVENTS
        //---------------------------------------------------------------------------------------------------------
        //-- Display progress timer -------------------------------------------------------------------------------
        private void Loadtimer_Tick(object sender, EventArgs e)
        {
            if (loadProgressBar.Value < loadProgressBar.Maximum) loadProgressBar.Value = LoadCount;               
            else CloseForm();
            LoadCount += 1;
        }
        //=========================================================================================================
    }
}