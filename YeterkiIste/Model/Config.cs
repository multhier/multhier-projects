using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Model
{
   public class Config
    {
        [Key]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
