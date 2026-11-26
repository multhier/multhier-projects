
using Core.Abstract;
using System.Collections.Generic;

namespace Model
{
    public class Cart : IData
    {
        public int Id { get; set; }
        public string UserId { get; set; } //nav prop ihtiyacı yok Identity kullanılır.Login olur olmaz alabilirz

        public List<CartItem> CartItems { get; set; }
    }
}
