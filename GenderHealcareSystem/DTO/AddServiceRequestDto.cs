using System.ComponentModel.DataAnnotations;

namespace GenderHealcareSystem.DTO
{
    public class AddServiceRequestDto
    {
        public string ServiceName { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public bool IsActive { get; set; }
        
    }
}
