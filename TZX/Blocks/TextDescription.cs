using System;
using System.Collections.Generic;

using System.Text;

using System.IO;
/*
    This will enable the emulators to display a message for a given time. 
    This should not stop the tape and it should not make silence. 
    If the time is 0 then the emulator should wait for the user to press a key.

    The text message should: 
        • stick to a maximum of 30 chars per line;
        • use single 0x0D (13 decimal) to separate lines;
        • stick to a maximum of 8 lines.
    If you do not obey these rules, emulators may display your message in any way they like.

    ID 30 - Text description,   length: [00]+01 
    Offset      Value   Type        Description 
    0x00            N   BYTE        Length of the text description 
    0x01            -   CHAR[N]     Text description in ASCII format 
*/
namespace ZXCassetteDeck
{
    public class TextDescription : ITZXBlock
    {
        public int Index { get; set; }
        public TZXBlockType ID { get { return TZXBlockType.TextDescription; } }

        char[] description;
        public int Length;
        public string Description
        {
            get { return new string(description); }
        }
        public TextDescription(byte[] rawdata, ref int pointer)
        {
            Length = rawdata[pointer++];
            description = new char[Length];
            for (int i = 0; i < Length; i++)
                description[i] = (char)rawdata[pointer++];
        }

        public string Details
        {
            get
            {
                string info = "";
                info += Description;
                return info;
            }
        }

        public override string ToString()
        {
            return "[" + Description+"]";

        }
    }
}
