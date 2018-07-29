using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZXCassetteDeck
{
    public partial class CassetteController
    {
        public static event LoadedEventHandler Loaded;
        public static event FoundBlockEventHandler FoundBlock;
        public static event BeginLoadFileEventHandler BeginLoadFile;
        public static event EndFileLoadEventHandler EndFileLoad;
        public static event FileClosedEventHandler FileClosed;
        public static event NextBlockEventHandler NextBlock;
        public static event EndBlockEventHandler EndBlock;
        public static event ProgressEventHandler Progress;
        public static event OutToneEventHandler OutTone;
        public static event PlayingChangedEventHandler PlayingChanged;
        public static event DeckToggleEventHandler DeckToggle;
        public static event MoveParentEvenHandlert MoveParent;

    }

    public delegate void LoadedEventHandler();
    public delegate void FoundBlockEventHandler(ITZXBlock block);
    public delegate void BeginLoadFileEventHandler(string filename);
    public delegate void EndFileLoadEventHandler(string filename, TZXFile tzxFile);
    public delegate void FileClosedEventHandler();
    public delegate void NextBlockEventHandler(ITZXBlock block, int blockIndex);
    public delegate void EndBlockEventHandler(ITZXBlock block, int blockIndex);
    public delegate void ProgressEventHandler(TZXFile tzxFile,int blocklength, int tapeCounter, int blockCounter, float overallProgress, float blockProgress, string block, ITZXDataBlock tZXDataBlock);
    public delegate void OutToneEventHandler(int tone);
    public delegate void PlayingChangedEventHandler(bool isplaying);
    public delegate void DeckToggleEventHandler(CassetteDeckStates cassetteDeckState);
    public delegate void MoveParentEvenHandlert();

}
