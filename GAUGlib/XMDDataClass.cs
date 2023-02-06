//=================================================================================================
//  Project:    QCS Center
//  Module:     XMDDataClass.cs                                                                         
//  Author:     Jim Zhang
//  Date:       16/12/2019
//  
//  Details:    Definition of XMD data structures
//  
//=================================================================================================
using System;
using M1ComNET;

namespace GAUGlib
{
    //-- Varaiable Types
    public enum VARTYPE_INDEX : ushort
    {
        BOOL=0,
        INTEGER,
        FLOAT,
        STRING,
        BOOLARRAY,      // DIO card
        INTEGERARRAY,   // Counter card
        FLOATARRAY,     // AIO card
        //STRINGARRAY,
        TOTAL_VARTYPE
    }
    public class VARTYPE_NUMBERS
    {
        public const UInt16 BOOL = 20;
        public const UInt16 INTEGER = 20;
        public const UInt16 FLOAT = 20;
        public const UInt16 STRING = 20;
        public const UInt16 BOOLARRAY = 20;
        public const UInt16 INTEGERARRAY = 20;
        public const UInt16 FLOATARRAY = 20;
        //public const UInt16 STRINGARRAY = 20;
    }
    //-- Bool data structure maintained in XMD ----------------------------------------------------
    [SerializableAttribute()]
    public class BoolClass
    {
        public bool[] Value = new bool[VARTYPE_NUMBERS.BOOL];
        public bool[] Modified = new bool[VARTYPE_NUMBERS.BOOL];
        public string[] Name = new string[VARTYPE_NUMBERS.BOOL];
        public Item[] Item = new Item[VARTYPE_NUMBERS.BOOL];

        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }   
    //-- Integer data structure maintained in XMD ----------------------------------------------------
    [SerializableAttribute()]
    public class IntegerClass
    {
        public int[] Value = new int[VARTYPE_NUMBERS.INTEGER];
        public bool[] Modified = new bool[VARTYPE_NUMBERS.INTEGER];
        public string[] Name = new string[VARTYPE_NUMBERS.INTEGER];
        public Item[] Item = new Item[VARTYPE_NUMBERS.INTEGER];

        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- Float data structure maintained in XMD ----------------------------------------------------
    [SerializableAttribute()]
    public class FloatClass
    {
        public float [] Value = new float[VARTYPE_NUMBERS.FLOAT];
        public bool[] Modified = new bool[VARTYPE_NUMBERS.FLOAT];
        public string[] Name = new string[VARTYPE_NUMBERS.FLOAT];
        public Item[] Item = new Item[VARTYPE_NUMBERS.FLOAT];

        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- Float array data structure maintained in XMD ----------------------------------------------------
    [SerializableAttribute()]
    public class FloatArrayClass
    {
        public float[][] Value = new float[VARTYPE_NUMBERS.FLOATARRAY][];
        public bool[][] Modified = new bool[VARTYPE_NUMBERS.FLOATARRAY][];
        public string[] Name = new string[VARTYPE_NUMBERS.FLOATARRAY];
        public Item[] Item = new Item[VARTYPE_NUMBERS.FLOATARRAY];
        public FloatArrayClass()
        {
            for (int i = 0; i < VARTYPE_NUMBERS.FLOATARRAY; i++)
            {
                Value[i] = new float[SIZE.FLOATARRAY];
                Modified[i] = new bool[SIZE.FLOATARRAY];
            }
        }
        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- String structure maintained in XMD ----------------------------------------------------
    [SerializableAttribute()]
    public class StringClass
    {
        public string[] Value = new string[VARTYPE_NUMBERS.STRING];
        public bool[] Modified = new bool[VARTYPE_NUMBERS.STRING];
        public string[] Name = new string[VARTYPE_NUMBERS.STRING];
        public Item[] Item = new Item[VARTYPE_NUMBERS.STRING];

        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- Bool Array structure maintained in XMD ----------------------------------------------------
    [SerializableAttribute()]
    public class BoolArrayClass
    {
        public bool[][] Value = new bool[VARTYPE_NUMBERS.BOOLARRAY][];
        public bool[][] Modified = new bool[VARTYPE_NUMBERS.BOOLARRAY][];
        public string[] Name = new string[VARTYPE_NUMBERS.BOOLARRAY];
        public Item[] Item = new Item[VARTYPE_NUMBERS.BOOLARRAY];
        public BoolArrayClass()
        {
            for (int i = 0; i < VARTYPE_NUMBERS.BOOLARRAY; i++)
            {
                Value[i] = new bool[SIZE.BOOLARRAY];
                Modified[i] = new bool[SIZE.BOOLARRAY];
            }
        }

        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //-- Integer Array structure maintained in XMD ----------------------------------------------------
    [SerializableAttribute()]
    public class IntegerArrayClass
    {
        public int[][] Value = new int[VARTYPE_NUMBERS.INTEGERARRAY][];
        public bool[][] Modified = new bool[VARTYPE_NUMBERS.INTEGERARRAY][];
        public string[] Name = new string[VARTYPE_NUMBERS.INTEGERARRAY];
        public Item[] Item = new Item[VARTYPE_NUMBERS.INTEGERARRAY];
        public IntegerArrayClass()
        {
            for (int i = 0; i < VARTYPE_NUMBERS.BOOLARRAY; i++)
            {
                Value[i] = new int[SIZE.INTEGERARRAY];
                Modified[i] = new bool[SIZE.INTEGERARRAY];
            }
        }

        //-- Shallow Copy using the IClonable interface
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //---------------------------------------------------------------------------------------------

}
//=================================================================================================
