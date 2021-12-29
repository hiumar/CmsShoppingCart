using CmsShoppingCart.Infrastructur;
using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
       { 
        private readonly CMSShoppingCartContext _context;
        public CategoriesController(CMSShoppingCartContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.categories.OrderBy(a=>a.Sorting).ToListAsync());
        }
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categories categories)
        {
            if (ModelState.IsValid)
            {
                categories.Slug = categories.Name.ToLower().Replace(" ", "-");
                categories.Sorting = 100;
                var slug = await _context.categories.FirstOrDefaultAsync(a => a.Slug == categories.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "This page is already exists");
                    return View(categories);
                }
                _context.categories.Add(categories);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Page has been added";
                return RedirectToAction("Index");
            }
            return View(categories);
        }

        
        
        public async Task<IActionResult> Edit(int id)
        {
            Categories category = await _context.categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Categories categories)
        {
            if (ModelState.IsValid)
            {
                categories.Slug = categories.Name.ToLower().Replace(" ", "-");
                categories.Sorting = 100;
                var slug = _context.categories.Where(a => a.Id !=categories.Id).FirstOrDefaultAsync(a => a.Slug == categories.Slug);
                if (slug.Result != null)
                {
                    ModelState.AddModelError("", "This page is already exists");
                    return View(categories);
                }
                _context.Update(categories);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Page has been edited";
                return RedirectToAction("Edit",new  {categories.Id });
            }
            return View(categories);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Categories categories = await _context.categories.FindAsync(id);
            if (categories == null)
            {
                TempData["Error"] = "This page does not exists";
            }
            else
            {
                _context.categories.Remove(categories);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Page has been removed";
            }


            return RedirectToAction("Index");
        }
    }
}
