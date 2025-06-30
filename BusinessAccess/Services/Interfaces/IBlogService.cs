using DataAccess.Entities;

namespace BusinessAccess.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog?> GetByIdAsync(Guid id);
        Task<Blog> CreateAsync(Blog dto);
        Task<Blog?> UpdateAsync(Guid id, Blog dto);
        Task<Blog> DeleteAsync(Guid id);
    }
}