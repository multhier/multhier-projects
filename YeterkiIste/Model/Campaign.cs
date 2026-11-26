using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public class Campaign:Base
    {
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public int  Click { get; set; }
    }
}
