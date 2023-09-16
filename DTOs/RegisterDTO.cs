using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Password should be between 8 and 15 characters.")]
        [DataType(DataType.Password, ErrorMessage = "Password should contain at least one uppercase, one lowercase, one number, and one special character.")]
        public string? Password { get; set; }
    }
}
