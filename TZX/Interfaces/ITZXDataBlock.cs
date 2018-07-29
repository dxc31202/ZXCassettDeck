namespace ZXCassetteDeck
{
    public interface ITZXDataBlock : ITZXBlock
    {
        int BlockLength { get; }
        int PulseLength { get; }
        int PulseToneLength { get; }
        int Sync1Length { get; }
        int Sync2Length { get; }
        int ZeroLength { get; }
        int OneLength { get; }
        int PauseLength { get; }
        int UsedBits { get; }
        int Progress { get; set; }
        int this[int index] { get; }
        byte? First();
        byte? Next();

        ITAPBlock TAPBlock { get; }

    }
}
