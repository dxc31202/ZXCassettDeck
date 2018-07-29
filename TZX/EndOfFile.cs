using System;
using System.Collections.Generic;
using System.Text;

namespace ZXCassetteDeck
{
    public class EndOfFile : ITZXBlock
    {
        public int Index { get; set; }

        public EndOfFile()
        {

        }

        public TZXBlockType ID
        {
            get
            {
                return TZXBlockType.EndOfFile;
            }
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
            return TZXFunctions.EnumToString(ID);

        }
    }
}
