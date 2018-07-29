using System;
using System.Collections.Generic;

using System.Text;


/*
    This indicates the end of a group. This block has no body.

    ID 22 - Group end       length: 00 

*/
namespace ZXCassetteDeck
{
    public class GroupEnd : ITZXBlock
    {
        public int Index { get; set; }
        public TZXBlockType ID { get { return TZXBlockType.GroupEnd; } }
        public GroupEnd()
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
            return "[End Group]";

        }
    }
}
