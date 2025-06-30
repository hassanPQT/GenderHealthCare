using DataAccess.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenderHealcareSystem.DTO
{
    public class StaffScheduleDto
    {
        public Guid StaffScheduleId { get; set; }

        public Guid ConsultantId { get; set; }

        public DateTime WorkingDate { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string? Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public UserDto? Consultant { get; set; }
    }
}
