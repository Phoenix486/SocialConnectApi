using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Data;
using SocialMedia.DTOs;
using SocialMedia.Models;

namespace SocialMedia.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LikeController(ApplicationDbContext context)
        {
            _context = context;
        }

        //add like to post
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(LikeDTO like)
        {
            var newLike = new Like()
            {
                AuthorId = like.AuthorId,
                PostId = like.PostId,
            };
            _context.Likes.Add(newLike);
            await _context.SaveChangesAsync();
            return Ok("Like Created Successfully.");
        }

        //delete like
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id, LikeDTO like)
        {
            var existingLike = await _context.Likes.FindAsync(id);
            if (existingLike == null)
            {
                return NotFound("Like not found");
            }
            _context.Likes.Remove(existingLike);
            await _context.SaveChangesAsync();
            return Ok("Like Deleted Successfully.");
        }
    }
}
