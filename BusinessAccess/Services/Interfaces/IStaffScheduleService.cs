using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services.Interfaces
{
    public interface IStaffScheduleService
    {
        Task<IEnumerable<StaffSchedule>> GetAllAsync(Guid? staffId, DateTime? fromDate, DateTime? toDate, TimeSpan? fromHour, TimeSpan? toHour);
        Task<StaffSchedule?> GetByIdAsync(Guid id, Guid staffId);
        Task<StaffSchedule> CreateAsync(StaffSchedule schedule);
        Task<StaffSchedule?> UpdateAsync(Guid id, StaffSchedule dto, Guid staffId);
        Task<bool> DeleteAsync(Guid id, Guid staffId);
    }
}
