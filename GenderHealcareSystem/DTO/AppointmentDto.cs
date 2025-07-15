using DataAccess.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenderHealcareSystem.DTO
{
    public class AppointmentDto
    {
        public Guid AppointmentId { get; set; }

        public Guid UserId { get; set; }

        public Guid? ConsultantId { get; set; }


        public DateOnly AppointmentDate { get; set; }

        public int Slot { get; set; }


        public string MeetingUrl { get; set; }

        public string Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public UserDto? User { get; set; }

        public StaffScheduleDto? StaffSchedule { get; set; }

        public UserDto? Consultant { get; set; }
    }
}
