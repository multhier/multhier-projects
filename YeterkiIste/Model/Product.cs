using Model.Comments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
  public  class Product:Base
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string ImageUrl { get; set; }
        [Range(1, 100000)]
        public decimal Price { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsHome { get; set; }


        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        [ForeignKey("Color")]
        public int ColorId { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Color Color { get; set; }



        public ICollection<CategoryProduct> CategoryProducts { get; set; }
        public ICollection<ProductAttribute> ProductAttributes { get; set; }
        public ICollection<Image> Images { get; set; }
        public virtual List<MainComment> MainComments { get; set; }


    }
}
