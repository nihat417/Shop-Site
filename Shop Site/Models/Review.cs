namespace Shop_Site.Models
{
    public class Review
    {
        public string AuthorName { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int Rating { get; set; }
    }
}
