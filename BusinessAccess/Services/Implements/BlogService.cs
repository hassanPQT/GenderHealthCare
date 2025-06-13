using BusinessAccess.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;

namespace BusinessAccess.Services.Implements
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repository;

        public BlogService(IBlogRepository blogRepository)
        {
            _repository = blogRepository;
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Blog?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<Blog> CreateAsync(Blog dto)
        {
            return await _repository.CreateAsync(dto);
        }

        public Task<Blog?> UpdateAsync(Guid id, Blog dto)
        {
            return _repository.UpdateAsync(id, dto);
        }

        public async Task<Blog> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
