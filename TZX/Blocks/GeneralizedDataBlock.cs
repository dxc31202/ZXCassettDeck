using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
/*
    This block has been specifically developed to represent an extremely wide range of data encoding techniques.
    The basic idea is that each loading component (pilot tone, sync pulses, data) is associated to a specific sequence of pulses, 
    where each sequence (wave) can contain a different number of pulses from the others. 
    
    In this way we can have a situation where bit 0 is represented with 4 pulses and bit 1 with 8 pulses.

    ID 19 - Generalized Data Block      length: [00,01,02,03]+04 

    Offset                                              Value       Type            Description 

    0x00                                                    -       DWORD           Block length (without these four bytes) 
    0x04                                                    -       WORD            Pause after this block (ms) 
    0x06                                                 TOTP       DWORD           Total number of symbols in pilot/sync block (can be 0) 
    0x0A                                                  NPP       BYTE            Maximum number of pulses per pilot/sync symbol
    0x0B                                                  ASP       BYTE            Number of pilot/sync symbols in the alphabet table (0 = 256)
    0x0C                                                 TOTD       DWORD           Total number of symbols in data stream (can be 0) 
    0x10                                                  NPD       BYTE            Maximum number of pulses per data symbol
    0x11                                                  ASD       BYTE            Number of data symbols in the alphabet table (0 = 256)
    0x12                                                    -       SYMDEF[ASP]     Pilot and sync symbols definition table (This field is present only if TOTP > 0)
    0x12+(2*NPP+1)*ASP                                      -       PRLE[TOTP]      Pilot and sync data stream (This field is present only if TOTP > 0)
    0x12+(TOTP>0)*((2*NPP+1)*ASP)+TOTP*3                    -       SYMDEF[ASD]     Data symbols definition table (This field is present only if TOTD > 0)
    0x12+(TOTP>0)*((2*NPP+1)*ASP)+TOTP*3+(2*NPD+1)*ASD      -       BYTE[DS]        Data stream (This field is present only if TOTD > 0)
*/
namespace ZXCassetteDeck
{
    public class GeneralizedDataBlock:ITZXBlock
    {
        public int Index { get; set; }
        byte[] rawData;
        public int BlockLength;
        public int PauseAfterThisBlock;
        public int TotalNumberOfSymbolsInPilotSyncBlock;        //  TOTP
        public byte MaximumNumberOfPulsesPerPilot;              //  NPP
        public byte NumberOfPilotSyncSymbolsInTheAlphabetTable; //  ASP

        public int TotalNumberOfSymbolsInDataStream;            //  TOTD
        public byte MaximumNumberOfPulsesPerDataSymbol;         //  NPD
        public byte NumberOfDataSymbolsInTheAlphabetTable;      //  ASD

        public SYMDEF PilotAndSyncSymbolsDefinitionTable;     //  (This field is present only if TOTP > 0)
        public PRLE PilotAndSyncDataStream;                   //  (This field is present only if TOTP > 0)

        public SYMDEF DataSymbolsDefinitionTable;             //  (This field is present only if TOTD > 0)
        public byte[] DataStream;                               //  (This field is present only if TOTD > 0)


        public TZXBlockType ID { get { return TZXBlockType.GeneralizedDataBlock; } }

