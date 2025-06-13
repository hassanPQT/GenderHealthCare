using DataAccess.Entities;

namespace DataAccess.Repositories.Interfaces
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Blog>> GetAllAsync();

        Task<Blog?> GetByIdAsync(Guid id);

        Task<Blog> CreateAsync(Blog blog);

        Task<Blog?> UpdateAsync(Guid id, Blog blog);

        Task<Blog> DeleteAsync(Guid id);
    }
}
