using CmsShoppingCart.Infrastructur;
using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Controllers
{
    public class ProductController : Controller
    {
      
            private readonly CMSShoppingCartContext _context;
        
            public ProductController(CMSShoppingCartContext context)
            {
                _context = context;
          
            }
        public async Task<IActionResult> Index(int p = 1)
        {
            int pageSize = 5;
            var Product = _context.products.OrderByDescending(u => u.Id).Skip((p - 1) * pageSize).Take(pageSize);
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((Decimal)_context.products.Count() / pageSize);

            return View(await Product.ToListAsync());
        }

            public async Task<IActionResult> GetProductByCategory(string categorySlug,int p = 1)
            {
                int pageSize = 5;
            Categories categories = _context.categories.Where(a => a.Slug == categorySlug).FirstOrDefault();
            if (categories == null)
                return RedirectToAction("Index");

                var Product = _context.products.OrderByDescending(u => u.Id).Where(a=>a.CategoryId==categories.Id).Skip((p - 1) * pageSize).Take(pageSize);
                ViewBag.PageNumber = p;
                ViewBag.PageRange = pageSize;
                ViewBag.TotalPages = (int)Math.Ceiling((Decimal)_context.products.OrderByDescending(u => u.CategoryId == categories.Id).Count() / pageSize);
               ViewBag.CategoryName = categories.Name;
               ViewBag.Slug = categorySlug;
                return View(await Product.ToListAsync());
            } 

    }
}
