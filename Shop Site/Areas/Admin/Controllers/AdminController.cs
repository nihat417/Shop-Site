using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop_Site.Helpers;
using Shop_Site.Models.ViewModel;
using Shop_Site.Models;
using Microsoft.AspNetCore.Authorization;
using Shop_Site.Repository;

namespace Shop_Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;

        public AdminController(IProductRepository _productRepository, ICategoryRepository _categoryRepository, IBrandRepository _brandRepository)
        {
            this._productRepository = _productRepository;
            this._categoryRepository = _categoryRepository;
            this._brandRepository = _brandRepository;
        }

        public async Task<IActionResult> AdminPage()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }

        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                await _productRepository.DeleteAsync(product);
                return RedirectToAction("AdminPage");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> AddProduct()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var brands = await _brandRepository.GetAllAsync();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.Brands = new SelectList(brands, "Id", "Name");

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
                    var product = new Products
                    {
                        ImageUrl = path,
                        Title = vm.Title,
                        Description = vm.Description,
                        StockQuantity = vm.StockQuantity,
                        Price = vm.Price,
                        CategoryId = vm.CategoryId,
                        BrandId = vm.BrandId,
                    };
                    await _productRepository.AddAsync(product);
                    return RedirectToAction("AdminPage");
                }
                catch (Exception)
                {
                    return View("error.cshtml");
                }
            }

            var categories = await _categoryRepository.GetAllAsync();
            var brands = await _brandRepository.GetAllAsync();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.Brands = new SelectList(brands, "Id", "Name");

            return View(vm);
        }


        public async Task<IActionResult> EditProduct(string Id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(Id);
                if (product == null) { return NotFound(); }

                var categories = await _categoryRepository.GetAllAsync();
                var brands = await _brandRepository.GetAllAsync();

                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                ViewBag.Brands = new SelectList(brands, "Id", "Name");

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
                    var product = await _productRepository.GetByIdAsync(vm.Id);

                    if (product == null)
                    {
                        return NotFound();
                    }

                    product.Title = vm.Title;
                    product.Description = vm.Description;
                    product.Price = vm.Price;
                    product.CategoryId = vm.CategoryId;
                    product.BrandId = vm.BrandId;

                    await _productRepository.UpdateAsync(product);

                    return RedirectToAction("AdminPage");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }

            var categories = await _categoryRepository.GetAllAsync();
            var brands = await _brandRepository.GetAllAsync();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.Brands = new SelectList(brands, "Id", "Name");

            return View(vm);
        }
    }
}
