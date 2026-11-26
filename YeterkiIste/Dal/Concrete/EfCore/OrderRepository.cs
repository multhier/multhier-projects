using Core.Concrete;
using Dal.Abstract;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dal.Concrete.EfCore
{
   public  class OrderRepository : BaseRepository<Order, AppDbContext>, IOrderRepository
    {
        public OrderRepository(AppDbContext prm) : base(prm)
        {
        }

        public List<Order> GetOrders(string userId)
        {
            var orders = context.Order.Include(x => x.OrderItems).ThenInclude(x => x.Product).AsQueryable();

            if (!string.IsNullOrEmpty(userId))
            {
                orders = orders.Where(i => i.UserId == userId);
            }
            return orders.ToList();

            //Order alırken yanında OrderItems larıda alacağız.
            //OrderItem dan ilişkili olan Product laride alacağız.
            //Fiyt bilisini product tan değil, sifariş verdiğimiz Orderdan almalıyız.
            //ThenInclude anlamı = en son yaptığın Include tablosunun içinden tablyu alıyoruz demektir.
            //Include OrderItems, ThenInclude (OrderItemsın içindeki tablo alınacak) Product.

            //En sonunda ToList() dersek sorgu oluşur ve databaseden bilgiler alınır.
             //ama biz henüz isteimyoruz çünkü UserId parametre gelme ihtimali var.
            //UserId gelmişse sorgunun üzerine where koşulu ekleyecğiz.O yüzden sorgu bitmemeli, AsQueryable diye devam etmeli.
        }
    }
}
