//using System;
//using System.Collections.Generic;

//using System.Text;

//using System.IO;
//using System.Windows.Forms;
//namespace ZXCassetteDeck
//{
//    public partial class CassetteController
//    {
//        public static int TZXBlockIndex
//        {
//            get { return tZXBlockIndex; }
//            set
//            {

//                tZXBlockIndex = value;
//                if (tZXBlockIndex >= TZXFile.Blocks.Count)
//                {
//                    tZXBlockIndex = 0;
//                    StopTheTape();
//                }
//                if (tZXBlockIndex < 0) tZXBlockIndex = 0;
//                OnNextBlock(TZXFile.Blocks[tZXBlockIndex], tZXBlockIndex);
//            }
//        }
//        public static void SelectPreviousBlock()
//        {
//            if (!FileLoaded) return;
//            if (TZXBlockIndex > 0)
//            {
//                TZXBlockIndex--;
//            }
//        }
//        public static void SelectNextBlock()
//        {
//            if (!FileLoaded) return;
//            if (TZXBlockIndex < TZXFile.Blocks.Count)
//            {
//                TZXBlockIndex++;
//            }
//        }
//        public static void SelectFirstBlock()
//        {
//            if (!FileLoaded) return;
//            if (TZXBlockIndex > 0)
//            {
//                TZXBlockIndex = 0;
//            }

//        }
//        public static void SelectLastBlock()
//        {
//            if (!FileLoaded) return;
//            if (TZXBlockIndex <= TZXFile.Blocks.Count)
//            {
//                TZXBlockIndex = TZXFile.Blocks.Count - 1;
//            }
//        }
//        public static TZXFile TZXFile { get; private set; }
//        public CassetteController()
//        {
//            if (deck == null)
//                deck = new CassetteDeck();

//        }
//        static int EarBit
//        {
//            get
//            {
//                return earBit;
//            }
//            set
//            {
//                earBit = value;
//                OnOutTone(earBit);
//            }
//        }

//        public static void OnLoaded()
//        {
//            Loaded?.Invoke();
//        }
//        public static void OnMoveParent()
//        {
//            MoveParent?.Invoke();
//        }
//        internal static void OnFoundBlock(ITZXBlock block)
//        {
//            FoundBlock?.Invoke(block);
//        }
//        public static void OnNextBlock(ITZXBlock block, int blockIndex)
//        {
//            NextBlock?.Invoke(block, blockIndex);
//        }
//        public static void OnEndBlock(ITZXBlock block, int blockIndex)
//        {
//            EndBlock?.Invoke(block, blockIndex);
//        }
//        public static void OnProgress(TZXFile tzxfile, int blocklength, int tapeCounter, int blockCounter, float overallProgress, float blockProgress, string block, ITZXDataBlock tZXDataBlock)
//        {
//            Progress?.Invoke(tzxfile, blocklength, tapeCounter, blockCounter, overallProgress, blockProgress, block, tZXDataBlock);
//        }
//        internal static void OnBeginLoadFile(string filename)
//        {
//            BeginLoadFile?.Invoke(filename);
//        }
//        public static void OnEndFileLoad(string filename, TZXFile tzxFile)
//        {
//            EndFileLoad?.Invoke(filename, tzxFile);
//        }
//        public static void OnFileClosed()
//        {
//            FileClosed?.Invoke();
//        }
//        internal static void OnDeckToggle(CassetteDeckStates cassetteDeckState)
//        {
//            DeckToggle?.Invoke(cassetteDeckState);
//        }
//        public static void ToggleDeck(CassetteDeckStates cassetteDeckState)
//        {

//            OnDeckToggle(cassetteDeckState);
//        }

//        private static void TZXFile_FoundBlock(ITZXBlock block)
//        {
//            OnFoundBlock(block);
//        }
//        public static string Filename;
//        public static void ReLoadFile()
//        {

//            if (Filename != null)
//                LoadFile(Filename);
//        }
//        public static void LoadFile(string filename)
//        {
//            Filename = filename;

