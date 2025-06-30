using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IStaffScheduleRepository
    {
        Task<IEnumerable<StaffSchedule>> GetAllAsync(Guid? staffId, DateTime? fromDate, DateTime? toDate, TimeSpan? fromHour, TimeSpan? toHour);
        Task<StaffSchedule?> GetByIdAsync(Guid id, Guid staffId);
        Task<StaffSchedule> CreateAsync(StaffSchedule schedule);
        Task<StaffSchedule?> UpdateAsync(Guid id, StaffSchedule dto, Guid staffId);
        Task<bool> DeleteAsync(Guid id, Guid staffId);
    }
}
