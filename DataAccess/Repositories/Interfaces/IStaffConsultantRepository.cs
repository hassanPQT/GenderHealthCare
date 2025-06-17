using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IStaffConsultantRepository
    {
        Task<IEnumerable<User>> GetAllAsync(Guid roleId);
        Task<User?> GetByIdAsync(Guid id, Guid roleId);
        Task<User> CreateAsync(User dto);
        Task<User?> UpdateAsync(Guid id, User dto, Guid roleId);
        Task<bool> DeleteAsync(Guid id, Guid roleId);
        Task<bool> ReviveAsync(Guid id, Guid roleId);
    }
}
