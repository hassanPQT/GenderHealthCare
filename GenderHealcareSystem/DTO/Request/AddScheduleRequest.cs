namespace GenderHealcareSystem.DTO.Request
{
    public class AddScheduleRequest
    {
        public Guid ConsultantId { get; set; }

        public DateTime WorkingDate { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
