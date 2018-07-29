using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
/*
    This block contains a sequence of raw pulses encoded in CSW format v2 (Compressed Square Wave).

    ID 18 - CSW Recording   length: [00,01,02,03]+04 
    Offset      Value       Type        Description 
    0x00         10+N       DWORD       Block length (without these four bytes) 
    0x04            -       WORD        Pause after this block (in ms). 
    0x06            -       BYTE[3]     Sampling rate 
    0x09            -       BYTE        Compression type
                                        0x01: RLE
                                        0x02: Z-RLE  
    0x0A            -       DWORD       Number of stored pulses (after decompression, for validation purposes)  
    0x0E            -       BYTE[N]     CSW data, encoded according to the CSW file format specification. 
*/
namespace ZXCassetteDeck
{
    class CSWRecording:ITZXBlock
    {
        public int Index { get; set; }
        int BlockLength;
        int PauseAfterThisBlockInMS;
        int SamplingRate;
        TZXCompressionType CompressionType;
        int NumberOfStoredPulses;
        byte[] CSWData;

        public TZXBlockType ID { get { return TZXBlockType.CswRecordingBlock; } }

        public CSWRecording(byte[] rawdata, ref int pointer)
        {
            int start = pointer;
            BlockLength = rawdata[pointer++] | (rawdata[pointer++] << 8) | (rawdata[pointer++] << 0x10) | (rawdata[pointer++] << 0x18);
            PauseAfterThisBlockInMS = rawdata[pointer++] | (rawdata[pointer++] << 8);
            SamplingRate = rawdata[pointer++] | (rawdata[pointer++] << 8) | (rawdata[pointer++] << 0x10);
            CompressionType = (TZXCompressionType)rawdata[pointer++];
            NumberOfStoredPulses = (rawdata[pointer++] | (rawdata[pointer++] << 8) | (rawdata[pointer++] << 0x10) | (rawdata[pointer++] << 0x18));
            CSWData = new byte[NumberOfStoredPulses];
            for (int i = 0; i < NumberOfStoredPulses; i++)
                CSWData[i] = rawdata[pointer++];

            blockLength = pointer - start;
        }

        int blockLength;
        //public byte[] RawData;
        //public int RawDataLength;
        public string Details
        {
            get
            {
                string info = "";
                info += "Block Length: " + BlockLength.ToString() + Environment.NewLine;
                info += "Pause After This Block In MS: " + PauseAfterThisBlockInMS.ToString() + Environment.NewLine;
                info += "Sampling Rate: " + SamplingRate.ToString() + Environment.NewLine;
                info += "Compression Type: " + CompressionType.ToString() + Environment.NewLine;
                info += "Number Of Stored Pulses: " + NumberOfStoredPulses.ToString() + Environment.NewLine;
                info += "CSW Data: " + TZXFunctions.ArrayToString(CSWData);

                return info;
            }
        }

        public override string ToString()
        {
            return TZXFunctions.EnumToString(ID);
        }
    }
}
