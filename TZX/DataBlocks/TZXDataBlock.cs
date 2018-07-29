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
    public class TZXDataBlock : ITZXDataBlock
    {
        public int Index { get; set; }
        byte[] RawData;
        int RawDataLength;
        TZXBlockType id;
        #region Interface members

        public TZXBlockType ID { get { return id; } set { id = value; } }
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
        int blockLength;
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

        public TZXDataBlock(ITZXDataBlock sourceDataBlock)
        {
            pulseLength = sourceDataBlock.PulseLength;
            pulseToneLength = sourceDataBlock.PulseToneLength;
            sync1Length = sourceDataBlock.Sync1Length;
            sync2Length = sourceDataBlock.Sync2Length;
            zeroLength = sourceDataBlock.ZeroLength;
            oneLength = sourceDataBlock.OneLength;
            pauseLength = sourceDataBlock.PauseLength;
            usedBits = sourceDataBlock.UsedBits;
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

        public override string ToString()
        {
            return tAPBlock.Description;
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