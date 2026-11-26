using Core.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Color : IData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
