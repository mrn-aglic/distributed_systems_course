namespace Vj05Web
{
    class WebPage
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Text { get; private set; }

        public WebPage(int id, string title, string text)
        {
            Id = id;
            Title = title;
            Text = text;
        }

        public override string ToString()
        {
            return "[Stranica] id: " + Id + "\tNaslov: " + Title + "\tTekst: " + Text;
        }
    }
}
