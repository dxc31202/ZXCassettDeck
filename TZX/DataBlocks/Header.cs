using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZXCassetteDeck
{
    class Globals
    {
        public static string Filename;
        public static string BlockType;
        public static int DataLength;
        public static int AutostartLine;
        public static int StartAddress;
        public static int ProgramLength;
        public static string VariableName;
        public static string Header(ITZXDataBlock block)
        {
            Filename = "";

            string text = "";
            if (block.TAPBlock.Length < 10)
                return "Fragment Block {" + (block.TAPBlock.Data.Length - 1).ToString() + " Bytes}";
            for (int i = 2; i < 12; i++)
            {
                char c = (char)block.TAPBlock.Data[i];
                if (block.TAPBlock.Data[i] < 32) c = '?';
                if (block.TAPBlock.Data[i] > 127) c = '?';
                Filename += c;
            }
            Filename = Filename.Trim() + " ";
            switch (block.TAPBlock.Data[0])
            {
                case 0: // Header
                    BlockType = "Program: ";
                    DataLength = block.TAPBlock.Data[12] | (block.TAPBlock.Data[13] << 8);
                    AutostartLine = block.TAPBlock.Data[14] | (block.TAPBlock.Data[15] << 8);
                    ProgramLength = block.TAPBlock.Data[16] | (block.TAPBlock.Data[17] << 8);
                    text = BlockType + "\"" + Filename.Trim() + "\"" + " LINE " + AutostartLine.ToString() + "; Header Block {" + (block.TAPBlock.Data.Length - 1).ToString() + " Bytes}";
                    break;
                case 1: // Numeric Array
                    BlockType = "Numeric Array: ";
                    VariableName = ((char)block.TAPBlock.Data[15]).ToString();
                    text = BlockType + Filename + " " + VariableName;
                    break;
                case 2: // Alphanumeric Array
                    BlockType = "Alphanumeric Array: ";
                    VariableName = ((char)block.TAPBlock.Data[15]).ToString();
                    text = BlockType + Filename + " " + VariableName;
                    break;
                case 3: // Byte header or  SCREEN$ header 
                    DataLength = block.TAPBlock.Data[12] | (block.TAPBlock.Data[13] << 8);
                    StartAddress = block.TAPBlock.Data[14] | (block.TAPBlock.Data[15] << 8);
                    BlockType = "Bytes: ";
                    if (StartAddress == 16384) BlockType = "Screen$: ";
                    if (StartAddress == 22528) BlockType = "Screen$: ";
                    text = BlockType + "\"" + Filename.Trim() + "\"" + " CODE " + StartAddress.ToString() + ", " + (DataLength + 1).ToString() + "; Header Block {" + (block.TAPBlock.Data.Length - 1).ToString() + " Bytes}";
                    break;
                default:
                    text = "Data Block {" + (block.TAPBlock.Data.Length - 1).ToString() + " Bytes}";
                    break;
            }
            return text;
        }
    }

}
