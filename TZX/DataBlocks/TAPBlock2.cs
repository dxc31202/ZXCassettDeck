//using System;
//using System.IO;

//namespace ZXCassetteDeck
//{
//    public class TAPBlock2 : ITAPBlock
//    {
//        byte[] RawData;
//        int RawDataLength;

//        public  int Length { get { return length; } }
//        public int Checksum { get { return checksum; } }
//        public int ID { get { return id; } }
//        public bool IsValid { get { return isValid; } }
//        public byte[] Data { get { return data; } }

//        int length;
//        public byte[] data;
//        int checksum;
//        int id;
//        bool isValid;
//        public TAPBlock2(byte[] rawdata, ref int pointer)
//        {
//            int start = pointer;
//            try
//            {

//                length = (rawdata[pointer++] | (rawdata[pointer++] << 8) | (rawdata[pointer++] << 0x10));
//                length = TZXFunctions.ReadBytes(ref data, ref rawdata, length, ref pointer);
//                id = data[0];
//                int chcksum = id;
//                for (int i = 1; i < data.Length - 1; i++)
//                {
//                    chcksum ^= data[i];
//                }
//                checksum = data[data.Length - 1];
//                isValid = (chcksum == checksum);

//                CacheRawData(rawdata, start, pointer);
//            }
//            catch
//            {
//                isValid = false;
//            }
//        }

//        void CacheRawData(byte[] sourceData, int start, int pointer)
//        {
//            // Cache Raw Data for this block
//            RawDataLength = pointer - start;
//            RawData = new byte[RawDataLength];
//            for (int i = 0; i < RawDataLength; i++)
//                RawData[i] = sourceData[start++];
//        }

//        public override string ToString()
//        {
//            return "TZXTAP2 Block: " + Environment.NewLine + "{" + Environment.NewLine + "  " +
//                    "ID: " + ID.ToString() + Environment.NewLine + "  " +
//                        TZXFunctions.ArrayToString(RawData, 16, 2) + Environment.NewLine +
//                   "}";

//        }

//    }
//}
