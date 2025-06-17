using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Implements
{
    public class StaffConsultantRepository : IStaffConsultantRepository
    {
        private readonly AppDbContext _context;

        public StaffConsultantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAsync(Guid id, Guid roleId)
        {
            var existedUser = await GetByIdAsync(id, roleId);

            if (existedUser == null)
                return false;

            existedUser.IsActive = false;
            _context.Users.Update(existedUser);
            await _context.SaveChangesAsync();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            return true;
        }

        public async Task<bool> ReviveAsync(Guid id, Guid roleId)
        {
            var deletedUser = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == id && !u.IsActive && u.RoleId.Equals(roleId));

            if (deletedUser == null)
                return false;

            deletedUser.IsActive = true;
            _context.Users.Update(deletedUser);
            await _context.SaveChangesAsync();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            return true;
        }

        public async Task<User?> GetByIdAsync(Guid id, Guid roleId)
        {        
                return await _context.Users
                    .FirstOrDefaultAsync(u => u.UserId == id && u.IsActive
                    && (u.RoleId.Equals(roleId)));
        }

        public async Task<User?> UpdateAsync(Guid id, User dto, Guid roleId)
        {
            var existedUser = await GetByIdAsync(id, roleId);

            if(existedUser == null)
                return null;
            else
            {
                existedUser.Username = dto.Username;
                existedUser.FullName = dto.FullName;
                existedUser.Password = dto.Password;
                existedUser.Gender = dto.Gender;
                existedUser.Email = dto.Email;
                existedUser.PhoneNumber = dto.PhoneNumber;
                existedUser.Address = dto.Address;
                existedUser.Birthday = dto.Birthday;
                existedUser.RoleId = dto.RoleId;

                _context.Users.Update(existedUser);
                await _context.SaveChangesAsync();

                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

                return user;
            }

        }

        public async Task<IEnumerable<User>> GetAllAsync(Guid roleId)
        {
            return await _context.Users
                .Where(u => u.IsActive && u.RoleId.Equals(roleId))
                .ToListAsync();
        }

        public async Task<User> CreateAsync(User dto)
        {
            await _context.Users.AddAsync(dto);
            await _context.SaveChangesAsync();
            var user = await _context.Users.FirstOrDefaultAsync(s => s.UserId == dto.UserId);
            return user;
        }
    }
}
