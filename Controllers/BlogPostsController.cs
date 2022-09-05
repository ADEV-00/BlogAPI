using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blog.Models;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly BlogContext _context;

        public BlogPostsController(BlogContext context)
        {
            _context = context;
        }

        // GET: api/BlogPosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPostDTO>>> GetBlogPosts()
        {
          if (_context.BlogPosts == null)
          {
              return NotFound();
          }
            return await _context.BlogPosts.Select(x => PostToDTO(x)).ToListAsync();
        }

        // GET: api/BlogPosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPostDTO>> GetBlogPost(long id)
        {
          if (_context.BlogPosts == null)
          {
              return NotFound();
          }
            var blogPost = await _context.BlogPosts.FindAsync(id);

            if (blogPost == null)
            {
                return NotFound();
            }

            return PostToDTO(blogPost);
        }

        // PUT: api/BlogPosts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogPost(long id, BlogPostDTO blogPost)
        {
            if (id != blogPost.Id)
            {
                return BadRequest();
            }

            //_context.Entry(blogPost).State = EntityState.Modified;
            var post = await _context.BlogPosts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            post.Title = blogPost.Title;
            post.Content = blogPost.Content;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!BlogPostExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/BlogPosts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BlogPostDTO>> PostBlogPost(BlogPostDTO blogPost)
        {
            var post = new BlogPost
            {
                Title = blogPost.Title,
                Content = blogPost.Content,
            };

            _context.BlogPosts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetBlogPost),
                new { id = post.Id },
                PostToDTO(post));

        }

        // DELETE: api/BlogPosts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(long id)
        {
            if (_context.BlogPosts == null)
            {
                return NotFound();
            }
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            _context.BlogPosts.Remove(blogPost);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogPostExists(long id)
        {
            return (_context.BlogPosts?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static BlogPostDTO PostToDTO(BlogPost blogPost) =>
            new BlogPostDTO
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Content = blogPost.Content
            };
    }
}
