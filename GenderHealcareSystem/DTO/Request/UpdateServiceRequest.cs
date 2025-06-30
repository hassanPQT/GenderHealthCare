using System.ComponentModel.DataAnnotations;

namespace GenderHealcareSystem.DTO.Request
{
    public class UpdateServiceRequest
    {
        public string ServiceName { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public bool IsActive { get; set; }

    }
}
