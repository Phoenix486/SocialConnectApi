using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;

namespace SocialMedia.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,IdentityRole<int>,int>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // This needs to go before the other rules!

        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Like> Likes { get;set;}
        
        public DbSet<Follow> Follows { get; set; }
        
    }
}
