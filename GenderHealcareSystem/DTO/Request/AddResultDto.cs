using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenderHealcareSystem.DTO.Request
{
    public class AddResultDto
    {
        [Required]
        public Guid TestBookingServiceId { get; set; }

        [Required]
        [MaxLength(50)]
        public string TestName { get; set; }

        [MaxLength(500)]
        public string? ResultDetail { get; set; }

    }
}
