using Core.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Brand : IData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }

    }
}
