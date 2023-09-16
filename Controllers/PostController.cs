using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.DTOs;
using SocialMedia.Models;

namespace SocialMedia.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ApplicationDbContext _context; 
        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: api/post
        [HttpGet]
        public async Task<ActionResult<List<Post>>> Get()
        {
            var posts = await _context.Posts
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
            return Ok(posts);
        }
        
        // GET: api/post/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> Get(int id)
        {
            var post = await _context.Posts
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return NotFound("Post not found");
            }
            return Ok(post);
        }
        
        //Get posts with pagination and filtering pageSize=10&pageNumber=1
        // GET: api/post/getPosts?pageNumber=1&pageSize=10
        [HttpGet("getPosts")]
        public async Task<ActionResult<List<Post>>> GetPosts([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var queryable = _context.Posts.AsQueryable();
            var posts = await queryable
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .OrderByDescending(p => p.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return Ok(posts);
        }

        //Post: api/post
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Post>> Post(UpsertPostDTO post)
        {
            var newPost = new Post()
            {
                AuthorId = post.AuthorId,
                Title = post.Title ?? "",
                Content = post.Content,
                ImageUrl = post.ImageUrl,
            };
            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();
            return Ok("Post Created Successfully.");
        }

        //Put: api/post/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, UpsertPostDTO post)
        {
            var existingPost = await _context.Posts.FindAsync(id);

            //asNoTracking() is used to prevent EF from tracking the entity
            //var existingPost = await _context.Posts.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            if (existingPost == null)
            {
                return NotFound("Post not found");
            }
            existingPost.Title = post.Title ?? existingPost.Title;
            existingPost.Content = post.Content ?? existingPost.Content;
            existingPost.ImageUrl = post.ImageUrl ?? existingPost.ImageUrl;
            _context.Posts.Update(existingPost);
            await _context.SaveChangesAsync();
            return Ok("Post Updated Successfully.");
        }

        //Delete: api/post/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingPost = await _context.Posts.FindAsync(id);
            if (existingPost == null)
            {
                return NotFound("Post not found");
            }
            _context.Posts.Remove(existingPost);
            await _context.SaveChangesAsync();
            return Ok("Post Deleted Successfully.");
        }
    }
}
