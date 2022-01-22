using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Infrastructur
{
    public class SmallCartViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<CartItemcs> cartItems = HttpContext.Session.Getjson<List<CartItemcs>>("cart");
            SmallCartViewModel cartViewModel;

            if(cartItems.Count==0 || cartItems == null)
            {
                cartViewModel = null;
            }
            else
            {
                cartViewModel = new SmallCartViewModel()
                {
                    TotalAmount = cartItems.Sum(a => a.Price * a.Quantity),
                    NumberOfItems = cartItems.Sum(a => a.Quantity)
                };
            }
            return View(cartViewModel);
        }
    }
}
