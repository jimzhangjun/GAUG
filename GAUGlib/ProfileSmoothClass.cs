//=================================================================================================
//  Project:    RM312/SIPRO SIPROlib
//  Module:     ProfileSmoothClass.cs                                                                         
//  Author:     Andrew Powell
//  Date:       06/06/2014
//  
//  Details:    Filter smoothing class for profile arrays
//
//              
//   
//                   
//=================================================================================================
using System;
using System.Collections.Generic;
using System.Text;


namespace SiPROlib
{
    class ProfileSmoothClass
    {
        public static int[] centreBase = { 0, 0, 1, 2, 8, 2, 1, 0, 0 };
        public static int[] edgeBase = { 8, 2, 1, 1, 0, 0, 0, 0, 0 };
        private static float[] centreFilter = new float[centreBase.Length];
        private static float[] edgeFilter = new float[edgeBase.Length];

        //-- Normalise filters ------------------------------------------------
        private static float[] CalcFilters(int[] baseF)
        {
            float[] filter = new float[baseF.Length];
            int filterSum = 0;
            foreach (int i in baseF) filterSum += Math.Abs(i);
            for (int i = 0; i < baseF.Length; i++)
                filter[i] = (float)baseF[i] / filterSum;
            return filter;
        }
        //-- Linear smoothing of cross profiles -------------------------------
        public static float[] SmoothProfileFloat(int oePos, int bePos, float[] profile)
        {
            try
            {
                //-- Read filter base
                float[] filteredThk = new float[SIZE.PROF];
                centreBase = (int[])ConfigDataClass.gauging.centreFilter.Clone();
                edgeBase = (int[])ConfigDataClass.gauging.edgeFilter.Clone();
                //-- Normalise filters
                centreFilter = CalcFilters(centreBase);
                edgeFilter = CalcFilters(edgeBase);
                //-- Apply smoothing
                int edgeFilterWidth = Array.IndexOf(edgeFilter, 0.0f);
                int filterHalfWidth = centreFilter.Length / 2;
                //-- Open edge filter
                for (int edgePos = oePos; edgePos < oePos + edgeFilterWidth; edgePos++) 
                {
                    float sum = 0;
                    for (int j = 0; j <= edgeFilterWidth; j++)
                    {
                        if (profile[edgePos + j] != 0)
                            sum = sum + edgeFilter[j] * profile[edgePos + j];
                    }
                    filteredThk[edgePos] = sum;
                }
                for (int edgePos = oePos; edgePos < oePos + edgeFilterWidth; edgePos++)
                {
                    profile[edgePos] = filteredThk[edgePos];
                }
                //-- Back edge filter
                for (int edgePos = bePos; edgePos > bePos - edgeFilterWidth; edgePos--)
                {
                    float sum = 0;
                    for (int j = 0; j <= edgeFilterWidth; j++)
                    {
                        if (profile[edgePos - j] != 0)
                            sum = sum + edgeFilter[j] * profile[edgePos - j];
                    }
                    filteredThk[edgePos] = sum;
                }
                for (int edgePos = bePos; edgePos > bePos - edgeFilterWidth; edgePos--)
                {
                     profile[edgePos] = filteredThk[edgePos];
                }
                //-- Centre filter
                for (int centrePos = oePos + edgeFilterWidth; centrePos <= bePos - edgeFilterWidth; centrePos++)
                {
                    float sum = 0;
                    for (int j = -filterHalfWidth; j <= filterHalfWidth; j++)
                    {
                        if (profile[centrePos + j] != 0)
                            sum = sum + centreFilter[j + filterHalfWidth] * profile[centrePos + j];
                    }
                    filteredThk[centrePos] = sum;
                }

                return filteredThk;
            }
            catch (Exception exc)
            {
                ErrorHandlerClass.ReportException("Fitting smoothing filter", exc);
                return profile;
            }
        }
        ////-- Linear smoothing of cross profiles -------------------------------
        //public static int[] SmoothProfileInt(int oePos, int bePos, int[] profile)
        //{
        //    try
        //    {
        //        //-- Read filter base
        //        int[] filteredThk = new int[SIZE.PROF];
        //        int[] baseF = (int[])filterBase.Clone();
        //        //-- Normalise filters
        //        centreFilter = CalcFilters(baseF);
        //        for (int edgePos = 3; edgePos >= 0; edgePos--)
        //        {
        //            baseF[3 - edgePos] = 0;
        //            oeFilter[edgePos] = CalcFilters(baseF);
        //        }
        //        baseF = (int[])filterBase.Clone();
        //        for (int edgePos = 3; edgePos >= 0; edgePos--)
        //        {
        //            baseF[5 + edgePos] = 0;
        //            beFilter[edgePos] = CalcFilters(baseF);
        //        }
        //        //-- Apply smoothing
        //        int filterHalfWidth = centreFilter.Length / 2;
        //        //-- Open edge filters
        //        for (int edgePos = oePos; edgePos < oePos + filterHalfWidth; edgePos++)
        //        {
        //            float sum = 0;
        //            for (int j = -filterHalfWidth; j <= filterHalfWidth; j++)
        //            {
        //                if (profile[edgePos + j] != 0)
        //                    sum = sum + oeFilter[edgePos - oePos][j + filterHalfWidth] * profile[edgePos + j];
        //            }
        //            filteredThk[edgePos] = (int)sum;
        //        }
        //        //-- Centre filter
        //        for (int centrePos = oePos + filterHalfWidth; centrePos <= bePos - filterHalfWidth; centrePos++)
        //        {
        //            float sum = 0;
        //            for (int j = -filterHalfWidth; j <= filterHalfWidth; j++)
        //            {
        //                if (profile[centrePos + j] != 0)
        //                    sum = sum + centreFilter[j + filterHalfWidth] * profile[centrePos + j];
        //            }
        //            filteredThk[centrePos] = (int)sum;
        //        }
        //        //-- Back edge filters
        //        for (int edgePos = bePos - filterHalfWidth + 1; edgePos < bePos; edgePos++)
        //        {
        //            float sum = 0;
        //            for (int j = -filterHalfWidth; j <= filterHalfWidth; j++)
        //            {
        //                if (profile[edgePos + j] != 0)
        //                    sum = sum + beFilter[edgePos - (bePos - filterHalfWidth + 1)][j + filterHalfWidth] * profile[edgePos + j];
        //            }
        //            filteredThk[edgePos] = (int)sum;
        //        }

        //        return filteredThk;
        //    }
        //    catch (Exception exc)
        //    {
        //        ErrorHandlerClass.ReportException("Fitting smoothing filter", exc);
        //        return profile;
        //    }
        //}
    }
    //-------------------------------------------------------------------------

}
//=================================================================================================
