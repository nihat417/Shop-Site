﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound(); 
                }

                context.Products.Remove(product);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error"); 
            }
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
            if (ModelState.IsValid)
            {
                try
                {
                    string path = (vm.ImageUrl != null) ? await UploadFileHelper.UploadFile(vm.ImageUrl) : "";
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
            ViewBag.Categories = new SelectList(context.Categories, "Id", "Name");
            ViewBag.Brands = new SelectList(context.Brands, "Id", "Name");
            return View(vm);
        }

    }
}
