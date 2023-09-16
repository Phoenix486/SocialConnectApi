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
    public class ReplyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ReplyController(ApplicationDbContext context)
        {
            _context = context;
        }

        //add reply to comment
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(ReplyDTO reply)
        {
            var newReply = new Reply()
            {
                AuthorId = reply.AuthorId,
                CommentId = reply.CommentId,
                Content = reply.Content,
            };
            _context.Replies.Add(newReply);
            await _context.SaveChangesAsync();
            return Ok("Reply Created Successfully.");
        }

        //edit reply
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, ReplyDTO reply)
        {
            var existingReply = await _context.Replies.FindAsync(id);
            if (existingReply == null)
            {
                return NotFound("Reply not found");
            }
            existingReply.Content = reply.Content ?? existingReply.Content;
            await _context.SaveChangesAsync();
            return Ok("Reply Updated Successfully.");
        }

        //delete reply
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var existingReply = await _context.Replies.FindAsync(id);
            if (existingReply == null)
            {
                return NotFound("Reply not found");
            }
            _context.Replies.Remove(existingReply);
            await _context.SaveChangesAsync();
            return Ok("Reply Deleted Successfully.");
        }
    }
}
