using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAsync(Guid? customerId, Guid? consultantId, DateTime? fromDate, DateTime? toDate, int? slot);
        Task<Appointment?> GetByIdAsync(Guid id);
        Task<Appointment> CreateAsync(Appointment appointment);
        Task<Appointment?> UpdateAsync(Guid id, Appointment appointment);
        Task<bool> DeleteAsync(Guid id);
    }
}
