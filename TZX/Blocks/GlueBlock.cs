using System;
using System.Collections.Generic;

using System.Text;
using System.IO;

/*
    This block is generated when you merge two ZX Tape files together. 
    It is here so that you can easily copy the files together and use them. 
    Of course, this means that resulting file would be 10 bytes longer than if this block was not used. 
    All you have to do if you encounter this block ID is to skip next 9 bytes.

    If you can avoid using this block for this purpose, then do so; it is preferable to use a utility to join the two files 
    and ensure that they are both of the higher version number.

    ID 5A - "Glue" block    length: 09 

    Offset      Value       Type        Description 
    0x00            -       BYTE[9]     Value: { "XTape!",0x1A,MajR,MinR } 
 
    Just skip these 9 bytes and you will end up on the next ID. 


 
*/
namespace ZXCassetteDeck
{
    public class GlueBlock : ITZXBlock
    {
        public int Index { get; set; }
        public char[] Value;

        public TZXBlockType ID { get { return TZXBlockType.GlueBlock; } }

        public GlueBlock(byte[] rawdata, ref int pointer)
        {
            Value = new char[9];
            for (int i = 0; i < 9; i++)
                Value[i] = (char)rawdata[pointer++];
        }
        public string Details
        {
            get
            {
                string info = "";
                info += "Value: " + new string(Value);
                return info;
            }
        }

        public override string ToString()
        {
            return TZXFunctions.EnumToString(ID);
        }

    }
}
