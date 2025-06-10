using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> FindAccountByUserName(string username);
        Task<User?> FindAccountByEmail(string email);

        Task<User?> FindAccountById(Guid userId);
        Task AddUser(User user);

        Task<User?> UpdateAsync(Guid id, User dto);
    }
}
