using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class CommentVM
    {
        [Required]
        public int MainCommentId { get; set; }
        public string Message { get; set; }
        [Required]
        public int PostId { get; set; }
    }
}
