using CmsShoppingCart.Infrastructur;
using CmsShoppingCart.Models;
using CmsShoppingCart.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Controllers
{
    public class CartController : Controller
    {
        private readonly CMSShoppingCartContext _context;
        public CartController(CMSShoppingCartContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<CartItemcs> cartItemcs = HttpContext.Session.Getjson<List<CartItemcs>>("cart") ?? new List<CartItemcs>();

            CartViewModel cartView = new CartViewModel();
            cartView.CartItemcs = cartItemcs;
            cartView.GrandTotal = cartItemcs.Sum(a => a.Price * a.Quantity);
            return View(cartView);
        }

        public async Task<IActionResult> Add(int id)
        {
            Product product =await _context.products.FindAsync(id);
            List<CartItemcs> cart = HttpContext.Session.Getjson<List<CartItemcs>>("cart") ?? new List<CartItemcs>();
            CartItemcs cartItems = cart.Where(a => a.ProductId == id).FirstOrDefault();
            if (cartItems == null)
            {
                cart.Add(new CartItemcs(product));
            }
            else
            {
                cartItems.Quantity += 1;
            }
            HttpContext.Session.SetJson("cart", cart);
            return Redirect("/Cart/Index");
        }
    }
}
