﻿using Microsoft.AspNetCore.Mvc;
using Shop_Site.Data;

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
    }
}
