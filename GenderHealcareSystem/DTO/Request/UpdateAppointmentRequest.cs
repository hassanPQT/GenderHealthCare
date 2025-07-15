namespace GenderHealcareSystem.DTO.Request
{
    public class UpdateAppointmentRequest
    {
        public Guid ConsultantId { get; set; }

        public Guid StaffScheduleId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public int Slot { get; set; }

        public string Status { get; set; }
    }
}
