using System.ComponentModel.DataAnnotations;

namespace GenderHealcareSystem.DTO.Request
{
    public class AddAppointmentRequest
    {
        [Required]
        public Guid UserId { get; set; }

        public Guid ConsultantId { get; set; }

        public DateOnly AppointmentDate { get; set; }

        public int Slot { get; set; }

    }
}
