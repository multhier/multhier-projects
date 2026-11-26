using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Comments
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime AddedTime { get; set; }
    }
}
