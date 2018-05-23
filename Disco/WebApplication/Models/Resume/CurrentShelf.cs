namespace WebApplication.Models.Resume
{
    public class CurrentShelf: Shelf
    {
        internal const string GoodReadsName = "currently-reading";

        public override string Name { get; } = "Currently Reading";

    }
}
