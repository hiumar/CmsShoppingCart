using CmsShoppingCart.Infrastructur;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context=new CMSShoppingCartContext(serviceProvider.GetRequiredService<DbContextOptions<CMSShoppingCartContext>>()))
            {
                if (context.pages.Any())
                {
                    return;
                }
                context.pages.AddRange(
                    new Page()
                    {
                        PageTitle="Home",
                        Slug="Home",
                        Content="Home Page",
                        Sorting=0
                    },
                     new Page()
                     {
                         PageTitle = "About",
                         Slug = "About-us",
                         Content = "About US",
                         Sorting = 100
                     },
                     new Page()
                     {
                         PageTitle = "Services",
                         Slug = "services",
                         Content = "service page",
                         Sorting = 100
                     },
                     new Page()
                     {
                         PageTitle = "Contact",
                         Slug = "Contact-us",
                         Content = "Contact us page",
                         Sorting = 100
                     }

                   );
                context.SaveChanges();
            }
        }
    }
}
