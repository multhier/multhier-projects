using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Comments
{
    public class SubComment : Comment
    {
        public virtual int MainCommentId { get; set; }
    }
}
