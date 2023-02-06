//=================================================================================================
//  Project:    SIPRO-library
//  Module:     PolynomialClass.cs                                                                         
//  Author:     Andrew Powell
//  Date:       28/02/2011
//  
//  Details:    Polynomial fitting routines	
//      
//  History:    The functionality of this class was inherited from the DSP project:
//                  File:		polynomial.c														
//                  Start:		27/1/03																
//                  Author:		P Kelly	
//                   
//=================================================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace GAUGlib
{
    public enum POLY_FIT_RESULT
    {
        FIT_OK = 0,		            /*	fitted without problem					*/
        FIT_MAX_POLY_ORDER = 1,		/*	fitted, order limited to MAX_POLY_ORDER	*/
        FIT_MIN_POLY_ORDER = 2,		/*	fitted, order set to MIN_POLY_ORDER		*/
        FIT_FAILED = 3,		        /*	singularity encountered					*/
    }

    public class PolynomialClass
    {
        private const int MAX_POLY_ORDER = 6;
        private const int MAX_COEFFS = MAX_POLY_ORDER + 1;

        private const int MIN_POLY_ORDER = 1;
        private const int MAX_SUMS = MAX_POLY_ORDER * 2 + 1;
        private const int POLY_ARRAY_SIZE = MAX_COEFFS + 1;

        /***********************************************************************************/
        public static float PolyEval(float[] coeff,
                                     int order,
                                     float x)
        {
            /*	Evaluate a polynomial at x */
            float y = coeff[order];
            for (int i = order - 1; i >= 0; i--)
                y = y * x + coeff[i];
            return y;
        }
        /***********************************************************************************/
        public static float diffPolyEval(float[] coeff,
                           int order,
                           float x)
        {
            /*	Evaluate the differential of a polynomial at x */
            float dydx = coeff[order] * (float)order;
            for (int i = order - 1; i > 0; i--) dydx = dydx * x + coeff[i] * (float)i;
            return dydx;
        }
        /***********************************************************************************/
        public static void IntCoeffs(float[] coeff,
                                     int order,
                                     float bound1,
                                     float[] int_coeff)
        {
            /*	Evaluate integrated coefficients */
            int_coeff[0] = bound1;
            for (int i = 1; i <= order + 1; ++i) int_coeff[i] = coeff[i - 1] / (float)i;
        }
        /***********************************************************************************/
        public static POLY_FIT_RESULT FitPoly(float[] x,
                                              float[] y,
                                              int n,
                                              int order,
                                              float[] coeffs)
        {
            double[] x_sums = new double[MAX_SUMS];
            int i, j, nc, test = 0;
            double d = 0, dummy;
            double[] coeff_dummy = new double[MAX_COEFFS + 1]; //-- indexed 1..7
            int[] indx = new int[POLY_ARRAY_SIZE];
            double[][] a = new double[POLY_ARRAY_SIZE][];
            for (int coeff = 0; coeff < POLY_ARRAY_SIZE; coeff++)
                a[coeff] = new double[POLY_ARRAY_SIZE];

            POLY_FIT_RESULT polyFitResult = POLY_FIT_RESULT.FIT_OK;

            if (order > MAX_POLY_ORDER)
            {
                order = MAX_POLY_ORDER;
                polyFitResult = POLY_FIT_RESULT.FIT_MAX_POLY_ORDER;
            }
            else if (order < MIN_POLY_ORDER)
            {
                order = MIN_POLY_ORDER;
                polyFitResult = POLY_FIT_RESULT.FIT_MIN_POLY_ORDER;
            }
            nc = order + 1;

            /*	clear the arrays	*/
            for (i = 0; i <= MAX_POLY_ORDER; ++i)
            {
                coeffs[i] = 0.0f;
                coeff_dummy[i] = 0.0;
            }
            for (i = 0; i < MAX_SUMS; ++i) x_sums[i] = 0.0;

            /*	calculate the elements of the sum_vector */
            x_sums[0] = (double)n;
            for (i = 0; i < n; ++i)
            {
                dummy = 1;
                for (j = 1; j <= order * 2; ++j)
                {
                    dummy *= (double)x[i];
                    x_sums[j] += dummy;
                }
            }

            /*	then assign to the sums matrix */
            for (i = 0; i <= order; ++i)
            {
                for (j = 0; j <= order; ++j) a[i + 1][j + 1] = x_sums[i + j];
            }

            /*	calculate the elements of the y_sums vector */
            for (i = 0; i < n; ++i)
            {
                dummy = (double)y[i];
                coeff_dummy[0] += dummy;
                for (j = 1; j <= order; ++j)
                {
                    dummy *= (double)x[i];
                    coeff_dummy[j] += dummy;
                }
            }

            /*	Run the LU decomposition and backsubstitution */
            test = ludcmpd(a, nc, indx, d);

            //double[] offsetCoeff = new double[MAX_COEFFS];
            //Buffer.BlockCopy(coeff_dummy, sizeof(double), offsetCoeff, 0, (MAX_COEFFS - 1) * sizeof(double));
            
            if (test == 1)
            {
                lubksbd(a, nc, indx, coeff_dummy);
                for (i = 0; i <= order; ++i) coeffs[i] = (float)coeff_dummy[i + 1];
            }
            else
            {
                for (i = 0; i < MAX_COEFFS; ++i) coeffs[i] = 0;
                polyFitResult = POLY_FIT_RESULT.FIT_FAILED;
            }

            return (polyFitResult);
        }

        const float TINY = 1.0e-20f;

        //-- LU decomposition ---------------------------------------------------------------------
        static int ludcmpd(double[][] a, int n, int[] indx, double d)
        {
            int i, imax = 0, j, k;
            double big, dum, sum, temp;
            double[] vv = new double[POLY_ARRAY_SIZE];

            d = 1.0;
            for (i = 1; i <= n; i++)
            {
                big = 0.0;
                for (j = 1; j <= n; j++)
                    if ((temp = Math.Abs(a[i][j])) > big) big = temp;
                if (big == 0.0) return (0);/* Singular matrix */
                vv[i] = 1.0 / big;
            }
            for (j = 1; j <= n; j++)
            {
                for (i = 1; i < j; i++)
                {
                    sum = a[i][j];
                    for (k = 1; k < i; k++) sum -= a[i][k] * a[k][j];
                    a[i][j] = sum;
                }
                big = 0.0;
                for (i = j; i <= n; i++)
                {
                    sum = a[i][j];
                    for (k = 1; k < j; k++)
                        sum -= a[i][k] * a[k][j];
                    a[i][j] = sum;
                    if ((dum = vv[i] * Math.Abs(sum)) >= big)
                    {
                        big = dum;
                        imax = i;
                    }
                }
                if (j != imax)
                {
                    for (k = 1; k <= n; k++)
                    {
                        dum = a[imax][k];
                        a[imax][k] = a[j][k];
                        a[j][k] = dum;
                    }
                    d = -d;
                    vv[imax] = vv[j];
                }
                indx[j] = imax;
                if (a[j][j] == 0.0) a[j][j] = TINY;
                if (j != n)
                {
                    dum = 1.0 / (a[j][j]);
                    for (i = j + 1; i <= n; i++) a[i][j] *= dum;
                }
            }
            return (1);
        }

        //-- LU back substitution -----------------------------------------------------------------
        static void lubksbd(double[][] a, int n, int[] indx, double[] b)
        {
            int i, ii = 0, ip, j;
            double sum;

            for (i = 1; i <= n; i++)
            {
                ip = indx[i];
                sum = b[ip];
                b[ip] = b[i];
                if (ii == 1)
                    for (j = ii; j <= i - 1; j++) sum -= a[i][j] * b[j];
                else if (sum == 1) ii = i;
                b[i] = sum;
            }
            for (i = n; i >= 1; i--)
            {
                sum = b[i];
                for (j = i + 1; j <= n; j++) sum -= a[i][j] * b[j];
                b[i] = sum / a[i][i];
            }
        }
    }
    //=================================================================================================
    //  Project:    RM312VE XMDmeas
    //  Module:     PolynomialClass.cs                                                                         
    //  Author:     Neil Brain
    //  Date:       04/04/2011
    //  
    //  Details:    Tata Specific Profile Fitting routines	
    //      
    //  History:    The functionality of this class was inherited from g1812 VME RM312-g1812_poly.pas
    //
    //
    //          a0	The offset.
    //          a1	The first order (wedge) coefficient of the fit curve
    //          a2	The second order (crown) coefficient of the fit curve
    //          a3	The edge drop representation of the actual strip profile. The edge drop is considered to be
    //              symmetric across the centreline and therefore has a single coefficient.
    //          a4	Right asymmetric wear / work roll expansion
    //          a5	Left asymmetric wear / work roll expansion
    //          Y	Working range of the two extra fit functions
    //          xe	The measured strip width divided by 2
    //
    //          Definition of the Y parameter:  
    //          We will have 3 parameters tuneable (p1 (default 30), p2 (default 0.05) and p3(default 40))
    //
    //=================================================================================================

    public class TataPFitDataClass
    {
        public class TFProfileArray { public double[] data = new double[SIZE.PROF]; }
        public class prof_coeffs { public double[] Coeff = new double[SIZE.COEFF]; }
        public class prof_poly_param_rec
        {
            public prof_coeffs raw_coeffs = new prof_coeffs();
            public prof_coeffs filtered_coeffs = new prof_coeffs();
            public float chi_sq;
            //public float cond_of_fit;
            //public int valid_indicator;
        }

        private const int max_poly_reg = 6;
        private const int max_sums = 12; // 2 * max_poly_reg 
        private const int maxr = 7; // maximum rows in normal equations 
        private const int maxc = 8; // maximum columns in normal equations (includes total column)

        private const int MAX_X = 400;
        private const int MAX_COEF = 6;
        private const int MAX_LMATX = 2400; // MAX_X*MAX_COEF
        private const int MAX_SMATX = 36; // MAX_COEF*MAX_COEF
        private const int MAX_SYMMATX = 72; // 2*MAX_SMATX

        public class Carys { public double[] arys = new double[maxc];}
        public class CLMatx { public double[] LMatx = new double[MAX_LMATX + 1];}
        public class CLVect { public double[] LVect = new double[MAX_X + 1];}
        public class CSMatx { public double[] SMatx = new double[MAX_SMATX + 1];}
        public class CSVect { public double[] SVect = new double[MAX_COEF + 1];}
        public class CSymMatx { public double[] SymMatx = new double[MAX_SYMMATX + 1];}
        public class CTestArray1 { public double[,] TestArray1 = new double[100 + 1, 100 + 1];}
        public class CTestArray2 { public double[] TestArray2 = new double[10000 + 1];}

        // -----------------------------------------------------------------------------
        private static void MatMultMatTrans(CLMatx A, CSMatx C, int m, int n)
        {
            double sum;
            int i, j, k;
            try
            {
                for (i = 1; i <= n; i++) // For Each Line
                {
                    for (j = 1; j <= n; j++)//and each column of the resul matrix 
                    {
                        sum = 0.0f;
                        for (k = 1; k <= m; k++)//and each column of the resul matrix 
                        {
                            sum = sum + A.LMatx[n * (k - 1) + j] * A.LMatx[n * (k - 1) + i];
                        }
                        C.SMatx[n * (i - 1) + j] = sum;
                    }
                }
            }
            catch (Exception exc)
            {
                //ErrorHandlerClass.ReportError("MatMultMatTrans", exc);
                ErrorHandlerClass.ReportException("MatMultMatTrans", exc);
            }
        }
        // -----------------------------------------------------------------------------
        private static void MatTransMultVector(CLMatx A, CLVect B, CSVect C, int m, int n)
        {
            double sum;
            int j, k;

            for (j = 1; j <= n; j++)//and each row in the result
            {
                sum = 0.0f;
                for (k = 1; k <= m; k++)//and each column of the resul matrix 
                {
                    sum = sum + A.LMatx[n * (k - 1) + j] * B.LVect[k];
                }
                C.SVect[j] = sum;
            }
        }
        //-----------------------------------------------------------------------------
        private static void MatMultVector(CSMatx A, CSVect B, CSVect C, int m, int n)
        {
            double sum;
            int j, k;

            for (j = 1; j <= m; j++)//and each row in the result
            {
                sum = 0.0f;
                for (k = 1; k <= n; k++)//and each column of the resul matrix 
                {
                    sum = sum + A.SMatx[n * (j - 1) + k] * B.SVect[k];
                }
                C.SVect[j] = sum;
            }
        }
        //-----------------------------------------------------------------------------
        private static void MatInv(CSMatx Matrix, int m)
        {

            int i, j, k;
            double pivot, factor;
            int downtoi;
            int n;
            CSymMatx A = new CSymMatx();

            //{ copy the mxm matrix into a mxn matrix such that n=2xm }
            //{  make sure that the new half of the matrix is the identity }
            n = 2 * m;
            //new(A);
            for (i = 1; i <= m; i++) // For Each Line
            {
                for (j = 1; j <= n; j++) //For Each column
                {
                    if (j <= m) A.SymMatx[n * (i - 1) + j] = Matrix.SMatx[m * (i - 1) + j];
                    else
                    {
                        if (i == (j - m)) A.SymMatx[n * (i - 1) + j] = 1.0f;
                        else A.SymMatx[n * (i - 1) + j] = 0.0f;
                    }
                }
            }
            //reduce the matrix 
            for (i = 1; i <= m; i++) // For Each Line
            {   //divide line i by pivot }
                pivot = A.SymMatx[n * (i - 1) + i];
                for (j = i; j <= n; j++)
                {
                    A.SymMatx[n * (i - 1) + j] = A.SymMatx[n * (i - 1) + j] / pivot;
                }

                // substract pivot accross remaining lines }
                for (k = (i + 1); k <= m; k++)
                {
                    factor = A.SymMatx[n * (k - 1) + i];
                    for (j = i; j <= n; j++) // for each remaining columns to be processed }
                        A.SymMatx[n * (k - 1) + j] = A.SymMatx[n * (k - 1) + j] - A.SymMatx[n * (i - 1) + j] * factor;
                }

                //// write output matrix to console
                //for (li = 0; li < m; li++)
                //{
                //    write("     ");
                //    for (co = 0; co < n; co++)
                //        write((float)(A.SymMatx[n * (li - 1) + co]).ToString());
                //    write("/n");
                //}
            }

            //stage 2 substarct back into matrix to get inverse }
            for (downtoi = 1; downtoi <= (m - 1); downtoi++)
            {//start processing from the one but last line and up to the first
                i = m - downtoi;
                //substract any lines below from the one we are processing
                for (k = (i + 1); k <= m; k++)
                {
                    factor = A.SymMatx[n * (i - 1) + k];
                    for (j = i; j <= n; j++)
                    {
                        A.SymMatx[n * (i - 1) + j] = A.SymMatx[n * (i - 1) + j] - factor * A.SymMatx[n * (k - 1) + j];
                    }
                }
                //// write output matrix to console
                //for (li = 0; li < m; li++)
                //{
                //    write("     ");
                //    for (co = 0; co < n; co++)
                //        write((float)(A[n * (li - 1) + co]).ToString());
                //    write("/n");
                //}
            }
            //{ finally output inverse }
            for (i = 1; i <= m; i++) // For Each Line
            {
                for (j = 1; j <= m; j++) //For Each column
                {
                    Matrix.SMatx[m * (i - 1) + j] = A.SymMatx[n * (i - 1) + j + m];
                }
            }
        }
        // -----------------------------------------------------------------------------
        public static void TataProfileFit(ref TFProfileArray inputProf, ref TFProfileArray EvaluatedProf,
                                       int open_edge, int back_edge, int edge_exclude,
                                       ref prof_poly_param_rec param, int Xe, double cfg_P1min, int cfg_P2, int testproc, ref CLMatx myInputMatrixTemp)
        {
            int lines, columns;
            int total_lines;
            int i, j;
            CLMatx pInputMatrixTemp = new CLMatx();
            CLMatx pInputMatrix = new CLMatx();
            CLVect pInputVectorTemp = new CLVect();
            CLVect pInputVector = new CLVect();
            CSMatx pOutputMatrix = new CSMatx();
            CSVect pOutputVector = new CSVect();
            CSVect pCoefVector = new CSVect();

            int X;
            double P1;
            double P2;
            DateTime start_time;
            DateTime end_time;

            CTestArray1 ATest1 = new CTestArray1();
            CTestArray2 ATest2 = new CTestArray2();
            double V1;
            bool ok;
            prof_coeffs poly_coeffs = new prof_coeffs();


            start_time = DateTime.Now;
            //add_trace_time('corusfit begun');
            ok = (open_edge + edge_exclude) < (back_edge - edge_exclude); // Validation...
            if (ok)
            {
                //populate input matrix and vector
                //double cfg_P1min = 70; // need to put this to form/file
                //int cfg_P2 = 30;

                if (0.05 * 2 * Xe > cfg_P1min) P1 = (double)(0.05 * 2 * Xe);
                else P1 = cfg_P1min;
                P2 = cfg_P2;
                columns = 6;
                lines = (back_edge - open_edge - (2 * edge_exclude) + 1);
                total_lines = lines + (2 * edge_exclude);

                // Work out the input data for the whole strip here}
                if (testproc == 1)
                {
                    for (i = 1; i <= lines; i++) // For Each Line
                    {
                        for (j = 1; j <= columns; j++) // For Each column
                        {
                            X = ((open_edge) + i - 1) * 5;
                            switch (j)
                            {
                                case 1: pInputMatrixTemp.LMatx[columns * (i - 1) + j] = 1; break;
                                case 2: pInputMatrixTemp.LMatx[columns * (i - 1) + j] = (double)X / Xe; break;
                                case 3: pInputMatrixTemp.LMatx[columns * (i - 1) + j] = ((double)X / Xe) * ((double)X / Xe); break;
                                case 4: pInputMatrixTemp.LMatx[columns * (i - 1) + j] = (double)Math.Exp((X - Xe) / P2) + (double)Math.Exp((-X - Xe) / P2); break;
                                case 5: pInputMatrixTemp.LMatx[columns * (i - 1) + j] = (double)Math.Exp((X - Xe) / P1); break;
                                case 6: pInputMatrixTemp.LMatx[columns * (i - 1) + j] = (double)Math.Exp((-X - Xe) / P1); break;
                            }
                            pInputVectorTemp.LVect[i] = inputProf.data[((open_edge + 300)) + i - 1];
                        }
                    }
                }
                else
                {
                    for (i = 1; i <= total_lines; i++) // For Each Line
                    {
                        X = (open_edge + i - 1) * 5;
                        V1 = X / Xe;
                        pInputMatrixTemp.LMatx[columns * (i - 1) + 1] = 1;
                        pInputMatrixTemp.LMatx[columns * (i - 1) + 2] = V1;
                        pInputMatrixTemp.LMatx[columns * (i - 1) + 3] = V1 * V1;
                        pInputMatrixTemp.LMatx[columns * (i - 1) + 4] = (double)Math.Exp((X - Xe) / P2) + (double)Math.Exp((-X - Xe) / P2);
                        pInputMatrixTemp.LMatx[columns * (i - 1) + 5] = (double)Math.Exp((X - Xe) / P1);
                        pInputMatrixTemp.LMatx[columns * (i - 1) + 6] = (double)Math.Exp((-X - Xe) / P1);
                        pInputVectorTemp.LVect[i] = inputProf.data[(open_edge + 300) + i - 1];
                    }
                }
                //  Cut out the edge excluded data to work out coefficients}
                for (i = 1; i <= (lines * columns); i++) // For Each Line/column
                {
                    pInputMatrix.LMatx[i] = pInputMatrixTemp.LMatx[i + (edge_exclude * columns)];
                }
                for (i = 1; i <= lines; i++) // For Each Line/column
                {
                    pInputVector.LVect[i] = pInputVectorTemp.LVect[i + edge_exclude];
                }

                //if (testproc == 1) //add_trace_time('matrix populated 1')
                //else add_trace_time('matrix populated 2');

                //Multiply input matrix with its transpose 
                MatMultMatTrans(pInputMatrix, pOutputMatrix, lines, columns);
                //add_trace_time('matmultmattrans completed');

                // Multiply input matrix transpose with vector 
                MatTransMultVector(pInputMatrix, pInputVector, pOutputVector, lines, columns);
                //add_trace_time('mattransmultvector completed');

                //Perform matrix inversion }
                MatInv(pOutputMatrix, columns);
                //add_trace_time('matinv completed');

                //Perform final matrix multiplication }
                MatMultVector(pOutputMatrix, pOutputVector, pCoefVector, columns, columns);
                //add_trace_time('matmult completed');

                // copy coeffs into prof_poly_param_rec structur 
                for (i = 1; i <= columns; i++) // For Each column
                {
                    param.raw_coeffs.Coeff[i - 1] = pCoefVector.SVect[i];
                    param.filtered_coeffs.Coeff[i - 1] = pCoefVector.SVect[i];
                }



                //evaluate fit and calculate chi_sq at the same time for maximum speed
                double sumsqdiff = 0.0f;
                for (i = 1; i <= total_lines; i++) // For Each column
                {
                    EvaluatedProf.data[(open_edge + 300) + i - 1] = pCoefVector.SVect[1]
                        + pInputMatrixTemp.LMatx[columns * (i - 1) + 2] * pCoefVector.SVect[2]
                        + pInputMatrixTemp.LMatx[columns * (i - 1) + 3] * pCoefVector.SVect[3]
                        + pInputMatrixTemp.LMatx[columns * (i - 1) + 4] * pCoefVector.SVect[4]
                        + pInputMatrixTemp.LMatx[columns * (i - 1) + 5] * pCoefVector.SVect[5]
                        + pInputMatrixTemp.LMatx[columns * (i - 1) + 6] * pCoefVector.SVect[6];
                    sumsqdiff = sumsqdiff + (double)Math.Pow((double)(EvaluatedProf.data[(open_edge + 300) + i - 1] - pInputVectorTemp.LVect[i]), (double)2.0);
                }
                param.chi_sq = (float)Math.Sqrt(sumsqdiff / i);

                myInputMatrixTemp = pInputMatrixTemp;

                //  output some tracing for debugging }
                end_time = DateTime.Now;
                TimeSpan TimeTaken = end_time - start_time;
                int millisecs = TimeTaken.Milliseconds;

                /* Following not required...
                
                //write(' open:', open_edge: 1);
                //writeln(' back:', back_edge: 1);
                //writeln(' OE Thick at Edge Ex ', poly_prof[open_edge + edge_exclude]:7:6);
                //writeln(' BE Thick at Edge Ex ', poly_prof[back_edge - edge_exclude]:7:6);
                //writeln(' took ', time_passed(start_time, end_time): 1, 'mS');
                //for i:= 1 to columns do
                //  begin
                //  writeln(' Coef ', i: 2, ' = ', pCoefVector^[i]);
                //  end;
                //write_trace_times;
                //writeln('-----------------------');

                //VME code then conducted some other speed tests. - Required |?|
                //conduct speed test 
                start_time = DateTime.Now;
                for (i = 1; i <= 100; i++) // For Each Line
                {
                    for (j = 1; j <= 100; j++) //For Each column
                    {
                        ATest1.TestArray1[i, j] = 1;
                    }
                }
                end_time = DateTime.Now;
                TimeSpan TimeTaken1 = end_time - start_time;
                int millisecs1 = TimeTaken.Milliseconds;
                //writeln('Test1  took ', time_passed(start_time, end_time): 1, 'mS');

                start_time = DateTime.Now;
                for (i = 1; i <= 100; i++) // For Each Line
                {
                    for (j = 1; j <= 100; j++) //For Each column
                    {
                        ATest2.TestArray2[100 * (i - 1) + j] = 1;
                    }
                }
                end_time = DateTime.Now;
                TimeSpan TimeTaken2 = end_time - start_time;
                int millisec2s = TimeTaken.Milliseconds;
                //writeln('Test2  took ', time_passed(start_time, end_time): 1, 'mS');

                start_time = DateTime.Now;
                k = 1;
                for (i = 0; i < 100; i++) // For Each Line
                {
                    for (j = 1; j <= 100; j++) //For Each column
                    {
                        ATest2.TestArray2[k] = 1;
                        k = k + 1; ;
                    }
                }
                end_time = DateTime.Now;
                TimeSpan TimeTaken3 = end_time - start_time;
                int millisecs2 = TimeTaken.Milliseconds;
                //writeln('Test3  took ', time_passed(start_time, end_time): 1, 'mS');

                start_time = DateTime.Now;
                k = 1;
                for (k = 1; k <= 10000; k++) //For Each column
                {
                    ATest2.TestArray2[k] = 1;
                }
                end_time = DateTime.Now;
                TimeSpan TimeTaken4 = end_time - start_time;
                int millisecs4 = TimeTaken.Milliseconds;
                //writeln('Test4  took ', time_passed(start_time, end_time): 1, 'mS');

                */

            }
            // -----------------------------------------------------------------------------

        }
        
    }
}
//=================================================================================================
