using System;
using System.Collections.Generic;

using System.Text;


namespace ZXCassetteDeck
{
    public class TZXFunctions
    {

        public static string EnumToString(object value)
        {
            string id = "";
            string description = "";
            string space = "";
            foreach (char c in value.ToString())
            {
                if ((c & 0x20) == 0x00)
                    description += space;
                description += c;
                space = " ";
            }
            return id + description;
        }

        public static string ArrayToString(byte[] array, int maxperline, int indent)
        {
            if (array == null)
                return "Empty";
            StringBuilder result = new StringBuilder();
            result.Append(Environment.NewLine+ new string(' ', indent) + "{" + Environment.NewLine + new string(' ', indent) + "0000: ");
            string comma = "";
            int perline = 0;
            int pos = 0;
            foreach (byte b in array)
            {

                result.Append(comma + b.ToString("X2"));
                comma = " ";
                perline++;
                pos++;
                if (perline >= maxperline)
                {
                    result.Append(Environment.NewLine + new string(' ', indent) + pos.ToString("X4") + ": ");
                    comma = "";
                    perline = 0;
                }
            }
            result.Append(Environment.NewLine + new string(' ', indent) + "}"+Environment.NewLine);
            return result.ToString();
        }


        public static string ArrayString(byte[] source)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine + "    ");
            foreach (byte b in source)
            {
                if(b==13)
                    sb.Append(Environment.NewLine + "    ");
                else
                    sb.Append((char)b);
            }
            return sb.ToString();
        }

        public static string ArrayToString(byte[] source)
        {
            StringBuilder sb = new StringBuilder();
            int n = 0;
            int c = 0;
            sb.Append(Environment.NewLine + "    {" + Environment.NewLine);
            sb.Append("    " + c.ToString("X4") + ": ");
            foreach (byte b in source)
            {
                if (n == 16)
                {
                    sb.Append(Environment.NewLine);
                    n = 0;
                    sb.Append("    " + c.ToString("X4") + ": ");
                }
                sb.Append(b.ToString("X2") + " ");
                n++;
                c++;
            }
            sb.Append(Environment.NewLine + "    " + "}" + Environment.NewLine);
            return sb.ToString();
        }
        public static string ArrayToString(int[] array)
        {
            StringBuilder result = new StringBuilder();
            result.Append("{");
            string comma = "";
            foreach (int b in array)
            {

                result.Append(comma + b.ToString("X4"));
                comma = ", ";
            }
            result.Append("}");
            return result.ToString();
        }
        public static string ArrayToString(List<SELECT> array)
        {
            StringBuilder result = new StringBuilder();
            result.Append("{");
            string comma = "";
            foreach (SELECT b in array)
            {

                result.Append(comma + b.Description);
                comma = ", ";
            }
            result.Append("}");
            return result.ToString();
        }
        public static string ArrayToDec(int[] array,int block)
        {
            StringBuilder result = new StringBuilder();
            result.Append("{");
            string comma = "";
            foreach (int b in array)
            {
                sbyte n = (sbyte)b;
                result.Append(comma + (block+n).ToString());
                comma = ", ";
            }
            result.Append("}");
            return result.ToString();
        }

        public static string ArrayToString(UInt16[] array)
        {
            StringBuilder result = new StringBuilder();
            result.Append("{");
            string comma = "";
            foreach (UInt16 b in array)
            {

                result.Append(comma + b.ToString("X4"));
                comma = ", ";
            }
            result.Append("}");
            return result.ToString();
        }

        public static string RightJustify(int value, int size)
        {
            string temp = new string(' ', size) + value.ToString();
            return temp.Substring(temp.Length - size, size)+" ";
        }

    }
}