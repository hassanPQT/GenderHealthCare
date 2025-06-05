using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Service
    {
        [Key]
        public Guid ServiceId { get; set; }

        [MaxLength(10)]
        public string ServiceName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public double Price { get; set; }

        public Guid TestResultId { get; set; }

        public Guid TestBookingId { get; set; }

        public Guid FeedbackId { get; set; }
        public Feedback Feedback { get; set; }

        [ForeignKey("TestResultId")]
        public TestResult TestResult { get; set; }

        [ForeignKey("TestBookingId")]
        public TestBooking TestBooking { get; set; }
        public ICollection<MedicalHistory> MedicalHistories { get; set; }
        
        
    }
}
