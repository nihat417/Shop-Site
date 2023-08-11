using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Site.Data;

namespace Shop_Site.Controllers
{
    [Authorize]
	public class ShopController : Controller
    {
        private readonly AppDbContext context;

        public ShopController(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult>FavoriteProduct()
        {
            var product = await context.Products.Where(product => product.IsFavorite).ToListAsync();
            return PartialView(product);
        }

        [HttpGet]
        public IActionResult Info(string id)
        {
            var products=context.Products.Find(id);
            if (products == null)
                return NotFound();
            return View(products);
        }

        public IActionResult Index()
        {
            return View(context.Products.ToList());
        }

        [HttpPost]
        public async Task<IActionResult>AddFavorite(string Id)
        {
            var product =await context.Products.FindAsync(Id);
            if (product != null)
            {
                product.IsFavorite = !product.IsFavorite;
                await context.SaveChangesAsync();
                var refererUrl = HttpContext.Request.Headers["Referer"].ToString();
                return Redirect(refererUrl);
            }
            return NotFound();
        }
    }
}