//            ToggleDeck(CassetteDeckStates.Show);
//            OnBeginLoadFile(filename);
//            TZXFile = new TZXFile();
//            TZXFile.FoundBlock += TZXFile_FoundBlock;
//            try
//            {
//                TZXFile.LoadFile(filename);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }

//            OnEndFileLoad(filename, TZXFile);
//            Reset();
//        }
//        public void FileClose()
//        {
//            Filename = "";
//            TZXFile = null;
//            IsPlaying = false;
//            OnFileClosed();
//        }
//        public static void CloseFile()
//        {
//            Filename = "";
//            TZXFile = null;
//            IsPlaying = false;
//            OnFileClosed();
//        }
//        public static void Play()
//        {
//            IsPlaying = true;
//        }
//        public static void Pause()
//        {
//            IsPlaying = false;
//        }
//        public static void Cancel()
//        {
//            IsPlaying = false;
//            SelectFirstBlock();
//            OnProgress(null, 0, 0, 0, 0, 0, "", null);

//        }
//        public static bool FileLoaded
//        {
//            get
//            {
//                if (TZXFile == null) return false;
//                return TZXFile.Blocks.Count > 0;
//            }
//        }
//        public static void Reset()
//        {
//            pulseLength = 0;
//            pulseDone = 0;
//            TZXTapeCounter = 0;
//            TZXBlockIndex = 0;
//            IsPlaying = false;
//            TZXState = TZXStates.PilotTone;
//            EarBit = 0;
//            pulseLength = 0;

//            usedBits = 0;
//            TZXCurrentByte = 0;
//            PulseCounter = 0;
//            usedBits = 0;
//            bitCounter = 0;
//            bitlimit = 0;

//            OnProgress(null, 0, 0, 0, 0, 0, "", null);
//        }

//        public static void LoadRequest()
//        {
//            if (IsPlaying) return;
//            //TZXTapeCounter = 0;
//            if (!isPlaying)
//            {
//                TZXTapeCounter = 0;
//                tZXBlockIndex = -1;
//                TZXState = TZXStates.PilotTone;
//                pulseLength = 0;
//                OnProgress(null, 0, 0, 0, 0, 0, "", null);
//            }
//            IsPlaying = true;
//            pulseDone = 0;
//            EarBit = 0;

//        }

//        int GetNextBlock()
//        {
//            return GetNextBlock(true);
//        }
//        Stack<int> stack = new Stack<int>();
//        int GetNextBlock(bool increment)
//        {


//            if (TZXBlockIndex == 0) TZXTapeCounter = 0;
//            if (increment) TZXBlockIndex++;
//            if (TZXBlockIndex > TZXFile.Blocks.Count)
//            {
//                IsPlaying = false;
//                OnLoaded();
//                return 0;
//            }
//            //if (TZXBlockIndex > 0) OnEndBlock(TZXBlock, TZXBlockIndex);

//            TZXState = TZXStates.EndPause;
//            pulseLength = 0;
//            TZXBlock = TZXFile.Blocks[TZXBlockIndex];
//            Console.WriteLine(TZXFunctions.EnumToString( TZXBlock.ID));

//            switch (TZXBlock.ID)
//            {
//                case TZXBlockType.TapeDataBlock:
//                case TZXBlockType.StandardSpeedDataBlock:
//                case TZXBlockType.TurboSpeedDataBlock:
//                    {
//                        ITZXDataBlock tZXBlock = TZXBlock as ITZXDataBlock;
//                        pulseLength = tZXBlock.PulseLength;
//                        tZXBlock.Progress = 0;
//                        usedBits = tZXBlock.UsedBits;
//                        TZXState = TZXStates.PilotTone;
//                        TZXCurrentByte = tZXBlock.TAPBlock.FirstByte();
//                        break;
//                    }
//                case TZXBlockType.PureDataBlock:
//                    {
//                        PureDataBlock tZXBlock = TZXBlock as PureDataBlock;
//                        PulseCounter = 2;

