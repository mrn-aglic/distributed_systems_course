namespace ChangRobertsAlgorithm
{
    public interface IMessage { }

    public class Init 
    {
    }

    public class Election : IMessage
    {
        public int Id { get; private set; }
        public int CurrentPhase { get; private set; }
        public int Hop { get; private set; }

        public Election(int id, int currentPhase, int hop)
        {
            Id = id;
            CurrentPhase = currentPhase;
            Hop = hop;
        }

        public override string ToString()
        {
            return "Id: " + Id + " Phase: " + CurrentPhase + " Hop: " + Hop;
        }
    }

    public class Reply : IMessage
    {
        public int Id { get; private set; }
        public int CurrentPhase { get; private set; }

        public Reply(int id, int currentPhase)
        {
            Id = id;
            CurrentPhase = currentPhase;
        }

        public override string ToString()
        {
            return "Id: " + Id + " Phase: " + CurrentPhase;
        }
    }

    public class Elected : IMessage
    {
        public int Id { get; private set; }

        public Elected(int id)
        {
            Id = id;
        }
    }
}
