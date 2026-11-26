using Core.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Order : IData
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
        //Aksi halde null referans hatası alırız.
        public List<OrderItem> OrderItems { get; set; }
        public int Id { get; set; }
        public string OrderNumber { get; set; } //Sifariş no
        public DateTime AddedTime { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; } //Kullanıcı sifarişi başkasının adına verebilir
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; } //Identityden alınabiir ama farklı bir email vermek isteyebilir üye
        public string Note { get; set; }
        public string PaymentId { get; set; } //API ile bağlanırsak, karşı siteye Bu Id ile bdetay alalım
        public string PaymentToken { get; set; }
        public string ConversationId { get; set; }
        public EnumOrderState OrderState { get; set; } //sifariş durumu çin ayrı bir enum metdu oluşturalım
        public EnumPaymentTypes PaymentTypes { get; set; }
    }
    //birden fazla OrderItem olabilir.Sifarişim içinden birtanesini direk order ile erişelim.
    public enum EnumOrderState
    { 
    Beklemede=0,
    Odenmemis=1,
    Tamamlandi = 2
    }

    public enum EnumPaymentTypes
    { 
        CreditCard=0,
        Eft=1
    }
}
