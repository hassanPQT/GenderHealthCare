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
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repo;

        public AppointmentService(IAppointmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<Appointment> CreateAsync(Appointment appointment)
        {
            return await _repo.CreateAsync(appointment);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync(Guid? customerId, Guid? consultantId, DateOnly? fromDate, DateOnly? toDate, int? slot)
        {
            return await _repo.GetAllAsync(customerId, consultantId, fromDate, toDate, slot);
        }

        public async Task<Appointment?> GetByIdAsync(Guid id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Appointment?> UpdateAsync(Guid id, Appointment appointment)
        {
            return await _repo.UpdateAsync(id, appointment);
        }
    }
}
