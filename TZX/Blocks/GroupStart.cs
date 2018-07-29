using System;
using System.Collections.Generic;

using System.Text;

using System.IO;
/*
    This block marks the start of a group of blocks which are to be treated as one single (composite) block. 
    This is very handy for tapes that use lots of subblocks like Bleepload (which may well have over 160 custom loading blocks). 
    You can also give the group a name (example 'Bleepload Block 1').

    For each group start block, there must be a group end block. Nesting of groups is not allowed.

    ID 21 - Group start     length: [00]+01 

    Offset      Value       Type        Description 
    0x00            L       BYTE        Length of the group name string 
    0x01            -       CHAR[L]     Group name in ASCII format (please keep it under 30 characters long) 

*/
namespace ZXCassetteDeck
{
    class GroupStart : ITZXBlock
    {
        public int Index { get; set; }
        public TZXBlockType ID { get { return TZXBlockType.GroupStart; } }

        public int LengthOfTheGroupNameString;
        public char[] GroupNameInASCIIFormat;

        public GroupStart(byte[] rawdata, ref int pointer)
        {
            LengthOfTheGroupNameString = rawdata[pointer++];
            GroupNameInASCIIFormat = new char[LengthOfTheGroupNameString];
            for (int i = 0; i < LengthOfTheGroupNameString; i++)
                GroupNameInASCIIFormat[i] = (char)rawdata[pointer++];

        }

        public string GroupName
        {
            get { return new string(GroupNameInASCIIFormat); }
        }

        public string Details
        {
            get
            {
                string info = "GroupName: " + GroupName;
                return info;
            }
        }


        public override string ToString()
        {
            return "[Group: " + GroupName + "]";
        }
    }
}
