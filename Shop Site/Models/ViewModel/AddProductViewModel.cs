namespace Shop_Site.Models.ViewModel
{
    public class AddProductViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
    }
}
