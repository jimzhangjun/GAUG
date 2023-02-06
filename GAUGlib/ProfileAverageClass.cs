//=================================================================================================
//  Project:    RM312/SIPRO SIPROlib
//  Module:     ProfileAveragerClass.cs                                                                         
//  Author:     Andrew Powell
//  Date:       24/05/2011
//  
//  Details:    Averager class for combined source view data
//
//              Maintains a running average for a number of data scans
//   
//                   
//=================================================================================================
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.ApplicationBlocks.ExceptionManagement;


namespace SiPROlib
{
    public class ProfileAverageClass
    {
        private Queue<float[]> profArrayQueue;
        private double[] runningTotal = new double[SIZE.PROF];
        private float[] runningAvg = new float[SIZE.PROF];
        private int avgCount = 0;
        private float clAvg = 0;

        //-- Constructor initialises data -------------------------------------
        public ProfileAverageClass(int count)
        {
            //-- Validate count to positive number less than one minute
            avgCount = ForceRange.FrcInt(1, 60 * (1000 / 5), count);
            profArrayQueue = new Queue<float[]>(avgCount);
        }
        //-- Add data to averager ---------------------------------------------
        public void AddData(float[] newData)
        {
            float[] oldData = new float[SIZE.PROF];
            try
            {
                //-- Remove oldest value from total
                if (profArrayQueue.Count >= avgCount)
                    oldData = profArrayQueue.Dequeue();
                //-- Add new data to queue
                profArrayQueue.Enqueue((float[])newData.Clone());
                //-- Calculate total from actual array data
                Array.Clear(runningTotal, 0, SIZE.PROF);
                foreach (float[] array in profArrayQueue)
                    for (int i = 0; i < SIZE.PROF; i++)
                        runningTotal[i] += array[i];
                //-- Calculate averages
                Array.Clear(runningAvg, 0, SIZE.PROF);
                for (int i = 0; i < SIZE.PROF; i++)
                    runningAvg[i] = (float)(runningTotal[i] / profArrayQueue.Count);
            }
            catch (Exception exc)
            {
                ExceptionManager.Publish(exc);
            }
        }
        //-- Retrieve data from averager ----------------------------------------------------------
        public int GetCount()
        {
            return avgCount;
        }
        //-- Retrieve count from averager ---------------------------------------------------------
        public float[] GetAvg()
        {
            return runningAvg;
        }
        //-- Retrieve count from averager ---------------------------------------------------------
        public float GetClAvg()
        {
            return clAvg;
        }
        //-- Set averager count -------------------------------------------------------------------
        public void SetAvgCount(int count)
        {
            avgCount = ForceRange.FrcInt(1, 60 * (1000 / 5), count);
            profArrayQueue = new Queue<float[]>(avgCount);
            for (int i = 0; i < SIZE.PROF; i++)
                runningTotal[i] = 0;
        }
        //-----------------------------------------------------------------------------------------
    }
}

