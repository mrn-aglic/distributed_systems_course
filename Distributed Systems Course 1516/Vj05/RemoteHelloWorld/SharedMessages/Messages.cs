namespace SharedMessages
{
    public class Answer
    {
        public string Text { get; private set; }

        public Answer(string text)
        {
            Text = text;
        }
    }

    public class Query
    {
        public string Text { get; private set; }

        public Query(string text)
        {
            Text = text;
        }
    }

    // Messages for client
    public class Send
    {
    }
}
