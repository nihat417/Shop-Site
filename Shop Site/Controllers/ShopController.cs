using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Site.Data;
using Shop_Site.Helpers;

namespace Shop_Site.Controllers
{
    [Authorize]
	public class ShopController : Controller
    {
        private readonly AppDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ShopController(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult>FavoriteProduct()
        {
            var product = await context.Products.Where(product => product.IsFavorite).ToListAsync();
            return PartialView(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite(string Id)
        {
            var product = await context.Products.FindAsync(Id);
            if (product != null)
            {
                product.IsFavorite = !product.IsFavorite;
                await context.SaveChangesAsync();
                var refererUrl = HttpContext.Request.Headers["Referer"].ToString();
                return Redirect(refererUrl);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Info(string id)
        {
            var products=context.Products.Find(id);
            if (products == null)
                return NotFound();
            return View(products);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = context.Products.Take(12).ToList();
            return View(products);
        }

        public IActionResult LoadMoreProducts(int skip)
        {
            var products = context.Products.Skip(skip).Take(12).ToList();
            return PartialView("LoadedProducts", products);
        }

        [HttpPost]
        public IActionResult AddToCart(string id, int quantity = 1)
        {
            var product = context.Products.Find(id);
            if (product != null)
            {
                httpContextAccessor.HttpContext.Session.AddToCart(product, quantity);
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        public IActionResult Cart()
        {
            var cart = httpContextAccessor.HttpContext.Session.GetCart();
            return PartialView(cart);
        }

        public IActionResult RemoveFromCart(string id)
        {
            httpContextAccessor.HttpContext.Session.RemoveFromCart(id);
            return RedirectToAction("Cart");
        }
    }
}
