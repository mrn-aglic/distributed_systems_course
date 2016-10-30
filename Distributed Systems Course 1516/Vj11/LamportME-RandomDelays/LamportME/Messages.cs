namespace LamportME
{
    class Initiate { }

    class Request
    {
        public int TimeStamp { get; private set; }

        public Request(int timeStamp)
        {
            TimeStamp = timeStamp;
        }
    }

    public class Reply
    {
        public int TimeStamp { get; private set; }

        public Reply(int timeStamp)
        {
            TimeStamp = timeStamp;
        }
    }

    public class Release { }
}
