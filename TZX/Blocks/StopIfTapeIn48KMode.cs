using System;
using System.Collections.Generic;

using System.Text;
using System.IO;

/*
    When this block is encountered, the tape will stop ONLY if the machine is an 48K Spectrum. 
    This block is to be used for multiloading games that load one level at a time in 48K mode, but load the entire tape at once if in 128K mode.

    This block has no body of its own, but follows the extension rule.

    ID 2A - Stop the tape if in 48K mode    length: 04 
    
    Offset      Value       Type        Description 
    0x00            0       DWORD       Length of the block without these four bytes (0) 
*/
namespace ZXCassetteDeck
{
   public class StopIfTapeIn48KMode : ITZXBlock
    {
        public int Index { get; set; }
        int LengthOfTheBlock;
        public TZXBlockType ID { get { return TZXBlockType.StopTheTapeIfIn48kMode; } }

        public StopIfTapeIn48KMode(byte[] rawdata, ref int pointer)
        {
            LengthOfTheBlock = rawdata[pointer++] | (rawdata[pointer++] << 8) | (rawdata[pointer++] << 0x10) | (rawdata[pointer++] << 0x10);
        }

        public string Details
        {
            get
            {
                string info = "";
                info += "Length Of The Block: " + LengthOfTheBlock.ToString();
                return info;
            }
        }
        public override string ToString()
        {
            return TZXFunctions.EnumToString(ID);
        }

    }
}
