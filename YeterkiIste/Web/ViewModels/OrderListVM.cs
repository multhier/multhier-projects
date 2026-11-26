using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class OrderListVM
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; } //Sifariş no
        public DateTime AddedTime { get; set; }
        public EnumOrderState OrderState { get; set; } //sifariş durumu çin ayrı bir enum metdu oluşturalım
        public EnumPaymentTypes PaymentTypes { get; set; }
        public string FirstName { get; set; } //Kullanıcı sifarişi başkasının adına verebilir
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; } //Identityden alınabiir ama farklı bir email vermek isteyebilir üye
        public string Note { get; set; }
        public List <OrderItemVM> OrderItemVM { get; set; }
        public decimal TotalPrice()
        {
            return OrderItemVM.Sum(x => x.Price * x.Quantity);
        }
    }

    public class OrderItemVM
    {
        public int OrderItemId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string  ImageUrl { get; set; }
        public int Quantity { get; set; }
    }




}






