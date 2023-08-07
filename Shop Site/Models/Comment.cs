namespace Shop_Site.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public int ProductId { get; set; }
        public virtual AppUser User { get; set; }
    }
}
