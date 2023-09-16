namespace SocialMedia.Models
{
    public class Reply
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int CommentId { get; set; }
        public int AuthorId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}