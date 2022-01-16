using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Models
{
    public class CartItemcs
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get { return Quantity * Price; } }
        public string Image { get; set; }
        //if we dont use empty constructor then data is null in product object
        public CartItemcs()
        {

        }

        public CartItemcs(Product product)
        {
            ProductId = product.Id;
            ProductName = product.Name;
            Price = product.Price;
            Quantity = 1;
            Image = product.Image;

        }

    }
}
