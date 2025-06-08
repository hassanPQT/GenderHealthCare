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

            return dto;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var service = await GetByIdAsync(id);

            if (service == null)
                return false;

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services.Where(s => s.IsActive).ToListAsync();
        }

        public async Task<Service?> GetByIdAsync(Guid id)
        {
            return await _context.Services
                .Include(s => s.Feedbacks)
                .Include(s => s.TestResults)
                .Include(s => s.TestBookings)
                .Include(s => s.MedicalHistories)
                .FirstOrDefaultAsync(s => s.ServiceId == id && s.IsActive);

        }

        public async Task<Service?> UpdateAsync(Guid id, Service dto)
        {
            var service = await GetByIdAsync(id);

            if (service == null)
                return null;
            else
            {
                service.ServiceName = dto.ServiceName;
                service.Description = dto.Description;
                service.Price = dto.Price;
                service.IsActive = dto.IsActive;

                _context.Services.Update(service);
                await _context.SaveChangesAsync();

                return service;
            }

        }
    }
}
