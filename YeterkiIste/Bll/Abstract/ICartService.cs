using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bll.Abstract
{
    public interface ICartService
    {
        void InitializeCart(string userId);
        Cart GetCartByUserId(string userId);
        void AddToCart(string userId, int productId, int quantity);
        void DeleteFromCart(string userId, int productId);
        void ClearitemsFromCart(int cartId);
    }
}
