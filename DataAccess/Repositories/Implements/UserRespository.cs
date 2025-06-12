using DataAccess.DBContext;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Respository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository.Repositories
{
    public class UserRespository : IUserRespository
    {
        private readonly AppDbContext _context;


        public UserRespository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User?> FindAccountByUserName(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> FindAccountByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> FindAccountById(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User?> UpdateAsync(Guid id, User dto)
        {
            var user = await _context.Users.FindAsync(id);
            
            if (user == null) return null;

            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.Address = dto.Address;
            if (dto.Dob != null)
                user.Dob = dto.Dob.Value;
            user.Gender = dto.Gender;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
    
    
}
