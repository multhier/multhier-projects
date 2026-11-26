using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Comments
{
    public class MainComment : Comment
    {
        public virtual List<SubComment> SubComments { get; set; }
    }
}