        public GeneralizedDataBlock(byte[] rawdata, ref int pointer)
        {
            // Just bypass this block for now
            BlockLength = rawdata[pointer++] | (rawdata[pointer++] << 8) | (rawdata[pointer++] << 0x10) | (rawdata[pointer++] << 0x18);
            rawData = new byte[BlockLength];
            for (int i = 0; i < BlockLength; i++)
                rawData[i] = rawdata[pointer++];

            //PauseAfterThisBlock = rawdata[pointer++] | (rawdata[pointer++] << 8);
            //TotalNumberOfSymbolsInPilotSyncBlock = rawdata[pointer++] | (rawdata[pointer++] << 8) | (rawdata[pointer++] << 0x10) | (rawdata[pointer++] << 0x18);

            //MaximumNumberOfPulsesPerPilot = rawdata[pointer++];
            //NumberOfPilotSyncSymbolsInTheAlphabetTable = rawdata[pointer++];
            //TotalNumberOfSymbolsInDataStream = rawdata[pointer++] | (rawdata[pointer++] << 8) | (rawdata[pointer++] << 0x10) | (rawdata[pointer++] << 0x18);

            //MaximumNumberOfPulsesPerDataSymbol = rawdata[pointer++];
            //NumberOfDataSymbolsInTheAlphabetTable = rawdata[pointer++];

            //if (TotalNumberOfSymbolsInPilotSyncBlock > 0)
            //{
            //    PilotAndSyncSymbolsDefinitionTable = new SYMDEF(br, NumberOfPilotSyncSymbolsInTheAlphabetTable);
            //    PilotAndSyncDataStream = new PRLE(br);
            //}
            //if (TotalNumberOfSymbolsInDataStream > 0)
            //{
            //   // DataSymbolsDefinitionTable = br.ReadBytes(NumberOfDataSymbolsInTheAlphabetTable);
            //    //DataStream;
            //}


        }

        public string Details
        {
            get
            {
                string info = "";
                return info;
            }
        }

        public override string ToString()
        {
            return TZXFunctions.EnumToString(ID);
        }


        // TODO
        #region SYMDEF
        /*
        The alphabet is stored using a table where each symbol is a row of pulses. 
        The number of columns (i.e. pulses) of the table is the length of the longest sequence amongst all 
        (MAXP=NPP or NPD, for pilot/sync or data blocks respectively); shorter waves are terminated by a zero-length pulse in the sequence.

        Any number of data symbols is allowed, so we can have more than two distinct waves; for example, imagine a loader which writes 
        two bits at a time by encoding them with four distinct pulse lengths: 
        this loader would have an alphabet of four symbols, each associated to a specific sequence of pulses (wave).

        SYMDEF structure format 

        Offset      Value       Type        Description 
        0x00            -       BYTE        Symbol flags
                                                b0-b1: starting symbol polarity
                                                  00: opposite to the current level (make an edge, as usual) - default
                                                  01: same as the current level (no edge - prolongs the previous pulse)
                                                  10: force low level
                                                  11: force high level
        0x01            -       WORD[MAXP]  Array of pulse lengths. 
        */
        public class SYMDEF
        {
            byte SymbolFlags;
            int[] ArrayOfPulseLengths;
            public SYMDEF(byte[] rawdata, ref int pointer, int ASP)
            {
                SymbolFlags = rawdata[pointer++];
                ArrayOfPulseLengths = new int[ASP];
                for (int i = 0; i < ASP; i++)
                    ArrayOfPulseLengths[i] = rawdata[pointer++] | (rawdata[pointer++] << 8);
            }
        }
        #endregion SYMDEF
        #region PRLE
        /*
            Most commonly, pilot and sync are repetitions of the same pulse, thus they are represented using a very simple RLE encoding 
            structure which stores the symbol and the number of times it must be repeated.

            Each symbol in the data stream is represented by a string of NB bits of the block data, where NB = ceiling(Log2(ASD)). 
            Thus the length of the whole data stream in bits is NB*TOTD, or in bytes DS=ceil(NB*TOTD/8). 
            
            PRLE structure format 

            Offset      Value       Type        Description 
            0x00            -       BYTE        Symbol to be represented 
            0x01            -       WORD        Number of repetitions 
        */
        public class PRLE
        {
            byte SymbolToBeRepresented;
            int NumberOfRepetitions;
            public PRLE(byte[] rawdata, ref int pointer)
            {
                SymbolToBeRepresented = rawdata[pointer++];
                NumberOfRepetitions = rawdata[pointer++] | (rawdata[pointer++] << 8);
            }
        }
        #endregion PRLE
    }
}
