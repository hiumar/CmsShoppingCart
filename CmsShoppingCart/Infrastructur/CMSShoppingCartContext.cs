using CmsShoppingCart.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Infrastructur
{
    public class CMSShoppingCartContext:DbContext
    {
        public CMSShoppingCartContext(DbContextOptions<CMSShoppingCartContext> options):base(options)
        {

        }

        public CMSShoppingCartContext(Func<object> getRequiredService)
        {
        }

        public DbSet<Page> pages { get; set; }
        public DbSet<Categories> categories { get; set; }
    }
}
