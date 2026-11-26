using Model;
using Model.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class ProductDetailVM
    {
        public Product Product { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<ProductAttribute> ProductAttributes { get; set; }
        public ICollection<Category> Categories { get; set; }
        public virtual List<MainComment> MainComments { get; set; }
    }
}
