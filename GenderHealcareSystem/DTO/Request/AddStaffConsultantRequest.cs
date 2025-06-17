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
        public string Password { get; set; }

        public bool? Gender { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(10)]
        public string? PhoneNumber { get; set; }

        [MaxLength(50)]
        public string? Address { get; set; }

        public string? Dob { get; set; }

        [Required]
        public Guid RoleId { get; set; }


    }
}
