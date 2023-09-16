using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int PostId { get; set; }
        public int AuthorId { get; set; }
        
        [MaxLength(15)]
        public string UserName { get; set; }
        [MaxLength(30)]
        public string AuthorPic { get; set; }

        public virtual ICollection<Reply>? Replies { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}