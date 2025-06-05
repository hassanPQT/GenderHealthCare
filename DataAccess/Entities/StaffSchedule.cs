using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class StaffSchedule
    {

        [Key]
        public Guid StaffScheduleId { get; set; }

        [MaxLength(10)]
        public Guid UserId { get; set; }

        public DateTime WorkingDate { get; set; }

        public TimeSpan WorkingTime { get; set; }

        [MaxLength(10)]
        public string Status { get; set; }

        public User User { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
