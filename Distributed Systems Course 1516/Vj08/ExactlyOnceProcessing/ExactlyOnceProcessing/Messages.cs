using Akka.Actor;

namespace ExactlyOnceProcessing
{
    class Post
    {
        public int Id { get; private set; }
        public string Content { get; private set; }

        public Post(int id, string content)
        {
            Id = id;
            Content = content;
        }
    }

    class PostAck
    {
        public int Id { get; private set; }

        public PostAck(int id)
        {
            Id = id;
        }
    }

    class SavePost
    {
        public ActorPath From { get; private set; }
        public Post Post { get; private set; }

        public SavePost(ActorPath _from, Post post)
        {
            From = _from;
            Post = post;
        }
    }

    class PostSaved
    {
        public ActorPath From { get; private set; }
        public int Id { get; private set; }

        public PostSaved(ActorPath _from, int id)
        {
            From = _from;
            Id = id;
        }
    }

    // klasa za slanje posta nekome 
    class Send
    {
        public string Text { get; private set; }
        
        public Send(string text)
        {
            Text = text;
        }
    }

    class Retry
    {
    }
}
