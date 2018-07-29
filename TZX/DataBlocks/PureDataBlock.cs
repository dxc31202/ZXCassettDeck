using System;
using System.IO;

/*
    This is the same as in the turbo loading data block, except that it has no pilot or sync pulses.

    ID 14 - Pure Data Block     length: [07,08,09]+0A 

    Offset      Value       Type        Description 
    0x00            -       WORD        Length of ZERO bit pulse 
    0x02            -       WORD        Length of ONE bit pulse 
    0x04            -       BYTE        Used bits in last byte (other bits should be 0)
                                            (e.g. if this is 6, then the bits used (x) in the last byte are: 
                                                xxxxxx00, where MSb is the leftmost bit, LSb is the rightmost bit) 
    0x05            -       WORD        Pause after this block (ms.) 
    0x07            N       BYTE[3]     Length of data that follow 
    0x0A            -       BYTE[N]     Data as in .TAP files 
*/
namespace ZXCassetteDeck
{
    public class PureDataBlock : ITZXDataBlock
    {
        public int Index { get; set; }
        byte[] RawData;
       int RawDataLength;
        #region Interface members
        public TZXBlockType ID { get { return TZXBlockType.PureDataBlock; } }
        /// <summary>
        /// Length of the whole block
        /// </summary>
        public int BlockLength { get { return blockLength; } }
        /// <summary>
        /// Length of PILOT pulse {2168}
        /// </summary>
        public int PulseLength { get { return pulseLength; } }
        /// <summary>
        /// Length of PILOT tone (number of pulses) {8063 header (flag &lt 128), 3223 data (flag &ge 128)} 
        /// </summary>
        public int PulseToneLength { get { return pulseToneLength; } }
        /// <summary>
        /// Length of SYNC first pulse {667} 
        /// </summary>
        public int Sync1Length { get { return sync1Length; } }
        /// <summary>
        /// Length of SYNC second pulse {735} 
        /// </summary>
        public int Sync2Length { get { return sync2Length; } }
        /// <summary>
        /// Length of ZERO bit pulse {855} 
        /// </summary>
        public int ZeroLength { get { return zeroLength; } }
        /// <summary>
        /// Length of ONE bit pulse {1710} 
        /// </summary>
        public int OneLength { get { return oneLength; } }
        /// <summary>
        /// Pause after this block (ms.) {1000} 
        /// </summary>
        public int PauseLength { get { return pauseLength; } }
        /// <summary>
        /// Used bits in the last byte (other bits should be 0) {8}
        /// (e.g. if this is 6, then the bits used(x) in the last byte are: xxxxxx00, 
        /// where MSb is the leftmost bit, LSb is the rightmost bit) 
        /// </summary>
        public int UsedBits
        {
            get
            {
                if (Progress < TAPBlock.Length)
                    return 8;
                return usedBits;
            }
        }
        /// <summary>
        /// Data as in .TAP files 
        /// </summary>
        public ITAPBlock TAPBlock { get { return tAPBlock; } }
        public int Progress { get; set; }
        #endregion Interface members

        #region Local Variables
        int pulseLength=0;
        int pulseToneLength=0;
        int sync1Length=0;
        int sync2Length=0;
        int zeroLength;
        int oneLength;
        int pauseLength;
        int usedBits;

        ITAPBlock tAPBlock;
        #endregion Local Variables

        public PureDataBlock(byte[] rawdata, ref int pointer)
        {

            int start = pointer;
            zeroLength= rawdata[pointer++] | (rawdata[pointer++] << 8);
            oneLength = rawdata[pointer++] | (rawdata[pointer++] << 8);
            
            usedBits = rawdata[pointer++];
            if (usedBits != 8)
                Console.WriteLine();
            pauseLength = rawdata[pointer++] | (rawdata[pointer++] << 8);
            tAPBlock = new TAPBlock(rawdata, ref pointer, true);

            blockLength = pointer - start;
        }

        int blockLength;

        public override string ToString()
        {
            return TZXFunctions.EnumToString(ID) + " {" + tAPBlock.Length.ToString() + " bytes}" ;
        }

        public int this[int index]
        {
            get
            {
                if (tAPBlock.Data != null)
                    return tAPBlock.Data[index];

                return 0;
            }
        }

        public byte? First()
        {
            Progress = 0;
            if (Progress < TAPBlock.Data.Length)
            {
                return TAPBlock.Data[Progress++];
            }
            return null;
        }

        public byte? Next()
        {
            if (Progress < TAPBlock.Data.Length)
            {
                return TAPBlock.Data[Progress++];
            }
            return null;
        }
        public string Details
        {
            get
            {
                return "Block Length: " + BlockLength.ToString() + Environment.NewLine +
                        "Zero Length: " + ZeroLength.ToString() + Environment.NewLine +
                        "One Length: " + OneLength.ToString() + Environment.NewLine +
                        "Pause Length: " + PauseLength.ToString() + Environment.NewLine +
                        "Used Bits: " + UsedBits.ToString() + Environment.NewLine +
                        TAPBlock.ToString() ;
            }
        }
    }
}
