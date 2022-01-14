using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Infrastructur
{
    public class MainMenuViewComponent:ViewComponent
    {
        private readonly CMSShoppingCartContext _context;
   
        public MainMenuViewComponent(CMSShoppingCartContext context)
        {
            _context = context;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
           var page = await GetPageAsync();
            return View(page);
        }

        private Task<List<Page>> GetPageAsync()
        {
            return _context.pages.OrderBy(a => a.Sorting).ToListAsync();
        }
    }
}
