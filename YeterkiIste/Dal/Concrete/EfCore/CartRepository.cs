using Core.Concrete;
using Dal.Abstract;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Dal.Concrete.EfCore
{
    public class CartRepository : BaseRepository<Cart, AppDbContext>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context)
        {

        }

        public void InitializeCart(string userId)
        {
            context.Add(new Cart() { UserId = userId });
        }
        public Cart GetCartByUserId(string userId)
        {
            return context.Cart.Include(x => x.CartItems).ThenInclude(x => x.Product).FirstOrDefault(i => i.UserId == userId);
        }

        public void AddToCart(string userId, int productId, int quantity)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null)
            {
                var index = cart.CartItems.FindIndex(x => x.ProductId == productId);
                if (index < 0)  //yeni eklenir
                {
                    cart.CartItems.Add(new CartItem() { ProductId = productId, Quantity = quantity, CartId = cart.Id });
                }
                else  //var zaten sadece quantity artır
                {
                    cart.CartItems[index].Quantity  += quantity;
                }
                Update(cart);
            }
        }
        public override Cart Update(Cart prm)
        {
            context.Cart.Update(prm);
            context.SaveChanges();
            return prm;
        }

        public void DeleteFromCart(int cartId, int productId)
        {
            
              var query  = @"delete from CartItem where CartId=@p0 and ProductId=@p1 ";
             context.Database.ExecuteSqlRaw(query, cartId, productId);
            //ExecuteSqlCommand
        }

        public void ClearitemsFromCart(int cartId)
        {
            var query = @"delete from CartItem where CartId=@p0";
            context.Database.ExecuteSqlRaw(query, cartId);
        }
    }
}

