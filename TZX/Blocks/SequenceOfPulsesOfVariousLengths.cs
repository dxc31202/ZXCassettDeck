using System;
using System.Collections.Generic;

using System.Text;

using System.IO;

/*
    This will produce N pulses, each having its own timing. 
    Up to 255 pulses can be stored in this block; this is useful for non-standard sync tones used by some protection schemes.

    ID 13 - Pulse sequence  length: [00]*02+01 

    Offset      Value       Type        Description 
    0x00            N       BYTE        Number of pulses 
    0x01            -       WORD[N]     Pulses' lengths 


*/
namespace ZXCassetteDeck
{
    public class SequenceOfPulsesOfVariousLengths : ITZXBlock
    {
        public int Index { get; set; }
        public TZXBlockType ID { get { return TZXBlockType.SequenceOfPulsesOfVariousLengths; } }

        public byte NumberOfPulses;
        public int[] PulsesLengths;

        int pointer = 0;
        public void Reset() { pointer = 0; }
        public void Next (){ if (!EndOfData) pointer++; }
        public bool EndOfData { get { return pointer >= NumberOfPulses; } }

        public SequenceOfPulsesOfVariousLengths(byte[] rawdata, ref int pointer)
        {
            NumberOfPulses = rawdata[pointer++];
            PulsesLengths = new int[NumberOfPulses];
            for (int i = 0; i < NumberOfPulses; i++)
                PulsesLengths[i] = (rawdata[pointer++] | (rawdata[pointer++] << 8));
        }

        public string Details
        {
            get
            {
                string info = "";
                info += "Number Of Pulses: " + NumberOfPulses.ToString() + Environment.NewLine;
                info += "Pulses Lengths: " + TZXFunctions.ArrayToString(PulsesLengths) + Environment.NewLine;

                return info;
            }
        }

        public int PulseLength { get { return NumberOfPulses; } }
        public int PulseToneLength { get { return PulsesLengths.Length; } }
        public int SyncLength1 { get { throw new NotImplementedException(); } }
        public int SyncLength2 { get { throw new NotImplementedException(); } }
        public int ZeroLength { get { throw new NotImplementedException(); } }
        public int OneLength { get { throw new NotImplementedException(); } }
        public int PauseLength { get { throw new NotImplementedException(); } }
        public int UsedBits { get { throw new NotImplementedException(); } }
        public int DataLength { get { return PulsesLengths.Length; } }
        public int Pointer { get { throw new NotImplementedException(); } }
        public int Progress { get; set; }


        public int this[int index]
        {
            get
            {
                return PulsesLengths[index];

            }
        }
        public override string ToString()
        {
            return "Sequence of " + NumberOfPulses.ToString() + " Pulses}";// " + TZXFunctions.ArrayToString(PulsesLengths) + "}";
        }
    }
}
