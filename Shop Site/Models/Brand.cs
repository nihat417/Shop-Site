namespace Shop_Site.Models
{
    public class Brand:BaseEntity
    {
        public string? Name { get; set; }
        public virtual IEnumerable<Products> ?Product { get; set; }
    }
}