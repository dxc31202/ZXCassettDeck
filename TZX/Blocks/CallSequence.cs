using System;
using System.Collections.Generic;

using System.Text;
using System.IO;

/*
    This block is an analogue of the CALL Subroutine statement. 
    It basically executes a sequence of blocks that are somewhere else and then goes back to the next block. 
    Because more than one call can be normally used you can include a list of sequences to be called. 
    The 'nesting' of call blocks is also not allowed for the simplicity reasons. 
    You can, of course, use the CALL blocks in the LOOP sequences and vice versa. 
    The value is relative for the obvious reasons - so that you can add some blocks in the beginning of the file without disturbing the call values. 
    Please take a look at 'Jump To Block' for reference on the values.

    ID 26 - Call sequence   length: [00,01]*02+02 

    Offset      Value       Type        Description 
    0x00            N       WORD        Number of calls to be made 
    0x02            -       WORD[N]     Array of call block numbers (relative-signed offsets) 
*/
namespace ZXCassetteDeck
{
    public class CallSequence : ITZXBlock
    {
        public int Index { get; set; }
        public TZXBlockType ID { get { return TZXBlockType.CallSequence; } }

        public int NumberOfCallsToBeMade;
        public int[] ArrayOfCallBlockNumbers;
        public CallSequence(byte[] rawdata, ref int pointer)
        {
            try {
                int start = pointer;
                NumberOfCallsToBeMade = rawdata[pointer++] | (rawdata[pointer++] << 8);
                ArrayOfCallBlockNumbers = new int[NumberOfCallsToBeMade];
                for (int i = 0; i < NumberOfCallsToBeMade; i++)
                    ArrayOfCallBlockNumbers[i] = rawdata[pointer++] | (rawdata[pointer++] << 8);

                blockLength = pointer - start;
            }
            catch
            {
                throw new Exception("Invalid Block: " + ID.ToString());
            }
        }

        int blockLength;
        public byte[] RawData;
        public int RawDataLength;

        public string Details
        {
            get
            {
                string info = "";

                info = "Number Of Calls to be made: " + NumberOfCallsToBeMade.ToString() + Environment.NewLine +
                    "Array Of Call Block Numbers: " + TZXFunctions.ArrayToString(ArrayOfCallBlockNumbers);
                return info;
            }
        }
        public override string ToString()
        {
            return "[Call Sequence:" + TZXFunctions.ArrayToDec(ArrayOfCallBlockNumbers,Index) + "]";
        }
    }
}
