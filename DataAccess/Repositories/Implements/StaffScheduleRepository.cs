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
    public class StaffScheduleRepository : IStaffScheduleRepository
    {
        private readonly AppDbContext _context;

        public StaffScheduleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<StaffSchedule> CreateAsync(StaffSchedule schedule)
        {
            await _context.StaffSchedules.AddAsync(schedule);
            await _context.SaveChangesAsync();
            schedule.Consultant = await _context.Users.FindAsync(schedule.ConsultantId);

            return schedule;
        }

        public async Task<bool> DeleteAsync(Guid id, Guid staffId)
        {
            var existedSchedule = await _context.StaffSchedules.FindAsync(id);
            if (existedSchedule == null)
                return false;
            if (existedSchedule.ConsultantId != staffId)
                return false;

            _context.Remove(existedSchedule);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<StaffSchedule>> GetAllAsync(Guid? staffId, DateTime? fromDate, DateTime? toDate, TimeSpan? fromHour, TimeSpan? toHour)
        {
            var query = _context.StaffSchedules.AsQueryable();

            if (staffId.HasValue)
                query = query.Where(s => s.ConsultantId == staffId.Value);


            if (fromDate.HasValue)
                query = query.Where(s => s.WorkingDate >= fromDate.Value);


            if (toDate.HasValue)
                query = query.Where(s => s.WorkingDate <= toDate.Value);


            if (fromHour.HasValue)
                query = query.Where(s => s.WorkingTime >= fromHour.Value);


            if (toHour.HasValue)
                query = query.Where(s => s.WorkingTime <= toHour.Value);


            return await query
                .Include(s => s.Consultant)
                .OrderBy(s => s.WorkingDate)
                .ThenBy(s => s.WorkingTime)
                .ToListAsync();
        }

        public async Task<StaffSchedule?> GetByIdAsync(Guid id, Guid staffId)
        {
            var existedSchedule = await _context.StaffSchedules.FirstOrDefaultAsync(s => s.StaffScheduleId == id);
            if (existedSchedule == null)
                return null;
            if (existedSchedule.ConsultantId != staffId)
                return null;

            return existedSchedule;
        }

        public async Task<StaffSchedule?> UpdateAsync(Guid id, StaffSchedule dto, Guid staffId)
        {
            var existedSchedule = await _context.StaffSchedules.FindAsync(id);
            if (existedSchedule == null)
                return null;
            if (existedSchedule.ConsultantId != staffId)
                return null;

            existedSchedule.Consultant = await _context.Users.FindAsync(dto.ConsultantId);
            existedSchedule.WorkingDate = dto.WorkingDate;
            existedSchedule.WorkingTime = dto.WorkingTime;
            existedSchedule.Status = dto.Status;
            existedSchedule.UpdatedAt = dto.UpdatedAt;

            _context.Update(existedSchedule);
            await _context.SaveChangesAsync();
            return existedSchedule;
        }
    }
}
