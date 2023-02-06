//=================================================================================================
//  Project:    GAUGlib
//  Module:     ByteClass.cs                                                                         
//  Author:     Andrew Powell
//  Date:       15/06/2012
//  
//  Details:    Byte manipulation functions 
//  
//=================================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GAUGlib
{
    //-- Reverse byte streams for big-endianess -------------------------------
    public class ReverseEndian
    {
        public static Int16 BytesInt16(Int16 value)
        {
            return (Int16)((value & 0xFFU) << 8 | (value & 0xFF00U) >> 8);
        }
        public static UInt16 BytesUInt16(UInt16 value)
        {
            return (UInt16)((value & 0xFFU) << 8 | (value & 0xFF00U) >> 8);
        }
        public static Int32 BytesInt32(Int32 value)
        {
            return (Int32)((value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
                   (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24);

        }
        public static UInt32 BytesUInt32(UInt32 value)
        {
            return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
                   (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;

        }
        public static UInt64 BytesUInt64(UInt64 value)
        {
            return (value & 0x00000000000000FFUL) << 56 | (value & 0x000000000000FF00UL) << 40 |
                   (value & 0x0000000000FF0000UL) << 24 | (value & 0x00000000FF000000UL) << 8 |
                   (value & 0x000000FF00000000UL) >> 8 | (value & 0x0000FF0000000000UL) >> 24 |
                   (value & 0x00FF000000000000UL) >> 40 | (value & 0xFF00000000000000UL) >> 56;
        }
        public static Single BytesSingle(Single value)
        {
            byte[] bytes = BitConverter.GetBytes(value);     
            Array.Reverse(bytes);    
            return BitConverter.ToSingle(bytes, 0);
        }
    }
     //-- Reverse byte streams for big-endianess -------------------------------
    public class EndianWriter
    {
        public static BinaryWriter WrInt32(BinaryWriter output, Int32 value, bool bigEndian)
        {
            if (bigEndian) output.Write(ReverseEndian.BytesInt32(value));
            else output.Write(value);
            return output;
        }
        public static BinaryWriter WrInt16(BinaryWriter output, Int16 value, bool bigEndian)
        {
            if (bigEndian) output.Write(ReverseEndian.BytesInt16(value));
            else output.Write(value);
            return output;
        }
        public static BinaryWriter WrSingle(BinaryWriter output, Single value, bool bigEndian)
        {
            if (bigEndian) output.Write(ReverseEndian.BytesSingle(value));
            else output.Write(value);
            return output;
        }
    }
    //-- Bit manipulation -----------------------------------------------------
    public class BitSet
    {
        public static bool IsOnByte(byte value, byte bit)
        {
            return (value >> bit & 1) == 1;
        }
        public static void OnByte(ref byte value, byte bit)
        {
            value |= (Byte)(1 << bit);
        }
        public static bool IsOnInt16(Int16 value, byte bit)
        {
            return (value >> bit & 1) == 1;
        }
        public static void OnInt16(ref Int16 value, byte bit)
        {
            value |= (Int16)(1 << bit);
        }
        public static bool IsOn(UInt32 value, byte bit)
        {
            return (value >> bit & 1) == 1;
        }
        public static void On(ref UInt32 value, byte bit)
        {
            value |= (UInt32)(1 << bit);
        }
    }
}
//=================================================================================================
