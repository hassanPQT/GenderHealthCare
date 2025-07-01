using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllAsync(Guid? customerId, Guid? consultantId, DateTime? fromDate, DateTime? toDate, TimeSpan? fromHour, TimeSpan? toHour);
        Task<Appointment?> GetByIdAsync(Guid id);
        Task<Appointment> CreateAsync(Appointment appointment);
        Task<Appointment?> UpdateAsync(Guid id, Appointment appointment);
        Task<bool> DeleteAsync(Guid id);
    }
}
