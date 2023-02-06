//=================================================================================================
//  Project:    SIPRO-library
//  Module:     StopWatchClass.cs                                                                         
//  Author:     Andrew Powell
//  Date:       04/07/2011
//  
//  Details:    Definition of stop watch data class
//  
//=================================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Threading;

namespace GAUGlib
{
    //-- Stop watch class only runs at 15msec interval --------------------------------------------
    public class StopWatchClass
    {
        private int _StartTime;
        private int _StopTime;

        private int StartTime
        {
            get { return this._StartTime; }
            set { this._StartTime = value; }
        }
        private int StopTime
        {
            get { return this._StopTime; }
            set { this._StopTime = value; }
        }
        public StopWatchClass()
        {
            StartTime = 0;
            StopTime = 0;
        }
        //-- Start stopwatch
        public void Start()
        {
            StartTime =
                DateTime.Now.Hour * 60 * 60 * 1000 +
                DateTime.Now.Minute * 60 * 1000 +
                DateTime.Now.Second * 1000 +
                DateTime.Now.Millisecond;
        }
        //-- Stop stopwatch
        public void Stop()
        {
            StopTime =
                DateTime.Now.Hour * 60 * 60 * 1000 +
                DateTime.Now.Minute * 60 * 1000 +
                DateTime.Now.Second * 1000 +
                DateTime.Now.Millisecond;
        }
        //-- Reset stopwatch
        public void Reset()
        {
            StartTime = DateTime.Now.Millisecond;
            StopTime = DateTime.Now.Millisecond;
        }
        //-- Return elapsed time
        public string GetTime()
        {
            int CurrentTime;
            float Elasped;

            CurrentTime =
                DateTime.Now.Hour * 60 * 60 * 1000 +
                DateTime.Now.Minute * 60 * 1000 +
                DateTime.Now.Second * 1000 +
                DateTime.Now.Millisecond;

            if (StopTime == 0)
                Elasped = (CurrentTime - StartTime);
            else
                Elasped = (StopTime - StartTime);
            return Elasped.ToString();
        }
    }
    //-- High Performance Stopwatch utility -------------------------------------------------------
    public class HiPerfTimer
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(
            out long lpPerformanceCount);
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(
            out long lpFrequency);
        private long startTime, stopTime;
        private long freq;

        //-- Constructor
        public HiPerfTimer()
        {
            startTime = 0;
            stopTime = 0;

            if (QueryPerformanceFrequency(out freq) == false)
            {
                // high-performance counter not supported
                throw new Win32Exception();
            }
        }
        //-- Start the timer
        public void Start()
        {
            // lets do the waiting threads there work
            Thread.Sleep(0);
            QueryPerformanceCounter(out startTime);
        }
        //-- Stop the timer
        public void Stop()
        {
            QueryPerformanceCounter(out stopTime);
        }
        //-- Returns the duration of the timer (in seconds)
        public double Duration
        {
            get
            {
                return (double)(stopTime - startTime) / (double)freq;
            }
        }
    }
    //---------------------------------------------------------------------------------------------
}
//=================================================================================================