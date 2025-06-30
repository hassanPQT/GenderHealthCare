using BusinessAccess.Services.Interfaces;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services.Implements
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repository;

        public ServiceService(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<Service> CreateAsync(Service dto)
        {
            return await _repository.CreateAsync(dto);

        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Service?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Service?> UpdateAsync(Guid id, Service dto)
        {
            return await _repository.UpdateAsync(id, dto);
        }
    }
}
