namespace Shop_Site.Models
{
    public class Category:BaseEntity
    {
        public string? Name { get; set; }
        public virtual IEnumerable<Products> ?Product { get; set; }
    }
}