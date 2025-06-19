using BusinessAccess.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services.Implements
{
    public class StaffConsultantService : IStaffConsultantService
    {
        private readonly IStaffConsultantRepository _repository;

        public StaffConsultantService(IStaffConsultantRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> DeleteAsync(Guid id, Guid roleId)
        {
            return await _repository.DeleteAsync(id, roleId);
        }

        public async Task<bool> ReviveAsync(Guid id, Guid roleId)
        {
            return await _repository.ReviveAsync(id, roleId);
        }

        public async Task<User?> GetByIdAsync(Guid id, Guid roleId)
        {
            return await _repository.GetByIdAsync(id, roleId);
        }

        public async Task<User?> UpdateAsync(Guid id, User dto, Guid roleId)
        {
            return await _repository.UpdateAsync(id, dto, roleId);
        }

        public Task<IEnumerable<User>> GetAllAsync(Guid roleId)
        {
            return _repository.GetAllAsync(roleId);
        }

        public Task<User> CreateAsync(User dto)
        {
            return _repository.CreateAsync(dto);
        }
    }
}
