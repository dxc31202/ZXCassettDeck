using System;
using System.Collections.Generic;

using System.Text;

// Not really a TZX Data Block, Just a TAP block wrapped as a TZX Data Block
namespace ZXCassetteDeck
{
    public class TapeDataBlock: ITZXDataBlock
    {
        public int Index { get; set; }
        public byte[] RawData;
        public int RawDataLength;
        #region Interface members
        public TZXBlockType ID { get { return TZXBlockType.TapeDataBlock; } }
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
        public int PulseToneLength { get { return TAPBlock.Data[0] <128 ? 3223 : 8063; } }
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
        public int PauseLength { get { return 1000; } }
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
        public int Progress { get; set; }
        #endregion Interface members

        #region Local Variables
        public ITAPBlock tAPBlock;
        #endregion Local Variables

        public TapeDataBlock(byte[] rawdata, ref int pointer)
        {

            int start = pointer;
            tAPBlock = new TAPBlock(rawdata, ref pointer);

            blockLength = pointer - start;
          
        }

        int blockLength;
        public override string ToString()
        {
            return Globals.Header(this);
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

        public int this[int index] { get { return TAPBlock.Data[index]; } }
        public string Details
        {
            get
            {
                return "Length: " + tAPBlock.Length.ToString() + Environment.NewLine + 
                    //"Checksum: " + TAPBlock.Checksum.ToString() + Environment.NewLine +
                    //"ID: " + TAPBlock.ID.ToString() + Environment.NewLine + 
                    //"Is Valid: " + TAPBlock.IsValid.ToString() + Environment.NewLine + 
                    "Data:" + Environment.NewLine +
                        TZXFunctions.ArrayToString(RawData, 16, 2) + Environment.NewLine;

            }

        }

    }
}
