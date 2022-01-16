using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Models.ViewModel
{
    public class CartViewModel
    {
        public List<CartItemcs> CartItemcs { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
