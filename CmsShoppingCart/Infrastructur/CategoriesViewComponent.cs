using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Infrastructur
{
    public class CategoriesViewComponent:ViewComponent
    {
        private readonly CMSShoppingCartContext _context;

        public CategoriesViewComponent(CMSShoppingCartContext context)
        {
            _context = context;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await GetPageAsync();
            return View(categories);
        }

        private Task<List<Categories>> GetPageAsync()
        {
            return _context.categories.OrderBy(a => a.Sorting).ToListAsync();
        }
    }
}
