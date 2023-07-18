using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop_Site.Data;
using Shop_Site.Helpers;
using Shop_Site.Models;
using Shop_Site.Models.ViewModel;

namespace Shop_Site.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext context;

        public ShopController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View(context.Products.ToList());
        }

        public IActionResult AddProduct()
        {
            ViewBag.Categories = new SelectList(context.Categories, "Id", "Name");
            ViewBag.Brands = new SelectList(context.Brands, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel vm)
        {
            try
            {
                string path= await UploadFileHelper.UploadFile(vm.ImageUrl);
                Products product = new()
                {
                    ImageUrl = path,
                    Title = vm.Title,
                    Description = vm.Description,
                    Price = vm.Price,
                    CategoryId = vm.CategoryId,
                    BrandId = vm.BrandId,
                };
                context.Add(product);
                await context.SaveChangesAsync();
                return View();
            }
            catch (Exception)
            {
                return View("error.cshtml");
            }
        }

    }
}
