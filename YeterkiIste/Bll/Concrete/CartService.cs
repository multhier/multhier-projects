using Bll.Abstract;
using Bll.Abstract.Repositories;
using Model; 

namespace Bll.Concrete
{
    public class CartService : ICartService
    {
        ICartRepository repo;
        public CartService(ICartRepository prm)
        {
            repo = prm;
        }

        public Cart GetCartByUserId(string userId)
        {
            return repo.GetCartByUserId(userId);
        }
        public void InitializeCart(string userId)
        {
            repo.InitializeCart(userId);
        }
        public void AddToCart(string userId, int productId, int quantity)
        {
            repo.AddToCart(userId, productId, quantity);
        }

        public void DeleteFromCart(string userId, int productId)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null)
            {
                repo.DeleteFromCart(cart.Id, productId);
            }
        }

        public void ClearitemsFromCart(int cartId)
        {
            repo.ClearitemsFromCart(cartId);
        }
    }
}
