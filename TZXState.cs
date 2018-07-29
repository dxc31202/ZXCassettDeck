using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZXCassetteDeck
{
    static class TZXState
    {
        public static TZXStates State = TZXStates.PilotTone;

        public static void Reset()
        {
            State = TZXStates.PilotTone;
        }

        public static void Next()
        {
            switch (State)
            {
                case TZXStates.PilotTone: State = TZXStates.SYNC1Pulse; break;
                case TZXStates.SYNC1Pulse: State = TZXStates.SYNC2Pulse; break;
                case TZXStates.SYNC2Pulse: State = TZXStates.DataBytes; break;
                case TZXStates.DataBytes: State = TZXStates.EndPause; break;
                case TZXStates.EndPause: State = TZXStates.PilotTone; break;
            }
        }
    }
}

