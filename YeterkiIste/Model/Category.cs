using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Category:Base
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; } 
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual Category ParentCategory { get; set; }
        public ICollection<CategoryProduct> CategoryProducts { get; set; }

        public List<Brand> Brands { get; set; }
    }
}
