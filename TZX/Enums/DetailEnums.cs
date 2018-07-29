public enum TZXHardwareTypes
{
    Computers,
    ExternalStorage,
    ROMRAMTypeAddOns,
    SoundDevices,
    Joysticks,
    Mice,
    OtherControllers,
    SerialPorts,
    ParallelPorts,
    Printers,
    Modems,
    Digitizers,
    NetworkAdapters,
    KeyboardsAndKeypads,
    ADDAConverters,
    EPROMProgrammers,
    Graphics,

}
public enum TZXSignalLevel
{
    Low,
    High,
}
public enum TZXCompressionType
{
    RLE = 0x01,
    ZRLE = 0x02,
}
public enum TZXArchiveInfo
{
    FullTitle = 0x00,
    SoftwareHousePublisher = 0x01,
    Authors = 0x02,
    YearOfPublication = 0x03,
    Language = 0x04,
    GameUtilityType = 0x05,
    Price = 0x06,
    ProtectionSchemeLoader = 0x07,
    Origin = 0x08,
    Comments = 0xFF,
}
public enum TZXComputers
{
    ZXSpectrum16k = 0x00,
    ZXSpectrum48k, Plus = 0x01,
    ZXSpectrum48kISSUE1 = 0x02,
    ZXSpectrum128kPlusSinclair = 0x03,
    ZXSpectrum128kPlus2GreyCase = 0x04,
    ZXSpectrum128kPlus2APlus3 = 0x05,
    TimexSinclairTC2048 = 0x06,
    TimexSinclairTS2068 = 0x07,
    Pentagon128 = 0x08,
    SamCoupe = 0x09,
    DidaktikM = 0x0A,
    DidaktikGama = 0x0B,
    ZX80 = 0x0C,
    ZX81 = 0x0D,
    ZXSpectrum128kSpanishVersion = 0x0E,
    ZXSpectrumArabicVersion = 0x0F,
    MicrodigitalTK9X = 0x10,
    MicrodigitalTK95 = 0x11,
    Byte = 0x12,
    Elwro8003 = 0x13,
    ZSScorpion256 = 0x14,
    AmstradCPC464 = 0x15,
    AmstradCPC664 = 0x16,
    AmstradCPC6128 = 0x17,
    AmstradCPC464Plus = 0x18,
    AmstradCPC6128Plus = 0x19,
    JupiterACE = 0x1A,
    Enterprise = 0x1B,
    Commodore64 = 0x1C,
    Commodore128 = 0x1D,
    InvesSpectrumplus = 0x1E,
    Profi = 0x1F,
    Grandrommax = 0x20,
    Kay1024 = 0x21,
    IceFelixHC91 = 0x22,
    IceFelixHC2000 = 0x23,
    AmaterskeRADIOMistrum = 0x24,
    Quorum128 = 0x25,
    MicroartATM = 0x26,
    MicroartATMTurbo2 = 0x27,
    Chrome = 0x28,
    ZXBadaloc = 0x29,
    TS1500 = 0x2A,
    Lambda = 0x2B,
    TK65 = 0x2C,
    ZX97 = 0x2D,
}

public enum TZXExternalStorage
{
    ZxMicrodrive = 0x00,
    OpusDiscovery = 0x01,
    MgtDisciple = 0x02,
    MgtPlusD = 0x03,
    RotronicsWafadrive = 0x04,
    TRDOSBetadisk = 0x05,
    ByteDrive = 0x06,
    Watsford = 0x07,
    Fiz = 0x08,
    Radofin = 0x09,
    DidaktikDiskDrives = 0x0A,
    BsDosMb02 = 0x0B,
    ZXSpectrumPlus3DiskDrive = 0x0C,
    JLOOligerDiskInterface = 0x0D,
    TimexFdd3000 = 0x0E,
    ZebraDiskDrive = 0x0F,
    RamexMillenia = 0x10,
    Larken = 0x11,
    KempstonDiskInterface = 0x12,
    Sandy = 0x13,
    ZXSpectrumPlus3eHardDisk = 0x14,
    Zxatasp = 0x15,
    Divide = 0x16,
    Zxcf = 0x17,
}

public enum TZXROMRAMTypeAddOns
{
    SamRam = 0x00,
    MultifaceOne = 0x01,
    Multiface128k = 0x02,
    MultifacePlus3 = 0x03,
    Multiprint = 0x04,
    MB02ROMRAMExpansion = 0x05,
    Softrom = 0x06,
    _1k = 0x07,
    _16k = 0x08,
    _48k = 0x09,
    MemoryIn816kUsed = 0x0A,
}

public enum TZXSoundDevices
{
    ClassicAYHardware_CompatibleWith128kZxs = 0x00,
    FullerBoxAYSoundHardware = 0x01,
    CurrahMicrospeech = 0x02,
    Specdrum = 0x03,
    AYACBStereo_Aplusc_Equals_Left_Bplusc_Equals_Right_Melodik = 0x04,
    AYABCStereo_Aplusb_Equals_Left_Bplusc_Equals_Right_ = 0x05,
    RAMMusicMachine = 0x06,
    Covox = 0x07,
    GeneralSound = 0x08,
    IntecElectronicsDigitalInterfaceB8001 = 0x09,
    ZonxAY = 0x0A,
    QuicksilvaAY = 0x0B,
    JupiterACE = 0x0C,
}

public enum TZXJoysticks
{
    Kempston = 0x00,
    CursorProtekAGF = 0x01,
    Sinclair2Left12345 = 0x02,
    Sinclair1Right67890 = 0x03,
    Fuller = 0x04,
}

public enum TZXMice
{
    AMXMouse,
    KempstonMouse,
}

public enum TZXOtherControllers
{
    Trickstick,
    ZXLightGun,
    ZebraGraphicsTablet,
    DefenderLightGun,
}

public enum TZXSerialPorts
{
    ZXInterface1,
    ZXSpectrum128k,
}

public enum TZXParallelPorts
{
    KempstonS = 0x00,
    KempstonE = 0x01,
    ZxSpectrumPlus3 = 0x02,
    Tasman = 0x03,
    Dktronics = 0x04,
    Hilderbay = 0x05,
    InesPrinterface = 0x06,
    ZXLprintInterface3 = 0x07,
    Multiprint = 0x08,
    OpusDiscovery = 0x09,
    Standard8255ChipWithPorts316395 = 0x0A,
}

public enum TZXPrinters
{
    ZXPrinter_Alphacom_32AndCompatibles,
    GenericPrinter,
    EPSONCompatible,
}

public enum TZXModems
{
    PrismVTX5000,
    TS2050OrWestridge2050,
}

public enum TZXDigitizers
{
    RDDigitalTracer,
    DKTronicsLightPen,
    BritishMicroGraphPad,
    RomanticRobotVideoface,
}
public enum TZXNetworkAdapters
{
    ZXInterface1
}
public enum TZXKeyboardsAndKeypads
{
    KeypadForZXSpectrum128k
}
public enum TZXADDAConverters
{
    HarleySystemsADC82,
    BlackboardElectronics,
}
public enum TZXEPROMProgrammers
{
    OrmeElectronics,
}
public enum TZXGraphics
{
    WRXHiRes,
    G007,
    Memotech,
    LambdaColour,
}