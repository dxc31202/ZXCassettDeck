using System;
using System.Collections.Generic;

using System.Text;
using System.IO;

/*

    This blocks contains information about the hardware that the programs on this tape use. 
    Please include only machines and hardware for which you are 100% sure that it either runs (or doesn't run) on or with, 
    or you know it uses (or doesn't use) the hardware or special features of that machine.

    If the tape runs only on the ZX81 (and TS1000, etc.) then it clearly won't work on any Spectrum or Spectrum variant, 
    so there's no need to list this information.

    If you are not sure or you haven't tested a tape on some particular machine/hardware combination then do not include it in the list.

    The list of hardware types and IDs is somewhat large, and may be found at the end of the format description

    ID 33 - Hardware type   length: [00]*03+01 

    Offset      Value       Type        Description 
    0x00            N       BYTE        Number of machines and hardware types for which info is supplied 
    0x01            -       HWINFO[N]   List of machines and hardware 
*/
namespace ZXCassetteDeck
{
    public class HardwareType : ITZXBlock
    {
        public int Index { get; set; }
        public byte NumberOfMachines;
        public List<HWINFO> ListOfMachinesAndHardware = new List<HWINFO>();

        public TZXBlockType ID { get { return TZXBlockType.HardwareType; } }

        public HardwareType(byte[] rawdata, ref int pointer)
        {
            NumberOfMachines = rawdata[pointer++];
            for (int i = 0; i < NumberOfMachines; i++)
            {
                ListOfMachinesAndHardware.Add(new HWINFO(rawdata, ref pointer));
            }

        }
        public string Details
        {
            get
            {
                string info = "";
                info += "Number Of Machines: " + NumberOfMachines.ToString() + Environment.NewLine;

                foreach (HWINFO hw in ListOfMachinesAndHardware)
                {
                    info += hw.ToString() + Environment.NewLine;
                }
                return info;
            }
        }

        public override string ToString()
        {
            return TZXFunctions.EnumToString(ID);
        }

        #region HWINFO
        /*
            HWINFO structure format 

            Offset      Value       Type        Description 
            0x00            -       BYTE        Hardware type 
            0x01            -       BYTE        Hardware ID 
            0x02            -       BYTE        Hardware information:
                                                 00 - The tape RUNS on this machine or with this hardware,
                                                       but may or may not use the hardware or special features of the machine.
                                                 01 - The tape USES the hardware or special features of the machine,
                                                       such as extra memory or a sound chip.
                                                 02 - The tape RUNS but it DOESN'T use the hardware
                                                       or special features of the machine.
                                                 03 - The tape DOESN'T RUN on this machine or with this hardware. 
        */
        public class HWINFO
        {
            public byte HardwareType;
            public byte HardwareID;
            public byte HaHardwareIInformation;
            public HWINFO(byte[] rawdata, ref int pointer)
            {
                HardwareType = rawdata[pointer++];
                HardwareID = rawdata[pointer++];
                HaHardwareIInformation = rawdata[pointer++];
            }


            public override string ToString()
            {
                string value = "{" + ((TZXHardwareTypes)HardwareType).ToString();
                switch ((TZXHardwareTypes)HardwareType)
                {
                    case TZXHardwareTypes.ADDAConverters: value += ", " + (TZXHardwareTypes)HardwareID; break;
                    case TZXHardwareTypes.Computers: value += ", " + (TZXComputers)HardwareID; break;
                    case TZXHardwareTypes.Digitizers: value += ", " + (TZXDigitizers)HardwareID; break;
                    case TZXHardwareTypes.EPROMProgrammers: value += ", " + (TZXEPROMProgrammers)HardwareID; break;
                    case TZXHardwareTypes.ExternalStorage: value += ", " + (TZXExternalStorage)HardwareID; break;
                    case TZXHardwareTypes.Graphics: value += ", " + (TZXGraphics)HardwareID; break;
                    case TZXHardwareTypes.Joysticks: value += ", " + (TZXJoysticks)HardwareID; break;
                    case TZXHardwareTypes.KeyboardsAndKeypads: value += ", " + (TZXKeyboardsAndKeypads)HardwareID; break;
                    case TZXHardwareTypes.Mice: value += ", " + (TZXMice)HardwareID; break;
                    case TZXHardwareTypes.Modems: value += ", " + (TZXModems)HardwareID; break;
                    case TZXHardwareTypes.NetworkAdapters: value += ", " + (TZXNetworkAdapters)HardwareID; break;
                    case TZXHardwareTypes.OtherControllers: value += ", " + (TZXOtherControllers)HardwareID; break;
                    case TZXHardwareTypes.ParallelPorts: value += ", " + (TZXParallelPorts)HardwareID; break;
                    case TZXHardwareTypes.Printers: value += ", " + (TZXPrinters)HardwareID; break;
                    case TZXHardwareTypes.ROMRAMTypeAddOns: value += ", " + (TZXROMRAMTypeAddOns)HardwareID; break;
                    case TZXHardwareTypes.SerialPorts: value += ", " + (TZXSerialPorts)HardwareID; break;
                    case TZXHardwareTypes.SoundDevices: value += ", " + (TZXSoundDevices)HardwareID; break;
                }
                value += "}";
                return value;
            }
        }
        #endregion HWINFO
    }
}
