//========================================================================
//  Project:    RM312/SIPRO GAUGlib
//  Module:     CSVParserClass.cs                                                                         
//  Author:     Andrew Powell
//  Date:       17/07/2006
//  
//  Details:    Universal CSV file reader/writer.
//              
//  
//========================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.IO;
using System.Threading;

namespace GAUGlib
{
    public class CsvParserClass
    {
        public static DataTable Parse(string data, bool headers)
        {
            return Parse(new StringReader(data), headers);
        }

        public static DataTable Parse(TextReader stream)
        {
            return Parse(stream, false);
        }

        public static DataTable Parse(TextReader stream, bool headers)
        {
            DataTable table = new DataTable();
            CsvStream csv = new CsvStream(stream);
            string[] row = csv.GetNextRow();
            if (row == null) return null;
            //-- Parse coumn headers into data table
            if (headers)
            {
                foreach (string header in row)
                {
                    if (header != null && header.Length > 0 && !table.Columns.Contains(header))
                        table.Columns.Add(header, typeof(string));
                    else if (table.Columns.Contains(header))
                    {
                        if (table.Columns.Contains(header + "a"))
                            table.Columns.Add(header + "b", typeof(string));
                        else table.Columns.Add(header + "a", typeof(string));
                    }
                    else
                        table.Columns.Add(GetNextColumnHeader(table), typeof(string));
                }
                row = csv.GetNextRow();
            }
            //-- Parse data into data table
            while (row != null)
            {
                while (row.Length > table.Columns.Count)
                    table.Columns.Add(GetNextColumnHeader(table), typeof(string));
                table.Rows.Add(row);
                row = csv.GetNextRow();
            }

            return table;
        }

        private static string GetNextColumnHeader(DataTable table)
        {
            int c = 1;
            while (true)
            {
                string h = "Column" + c++;
                if (!table.Columns.Contains(h))
                    return h;
            }
        }
    }
    //The CsvStream class does the actual work - read the CSV source in one character at a time 
    //and return meaningful chunks of decoded data, namely data items and rows.

    public class CsvStream
    {
        private TextReader stream;

        public CsvStream(TextReader s)
        {
            stream = s;
        }

        public string[] GetNextRow()
        {
            ArrayList row = new ArrayList();
            while (true)
            {
                string item = GetNextItem();
                if (item == null)
                    return row.Count == 0 ? null : (string[])row.ToArray(typeof(string));
                row.Add(item);
            }
        }

        private bool EOS = false;
        private bool EOL = false;

        private string GetNextItem()
        {
            if (EOL)
            {
                // previous item was last in line, start new line
                EOL = false;
                return null;
            }

            bool quoted = false;
            bool predata = true;
            bool postdata = false;
            StringBuilder item = new StringBuilder();

            while (true)
            {
                char c = GetNextChar(true);
                if (EOS)
                    return item.Length > 0 ? item.ToString() : null;

                if ((postdata || !quoted) && c == ',')
                    // end of item, return
                    return item.ToString();

                if ((predata || postdata || !quoted) && (c == '\x0A' || c == '\x0D'))
                {
                    // we are at the end of the line, eat newline characters and exit
                    EOL = true;
                    if (c == '\x0D' && GetNextChar(false) == '\x0A')
                        // new line sequence is 0D0A
                        GetNextChar(true);
                    return item.ToString();
                }

                if (predata && c == ' ')
                    // whitespace preceeding data, discard
                    continue;

                if (predata && c == '"')
                {
                    // quoted data is starting
                    quoted = true;
                    predata = false;
                    continue;
                }

                if (predata)
                {
                    // data is starting without quotes
                    predata = false;
                    item.Append(c);
                    continue;
                }

                if (c == '"' && quoted)
                {
                    if (GetNextChar(false) == '"')
                        // double quotes within quoted string means add a quote 
                        item.Append(GetNextChar(true));
                    else
                        // end-quote reached
                        postdata = true;
                    continue;
                }

                // all cases covered, character must be data
                item.Append(c);
            }
        }

        private char[] buffer = new char[4096];
        private int pos = 0;
        private int length = 0;

        private char GetNextChar(bool eat)
        {
            if (pos >= length)
            {
                length = stream.ReadBlock(buffer, 0, buffer.Length);
                if (length == 0)
                {
                    EOS = true;
                    return '\0';
                }
                pos = 0;
            }
            if (eat)
                return buffer[pos++];
            else
                return buffer[pos];
        }
    }

    public class CsvWriter
    {

        public static void WriteToStream(StreamWriter stream, DataTable table, bool header, bool quoteall)
        {
            if (header)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    WriteItem(stream, table.Columns[i].Caption, quoteall);
                    if (i < table.Columns.Count - 1)
                        stream.Write(',');
                    else
                        stream.Write(stream.NewLine);
                }
            }
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    WriteItem(stream, row[i], quoteall);
                    if (i < table.Columns.Count - 1)
                        stream.Write(',');
                    else
                        stream.Write(stream.NewLine);
                }
            }
        }

        private static void WriteItem(TextWriter stream, object item, bool quoteall)
        {
            if (item == null)
                return;
            string s = item.ToString();
            if (quoteall || s.IndexOfAny("\",\x0A\x0D".ToCharArray()) > -1)
                stream.Write("\"" + s.Replace("\"", "\"\"") + "\"");
            else
                stream.Write(s);
        }
    }
    //---------------------------------------------------------------------------------------------
}
//=================================================================================================

