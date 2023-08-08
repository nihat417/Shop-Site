using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_Site.Data;
using Shop_Site.Models;

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
    }
}
