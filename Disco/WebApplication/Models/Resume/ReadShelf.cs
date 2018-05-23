namespace WebApplication.Models.Resume
{
    public class ReadShelf: Shelf
    {
        internal const string GoodReadsName = "read";

        public override string Name { get; } = "Recently Read";
    }
}
