using System;
using System.Collections.Generic;

using System.Text;


namespace ZXCassetteDeck
{
    public interface ITAPBlock
    {
        byte? FirstByte();
        byte? NextByte();
        int Next { get; set; }
        int Length { get; }
        //int Checksum { get; }
        //int ID { get; }
        byte[] Data { get; }
        //bool IsValid { get; }
        string Description { get; }

    }
}
