using System;
using System.IO;

namespace ZXCassetteDeck
{
    public class TAPBlock : ITAPBlock
    {
        public byte[] RawData;
        public int RawDataLength;
        public int Next { get; set; }
        public byte? FirstByte()
        {
            Next = 0;
            if (data.Length > 0) {
                return data[0];
            }
            return null;

        }

        public byte? NextByte()
        {
            Next++;
            if (Next < data.Length)
            {
                return data[Next];
            }
            Next--;
            return null;
        }


        public int Length { get { return length; } }
        public int Checksum { get; private set; }
        //public int ID { get { return id; } }
        //public bool IsValid { get { return isValid; } }

        int length;
        byte[] data;
        //int id;
        //bool isValid;

        public TAPBlock()
        { }
        public TAPBlock(byte[] rawdata, ref int pointer)
            : this(rawdata, ref pointer, false)
        {

        }
        public TAPBlock(byte[] rawdata, ref int pointer, bool extended)
        {
            int start = pointer;
            length = rawdata[pointer++];
            length |= rawdata[pointer++] << 8;
            //if (length < 2) return;
            if (extended) length |= rawdata[pointer++] << 0x10;
            //if (length < 2) return;
            data = new byte[length];
            try
            {
                for (int i = 0; i < length; i++)
                {
                    data[i] = rawdata[pointer++];
                }
            }catch(Exception ex)
            {
                throw new Exception("Invalid Data");
            }
        }

        public byte[] Data { get { return data; } }


        public override string ToString()
        {
            return "TZXTAP Block: " + TZXFunctions.ArrayToString(data, 16, 2);

        }

        public static string Filename;
        public static string BlockType;
        public static int DataLength;
        public static int AutostartLine;
        public static int StartAddress;
        public static int ProgramLength;
        public static string VariableName;

        public string Description
        {
            get
            {
                string text = "Data Block {" + (Data.Length-1).ToString() + " Bytes}";
                if (data[0] == 0)
                    text = Header;
                return text;


            }
        }

        string Header
        {
            get
            {
                if (data.Length < 10)
                    return "Fragment Block {" + Data.Length.ToString() + " Bytes}";
                string text = "";
                Filename = "";
                for (int i = 2; i < 12; i++)
                {
                    char c = (char)data[i];
                    if (data[i] < 32) c = '?';
                    if (data[i] > 127) c = '?';
                    Filename += c;
                }
                Filename = Filename.Trim() + " ";
                switch (data[1])
                {
                    case 0: // Header
                        BlockType = "Program: ";
                        DataLength = data[12] | (data[13] << 8);
                        AutostartLine = data[14] | (data[15] << 8);
                        ProgramLength = data[16] | (data[17] << 8);
                        text = BlockType + "\"" + Filename.Trim() + "\"" + " LINE " + AutostartLine.ToString() + "; Header Block {" + (data.Length - 1).ToString() + " Bytes}";
                        break;
                    case 1: // Numeric Array
                        BlockType = "Numeric Array: ";
                        VariableName = ((char)data[15]).ToString();
                        text = BlockType + Filename + " " + VariableName;
                        break;
                    case 2: // Alphanumeric Array
                        BlockType = "Alphanumeric Array: ";
                        VariableName = ((char)data[15]).ToString();
                        text = BlockType + Filename + " " + VariableName;
                        break;
                    case 3: // Byte header or  SCREEN$ header 
                        DataLength = data[12] | (data[13] << 8);
                        StartAddress = data[14] | (data[15] << 8);
                        BlockType = "Bytes: ";
                        if (StartAddress == 16384) BlockType = "Screen$: ";
                        if (StartAddress == 22528) BlockType = "Screen$: ";
                        text = BlockType + "\"" + Filename.Trim() + "\"" + " CODE " + StartAddress.ToString() + ", " + (DataLength + 1).ToString() + "; Header Block {" + (data.Length - 1).ToString() + " Bytes}";
                        break;
                    default:
                        text = "Data Block {" + (Data.Length + 1).ToString() + " Bytes}";
                        break;
                }
                return text;
            }
        }
    }
}
