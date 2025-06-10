using System.ComponentModel.DataAnnotations;

namespace GenderHealcareSystem.DTO
{
    public class ServiceDto
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }
        
    }
}
