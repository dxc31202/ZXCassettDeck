namespace ZXCassetteDeck
{
    public interface ITZXBlock
    {
        TZXBlockType ID { get; }
        int Index { get; set; }

        string Details { get; }

    }
}
