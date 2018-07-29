using System;
using System.Collections.Generic;

using System.Text;
using System.IO;

/*
    This block indicates the end of the Called Sequence. 
    The next block played will be the block after the last CALL block (or the next Call, if the Call block had multiple calls).

    This block has no body.

    ID 27 - Return from sequence        length: 00

*/
namespace ZXCassetteDeck
{
    public class ReturnFromSequence : ITZXBlock
    {
        public int Index { get; set; }
        public TZXBlockType ID { get { return TZXBlockType.ReturnFromSequence; } }
        public ReturnFromSequence()
        {

        }

        public ReturnFromSequence(byte[] rawdata, ref int pointer)
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
            return "[Return From Sequence]";
        }
    }
}
