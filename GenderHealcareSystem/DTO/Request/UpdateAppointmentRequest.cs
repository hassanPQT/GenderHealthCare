namespace GenderHealcareSystem.DTO.Request
{
    public class UpdateAppointmentRequest
    {
        public Guid ConsultantId { get; set; }

        public Guid StaffScheduleId { get; set; }

        public DateTime AppointmentDate { get; set; }
    }
}
