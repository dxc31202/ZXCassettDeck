using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
/*
    If you have a sequence of identical blocks, or of identical groups of blocks, you can use this block to tell how many times they should be repeated. 
    This block is the same as the FOR statement in BASIC.

    For simplicity reasons don't nest loop blocks!

    ID 24 - Loop start      length: 02 
    Offset      Value       Type        Description 
    0x00            -       WORD        Number of repetitions (greater than 1) 

*/
namespace ZXCassetteDeck
{
    public class LoopStart:ITZXBlock
    {
        public int Index { get; set; }
        int numberOfRepetitions;
        public TZXBlockType ID { get { return TZXBlockType.LoopStart; } }

        public LoopStart(byte[] rawdata, ref int pointer)
        {
            numberOfRepetitions = rawdata[pointer++] | (rawdata[pointer++] << 8);
        }
        public string Details
        {
            get
            {
                string info = "";
                info += "Number Of Repetitions: " + numberOfRepetitions + Environment.NewLine;
                return info;
            }
        }

        public int NumberOfRepetitions { get { return numberOfRepetitions; } }

        public override string ToString()
        {
            return "[Loop * " + numberOfRepetitions.ToString()+"]";
        }
    }
}
