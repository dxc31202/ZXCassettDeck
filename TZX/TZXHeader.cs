using System;
using System.Collections.Generic;

using System.Text;

using System.IO;
/*
    TZX Header: length: 10 bytes 

    Offset      Value   Type        Description 
    0x00    "ZXTape!"   ASCII[7]    TZX signature 
    0x07        0x1A    BYTE        End of text file marker 
    0x08           1    BYTE        TZX major revision number 
    0x09          20    BYTE        TZX minor revision number 
*/
namespace ZXCassetteDeck
{
    public class TZXHeader : ITZXBlock
    {
        public int Index { get; set; }
        public TZXBlockType ID { get { return TZXBlockType.Header; } }
        public char[] TZXSignature = new char[7];
        public byte EndOfTextFileMarker;
        public byte MajorRevisionNumber;
        public byte MinorRevisionNumber;
        public string Signature
        {
            get { return new string(TZXSignature); }
        }
        public TZXHeader(byte[] rawdata, ref int pointer)
        {
            int start = pointer;
            for (int i=0;i<7;i++)
            {
                TZXSignature[i] = (char)rawdata[pointer++];
            }
            EndOfTextFileMarker = rawdata[pointer++];
            MajorRevisionNumber = rawdata[pointer++];
            MinorRevisionNumber = rawdata[pointer++];

        }

        public string Details
        {
            get
            {
                return "Signature: " + Signature + Environment.NewLine +
                    "End Of Text File Marker: " + EndOfTextFileMarker.ToString() + Environment.NewLine +
                    "Major Revision Number: " + MajorRevisionNumber.ToString() + Environment.NewLine +
                    "Minor Revision Number: " + MinorRevisionNumber.ToString();
            }
        }

        public override string ToString()
        {
            return "["+Signature+" {Version "+ MajorRevisionNumber.ToString() +"."+ MinorRevisionNumber.ToString() +"}]";
        }

    }
}
