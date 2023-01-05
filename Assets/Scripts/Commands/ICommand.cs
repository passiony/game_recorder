namespace Record
{

    public enum ECommandReply : byte
    {
        Y,
        N,
    }

    public class ICommand
    {
        public int frame;
        public int owner;
        public ECommand type { get; set; }
    }
}