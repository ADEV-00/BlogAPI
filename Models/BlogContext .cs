using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Blog.Models
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; } = null!;
    }
}
