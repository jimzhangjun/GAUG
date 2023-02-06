//=================================================================================================
//  Project:    SIPRO-library
//  Module:     SummaryResultsData.cs                                                                         
//  Author:     Neil Brain
//  Date:       15/10/2013
//  
//  Details:    Definition of SUMMARY Results data structures for Interface between Apps
//  
//=================================================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace GAUGlib
{
    //-------------------------------------------------------
    [SerializableAttribute()]
    public struct basic_stats
    {
        public Single mean;
        public Single sigma;
        public Single min;
        public Single max;
        public Single cp;
        public Single cpk;
    }
    //-------------------------------------------------------
    [SerializableAttribute()]
    public struct length_stats
    {
        public Single body_mean;
        public Single body_min;
        public Single body_max;
        public Single body_sigma;
        public Single body_cp;
        public Single body_cpk;
        public Single head_mean;
        public Single head_min;
        public Single head_max;
        public Single head_sigma;
        public Single head_cp;
        public Single head_cpk;
        public Single tail_mean;
        public Single tail_min;
        public Single tail_max;
        public Single tail_sigma;
        public Single tail_cp;
        public Single tail_cpk;
    }
    //-------------------------------------------------------
    [SerializableAttribute()]
    public class Summ_prof_float_array
    {
        public float[] data = new float[SIZE.PROF];
        public int pts_used;
    }
    //-------------------------------------------------------
    //-- SiproReport -> SiPROLib -> SiproNet ----------------
    [SerializableAttribute()]
    public class SummaryResultsData
    {
        public SetupData resSetupData = new SetupData();
        public string SOP_Time;
        public string EOP_Time = "EOP_TIME";

        //Results Stats Data
        public basic_stats cthick_stats = new basic_stats();
        public length_stats cthick_len_stats = new length_stats();

        public basic_stats width_stats = new basic_stats();
        public length_stats width_len_stats = new length_stats();

        public basic_stats cTemp_stats = new basic_stats();
        public length_stats cTemp_len_stats = new length_stats();

        public basic_stats[] BEdgeThk_stats = new basic_stats[SIZE.CWPOS];
        public length_stats[] BEdgeThk_len_stats = new length_stats[SIZE.CWPOS];

        public basic_stats[] OEdgeThk_stats = new basic_stats[SIZE.CWPOS];
        public length_stats[] OEdgeThk_len_stats = new length_stats[SIZE.CWPOS];

        public basic_stats[] cwn_stats = new basic_stats[SIZE.CWPOS];
        public length_stats[] cwn_len_stats = new length_stats[SIZE.CWPOS];

        public basic_stats[] wdg_stats = new basic_stats[SIZE.CWPOS];
        public length_stats[] wdg_len_stats = new length_stats[SIZE.CWPOS];

        public basic_stats[] thcwn_stats = new basic_stats[SIZE.CWPOS];
        public length_stats[] thcwn_len_stats = new length_stats[SIZE.CWPOS];

        public basic_stats[] thwdg_stats = new basic_stats[SIZE.CWPOS];
        public length_stats[] thwdg_len_stats = new length_stats[SIZE.CWPOS];

        public basic_stats SymmetricShape_stats = new basic_stats();
        public length_stats SymmetricShape_len_stats = new length_stats();
        public basic_stats AsymmetricShape_stats = new basic_stats();
        public length_stats AsymmetricShape_len_stats = new length_stats();

        public basic_stats FWGauge_stats = new basic_stats();
        public length_stats FWGauge_len_stats = new length_stats();

        public basic_stats CLOffset_stats = new basic_stats();
        public length_stats CLOffset_len_stats = new length_stats();

        public basic_stats AreaWedge_stats = new basic_stats();
        public length_stats AreaWedge_len_stats = new length_stats();

        //Results Thickness Profiles data
        public Summ_prof_float_array profile_meas = new Summ_prof_float_array();
        public Summ_prof_float_array bd_prof_meas = new Summ_prof_float_array();
        public Summ_prof_float_array he_prof_meas = new Summ_prof_float_array();
        public Summ_prof_float_array te_prof_meas = new Summ_prof_float_array();
        //Results thermal profile data  
        public Summ_prof_float_array therm_profile_meas = new Summ_prof_float_array();
        public Summ_prof_float_array bd_therm_prof_meas = new Summ_prof_float_array();
        public Summ_prof_float_array he_therm_prof_meas = new Summ_prof_float_array();
        public Summ_prof_float_array te_therm_prof_meas = new Summ_prof_float_array();
        //Results Shape profile.
        public Summ_prof_float_array prof_shape = new Summ_prof_float_array();

        //Results Thick Polynomials
        public int ThickPolyOrder;
        public double[] ThickPolyCoeffs_metric = new double[7];
        public double[] ThickPolyCoeffs_imperial = new double[7];
        public float PolyWidth;
        //Results Thermal Poly
        public int ThermalPolyOrder;
        public double[] ThermalPolyCoeffs_metric = new double[7];
        public double[] ThermalPolyCoeffs_imperial = new double[7];
        public float ThermalPolyWidth; 

        //Results Ridges and CatEars.
        public float[] suMost_sig_ridges_loc = new Single[5];
        public float[] suRidge_values = new Single[5];
        public int CatEar_OE_index; // relative to CL
        public float CatEar_OE_val;
        public int CatEar_BA_index; // relative to CL
        public float CatEar_BA_val;

        public Single suUsedAlloyComp;
        public Single suUsedTempComp;
        public Single suAvgS1StdzOffset;
        public Single suAvgS2StdzOffset;
        public float[] suRidge_widths = new Single[5];
        public basic_stats HotThick_stats = new basic_stats();
        public length_stats HotThick_len_stats = new length_stats();
        public basic_stats HotWidth_stats = new basic_stats();
        public length_stats HotWidth_len_stats = new length_stats();

        public float TotalLength;
        public float TotalTime;
        public float Body_start_len;
        public float Tail_start_len;
        public float HeadLength;
        public float HeadTime;
        public float BodyLength;
        public float BodyTime;
        public float TailLength;
        public float TailTime;
        public UInt16 MeasStat;
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    };

    [SerializableAttribute()]
    public class NewErrorData
    {
        public string eType = "ET";
        public int eNum;
        public string eText = "EText";
        public string eDate = "EDate";
        public string eTime = "ETime";
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }

    };

    //=============================================================================================
}
