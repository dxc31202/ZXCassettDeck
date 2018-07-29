using System;
using System.Collections.Generic;

using System.Text;
using System.IO;

/*

    This block can be used to save any information you want. 
    For example, it might contain some information written by a utility, extra settings required by a particular emulator, or even poke data. 

    ID 35 - Custom info block   length: [10,11,12,13]+14 
    
    Offset      Value       Type        Description 
    0x00            -       CHAR[10]    Identification string (in ASCII) 
    0x10            L       DWORD       Length of the custom info 
    0x14            -       BYTE[L]     Custom info 
*/
namespace ZXCassetteDeck
{
    public class CustomInfoBlock : ITZXBlock
    {
        public int Index { get; set; }
        public char[] IdentificationString;
        public int LengthOfTheCustomInfo;
        public byte[] CustomInfo;

        public TZXBlockType ID { get { return TZXBlockType.CustomInfoBlock; } }

        public CustomInfoBlock(byte[] rawdata, ref int pointer)
        {
            try
            {
                int start = pointer;
                IdentificationString = new char[0x10];
                for (int i = 0; i < 0x10; i++)
                    IdentificationString[i] = (char)rawdata[pointer++];
                LengthOfTheCustomInfo = rawdata[pointer++] + (rawdata[pointer++] << 0x08) + (rawdata[pointer++] << 0x10) + (rawdata[pointer++] << 0x18);
                CustomInfo = new byte[LengthOfTheCustomInfo];
                for (int i = 0; i < LengthOfTheCustomInfo; i++)
                {
                    if (i >= rawdata.Length - 1) break;
                    CustomInfo[i] = rawdata[pointer++];
                }
                blockLength = pointer - start;
            }
            catch (Exception ex)
            {
                throw new CustomException("Block Error: " + TZXFunctions.EnumToString(ID) + " [" + ex.Message + "]");
            }

        }

        int blockLength;
        public byte[] RawData;
        public int RawDataLength;
        public string Identification { get { return new string(IdentificationString); } }
        public string Details
        {
            get
            {
                string info = "";
                info += "Identification String: " + Identification + Environment.NewLine;
                info += "Custom Info: " + TZXFunctions.ArrayString(CustomInfo) + Environment.NewLine;

                return info;
            }
        }

        public override string ToString()
        {
            return TZXFunctions.EnumToString(ID);
        }


    }
}
