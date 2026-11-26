using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
namespace Web.ViewModels
{
    public class MyProductVM
    {
        public IEnumerable<Product> Products { get; set; }
        public Pager Pager { get; set; }

        public string CategoryRoute { get; set; }

        public IEnumerable<Brand> Brands { get; set; }
        public int? BrandRoute { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public int? sortBy { get; set; }

        public string search { get; set; }

        public int? minPrice { get; set; }
        public int? maxPrice { get; set; }
    }

}