//                        if ((tZXBlock[0] & 0x80) > 0)
//                            pulseLength = tZXBlock.OneLength;
//                        else
//                            pulseLength = tZXBlock.ZeroLength;
//                        //if (tZXBlock.TAPBlock.Length == 1)
//                        usedBits = tZXBlock.UsedBits;
//                        //else
//                        //    usedBits = 8;
//                        tZXBlock.Progress = 0;
//                        TZXState = TZXStates.DataBytes;
//                        TZXCurrentByte = tZXBlock.TAPBlock.FirstByte();
//                        break;
//                    }

//                case TZXBlockType.PureTone:
//                    {
//                        PureTone tZXBlock = TZXBlock as PureTone;
//                        PulseToneLength = tZXBlock.numberOfPulses;
//                        pulseLength = tZXBlock.PulseLength;
//                        tZXBlock.Progress = 0;
//                        TZXState = TZXStates.PilotTone;
//                        break;
//                    }

//                case TZXBlockType.SequenceOfPulsesOfVariousLengths:
//                    {
//                        SequenceOfPulsesOfVariousLengths tZXBlock = TZXBlock as SequenceOfPulsesOfVariousLengths;
//                        PulseToneLength = tZXBlock.PulseToneLength;
//                        pulseLength = tZXBlock.PulsesLengths[0];
//                        tZXBlock.Progress = 0;
//                        TZXState = TZXStates.PilotTone;
//                        break;
//                    }

//                case TZXBlockType.PauseSilenceOrStopTheTapeCommand:
//                    {

//                        PauseStopTheTape tZXBlock = TZXBlock as PauseStopTheTape;
//                        if (tZXBlock.PauseDuration == 0)
//                            StartStopTheTape();
//                        else
//                        {
//                            pulseLength = tZXBlock.PauseDuration * 3500;
//                            TZXState = TZXStates.EndPause;
//                        }
//                        break;
//                    }
//                case TZXBlockType.StopTheTapeIfIn48kMode:
//                    {
//                        StartStopTheTape();
//                        break;
//                    }

//                case TZXBlockType.JumpToBlock:
//                    {
//                        JumpToBlock tZXBlock = TZXBlock as JumpToBlock;
//                        sbyte n = (sbyte)tZXBlock.RelativeJumpValue;
//                        TZXBlockIndex += n;
//                        GetNextBlock(false);
//                        break;
//                    }

//                case TZXBlockType.LoopStart:
//                    {
//                        LoopStart tZXBlock = TZXBlock as LoopStart;
//                        loopCounter = tZXBlock.NumberOfRepetitions;
//                        TZXBlockIndex++;
//                        loopNext = TZXBlockIndex;
//                        GetNextBlock(false);
//                        break;

//                    }
//                case TZXBlockType.LoopEnd:
//                    {
//                        LoopEnd tZXBlock = TZXBlock as LoopEnd;
//                        loopCounter--;
//                        if (loopCounter > 0)
//                            TZXBlockIndex = loopNext;
//                        else
//                            TZXBlockIndex++;
//                        GetNextBlock(false);
//                        break;


//                    }
//                case TZXBlockType.CallSequence:
//                    {
//                        CallSequence tZXBlock = TZXBlock as CallSequence;
//                        stack.Push(TZXBlockIndex);
//                        numberOfCalls = tZXBlock.NumberOfCallsToBeMade;
//                        callList = tZXBlock.ArrayOfCallBlockNumbers;
//                        callCounter = 0;
//                        sbyte n = (sbyte)tZXBlock.ArrayOfCallBlockNumbers[callCounter++];
//                        TZXBlockIndex = TZXBlockIndex += n;
//                        GetNextBlock(false);
//                        break;
//                    }
//                case TZXBlockType.ReturnFromSequence:
//                    {
//                        if (callCounter < numberOfCalls)
//                        {
//                            sbyte n = (sbyte)callList[callCounter++];
//                            TZXBlockIndex = callSequenceOrigin + n;
//                            GetNextBlock(false);
//                        }
//                        else
//                        {
//                            TZXBlockIndex = stack.Pop();
//                            GetNextBlock();
//                        }

