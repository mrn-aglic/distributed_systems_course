namespace ChangRobertsAlgorithm
{
    public class Init
    {
    }

    public class Election
    {
        public int Id { get; private set; }

        public Election(int id)
        {
            Id = id;
        }
    }

    public class Elected
    {
        public int LeaderId { get; private set; }

        public Elected(int id)
        {
            LeaderId = id;
        }
    }
}
