using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class E_Page
    {
      
            public int CurrentPage { get; set; }  //o anki sayfa
            public int ItemsPerPage { get; set; }  //Kaç ürün gelecek sayfa başına
            public int TotalItems { get; set; }  //toplam eleman (örn;100 eleman)

            public int TotalPages()   //Toplam kaç sayfası var
            {
                //örneğin yuvarlamayapıcaz. toplam ürünlerini, kaç tane ürün gelicek ona böl
                return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
            }

    }
}
