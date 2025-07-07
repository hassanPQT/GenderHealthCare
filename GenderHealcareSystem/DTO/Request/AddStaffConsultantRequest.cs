using DataAccess.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenderHealcareSystem.DTO.Request
{
    public class AddStaffConsultantRequest
    {
        [MaxLength(20)]
        public string Username { get; set; }

        public string? FullName { get; set; }

        [EmailAddress]
        public string PersonalEmail { get; set; }

        public bool? Gender { get; set; }

        [MaxLength(10)]
        public string? PhoneNumber { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        public string? Birthday { get; set; }

        [Required]
        public Guid RoleId { get; set; }


    }
}
