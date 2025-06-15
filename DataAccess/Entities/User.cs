using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        public Guid? RoleId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        public string? FullName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool? Gender { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(10)]
        public string? PhoneNumber { get; set; }

        [MaxLength(100)]
        public string? Address { get; set; }

        public string? Birthday { get; set; }

        public bool IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public MedicalHistory MedicalHistory { get; set; }

        [InverseProperty("User")]
        public ICollection<TestBooking> TestBookings { get; set; }

        //[InverseProperty("Staff")]
        public ICollection<TestBooking> StaffTestBookings { get; set; }

        [InverseProperty("User")]
        public ICollection<Appointment> PatientAppointments { get; set; }

        [InverseProperty("Consultant")]
        public ICollection<Appointment> ConsultingAppointments { get; set; }

        public ICollection<StaffSchedule> StaffSchedules { get; set; }

        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<MenstrualCycle> MenstrualCycles { get; set; }
        

        [InverseProperty("User")]
        public ICollection<Question> Questions { get; set; }

        [InverseProperty("Consultant")]
        public ICollection<Question> AnsweredQuestions { get; set; }
    }
}
