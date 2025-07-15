namespace GenderHealcareSystem.DTO.Request
{
    public class UpdateAppointmentRequest
    {
        public Guid ConsultantId { get; set; }


        public DateOnly AppointmentDate { get; set; }

        public int Slot { get; set; }

        public string Status { get; set; }
    }
}
