using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implements
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext _context;

        public BlogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _context.Blogs.Include(b => b.User).ToListAsync();
        }

        public async Task<Blog?> GetByIdAsync(Guid id)
        {
            return await _context.Blogs.FirstOrDefaultAsync(b => b.UserId == id);
        }

        public async Task<Blog> CreateAsync(Blog blog)
        {
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();

            return await _context.Blogs.FirstOrDefaultAsync(b => b.UserId == blog.UserId);
        }

        public async Task<Blog?> UpdateAsync(Guid id, Blog blog)
        {
            var existingBlog = await _context.Blogs.FirstOrDefaultAsync(b => b.BlogId == blog.BlogId && b.UserId == id);
            if (existingBlog != null)
            {
                return null;
            }

            existingBlog.Tittle = blog.Tittle;
            existingBlog.Content = blog.Content;
            _context.Blogs.Update(existingBlog);
            await _context.SaveChangesAsync();

            return existingBlog;
        }

        public async Task<Blog> DeleteAsync(Guid id)
        {
            var existingBlog = await _context.Blogs.FirstOrDefaultAsync(b => b.BlogId == id);
            if (existingBlog != null)
            {
                return null;
            }

            _context.Blogs.Remove(existingBlog);
            await _context.SaveChangesAsync();
            return existingBlog;
        }
    }
}
