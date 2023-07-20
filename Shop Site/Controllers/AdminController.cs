using Microsoft.AspNetCore.Mvc;
using Shop_Site.Data;

namespace Shop_Site.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext context;

        public AdminController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult AdminPage()
        {
            return View(context.Products.ToList());
        }

    }
}
