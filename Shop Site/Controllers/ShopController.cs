using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop_Site.Data;
using Shop_Site.Data.Repositories;
using Shop_Site.Helpers;
using Shop_Site.Models;
using Shop_Site.Models.ViewModel;
using System.Collections.Generic;

namespace Shop_Site.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext context;
        private readonly DbSet<Products> dbset;


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
            if(ModelState.IsValid)
            {
                try
                {
                    string path = await UploadFileHelper.UploadFile(vm.ImageUrl);
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
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return View("error.cshtml");
                }
            }
            return RedirectToAction("AddProduct");
        }

    }
}
