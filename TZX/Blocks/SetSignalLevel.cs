using System;
using System.Collections.Generic;

using System.Text;
using System.IO;

/*
    This block sets the current signal level to the specified value (high or low). 
    It should be used whenever it is necessary to avoid any ambiguities, e.g. with custom loaders which are level-sensitive.

    ID 2B - Set signal level    length: 05 
    
    Offset      Value       Type        Description 
    0x00            1       DWORD       Block length (without these four bytes) 
    0x04            -       BYTE        Signal level (0=low, 1=high) 


*/
namespace ZXCassetteDeck
{
    public class SetSignalLevel : ITZXBlock
    {
        public int Index { get; set; }

        public int BlockLength;
        public TZXSignalLevel SignalLevel;
        public TZXBlockType ID { get { return TZXBlockType.SetSignalLevel; } }

        public SetSignalLevel(byte[] rawdata, ref int pointer)
        {
            BlockLength = rawdata[pointer++] | (rawdata[pointer++] << 8) | (rawdata[pointer++] << 0x10) | (rawdata[pointer++] << 0x10);
            SignalLevel = (TZXSignalLevel)rawdata[pointer++];
        }

        public string Details
        {
            get
            {
                string info = "";
                info += "Block Length: " + BlockLength.ToString() + Environment.NewLine;
                info += "Signal Level: " + TZXFunctions.EnumToString(SignalLevel) + Environment.NewLine;
                return info;
            }
        }

        public override string ToString()
        {
            return TZXFunctions.EnumToString(ID);
        }


    }
}
