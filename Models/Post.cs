namespace SocialMedia.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }

        public int AuthorId { get; set; } 
        public virtual ICollection<Comment>? Comments { get; set; }   //one-to-many
        public virtual ICollection<Like>? Likes { get; set; }  //one-to-many

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}