using CmsShoppingCart.Infrastructur;
using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Areas.Admin.Controllers
{

        [Area("Admin")]
        public class ProductController : Controller
        {
        private readonly CMSShoppingCartContext _context;
        private readonly IWebHostEnvironment _webHostingh;
        public ProductController(CMSShoppingCartContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
           _webHostingh = webHostEnvironment;
        }
        public async Task<IActionResult> Index(int p=1)
        {
            int pageSize = 5;
            var Product = _context.products.OrderByDescending(u => u.Id).Include(u => u.Categories).Skip((p - 1) * pageSize).Take(pageSize);
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((Decimal)_context.products.Count() / pageSize);

            return View(await Product.ToListAsync());
        }

        public IActionResult Create()
        {
            //List<Categories> categories = _context.categories.OrderBy(a => a.Sorting).ToList();
            //categories.Insert(0, new Categories { Id = 0, Name = "Select a Category" });

            ViewBag.CategoryId = new SelectList(_context.categories.OrderBy(a => a.Sorting), "Id", "Name"); ;
          
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            ViewBag.CategoryId = new SelectList(_context.categories.OrderBy(a => a.Sorting), "Id", "Name");
            if (ModelState.IsValid)
            {
                product.Slug = product.Name.ToLower().Replace(" ", "-");
                var slug = await _context.products.FirstOrDefaultAsync(a => a.Slug == product.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "This page is already exists");
                    return View(product);
                }
                string imageName = "NoImage.png";
                if (product.ImageUpload != null)
                {
                    string Updir = Path.Combine(_webHostingh.WebRootPath, "Media/Products");
                    imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                    string FilePath = Path.Combine(Updir, imageName);
                    FileStream fs = new FileStream(FilePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                }
                product.Image = imageName;
                _context.products.Add(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Product has been added";
                return RedirectToAction("Index");
            }
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            ViewBag.CategoryId = new SelectList(_context.categories.OrderBy(a => a.Sorting), "Id", "Name");
            if (ModelState.IsValid)
            {
                product.Slug = product.Name.ToLower().Replace(" ", "-");
                var slug = await _context.products.Where(u=>u.Id!=product.Id).FirstOrDefaultAsync(a => a.Slug == product.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "This page is already exists");
                    return View(product);
                }
                
                if (product.ImageUpload != null)
                {
                    string Updir = Path.Combine(_webHostingh.WebRootPath, "Media/Products");
                    if (!String.Equals(product.Image, "NoImage.png"))
                    {
                        string OldImagePath = Path.Combine(Updir, product.Image);
                        if (System.IO.File.Exists(OldImagePath))
                        {
                            System.IO.File.Delete(OldImagePath);
                        }
                    }
                   
                    string   imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                    string FilePath = Path.Combine(Updir, imageName);
                    FileStream fs = new FileStream(FilePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                    product.Image = imageName;
                }
               
                _context.products.Update(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Product has been edited";
                return RedirectToAction("Index");
            }
            return View(product);
        }


        public async Task<IActionResult> Edit(int id)
        {
            Product product = await _context.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.CategoryId = new SelectList(_context.categories.OrderBy(a => a.Sorting), "Id", "Name");
            return View(product);
        }
        public async Task<IActionResult> Details(int id)
        {
            Product product = await _context.products.Include(a => a.Categories).FirstOrDefaultAsync(a => a.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Product product = await _context.products.FindAsync(id);
            if (product == null)
            {
                TempData["Error"] = "This page does not exists";
            }
            else
            {
                _context.products.Remove(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Product has been removed";
            }


            return RedirectToAction("Index");
        }


    }
}
