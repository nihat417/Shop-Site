using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop_Site.Data;
using Shop_Site.Helpers;
using Shop_Site.Models.ViewModel;
using Shop_Site.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Shop_Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly AppDbContext context;
		private readonly UserManager<AppUser> userManager;

		public AdminController(AppDbContext context, UserManager<AppUser> userManager)
		{
			this.context = context;
			this.userManager = userManager;
		}

		public async Task<IActionResult> AdminPage([FromQuery(Name = "sortOrder")] string sortOrder)
        {
			ViewData["CurrentSort"] = sortOrder;
			ViewData["TitleSortParam"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
			ViewData["PriceSortParam"] = sortOrder == "Price" ? "price_desc" : "Price";
			ViewData["BrandSortParam"] = sortOrder == "Brand" ? "brand_desc" : "Brand";
			ViewData["CategorySortParam"] = sortOrder == "Category" ? "category_desc" : "Category";

			var products = context.Products.Include(p => p.brand).Include(p => p.category).AsQueryable();

			switch (sortOrder)
			{
				case "title_desc":
					products = products.OrderByDescending(p => p.Title);
					break;
				case "Price":
					products = products.OrderBy(p => p.Price);
					break;
				case "price_desc":
					products = products.OrderByDescending(p => p.Price);
					break;
				case "Brand":
					products = products.OrderBy(p => p.brand.Name);
					break;
				case "brand_desc":
					products = products.OrderByDescending(p => p.brand.Name);
					break;
				case "Category":
					products = products.OrderBy(p => p.category.Name);
					break;
				case "category_desc":
					products = products.OrderByDescending(p => p.category.Name);
					break;
				default:
					products = products.OrderBy(p => p.Title);
					break;
			}

			var productList = await products.ToListAsync();

			var users = await userManager.Users.ToListAsync();
			ViewBag.Users = users;


			return View(productList);
		}

        public async Task<IActionResult> DeleteProduct(string id)
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
                return RedirectToAction("AdminPage");
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
                        StockQuantity = vm.StockQuantity,
                        Price = vm.Price,
                        CategoryId = vm.CategoryId,
                        BrandId = vm.BrandId,
                    };
                    context.Add(product);
                    await context.SaveChangesAsync();
                    return RedirectToAction("AdminPage");
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

        public async Task<IActionResult> EditProduct(string Id)
        {
            try
            {
                var product = await context.Products.FindAsync(Id);
                if (product == null) { return NotFound(); }
                ViewBag.Categories = new SelectList(context.Categories, "Id", "Name");
                ViewBag.Brands = new SelectList(context.Brands, "Id", "Name");

                var ViewModel = new AddProductViewModel
                {
                    Title = product.Title,
                    Description = product.Description,
                    Price = product.Price,
                    BrandId = product.BrandId,
                    CategoryId = product.CategoryId,
                };
                return View(ViewModel);


            }
            catch (Exception)
            {
                return View("error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(AddProductViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var product = await context.Products.FirstOrDefaultAsync(p => p.Id == vm.Id);

                    if (product == null)
                    {
                        return NotFound();
                    }
                    product.Title = vm.Title;
                    product.Description = vm.Description;
                    product.Price = vm.Price;
                    product.CategoryId = vm.CategoryId;
                    product.BrandId = vm.BrandId;

                    await context.SaveChangesAsync();
                    return RedirectToAction("AdminPage");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            ViewBag.Categories = new SelectList(context.Categories, "Id", "Name");
            ViewBag.Brands = new SelectList(context.Brands, "Id", "Name");
            return View(vm);
        }
    }
}