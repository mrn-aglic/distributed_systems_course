namespace AtLeastOnce
{
    class Post
    {
        public int Id { get; private set; }
        public string Content { get; private set; }
        public int Key { get; private set; }

        public Post(int id, string content, int key)
        {
            Id = id;
            Content = content;
            Key = key;
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
        public int Key { get; private set; }
        public Post Post { get; private set; }

        public SavePost(int key, Post post)
        {
            Key = key;
            Post = post;
        }
    }

    class PostSaved
    {
        public int Key { get; private set; }

        public PostSaved(int key)
        {
            Key = key;
        }
    }

    // klasa za slanje posta nekome 
    class Send { }

    class RetrySaveAll { }
}
