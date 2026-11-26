using Core.Abstract;
using Model;

namespace Bll.Abstract.Repositories
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        void InitializeCart(string userId);
        Cart GetCartByUserId(string userId);
        void AddToCart(string userId, int productId, int quantity);
        void DeleteFromCart(int cartId, int productId);
        void ClearitemsFromCart(int cartId);
    }
}
