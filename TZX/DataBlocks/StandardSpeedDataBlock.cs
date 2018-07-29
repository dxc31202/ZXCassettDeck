using System;
using System.IO;

/*

    This block must be replayed with the standard Spectrum ROM timing values - see the values in curly brackets in block ID 11. 
    The pilot tone consists in 8063 pulses if the first data byte (flag byte) is < 128, 3223 otherwise. 
    This block can be used for the ROM loading routines AND for custom loading routines that use the same timings as ROM ones do.

    ID 10 - Standard Speed Data Block:  length: [02,03]+04 

    Offset  Value   Type        Description 
    0x00        -   WORD        Pause after this block (ms.) {1000} 
    0x02        N   WORD        Length of data that follow 
    0x04        -   BYTE[N]     Data as in .TAP files 


            Machine         Pilot   Length  Sync1   Sync2   Bit 0    Bit 1 
            ZX Spectrum     2168    See (1)   667     735     855     1710 

            (1) The Spectrum uses different pilot lengths for header and data blocks. 
            Header blocks have 8063 and data blocks have 3223 pilot pulses.
*/
namespace ZXCassetteDeck
{
    public class StandardSpeedDataBlock : ITZXDataBlock
    {
        public int Index { get; set; }
        byte[] RawData;
        int RawDataLength;
        #region Interface members
        public TZXBlockType ID { get { return TZXBlockType.StandardSpeedDataBlock; } }
        /// <summary>
        /// Length of the whole block
        /// </summary>
        public int BlockLength { get { return blockLength; } }
        /// <summary>
        /// Length of PILOT pulse {2168}
        /// </summary>
        public int PulseLength { get { return 2168; } }
        /// <summary>
        /// Length of PILOT tone (number of pulses) {8063 header (flag &lt 128), 3223 data (flag &ge 128)} 
        /// </summary>
        public int PulseToneLength { get { return tAPBlock.Data[0] < 128 ? 3223 : 8063; } }
        /// <summary>
        /// Length of SYNC first pulse {667} 
        /// </summary>
        public int Sync1Length { get { return 667; } }
        /// <summary>
        /// Length of SYNC second pulse {735} 
        /// </summary>
        public int Sync2Length { get { return 735; } }
        /// <summary>
        /// Length of ZERO bit pulse {855} 
        /// </summary>
        public int ZeroLength { get { return 855; } }
        /// <summary>
        /// Length of ONE bit pulse {1710} 
        /// </summary>
        public int OneLength { get { return 1710; } }
        /// <summary>
        /// Pause after this block (ms.) {1000} 
        /// </summary>
        public int PauseLength { get { return pauseLength; } }
        /// <summary>
        /// Used bits in the last byte (other bits should be 0) {8}
        /// (e.g. if this is 6, then the bits used(x) in the last byte are: xxxxxx00, 
        /// where MSb is the leftmost bit, LSb is the rightmost bit) 
        /// </summary>
        public int UsedBits { get { return 8; } }
        /// <summary>
        /// Data as in .TAP files 
        /// </summary>
        public ITAPBlock TAPBlock { get { return tAPBlock; } }
        int progress;
        public int Progress { get { return progress; } set { progress = value; } }
        #endregion Interface members

        #region Local Variables
        int pauseLength;
        ITAPBlock tAPBlock;
        #endregion Local Variables

        public StandardSpeedDataBlock(byte[] rawdata, ref int pointer)
        {

            int start = pointer;
            pauseLength = rawdata[pointer++] | (rawdata[pointer++] << 8);
            tAPBlock = new TAPBlock(rawdata, ref pointer);

            blockLength = pointer - start;

        }

        public byte? First()
        {
            Progress = 0;
            if (Progress < TAPBlock.Data.Length)
            {
                Progress++;
                return TAPBlock.Data[Progress-1];
            }
            return null;
        }

        public byte? Next()
        {
            if (Progress < TAPBlock.Data.Length)
            {
                Progress++;
                return TAPBlock.Data[Progress-1];
            }
            return null;
        }

        int blockLength;
        public override string ToString()
        {
            return tAPBlock.Description ;
            //return TZXFunctions.EnumToString(ID);
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
                        TAPBlock.ToString() ;
            }

        }
    }
}