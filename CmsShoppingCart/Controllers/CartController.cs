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

        public IActionResult Descreas(int id)
        {
          
            List<CartItemcs> cart = HttpContext.Session.Getjson<List<CartItemcs>>("cart") ?? new List<CartItemcs>();
            CartItemcs cartItems = cart.Where(a => a.ProductId == id).FirstOrDefault();
            if (cartItems.Quantity>1)
            {
                --cartItems.Quantity;
            }
            else
            {
                cart.RemoveAll(x => x.ProductId == id);
            }
            
            if (cart.Count == 0) {
                HttpContext.Session.Remove("cart");
            }
            else
            {
                HttpContext.Session.SetJson("cart", cart);
            }
            return Redirect("/Cart/Index");
        }

        public IActionResult Remove(int id)
        {

            List<CartItemcs> cart = HttpContext.Session.Getjson<List<CartItemcs>>("cart") ?? new List<CartItemcs>();
            cart.RemoveAll(x => x.ProductId == id);
          

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("cart");
            }
            else
            {
                HttpContext.Session.SetJson("cart", cart);
            }
            return Redirect("/Cart/Index");
        }

        public IActionResult Clear(int id)
        {

           


          
                HttpContext.Session.Remove("cart");
   
            
         
            return Redirect("/Cart/Index");
        }
    }
}
