using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Models
{
    public class ProductPagesVM
    {
        public IEnumerable<Product> Products { get; set; }
        public E_Page E_Page { get; set; }

        public string Category { get; set; }
        public Pager Pager { get; set; }
    }
}
