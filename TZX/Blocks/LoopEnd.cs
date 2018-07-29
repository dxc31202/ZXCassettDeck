using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
/*
    This is the same as BASIC's NEXT statement. 
    It means that the utility should jump back to the start of the loop if it hasn't been run for the specified number of times.

    This block has no body.

    ID 25 - Loop end     length: 00 
*/
namespace ZXCassetteDeck
{
    public class LoopEnd : ITZXBlock
    {
        public int Index { get; set; }
        public TZXBlockType ID { get { return TZXBlockType.LoopEnd; } }
        public LoopEnd()
        {

        }

        public LoopEnd(byte[] rawdata, ref int pointer)
        {

        }
        public string Details
        {
            get
            {
                string info = "";
                return info;
            }
        }

        public override string ToString()
        {
            return "[End Loop]";

        }
    }
}
