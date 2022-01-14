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
    public class PagesController : Controller
    {
        private readonly CMSShoppingCartContext _context;
        public PagesController(CMSShoppingCartContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Page(string slug)
        {
            if (slug == null)
            {
                return View(await _context.pages.Where(a => a.Slug =="home").FirstOrDefaultAsync());
            }
            Page page = await _context.pages.Where(a => a.Slug == slug).FirstOrDefaultAsync();

            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }
    }
}
