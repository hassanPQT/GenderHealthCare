using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Appointment
    {
        [Key]
        [MaxLength(10)]
        public Guid AppointmentId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public Guid? ConsultantId { get; set; }
        [MaxLength(10)]
        public Guid StaffScheduleId { get; set; }

        public DateTime AppointmentDate { get; set; }
        public string MeetingUrl { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("UserId")]
        //[InverseProperty("Appointments")]
        public User User { get; set; }

        [ForeignKey("StaffScheduleId")]
        public StaffSchedule StaffSchedule { get; set; }

        [ForeignKey("ConsultantId")]
        //[InverseProperty("ConsultingAppointments")]
        public User Consultant { get; set; }
    }
}
