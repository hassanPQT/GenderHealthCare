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
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Appointment> CreateAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            appointment.User = await _context.Users.FindAsync(appointment.UserId);
            appointment.Consultant = await _context.Users.FindAsync(appointment.ConsultantId);
            

            return appointment;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existedAppointment = await _context.Appointments.FindAsync(id);

            if (existedAppointment == null)
                return false;
            existedAppointment.Status = "Canceled";

            _context.Appointments.Update(existedAppointment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync(Guid? customerId, Guid? consultantId, DateOnly? fromDate, DateOnly? toDate, int? slot)
        {
            var query = _context.Appointments.AsQueryable();

            if (customerId != null)
                query = query.Where(a => a.UserId == customerId);

            if (consultantId != null)
                query = query.Where(a => a.ConsultantId == consultantId);

            if (fromDate != null)
                query = query.Where(a => a.AppointmentDate >= fromDate);

            if (toDate != null)
                query = query.Where(a => a.AppointmentDate <= toDate);

            if (slot != null)
                query = query.Where(a => a.Slot == slot);

            return await query.Include(a => a.User)
                        .Include(a => a.Consultant)
                        .OrderBy(a => a.AppointmentDate)
                        .ToListAsync();
        }

        public async Task<Appointment?> GetByIdAsync(Guid id)
        {
            return await _context.Appointments
                .Include(a => a.User)
                .Include(a => a.Consultant)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);
        }

        public async Task<Appointment?> UpdateAsync(Guid id, Appointment appointment)
        {
            var existedAppointment = await _context.Appointments.FindAsync(id);

            if (existedAppointment != null)
                return null;

            existedAppointment.Consultant = await _context.Users.FindAsync(appointment.ConsultantId);
            existedAppointment.AppointmentDate = appointment.AppointmentDate;
            existedAppointment.Slot = appointment.Slot;
            existedAppointment.Status = appointment.Status;
            existedAppointment.UpdatedAt = appointment.UpdatedAt;

            _context.Appointments.Update(existedAppointment);
            await _context.SaveChangesAsync();

            return existedAppointment;
        }
    }
}
