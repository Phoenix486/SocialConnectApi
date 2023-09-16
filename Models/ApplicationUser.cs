using Microsoft.AspNetCore.Identity;

namespace SocialMedia.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public bool Verified { get; set; } = false;
        public string Bio { get; set; } = "Hey there! I'm using Social Media";
        public string ProfilePicture { get; set; } = string.Empty;
        public string? BannerPicture { get; set; } = string.Empty;
        
        public virtual ICollection<Post>? Posts { get; set; }
        
        public virtual ICollection<Comment>? Comments { get; set; }
        
        public virtual ICollection<Like>? Likes { get; set; }
        
        public virtual ICollection<Follow>? Followers { get; set; }
        
        public virtual ICollection<Follow>? Followings { get; set; }
    }
}
