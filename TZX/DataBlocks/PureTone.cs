using System;
using System.Collections.Generic;

using System.Text;

using System.IO;
/*
    This will produce a tone which is basically the same as the pilot tone in the ID 10, ID 11 blocks. 
    You can define how long the pulse is and how many pulses are in the tone. 

    ID 12 - Pure Tone:  length: 04 
    Offset  Value   Type    Description 
    0x00        -   WORD    Length of one pulse in T-states 
    0x02        -   WORD    Number of pulses 
*/
namespace ZXCassetteDeck
{
    public class PureTone : ITZXDataBlock
    {
        public int Index { get; set; }
        public TZXBlockType ID { get { return TZXBlockType.PureTone; } }

        public int pulseLength;
        public int numberOfPulses;
        ITAPBlock tAPBlock;

        public PureTone(byte[] rawdata, ref int pointer)
        {
            pulseLength = (rawdata[pointer++] | (rawdata[pointer++] << 8));
            numberOfPulses = (rawdata[pointer++] | (rawdata[pointer++] << 8));
            tAPBlock = new TAPBlock();
        }

        public string Details
        {
            get
            {
                string info = "";
                info += "Cycles in Pulse : " + PulseLength.ToString() + Environment.NewLine;
                info += "Number Of Pulses: " + numberOfPulses.ToString() + Environment.NewLine;
                return info;
            }
        }

        public int PulseLength { get { return pulseLength; } }
        public int PulseToneLength { get { return numberOfPulses; } }

        public int Sync1Length { get { return 0; } }
        public int Sync2Length { get { return 0; } }
        public int ZeroLength { get { throw new NotImplementedException(); } }
        public int OneLength { get { throw new NotImplementedException(); } }
        public int PauseLength { get { throw new NotImplementedException(); } }
        public int UsedBits { get { throw new NotImplementedException(); } }
        public int Progress { get; set; }
        public int DataLength
        {
            get
            {
                return 0;
            }
        }
        public bool EndOfData { get { throw new NotImplementedException(); } }
        public int Pointer { get { throw new NotImplementedException(); } }

        public int BlockLength { get { return PulseToneLength; } }

        public ITAPBlock TAPBlock
        {
            get
            {
                return tAPBlock;
            }
        }

        public int this[int index]
        {
            get
            {
                return 0;

            }
        }
        public byte? First() { throw new NotImplementedException(); }
        public byte? Next() { throw new NotImplementedException(); }

        public override string ToString()
        {
            return TZXFunctions.EnumToString(ID) + " {" + numberOfPulses.ToString() + " pulses of " + PulseLength.ToString() + " Cycles}";
        }
    }
}
