namespace SocialMedia.DTOs;

public class ProfileDTO
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Bio { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool Verified { get; set; }
    public string ProfilePicture { get; set; }
    
}