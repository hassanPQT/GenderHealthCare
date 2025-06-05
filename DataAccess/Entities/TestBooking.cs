using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class TestBooking
    {
        [Key]
        public Guid TestBookingId { get; set; }

        public Guid ServiceId { get; set; }

        public Guid UserId { get; set; }

        public Guid? StaffId { get; set; }
        public DateTime BookingDate { get; set; }

        [MaxLength(10)]
        public string BookingStaff { get; set; }

        [MaxLength(10)]
        public string Status { get; set; }

        [MaxLength(100)]
        public string Note { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("TestBookings")]
        public User User { get; set; }

        public ICollection<Service> Services { get; set; }

        [ForeignKey("StaffId")]
        [InverseProperty("HandledTestBookings")]
        public User Staff { get; set; }
    }
}
