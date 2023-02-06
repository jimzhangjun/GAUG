//=================================================================================================
//  Project:    RM312/SIPRO GAUGlib
//  Module:     DiodeAveragerClass.cs                                                                         
//  Author:     Andrew Powell
//  Date:       16/04/2015
//  
//  Details:    Averager class for diode based data
//
//              Maintains a running average for a number of data scans
//   
//                   
//=================================================================================================
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.ApplicationBlocks.ExceptionManagement;



namespace GAUGlib
{
    public class ArrayAverageClass
    {
        private Queue<int[]> profArrayQueue;
        private double[] runningTotal = new double[SIZE.RAW];
        private float[] runningAvg = new float[SIZE.RAW];
        private int avgCount = 0;
        private float clAvg = 0;

        //-- Constructor initialises data -------------------------------------
        public ArrayAverageClass(int count)
        {
            //-- Validate count to positive number less than one minute
            avgCount = ForceRange.FrcInt(1, 60 * (1000 / 5), count);
            profArrayQueue = new Queue<int[]>(avgCount);
        }
        //-- Add data to averager ---------------------------------------------
        public void AddData(int[] newData)
        {
            int[] oldData = new int[SIZE.RAW];
            try
            {
                //-- Remove oldest value from total
                if (profArrayQueue.Count >= avgCount)
                    oldData = profArrayQueue.Dequeue();
                //-- Add new data to queue
                profArrayQueue.Enqueue((int[])newData.Clone());
                //-- Calculate total from actual array data
                Array.Clear(runningTotal, 0, SIZE.RAW);
                foreach (int[] array in profArrayQueue)
                    for (int i = 0; i < SIZE.RAW; i++)
                        runningTotal[i] += array[i];
                //-- Calculate averages
                Array.Clear(runningAvg, 0, SIZE.RAW);
                for (int i = 0; i < SIZE.RAW; i++)
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
            profArrayQueue = new Queue<int[]>(avgCount);
            for (int i = 0; i < SIZE.RAW; i++)
                runningTotal[i] = 0;
        }
        //-----------------------------------------------------------------------------------------
    }
}

