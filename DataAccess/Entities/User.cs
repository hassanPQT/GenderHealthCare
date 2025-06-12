using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
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

        public DateOnly? Dob { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public ICollection<MedicalHistory> MedicalHistories { get; set; }
        
        [InverseProperty("User")]
        public ICollection<TestBooking> TestBookings { get; set; }
        public ICollection<TestResult> TestResults { get; set; }
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<MenstrualCycle> MenstrualCycles { get; set; }
        public ICollection<StaffSchedule> StaffSchedules { get; set; }

        [InverseProperty("User")]
        public ICollection<Appointment> Appointments { get; set; }

        [InverseProperty("Consultant")]
        public ICollection<Appointment> ConsultingAppointments { get; set; }

        [InverseProperty("Staff")]
        public ICollection<TestBooking> HandledTestBookings { get; set; }

        [InverseProperty("User")]
        public ICollection<Question> QuestionsAsked { get; set; }

        [InverseProperty("Consultant")]
        public ICollection<Question> AnsweredQuestions { get; set; }
    }
}
