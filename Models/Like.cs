namespace SocialMedia.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int PostId { get; set; }

        public int AuthorId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}