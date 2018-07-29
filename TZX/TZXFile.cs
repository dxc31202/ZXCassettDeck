using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.IO.Compression;

namespace ZXCassetteDeck
{
    public class TZXFile
    {
        public List<ITZXBlock> Blocks { get; private set; }

        public TZXFile()
        {
            thelength = -1;
            Blocks = new List<ITZXBlock>();
        }
        int thelength = -1;
        public int TZXLength
        {
            get
            {
                if (thelength == -1)
                {
                    int result = 0;
                    for (int i = 0; i < Blocks.Count; i++)
                    {
                        if (Blocks[i] is ITZXDataBlock)
                            if (((ITZXDataBlock)Blocks[i]).TAPBlock != null)
                                result += ((ITZXDataBlock)Blocks[i]).TAPBlock.Length;
                    }
                    thelength = result;
                }

                return thelength;
            }
        }

        public int TZXBlocks
        {
            get
            {
                int result = 0;
                for (int i = 0; i < Blocks.Count; i++)
                {
                    if (Blocks[i] is ITZXDataBlock)
                        result++;
                }
                return result;
            }
        }

        public event FoundBlockEventHandler FoundBlock;

        void OnFoundBlock(ITZXBlock block)
        {
            if (block.ID == TZXBlockType.EndOfFile)
                return;
            block.Index = Blocks.Count;
            Blocks.Add(block);
            contents.AppendLine(block.ID.ToString() + "[" + block.Details+ "]");
            if (FoundBlock != null)
                FoundBlock(block);

        }

        string lastErrorCode = "";
        //static int index;
        StringBuilder contents;
        public bool LoadFile(string filename)
        {
            contents = new StringBuilder();
           lastErrorCode = "";
            Blocks = new List<ITZXBlock>();
            try
            {

                //Console.WriteLine(filename);
                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {

                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] data = br.ReadBytes((int)br.BaseStream.Length);// + 20);

                        int pointer = 0;
                        if (filename.ToLower().EndsWith(".tap"))
                        {
                            while (pointer < data.Length)
                            {
                                TapeDataBlock tdb = new TapeDataBlock(data, ref pointer);
                                OnFoundBlock(tdb);
                            }


                        }
                        else
                        {
                            TZXHeader header = new TZXHeader(data, ref pointer);
                            OnFoundBlock(header);
                            while (pointer < data.Length)
                            {
                                TZXBlockType tzxblocktype = (TZXBlockType)data[pointer++];
                                //Console.WriteLine(tzxblocktype);
                                switch (tzxblocktype)
                                {
                                    case TZXBlockType.StandardSpeedDataBlock: OnFoundBlock(new StandardSpeedDataBlock(data, ref pointer)); break;
                                    case TZXBlockType.TurboSpeedDataBlock: OnFoundBlock(new TurboSpeedDataBlock(data, ref pointer)); break;
                                    case TZXBlockType.PureDataBlock: OnFoundBlock(new PureDataBlock(data, ref pointer)); break;
                                    case TZXBlockType.PureTone: OnFoundBlock(new PureTone(data, ref pointer)); break;
                                    case TZXBlockType.SequenceOfPulsesOfVariousLengths: OnFoundBlock(new SequenceOfPulsesOfVariousLengths(data, ref pointer)); break;
                                    case TZXBlockType.DirectRecordingBlock: OnFoundBlock(new DirectRecordingBlock(data, ref pointer)); break;
                                    case TZXBlockType.CswRecordingBlock: OnFoundBlock(new CSWRecording(data, ref pointer)); break;
                                    case TZXBlockType.GeneralizedDataBlock: OnFoundBlock(new GeneralizedDataBlock(data, ref pointer)); break;
                                    case TZXBlockType.PauseSilenceOrStopTheTapeCommand: OnFoundBlock(new PauseStopTheTape(data, ref pointer)); break;
                                    case TZXBlockType.GroupStart: OnFoundBlock(new GroupStart(data, ref pointer)); break;
                                    case TZXBlockType.GroupEnd: OnFoundBlock(new GroupEnd()); break;
                                    case TZXBlockType.JumpToBlock: OnFoundBlock(new JumpToBlock(data, ref pointer)); break;
                                    case TZXBlockType.LoopStart: OnFoundBlock(new LoopStart(data, ref pointer)); break;
                                    case TZXBlockType.LoopEnd: OnFoundBlock(new LoopEnd(data, ref pointer)); break;
                                    case TZXBlockType.CallSequence: OnFoundBlock(new CallSequence(data, ref pointer)); break;
                                    case TZXBlockType.ReturnFromSequence: OnFoundBlock(new ReturnFromSequence(data, ref pointer)); break;
                                    case TZXBlockType.SelectBlock: OnFoundBlock(new SelectBlock(data, ref pointer)); break;
                                    case TZXBlockType.StopTheTapeIfIn48kMode: OnFoundBlock(new StopIfTapeIn48KMode(data, ref pointer)); break;
                                    case TZXBlockType.SetSignalLevel: OnFoundBlock(new SetSignalLevel(data, ref pointer)); break;
                                    case TZXBlockType.TextDescription: OnFoundBlock(new TextDescription(data, ref pointer)); break;
                                    case TZXBlockType.MessageBlock: OnFoundBlock(new MessageBlock(data, ref pointer)); break;
                                    case TZXBlockType.ArchiveInfo: OnFoundBlock(new ArchiveInfo(data, ref pointer)); break;
                                    case TZXBlockType.HardwareType: OnFoundBlock(new HardwareType(data, ref pointer)); break;
                                    case TZXBlockType.CustomInfoBlock: OnFoundBlock(new CustomInfoBlock(data, ref pointer)); break;
                                    case TZXBlockType.GlueBlock: OnFoundBlock(new GlueBlock(data, ref pointer)); break;
                                    default:
                                        {
                                            //if (TZXFunctions.EnumToString(tzxblocktype) != "Header")
                                            //{
                                            //    lastErrorCode += filename + '\t' + TZXFunctions.EnumToString(tzxblocktype) + Environment.NewLine;
                                            //    throw new Exception("Unknown Block: " + TZXFunctions.EnumToString(tzxblocktype));
                                            //}
                                            continue;
                                        }
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lastErrorCode = filename + '\t' + ex.Message + Environment.NewLine;
                Console.WriteLine("Error: " + lastErrorCode);
                //throw new Exception(lastErrorCode);
            }

            EndOfFile eof = new EndOfFile();
            OnFoundBlock(eof);
            //Console.WriteLine(contents.ToString());
            File.WriteAllText(filename+".txt", contents.ToString());
            return true;
        }

    }
}
