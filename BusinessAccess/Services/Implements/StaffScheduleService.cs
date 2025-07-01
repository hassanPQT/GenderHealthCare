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
    public class StaffScheduleService : IStaffScheduleService
    {
        private readonly IStaffScheduleRepository _repo;

        public StaffScheduleService(IStaffScheduleRepository repo)
        {
            _repo = repo;
        }

        public async Task<StaffSchedule> CreateAsync(StaffSchedule schedule)
        {
            return await _repo.CreateAsync(schedule);
        }

        public async Task<bool> DeleteAsync(Guid id, Guid staffId)
        {
            return await _repo.DeleteAsync(id, staffId);
        }

        public async Task<IEnumerable<StaffSchedule>> GetAllAsync(Guid? staffId, DateTime? fromDate, DateTime? toDate, TimeSpan? fromHour, TimeSpan? toHour)
        {
            return await _repo.GetAllAsync(staffId, fromDate, toDate, fromHour, toHour);
        }

        public async Task<StaffSchedule?> GetByIdAsync(Guid id, Guid staffId)
        {
            return await _repo.GetByIdAsync(id, staffId);
        }

        public async Task<StaffSchedule?> UpdateAsync(Guid id, StaffSchedule dto, Guid staffId)
        {
            return await _repo.UpdateAsync(id, dto, staffId);
        }
    }
}
