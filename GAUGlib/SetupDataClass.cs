//=================================================================================================
//  Project:    SIPRO-library
//  Module:     SetupDataClass.cs                                                                         
//  Author:     Andrew Powell
//  Date:       09/09/2008
//  
//  Details:    Definition of SETUP data structure
//  
//=================================================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace GAUGlib
{
    //-- Setup data structure -------------------------------------------------
    [SerializableAttribute()]
    public struct SetupData
    {
        public char[] sCoilID;
        public float sNomThick;
        public float sNomThick2;
        public float sNomWidth;
        public float sVThinLim;
        public float sThinLim;
        public float sThickLim;
        public float sVThickLim;
        public float sStripTemp;
        public int sAlloyCompMode;
        public float sAlloyComp;
        public char[] sAlloyName;
        public float[] sCompositionData;
        public float sMiscComp;
        public float sStripAngle;
        public float sTempComp;
        public float sProfSeq;
        public char[] sChargeNo;
        public float sInputAI;
        public int sSeqNum;
        public char[] sTempName;
        public char[] sMisc1;
        public char[] sMisc2;
        public char[] sMisc3;
        public float sNomThickHot;
        public float sNomWidthHot;
        public float sSpareFloat3;
        public float sSpareFloat4;
        public float sSpareFloat5;
        public float sSpareFloat6;
        public float sSpareFloat7;
        public float sSpareFloat8;
        public float sSpareFloat9;
        public int sSpareInt1;
        public int sSpareInt2;
        public int sSpareInt3;
        public int sSpareInt4;
        public bool newSetupData;
    }  
}
//=================================================================================================
