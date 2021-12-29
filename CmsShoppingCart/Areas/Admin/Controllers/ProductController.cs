using CmsShoppingCart.Infrastructur;
using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Areas.Admin.Controllers
{

        [Area("Admin")]
        public class ProductController : Controller
        {
        private readonly CMSShoppingCartContext _context;
        public ProductController(CMSShoppingCartContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            

            return View(await _context.products.OrderByDescending(u => u.Id).Include(u => u.Categories).ToListAsync());
        }

        public IActionResult Create()
        {
            List<Categories> categories = _context.categories.OrderBy(a => a.Sorting).ToList();
            categories.Insert(0, new Categories { Id = 0, Name = "Select a Category" });

            ViewBag.CategoryId = categories;
          
            return View();
        }

       
    }
}
