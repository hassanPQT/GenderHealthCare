using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class TestBookingService
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid TestBookingId { get; set; }

        [Required]
        public Guid ServiceId { get; set; }

        [ForeignKey("TestBookingId")]
        public TestBooking TestBooking { get; set; }
        [ForeignKey("ServiceId")]
        public Service Service { get; set; }
        public ICollection<TestResult> TestResults { get; set; }
    }
}
