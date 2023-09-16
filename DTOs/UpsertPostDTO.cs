using System.ComponentModel.DataAnnotations;

namespace SocialMedia.DTOs
{
    public class UpsertPostDTO
    {
        public string? Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }
}