//                        break;
//                    }
//                case TZXBlockType.EndOfFile:
//                    {
//                        IsPlaying = false;
//                        OnLoaded();
//                    }
//                    break;
//                default:
//                    GetNextBlock();
//                    break;
//            }
//            return pulseLength;
//        }
//        void StartStopTheTape()
//        {
//            if (IsPlaying) StopTheTape(); else StartTheTape();
//        }
//        public static void StopTheTape()
//        {
//            if (IsPlaying) IsPlaying = false;
//        }
//        public static void Rewind()
//        {
//            SelectFirstBlock();
//            if (Filename != null && Filename.Length > 0) LoadFile(Filename);
//        }
//        static void StartTheTape()
//        {
//            IsPlaying = true;
//            pulseDone = 0;
//            EarBit = 0;
//        }
//        public static bool IsPlaying
//        {
//            get { return isPlaying; }
//            set
//            {
//                isPlaying = value;
//                if (PlayingChanged != null)
//                    PlayingChanged(isPlaying);
//            }
//        }
//        internal static void OnOutTone(int tone)
//        {
//            if (OutTone != null)
//                OutTone(tone);
//        }

//        public void NextPulse(int Cycles)
//        {
//            pulseDone += Cycles;
//            while (pulseLength < pulseDone)
//            {
//                pulseDone -= pulseLength;
//                if (TZXBlockIndex == -1)
//                    TZXBlockIndex = 0;
//                TZXBlock = TZXFile.Blocks[TZXBlockIndex];
//                switch (TZXBlock.ID)
//                {
//                    case TZXBlockType.TapeDataBlock:
//                    case TZXBlockType.StandardSpeedDataBlock:
//                    case TZXBlockType.TurboSpeedDataBlock:
//                    case TZXBlockType.PureDataBlock:
//                        pulseLength = NextPulseLength((ITZXDataBlock)TZXBlock);
//                        break;
//                    case TZXBlockType.PureTone:
//                        pulseLength = NextPulseLength((PureTone)TZXBlock);
//                        break;
//                    case TZXBlockType.SequenceOfPulsesOfVariousLengths:
//                        pulseLength = NextPulseLength((SequenceOfPulsesOfVariousLengths)TZXBlock);
//                        break;
//                    default:
//                        pulseLength = GetNextBlock();
//                        break;
//                }
//            }
//        }

//        int NextPulseLength(ITZXDataBlock TZXDataBlock)
//        {
//            switch (TZXState)
//            {
//                case TZXStates.PilotTone: return PilotTone(TZXDataBlock);
//                case TZXStates.SYNC1Pulse: return SYNC1Pulse(TZXDataBlock);
//                case TZXStates.SYNC2Pulse: return SYNC2Pulse(TZXDataBlock);
//                case TZXStates.DataBytes: return DataBytes(TZXDataBlock);
//                case TZXStates.EndPause: return GetNextBlock();
//            }
//            return 0;
//        }
//        int PilotTone(ITZXDataBlock TZXDataBlock)
//        {
//            EarBit = EarBit ^ 64;
//            if (TZXDataBlock.Progress < TZXDataBlock.PulseToneLength)
//            {
//                TZXDataBlock.Progress++;
//                return TZXDataBlock.PulseLength;
//            }
//            else
//            {
//                TZXDataBlock.Progress = 0;
//                TZXState = TZXStates.SYNC1Pulse;
//                return TZXDataBlock.Sync1Length;
//            }
//        }
//        int SYNC1Pulse(ITZXDataBlock TZXDataBlock)
//        {
//            EarBit = EarBit ^ 64;
//            TZXState = TZXStates.SYNC2Pulse;
//            return TZXDataBlock.Sync2Length;
//        }
//        int SYNC2Pulse(ITZXDataBlock TZXDataBlock)
//        {
//            EarBit = EarBit ^ 64;
//            TZXState = TZXStates.DataBytes;
//            PulseCounter = 2;
//            usedBits = 8;
//            bitCounter = 0x80;
//            bitlimit = 1;
//            if ((TZXDataBlock[0] & 128) > 0)
//                return TZXDataBlock.OneLength;
//            else
//                return TZXDataBlock.ZeroLength;
//        }

