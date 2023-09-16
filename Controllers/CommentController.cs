using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CommentController(ApplicationDbContext context)
        {
            _context = context;
        }
        
       

        //add comment to post
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Comment>> Post(UpsertCommentDTO comment)
        {
            var newComment = new Comment()
            {
                AuthorId = comment.AuthorId,
                PostId = comment.PostId,
                Content = comment.Content,
                AuthorPic = comment.AuthorPic,
                UserName = comment.UserName,
            };
            _context.Comments.Add(newComment);
            await _context.SaveChangesAsync();
            return Ok("Comment Created Successfully.");
        }

        //edit comment
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, UpsertCommentDTO comment)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return NotFound("Comment not found");
            }
            existingComment.Content = comment.Content ?? existingComment.Content;
            await _context.SaveChangesAsync();
            return Ok("Comment Updated Successfully.");
        }


        //delete comment
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return NotFound("Comment not found");
            }
            _context.Comments.Remove(existingComment);
            await _context.SaveChangesAsync();
            return Ok("Comment Deleted Successfully.");
        }
    }
}
