using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Site.Data;
using Shop_Site.Helpers;
using Shop_Site.Models;

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

        [HttpPost]
        public async Task<IActionResult> RemoveFavorite(string Id)
        {
            var product= await context.Products.FindAsync(Id);
            if (product != null)
            {
                product.IsFavorite = false ;
                await context.SaveChangesAsync();
                return Ok(true);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Info(string id)
        {
            var products=context.Products.Find(id);
            if (products == null)
                return NotFound();
            var reviews = context.PurchasedProducts
                .Include(p => p.User)
                .Where(p => p.ProductId == id && p.ReviewContent != null)
                .ToList();

            ViewBag.Product = products;
            ViewBag.Reviews = reviews;

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await context.Products.Take(12).ToListAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> LoadMoreProducts(int skip, int take) 
        {
            var products = await context.Products.Skip(skip).Take(take).ToListAsync();
            return PartialView("LoadedProducts", products);
        }

        [HttpPost]
        public IActionResult AddToCart(string id, int quantity = 1)
        {
            var product = context.Products.Find(id);
            if (product != null)
            {
                httpContextAccessor.HttpContext!.Session.AddToCart(product, quantity);
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult RemoveFromCart(string id)
        {
            httpContextAccessor.HttpContext!.Session?.RemoveFromCart(id);
            var cart = httpContextAccessor.HttpContext.Session?.GetCart();
            var totalPrice = CartItem.CalculateTotalPrice(cart);
            return PartialView("Cart", cart);
        }

        public IActionResult Cart()
        {
            var cart = httpContextAccessor.HttpContext!.Session.GetCart();
            return PartialView(cart);
        }

        public IActionResult BuyedProducts()
        {
            var user = context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            if (user != null)
            {
                var purchasedProducts = context.PurchasedProducts
                    .Where(p => p.User.Id == user.Id)
                    .ToList();

                return PartialView(purchasedProducts);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult BuyProduct(Dictionary<string, int> productCounts)
        {

            foreach (var productId in productCounts.Keys)
            {
                var product = context.Products.Find(productId);
                int productCount = productCounts[productId];
                if (product != null && product.StockQuantity >= productCount)
                {
                    var user = context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
                    if (user != null)
                    {
                        var purchasedProduct = new PurchasedProduct
                        {
                            ProductId = product.Id,
                            ProductCount = productCount,
                            ProductName = product.Title,
                            User = user
                        };
                        context.PurchasedProducts.Add(purchasedProduct);

                        product.StockQuantity -= productCount;

                        context.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult BuyProductFast(string id)
        {
            var product=context.Products.Find(id);
            var refererUrl = HttpContext.Request.Headers["Referer"].ToString();
            if(product != null && product.StockQuantity > 0)
            {
                var user = context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
                if (user != null)
                {
                    var purchasedProduct = new PurchasedProduct()
                    {
                        ProductId = product.Id,
                        ProductCount = 1,
                        ProductName = product.Title,
                        User = user
                    };
                    product.StockQuantity -= 1;
                    context.PurchasedProducts.Add(purchasedProduct);
                    context.SaveChanges();
                    return Redirect(refererUrl);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult WriteReview(string productId, string reviewContent, int rating)
        {
            var purchasedProduct = context.PurchasedProducts.Find(productId);

            if (purchasedProduct != null && purchasedProduct.ReviewContent == null)
            {
                purchasedProduct.ReviewContent = reviewContent;
                purchasedProduct.Rating = rating;
                context.SaveChanges();

                var product = context.Products.Find(purchasedProduct.ProductId);
                if (product != null)
                {
                    var reviews = context.PurchasedProducts.Where(p => p.ProductId == productId && p.ReviewContent != null).ToList();

                    product.TotalReviews = reviews.Count;
                    if(reviews.Any())
                    {
                        product.TotalRating = reviews.Average(r => r.Rating);
                    }
                    else { product.TotalRating = rating; }

                    context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SearchProducts(string searchTerm)
        {
            var products =await context.Products.ToListAsync();
            var searchResults = products.FindAll(product => product.Title.Contains(searchTerm));
            return PartialView("SearchProducts" , searchResults);
        }

    }
}
