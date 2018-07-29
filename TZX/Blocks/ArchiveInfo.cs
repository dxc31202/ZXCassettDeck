using System;
using System.Collections.Generic;

using System.Text;

using System.IO;

/*
    Use this block at the beginning of the tape to identify the title of the game, author, publisher, year of 
    publication, price (including the currency), type of software (arcade adventure, puzzle, word processor, ...), 
    protection scheme it uses (Speedlock 1, Alkatraz, ...) and its origin (Original, Budget re-release, ...), etc. 
    This block is built in a way that allows easy future expansion. 
    The block consists of a series of text strings. 
    Each text has its identification number (which tells us what the text means) and then the ASCII text. 
    To make it possible to skip this block, if needed, the length of the whole block is at the beginning of it.

    If all texts on the tape are in English language then you don't have to supply the 'Language' field

    The information about what hardware the tape uses is in the 'Hardware Type' block, so no need for it here.

    ID 32 - Archive info length: [00,01]+02 
    Offset      Value       Type        Description 
    0x00            -       WORD        Length of the whole block (without these two bytes) 
    0x02            N       BYTE        Number of text strings 
    0x03            -       TEXT[N]     List of text strings 
*/
namespace ZXCassetteDeck
{

    public class ArchiveInfo : ITZXBlock
    {
        public int Index { get; set; }

        public TZXBlockType ID { get { return TZXBlockType.ArchiveInfo; } }

        public int LengthOfTheWholeBlock;
        public int NumberOfTextStrings;
        public List<TEXT> ListOfTextStrings = new List<TEXT>();

        public ArchiveInfo(byte[] rawdata, ref int pointer)
        {
            LengthOfTheWholeBlock = rawdata[pointer++] | (rawdata[pointer++] << 8);
            NumberOfTextStrings = rawdata[pointer++];
            for (int i = 0; i < NumberOfTextStrings; i++)
            {
                ListOfTextStrings.Add(new TEXT(rawdata, ref pointer));
            }
        }

        public string Details
        {
            get
            {
                string s = "";
                int i = 0;
                s += Environment.NewLine;
                foreach (TEXT t in ListOfTextStrings)
                {
                    s += "    "+t.ToString();
                    i++;
                    if (i < ListOfTextStrings.Count)
                        s += Environment.NewLine;
                }
                return s;
            }
        }
        public override string ToString()
        {
            return "[Archive Info]";
        }

        /*
            TEXT structure format 

            Offset      Value       Type        Description 
            0x00            -       BYTE        Text identification byte:
                                                    00 - Full title
                                                    01 - Software house/publisher
                                                    02 - Author(s)
                                                    03 - Year of publication
                                                    04 - Language
                                                    05 - Game/utility type
                                                    06 - Price
                                                    07 - Protection scheme/loader
                                                    08 - Origin
                                                    FF - Comment(s)
            0x01            L       BYTE        Length of text string 
            0x02            -       CHAR[L]     Text string in ASCII format 
        */
        public class TEXT
        {
            public byte TextIdentificationByte;
            public byte LengthOfTextString;
            public string TextStringInASCIIFormat;


            public TEXT(byte[] rawdata, ref int pointer)
            {
                int start = pointer;
                TextIdentificationByte = rawdata[pointer++];
                LengthOfTextString = rawdata[pointer++];
                TextStringInASCIIFormat = "";
                for (int i = 0; i < LengthOfTextString; i++)
                    TextStringInASCIIFormat+= Convert.ToChar(rawdata[pointer++]);

                blockLength = pointer - start;
            }

            int blockLength;
            public byte[] RawData;
            public int RawDataLength;


            public override string ToString()
            {
                return TextStringInASCIIFormat;

            }
        }

    }


}
