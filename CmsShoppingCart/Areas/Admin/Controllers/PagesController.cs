﻿using CmsShoppingCart.Infrastructur;
using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Areas.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        private readonly CMSShoppingCartContext _context;
        public PagesController(CMSShoppingCartContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IQueryable<Page> pages = from p in _context.pages orderby p.Sorting select p;
           List<Page> pagesList = await pages.ToListAsync();

            return View(pagesList);
        }
        public IActionResult Create() => View();

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.PageTitle.ToLower().Replace(" ", "-");
                page.Sorting = 100;
                var slug = _context.pages.FirstOrDefaultAsync(a => a.Slug == page.Slug);
                if(slug.Result != null)
                {
                    ModelState.AddModelError("", "This page is already exists");
                    return View(page);
                }
                _context.pages.Add(page);
               await _context.SaveChangesAsync();
                TempData["Success"] = "Page has been added";
                return RedirectToAction("Index");
            }
            return View(page);
        }

        public async Task<IActionResult> Details(int id)
        {
            Page page = await _context.pages.FirstOrDefaultAsync(u => u.PageId == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        public async Task<IActionResult> Edit()
        {
            IQueryable<Page> pages = from p in _context.pages orderby p.Sorting select p;
            List<Page> pagesList = await pages.ToListAsync();

            return View(pagesList);
        }
        public async Task<IActionResult> Delete()
        {
            IQueryable<Page> pages = from p in _context.pages orderby p.Sorting select p;
            List<Page> pagesList = await pages.ToListAsync();

            return View(pagesList);
        }
    }
}