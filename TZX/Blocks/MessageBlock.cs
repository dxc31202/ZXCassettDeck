using System;
using System.Collections.Generic;

using System.Text;
using System.IO;

/*
    This will enable the emulators to display a message for a given time. 
    This should not stop the tape and it should not make silence. 
    If the time is 0 then the emulator should wait for the user to press a key.

    The text message should: 
        •   stick to a maximum of 30 chars per line;
        •   use single 0x0D (13 decimal) to separate lines;
        •   stick to a maximum of 8 lines.
    
    If you do not obey these rules, emulators may display your message in any way they like.

    ID 31 - Message block   length: [01]+02 

    Offset      Value       Type        Description 
    0x00            -       BYTE        Time (in seconds) for which the message should be displayed 
    0x01            N       BYTE        Length of the text message 
    0x02            -       CHAR[N]     Message that should be displayed in ASCII format 


*/
namespace ZXCassetteDeck
{
    public class MessageBlock : ITZXBlock
    {
        public int Index { get; set; }
        public byte TimeForWhichTheMessageShouldBeDisplayed;
        public byte LengthOfTheTextMessage;
        public char[] MessageThatShouldBeDisplayed;

        public TZXBlockType ID { get { return TZXBlockType.MessageBlock; } }
        public MessageBlock(byte[] rawdata, ref int pointer)
        {
            TimeForWhichTheMessageShouldBeDisplayed = rawdata[pointer++];
            LengthOfTheTextMessage = rawdata[pointer++];
            MessageThatShouldBeDisplayed = new char[LengthOfTheTextMessage];
            for (int i = 0; i < LengthOfTheTextMessage; i++)
                MessageThatShouldBeDisplayed[i] = (char)rawdata[pointer++];
        }

        public string Message { get { return new string(MessageThatShouldBeDisplayed); } }
        public string Details
        {
            get
            {
                string info = "";
                info += "TimeForWhichTheMessageShouldBeDisplayed: " + TimeForWhichTheMessageShouldBeDisplayed.ToString() + Environment.NewLine;
                info += "Message That Should Be Displayed: " + Message + Environment.NewLine;
                return info;
            }
        }

        public override string ToString()
        {
            return TZXFunctions.EnumToString(ID);
        }


    }
}
