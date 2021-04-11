namespace ProjectA
{
    public class Dashboard
    {
        public string Title { get; internal set; }

        internal void SetTitle(string title)
        {
            Title = title;
        }
    }
}
