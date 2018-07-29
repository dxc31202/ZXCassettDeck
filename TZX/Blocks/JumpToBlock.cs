using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
/*
    This block will enable you to jump from one block to another within the file. 
    The value is a signed short word (usually 'signed short' in C); 
    
    Some examples: 
        •   Jump 0 = 'Loop Forever' - this should never happen
        •   Jump 1 = 'Go to the next block' - it is like NOP in assembler ;)
        •   Jump 2 = 'Skip one block'
        •   Jump -1 = 'Go to the previous block'

    All blocks are included in the block count!

    ID 23 - Jump to block     length: 02 

    Offset      Value       Type        Description 
    0x00            -       WORD        Relative jump value 
*/
namespace ZXCassetteDeck
{
    public class JumpToBlock : ITZXBlock
    {
        public int Index { get; set; }
        int relativeJumpValue;
        public TZXBlockType ID { get { return TZXBlockType.JumpToBlock; } }

        public JumpToBlock(byte[] rawdata, ref int pointer)
        {
            relativeJumpValue = rawdata[pointer++] | (rawdata[pointer++] << 8);
        }
        public string Details
        {
            get
            {
                string info = "";
                info += "Relative Jump Value: " + relativeJumpValue + Environment.NewLine;
                return info;
            }
        }

        public int RelativeJumpValue { get { return relativeJumpValue; } }

        public override string ToString()
        {
            return "[Jump To Block:" + relativeJumpValue.ToString() + "]";
        }
    }
}
