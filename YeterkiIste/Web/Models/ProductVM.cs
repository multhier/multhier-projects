using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class ProductVM
    {
        public List<Product> Products { get; set; }
        public Product Product { get; set; }
    }
}
