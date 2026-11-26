using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class CartVM
    {
        public int CartId { get; set; }
        public List<CartItemtVM> CartItems { get; set; }

        public decimal TotalPrice()
        {
            return CartItems.Sum(x => x.Price * x.Quantity);
        }
    }

    public class CartItemtVM
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
    }
}
