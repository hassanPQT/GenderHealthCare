using DataAccess.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenderHealcareSystem.DTO
{
    public class StaffConsultantDto
    {
        public Guid UserId { get; set; }

        public Guid? RoleId { get; set; }

        public string Username { get; set; }

        public string? FullName { get; set; }

        public string Password { get; set; }

        public bool? Gender { get; set; }

        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? Birthday { get; set; }

        public bool IsActive { get; set; }

        public Role Role { get; set; }
    }
}
