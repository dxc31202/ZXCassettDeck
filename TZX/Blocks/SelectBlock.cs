using System;
using System.Collections.Generic;

using System.Text;
using System.IO;

/*
    This block is useful when the tape consists of two or more separately-loadable parts. 
    With this block, you are able to select one of the parts and the utility/emulator will start loading from that block. 
    For example you can use it when the game has a separate Trainer or when it is a multiload. 
    Of course, to make some use of it the emulator/utility has to show a menu with the selections when it encounters such a block. 
    All offsets are relative signed words.

    ID 28 - Select block    length:  [00,01]+02 

    Offset      Value       Type        Description 
    0x00            -       WORD        Length of the whole block (without these two bytes) 
    0x02            N       BYTE        Number of selections 
    0x03            -       SELECT[N]   List of selections 
*/
namespace ZXCassetteDeck
{
    public class SelectBlock : ITZXBlock
    {
        public int Index { get; set; }
        int LengthOfTheWholeBlock;
        byte NumberOfSelections;
        List<SELECT> ListOfSelections = new List<SELECT>();

        public TZXBlockType ID { get { return TZXBlockType.SelectBlock; } }

        public SelectBlock(byte[] rawdata, ref int pointer)
        {
            LengthOfTheWholeBlock = rawdata[pointer++] | (rawdata[pointer++] << 8);
            NumberOfSelections = rawdata[pointer++];
            for(int i=0;i<NumberOfSelections;i++)
            {
                ListOfSelections.Add(new SELECT(rawdata,ref pointer));
            }
        }
        public string Details
        {
            get
            {
                string info = "";
                info += "Length Of The Whole Block: " + LengthOfTheWholeBlock.ToString() + Environment.NewLine;
                info += "Number Of Selections: " + NumberOfSelections.ToString() + Environment.NewLine;
                for(int i=0;i<NumberOfSelections;i++)
                {
                    info += ListOfSelections[i].ToString() + Environment.NewLine;
                }
                return info;
            }
        }

        public override string ToString()
        {
            return TZXFunctions.EnumToString(ID) + " " + TZXFunctions.ArrayToString(ListOfSelections); ;
        }

    }
    #region SELECT
    /*
        SELECT structure format     

        Offset      Value       Type        Description 
        0x00            -       WORD        Relative Offset 
        0x02            L       BYTE        Length of description text 
        0x03            -       CHAR[L]     Description text (please use single line and max. 30 chars) 
    */
    public class SELECT
    {
        public int RelativeOffset;
        public byte LengthOfDescriptionText;
        char[] Descriptiontext;
        public SELECT(byte[] rawdata, ref int pointer)
        {
            RelativeOffset = rawdata[pointer++] | (rawdata[pointer++] << 8);
            LengthOfDescriptionText = rawdata[pointer++];
            Descriptiontext = new char[LengthOfDescriptionText];
            for (int i = 0; i < LengthOfDescriptionText; i++)
                Descriptiontext[i] = (char)rawdata[pointer++];

        }

        public string Description { get { return new string(Descriptiontext); } }

        public override string ToString()
        {
            return "{" + RelativeOffset.ToString() + ": " + Description + "}";
        }
    }
    #endregion
}
