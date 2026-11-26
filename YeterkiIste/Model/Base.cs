using Core.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public class Base:IData
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime AddedTime { get; set; }
    }
}
