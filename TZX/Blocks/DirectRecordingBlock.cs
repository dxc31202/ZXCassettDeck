using System;
using System.Collections.Generic;

using System.Text;

using System.IO;
/*
    This block is used for tapes which have some parts in a format such that the turbo loader block cannot be used. 
    This is not like a VOC file, since the information is much more compact. 
    
    Each sample value is represented by one bit only (0 for low, 1 for high) which means that the block will be at most 1/8 the size of the equivalent VOC.
    
    The preferred sampling frequencies are 22050 or 44100 Hz (158 or 79 T-states/sample). 
    Please, if you can, don't use other sampling frequencies.

    Please use this block only if you cannot use any other block.

    ID 15 - Direct Recording    length: [05,06,07]+08 
    Offset      Value       Type        Description 
    0x00            -       WORD        Number of T-states per sample (bit of data) 
    0x02            -       WORD        Pause after this block in milliseconds (ms.) 
    0x04            -       BYTE        Used bits (samples) in last byte of data (1-8)
                                        (e.g. if this is 2, only first two samples of the last byte will be played)  
    0x05            N       BYTE[3]     Length of samples' data 
    0x08            -       BYTE[N]     Samples data. Each bit represents a state on the EAR port (i.e. one sample). MSb is played first. 
*/
namespace ZXCassetteDeck
{
    public class DirectRecordingBlock : ITZXBlock
    {
        public int Index { get; set; }
        public int NumberOfCyclesPerSample;
        public int PauseAfterThisBlockInMilliseconds;
        public byte UsedBitsSamplesInLastByteOfData;
        public int LengthOfSamplesData;
        public byte[] SamplesData;

        public TZXBlockType ID { get { return TZXBlockType.DirectRecordingBlock; } }

        public DirectRecordingBlock(byte[] rawdata, ref int pointer)
        {
            NumberOfCyclesPerSample = (rawdata[pointer++] | (rawdata[pointer++] << 8));
            PauseAfterThisBlockInMilliseconds = (rawdata[pointer++] | (rawdata[pointer++] << 8));
            UsedBitsSamplesInLastByteOfData = rawdata[pointer++];

            LengthOfSamplesData = rawdata[pointer++] | (rawdata[pointer++] << 8) | (rawdata[pointer++] << 0x10);
            SamplesData = new byte[LengthOfSamplesData];
            for (int i = 0; i < LengthOfSamplesData; i++)
                SamplesData[i] = rawdata[pointer++];
        }

        public string Details
        {
            get
            {
                string info = "";
                info += "Number Of Cycles Per Sample: " + NumberOfCyclesPerSample.ToString() + Environment.NewLine;
                info += "Pause After This Block In Milliseconds: " + PauseAfterThisBlockInMilliseconds.ToString() + Environment.NewLine;
                info += "Used Bits Samples In Last Byte Of Data: " + UsedBitsSamplesInLastByteOfData.ToString() + Environment.NewLine;
                info += "Length Of Samples Data: " + LengthOfSamplesData.ToString() + Environment.NewLine;
                info += "Samples Data: " + TZXFunctions.ArrayToString(SamplesData);
                return info;
            }
        }

        public override string ToString()
        {
            return TZXFunctions.EnumToString(ID);
        }
    }
}
