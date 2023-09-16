using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.DTOs;
using SocialMedia.Models;

namespace SocialMedia.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;
    
    public UserController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    
    //find by id
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> Get(int id)
    {
        var appUser = await _userManager.FindByIdAsync(id.ToString());
        if (appUser == null)
        {
            return NotFound("User not found");
        }
        UserDTO user = new()
        {
            Id = appUser.Id,
            Username = appUser.UserName,
            ImageUrl = appUser.ProfilePicture
        };
        return Ok(user);
    }
    
    // profile/{id}
    [HttpGet("profile/{id}")]   
    public async Task<ActionResult<ProfileDTO>> GetProfile(int id)
    {
        var appUser = await _userManager.FindByIdAsync(id.ToString());
        if(appUser == null)
        {
            return NotFound("User not found");
        }
        ProfileDTO profile = new()
        {
            Id = appUser.Id,
            UserName = appUser.UserName,
            Bio = appUser.Bio,
            Email = appUser.Email,
            PhoneNumber = appUser.PhoneNumber,
            Verified = appUser.EmailConfirmed,
            ProfilePicture = appUser.ProfilePicture
        };
        return profile;
    }
    
    //get posts by user id
    [HttpGet("{id}/posts")]
    
    public async Task<ActionResult<List<Post>>> GetPostsByUserId(int id)
    {
        var posts = await _context.Posts
            .Include(p => p.Comments)
            .Include(p => p.Likes)
            .Where(p => p.AuthorId == id)
            .ToListAsync();
        return Ok(posts);
    }
    
    
}