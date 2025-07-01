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
            appointment.StaffSchedule = await _context.StaffSchedules.FindAsync(appointment.StaffScheduleId);

            return appointment;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existedAppointment = await _context.Appointments.FindAsync(id);

            if (existedAppointment != null)
                return false;
            _context.Appointments.Remove(existedAppointment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync(Guid? customerId, Guid? consultantId, DateTime? fromDate, DateTime? toDate, TimeSpan? fromHour, TimeSpan? toHour)
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

            if (fromHour != null)
                query = query.Where(a => a.AppointmentDate.TimeOfDay >= fromHour);

            if (toHour != null)
                query = query.Where(a => a.AppointmentDate.TimeOfDay <= toHour);

            return await query.Include(a => a.User)
                        .Include(a => a.Consultant)
                        .Include(a => a.StaffSchedule)
                        .OrderBy(a => a.AppointmentDate)
                        .ThenBy(a => a.AppointmentDate.TimeOfDay)
                        .ToListAsync();
        }

        public async Task<Appointment?> GetByIdAsync(Guid id)
        {
            return await _context.Appointments
                .Include(a => a.User)
                .Include(a => a.Consultant)
                .Include(a => a.StaffSchedule)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);
        }

        public async Task<Appointment?> UpdateAsync(Guid id, Appointment appointment)
        {
            var existedAppointment = await _context.Appointments.FindAsync(id);

            if (existedAppointment != null)
                return null;

            existedAppointment.User = await _context.Users.FindAsync(appointment.UserId);
            existedAppointment.Consultant = await _context.Users.FindAsync(appointment.ConsultantId);
            existedAppointment.StaffSchedule = await _context.StaffSchedules.FindAsync(appointment.StaffScheduleId);
            existedAppointment.AppointmentDate = appointment.AppointmentDate;
            existedAppointment.MeetingUrl = appointment.MeetingUrl;
            existedAppointment.Status = appointment.Status;
            existedAppointment.UpdatedAt = appointment.UpdatedAt;

            _context.Appointments.Update(existedAppointment);
            await _context.SaveChangesAsync();

            return existedAppointment;
        }
    }
}
