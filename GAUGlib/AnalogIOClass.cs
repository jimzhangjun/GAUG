//=================================================================================================
//  Project:    SIPRO-library
//  Module:     AnalogDataClass.cs                                                                         
//  Author:     Andrew Powell
//  Date:       20/07/2010
//  
//  Details:    Definition of ANALOGUE data structures 
//  
//=================================================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace GAUGlib
{
    //-- M1 -> SUPER Analogue input data ----------------------------------------------------------
    [SerializableAttribute()]
    public class AnalogInputClass
    {
        public float aStripSpeed;
        public float aAGT;
        public float aDetTemp;
        public float aDewPoint;
        public float aTubeTemp1;
        public float aTubeTemp2;
        public float aToparmTemp1;
        public float aToparmTemp2;
       //public SourceSettings Act;
        //-- Mill inputs
        public float aMillPyroTemp;
        public float aMillStripAngle;
        public float aMillAI3;
        public float aMillAI4;
        public float aMillAI5;
        public float aMillAI6;
        public float aMillAI7;
        public float aMillAI8;
        //-- Gulmay status
        public float aMaxSrcTemp;
        public int aActGulmayModeS1;
        public int aActGulmayModeS2;
        public int aS1LastGulErr;
        public char[] aS1LastGulErrDateTimeStr;
        public int aS2LastGulErr;
        public char[] aS2LastGulErrDateTimeStr;
        //-- Following data from M1 profibus and is stored here
        public int StripSpeed = 0;
        public int StripLength = 0;
        public int CastLength = 0;
        public int RolledLength = 0;
        //-- Alarms from M1 AI stored here
        public int AnalogueInputAlarms = 0;
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    };
    //=============================================================================================
}
