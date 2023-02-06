//=================================================================================================================
//  Project:    SIPRO-library
//  Module:     DSPhost.cs                                                                         
//  Author:     Andrew Powell
//  Date:       02/05/2006
//  
//  Details:    Definition of DSP interface structures
//  
//=================================================================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace GAUGlib
{
    //-- Force number into specified range ------------------------------------------------------------------------
    public class InRange
    {
        public static bool IntRng(int Lower, int Upper, int Val)
        {
            if ((Val < Upper) && (Val > Lower)) return true;
            else return false;
        }
        public static bool FloatRng(float Lower, float Upper, float Val)
        {
            if ((Val < Upper) && (Val > Lower)) return true;
            else return false;
        }
    }
    //-- Force number into specified range ------------------------------------------------------------------------
    public class ForceRange
    {
        public static int FrcInt(int Lower, int Upper, int Val)
        {
            return Math.Max(Lower, Math.Min(Upper, Val));
        }
        public static float FrcFloat(float Lower, float Upper, float Val)
        {
            return Math.Max(Lower, Math.Min(Upper, Val));
        }
    }
    //-- Force number to zero -------------------------------------------------------------------------------------
    public class ToZero
    {
        public static int ZeroInt(int Lower, int Upper, int Val)
        {
            if ((Val < Upper) && (Val > Lower)) return 0;
            else return Val;
        }
        public static float ZeroFloat(float Lower, float Upper, float Val)
        {
            if ((Val < Upper) && (Val > Lower)) return 0f;
            else return Val;
        }
    }
    //-- Percentage functions -------------------------------------------------------------------------------------
    public class Percent
    {
        public static float Plus(float Perc, float Val)
        {
            return Val + ((Val * Perc) / 100);
        }
        public static float Minus(float Perc, float Val)
        {
            return Val - ((Val * Perc) / 100);
        }
        public static bool Within(float Perc, float Target, float Val)
        {
            return ((Val >= Minus(Perc, Target)) && (Val <= Plus(Perc, Target)));
        }
    }
    //-- Linear interpolation function ----------------------------------------------------------------------------
    public class Interpolate
    {
        public static float Value(float Y, float X1, float Y1, float X2, float Y2)
        {
            if (Y1 != Y2) return X1 + (((Y - Y1) * (X2 - X1)) / (Y2 - Y1));              
            else return ((X1 + X2) / 2);
        }
    }
    //-- Return the mean of an array of floats --------------------------------------------------------------------
    public class Avg
    {
        public static double Mean(double[] data)
        {
            double Sum = 0.0;
            for (int i = 0; i < data.Length; i++)
            {
                Sum += data[i];
            }
            double Avg = Sum / System.Convert.ToDouble(data.Length);

            return Avg;
        }
        public static double Mean(double[] data, int elems)
        {
            double Sum = 0.0;
            for (int i = 0; i < elems; i++)
            {
                Sum += data[i];
            }
            double Avg = 0.0;
            if (elems > 0) Avg = Sum / System.Convert.ToDouble(elems);

            return Avg;
        }
        public static int Mean(int[] data, int elems)
        {
            int Sum = 0;
            for (int i = 0; i < elems; i++)
            {
                Sum += data[i];
            }
            int Avg = 0; // Avoid div by Zero exception
            if (elems > 0) Avg = Sum / elems;

            return (int)Avg;
        }
    }
    //-- Return the standard deviation of an array of floats ------------------------------------------------------
    public class StdDev
    {
        public static double Value(double[] data)
        {
            double stdDev = 0;
            double dataAvg = 0;
            double totalVariance = 0;

            try
            {
                dataAvg = Avg.Mean(data);

                for (int i = 0; i < data.Length; i++)
                    totalVariance += Math.Pow(data[i] - dataAvg, 2);

                stdDev = Math.Sqrt(totalVariance / data.Length);

            }
            catch (Exception) { throw; }
            return stdDev;
        }
    }
    //-- Returns the length of the hypotenuse ---------------------------------------------------------------------
    public class Pythagorem
    {
        public static float Hypo(float valA, float valB)
        {
            float valC = 0;
            float absA = Math.Abs(valA);
            float absB = Math.Abs(valB);
            if (absA > absB) valC = absA * (float)Math.Sqrt(1.0 + ((absB / absA) * (absB / absA)));
            else if (absB > 0) valC = absB * (float)Math.Sqrt(1.0 + ((absA / absB) * (absA / absB)));
            return valC;
        }
    }
    //-- Increment an integer with wrap around ------------------------------------------------
    public class Increment
    {
        public static void Integer(ref Int32 counter)
        {
            if (counter < 999999) counter++;
            else counter = 0;
        }
        public static void Word(ref Int16 counter)
        {
            if (counter < 9999) counter++;
            else counter = 0;
        }
    }
    //-- Check that a given value is not zero -----------------------------------------------------
    public class IsNotZero
    {
        public static bool Double(double val)
        {
            return val != 0.0;
        }
    }
    //=============================================================================================================
}
