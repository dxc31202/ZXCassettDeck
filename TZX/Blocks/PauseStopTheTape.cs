using System;
using System.Collections.Generic;

using System.Text;

using System.IO;
/*
    This will make a silence (low amplitude level (0)) for a given time in milliseconds. 
    If the value is 0 then the emulator or utility should (in effect) STOP THE TAPE, 
    i.e. should not continue loading until the user or emulator requests it.

    ID 20 - Pause (silence) or 'Stop the Tape' command      length: 02 

    Offset      Value       Type        Description 
    0x00            -       WORD        Pause duration (ms.) 

*/
namespace ZXCassetteDeck
{
    public class PauseStopTheTape : ITZXBlock
    {
        public int Index { get; set; }
        public TZXBlockType ID { get { return TZXBlockType.PauseSilenceOrStopTheTapeCommand; } }
        public bool StopTheTape { get { return PauseDuration == 0 ? true : false; } }

        public int PauseDuration;
        public PauseStopTheTape(byte[] rawdata, ref int pointer)
        {
            PauseDuration = rawdata[pointer++] | (rawdata[pointer++] << 8);
        }

        public string Details
        {
            get
            {
                string info = "";
                info += "Pause Duration: " + PauseDuration.ToString();
                return info;
            }
        }

        public override string ToString()
        {
            return "[Pause (Silence) or Stop the Tape]";
        }
    }
}
