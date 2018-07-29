using System;
using System.IO;

/*

    This block is very similar to the normal TAP block but with some additional info on the timings and other important differences. 
    The same tape encoding is used as for the standard speed data block. 
    If a block should use some non-standard sync or pilot tones (i.e. all sorts of protection schemes) then use the next three blocks to describe it. 

    ID 11 - Turbo Speed Data Block: length: [0F,10,11]+12 

    Offset  Value   Type    Description 
    0x00        -   WORD    Length of PILOT pulse {2168} 
    0x02        -   WORD    Length of SYNC first pulse {667} 
    0x04        -   WORD    Length of SYNC second pulse {735} 
    0x06        -   WORD    Length of ZERO bit pulse {855} 
    0x08        -   WORD    Length of ONE bit pulse {1710} 
    0x0A        -   WORD    Length of PILOT tone (number of pulses) {8063 header (flag<128), 3223 data (flag>=128)} 
    0x0C        -   BYTE    Used bits in the last byte (other bits should be 0) {8}
                            (e.g. if this is 6, then the bits used (x) in the last byte are: xxxxxx00, 
                            where MSb is the leftmost bit, LSb is the rightmost bit) 
    0x0D        -   WORD    Pause after this block (ms.) {1000} 
    0x0F        N   BYTE[3] Length of data that follow 
    0x12        -   BYTE[N] Data as in .TAP files 
*/
namespace ZXCassetteDeck
{
    public class TurboSpeedDataBlock : ITZXDataBlock
    {
        public int Index { get; set; }
        byte[] RawData;
        int RawDataLength;
        #region Interface members
        public TZXBlockType ID { get { return TZXBlockType.TurboSpeedDataBlock; } }
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
                if (Progress == TAPBlock.Length - 1)
                    return usedBits;
                return 8;
            }
        }
        /// <summary>
        /// Data as in .TAP files 
        /// </summary>
        public ITAPBlock TAPBlock { get { return tAPBlock; } }
        public int Progress { get; set; }
        #endregion Interface members

        #region Local Variables
        int pulseLength;
        int pulseToneLength;
        int sync1Length;
        int sync2Length;
        int zeroLength;
        int oneLength;
        int pauseLength;
        int usedBits;

        ITAPBlock tAPBlock;
        #endregion Local Variables

        public TurboSpeedDataBlock(byte[] rawdata, ref int pointer)
        {
            int start = pointer;
            pulseLength = rawdata[pointer++] | (rawdata[pointer++] << 8);
            sync1Length = rawdata[pointer++] | (rawdata[pointer++] << 8);
            sync2Length = rawdata[pointer++] | (rawdata[pointer++] << 8);
            zeroLength = rawdata[pointer++] | (rawdata[pointer++] << 8);
            oneLength = rawdata[pointer++] | (rawdata[pointer++] << 8);
            pulseToneLength = rawdata[pointer++] | (rawdata[pointer++] << 8);
            usedBits = rawdata[pointer++];
            if (usedBits != 8)
                Console.WriteLine();
            pauseLength = rawdata[pointer++] | (rawdata[pointer++] << 8);
            tAPBlock = new TAPBlock(rawdata, ref pointer, true);

            blockLength = pointer - start;

        }

        int blockLength;
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

        public override string ToString()
        {
            return TZXFunctions.EnumToString(ID) + " {" + tAPBlock.Length.ToString() + " bytes}" ;
        }
        public int this[int index] { get { return tAPBlock.Data[index]; } }

        public string Details
        {
            get
            {
                return "Block Length: " + BlockLength.ToString() + Environment.NewLine +
                        "Pulse Length: " + PulseLength.ToString() + Environment.NewLine +
                        "Pulse Tone Length: " + PulseToneLength.ToString() + Environment.NewLine +
                        "Sync 1 Length: " + Sync1Length.ToString() + Environment.NewLine +
                        "Sync 2 Length: " + Sync2Length.ToString() + Environment.NewLine +
                        "Zero Length: " + ZeroLength.ToString() + Environment.NewLine +
                        "One Length: " + OneLength.ToString() + Environment.NewLine +
                        "Pause Length: " + PauseLength.ToString() + Environment.NewLine +
                        "Used Bits: " + UsedBits.ToString() + Environment.NewLine +
                        TAPBlock.ToString();
            }

        }
    }
}