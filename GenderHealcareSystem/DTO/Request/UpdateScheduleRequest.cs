using System.ComponentModel.DataAnnotations;

namespace GenderHealcareSystem.DTO.Request
{
    public class UpdateScheduleRequest
    {
        public Guid ConsultantId { get; set; }

        public DateTime WorkingDate { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        [MaxLength(10)]
        public string? Status { get; set; }
    }
}
