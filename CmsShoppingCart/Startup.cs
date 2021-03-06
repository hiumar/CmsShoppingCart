using CmsShoppingCart.Infrastructur;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSession(option=> {
               // option.IdleTimeout = TimeSpan.FromMinutes(10);
            });
            services.AddControllersWithViews();
            services.AddRazorPages()
           .AddRazorRuntimeCompilation();

            services.AddDbContext<CMSShoppingCartContext>(option=>option.UseSqlServer(Configuration.GetConnectionString("CMSShoppingCartContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

            endpoints.MapControllerRoute(
                "pages",
                "{slug?}",
                defaults: new {controller="Pages",action="Page"}
                );
                endpoints.MapControllerRoute(
                "product",
                "product/{categorySlug}",
                defaults: new { controller = "Product", action = "GetProductByCategory" }
                );


                endpoints.MapControllerRoute(
                    name: "admin",
                    pattern: "{area:exists}/{controller=Pages}/{action=Index}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Index}/{id?}");
            });
        }
    }
}
