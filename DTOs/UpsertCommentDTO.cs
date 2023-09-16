using System.ComponentModel.DataAnnotations;

namespace SocialMedia.DTOs
{
    public class UpsertCommentDTO
    {
        public string Content { get; set; }
        public int AuthorId { get; set; }
        
        [MaxLength(50)]
        public string UserName { get; set; }
        [MaxLength(50)]
        public string? AuthorPic { get; set; }
        public int PostId { get; set; }
    }
}
