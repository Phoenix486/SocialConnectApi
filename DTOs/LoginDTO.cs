using System.ComponentModel.DataAnnotations;

namespace SocialMedia.DTOs
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
