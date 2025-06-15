using DataAccess.DBContext;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Implements
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;

        public ServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Service> CreateAsync(Service dto)
        {
            await _context.Services.AddAsync(dto);
            await _context.SaveChangesAsync();

            var service = await _context.Services.FirstOrDefaultAsync(s => s.ServiceId == dto.ServiceId);

            return service;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existedService = await GetByIdAsync(id);

            if (existedService == null)
                return false;

            existedService.IsActive = false;
            _context.Services.Update(existedService);
            await _context.SaveChangesAsync();

            var service = await _context.Services.FirstOrDefaultAsync(s => s.ServiceId == id);

            return true;
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services.Where(s => s.IsActive).ToListAsync();
        }

        public async Task<Service?> GetByIdAsync(Guid id)
        {
            return await _context.Services
                .FirstOrDefaultAsync(s => s.ServiceId == id && s.IsActive);

        }

        public async Task<Service?> UpdateAsync(Guid id, Service dto)
        {
            var existedService = await _context.Services.FindAsync(id);

            if (existedService == null)
                return null;
            else
            {
                existedService.ServiceName = dto.ServiceName;
                existedService.Description = dto.Description;
                existedService.Price = dto.Price;
                existedService.IsActive = dto.IsActive;

                _context.Services.Update(existedService);
                await _context.SaveChangesAsync();

                var service = await _context.Services.FirstOrDefaultAsync(s => s.ServiceId == id);

                return service;
            }

        }
    }
}