//        // TODO
//        static int bitCounter = 0x80;
//        static int bitlimit;
//        int[] bitvalues = { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };
//        int DataBytes(ITZXDataBlock TZXDataBlock)
//        {
//            EarBit = EarBit ^ 64;
//            PulseCounter--;
//            if (PulseCounter > 0)
//            {
//                if ((TZXCurrentByte & 0x80) == 0x80)
//                    return TZXDataBlock.OneLength;
//                else
//                    return TZXDataBlock.ZeroLength;
//            }
//            // Exit Pulse
//            if (usedBits > 1)
//            {
//                usedBits--;
//                PulseCounter = 2;
//                TZXCurrentByte <<= 1;
//                return ((TZXCurrentByte & 0x80) == 0x80) ? TZXDataBlock.OneLength : TZXDataBlock.ZeroLength;
//            }
//            // Next Byte?
//            // While    any bytes left
//            TZXDataBlock.Progress++;
//            TZXTapeCounter++;
//            if (TZXDataBlock.Progress < TZXDataBlock.TAPBlock.Length)
//            {

//                OnProgress(TZXFile, TZXDataBlock.TAPBlock.Length, TZXTapeCounter, TZXDataBlock.Progress, Percent(TZXFile.TZXLength, TZXTapeCounter), Percent(TZXDataBlock.TAPBlock.Length - 1, TZXDataBlock.Progress), TZXDataBlock.ToString(), TZXDataBlock);
//                if (TZXDataBlock.Progress == TZXDataBlock.TAPBlock.Length - 1)
//                    usedBits = TZXDataBlock.UsedBits;
//                else
//                    usedBits = 8;
//                bitlimit = bitvalues[usedBits - 1];
//                PulseCounter = 2;
//                TZXCurrentByte = TZXDataBlock.TAPBlock.NextByte(); //[TZXDataBlock.Progress];
//                return ((TZXCurrentByte & 0x80) == 0x80) ? TZXDataBlock.OneLength : TZXDataBlock.ZeroLength;

//            }
//            else
//            {
//                if (TZXDataBlock.PauseLength > 0)
//                {
//                    // Finished, Do Pause.
//                    TZXState = TZXStates.EndPause;
//                    OnLoaded();
//                    return TZXDataBlock.PauseLength * 3500;
//                }
//            }
//            TZXState = TZXStates.PilotTone;
//            return GetNextBlock();

//        }

//        int NextPulseLength(PureTone TZXDataBlock)
//        {
//            EarBit = EarBit ^ 64;
//            TZXDataBlock.Progress++;
//            if (TZXDataBlock.Progress < TZXDataBlock.numberOfPulses)
//                return TZXDataBlock.PulseLength;
//            OnLoaded();
//            return GetNextBlock();
//        }
//        int NextPulseLength(SequenceOfPulsesOfVariousLengths TZXDataBlock)
//        {
//            EarBit = EarBit ^ 64;
//            TZXDataBlock.Progress++;
//            if ((TZXDataBlock.Progress < TZXDataBlock.NumberOfPulses))
//                return TZXDataBlock.PulsesLengths[TZXDataBlock.Progress];
//            OnLoaded();
//            return GetNextBlock();
//        }

//        public static float Percent(int max, int current)
//        {
//            return (float)(current / (float)max * 100d);

//        }


//        int[] callList;
//        int callSequenceOrigin;
//        int numberOfCalls;
//        int callCounter;
//        int loopCounter;
//        int loopNext;
//        static int pulseDone;
//        static bool isPlaying;
//        static int usedBits;
//        static int tZXBlockIndex;
//        public static int TZXTapeCounter;
//        static int earBit;
//        static int pulseLength;
//        static int PulseToneLength;
//        static int PulseCounter;
//        ITZXBlock TZXBlock;
//        static TZXStates TZXState;
//        public CassetteDeck deck;
//        static int? TZXCurrentByte;

//    }

//}
