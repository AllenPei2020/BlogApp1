using BlogAppAPI.Data;
using BlogAppAPI.Entity;
using BlogAppAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IRepository<Blog> _blogRepository;
        public BlogController(IRepository<Blog> blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBlogs()
        {
            var blogs = await _blogRepository.GetAllAsync();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlog(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return Ok(blog);
        }

        [HttpGet("featured")]
        public async Task<IActionResult> GetFeaturedBlogs()
        {
            var blogs = await _blogRepository.GetAllFilteredAsync(b=>b.IsFeatured == true);

            return Ok(blogs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] BlogCreateModel blog)
        {
            var newBlog = new Blog
            {
                Title = blog.Title,
                Description = blog.Description,
                Content = blog.Content,
                Image = blog.Image,
                IsFeatured = blog.IsFeatured,
                CategoryId = blog.CategoryId
            };
            await _blogRepository.AddAsync(newBlog);
            await _blogRepository.SaveChangesAsync();
            return Ok(newBlog);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlog([FromRoute] int id, [FromBody] BlogUpdateModel blog)
        {
            var existingBlog = await _blogRepository.GetByIdAsync(id);
            if (existingBlog == null)
            {
                return NotFound();
            }
            existingBlog.Title = blog.Title;
            existingBlog.Description = blog.Description;
            existingBlog.Content = blog.Content;
            existingBlog.Image = blog.Image;
            existingBlog.IsFeatured = blog.IsFeatured;
            existingBlog.CategoryId = blog.CategoryId;
            _blogRepository.Update(existingBlog);
            await _blogRepository.SaveChangesAsync();
            return Ok(existingBlog);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog([FromRoute]int id)
        {
            var existingBlog = await _blogRepository.GetByIdAsync(id);
            if (existingBlog == null)
            {
                return NotFound();
            }
            await _blogRepository.DeleteAsync(id);
            await _blogRepository.SaveChangesAsync();
            return Ok();
        }
    }
}
