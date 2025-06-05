using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class TestResult
    {
        [Key]
        public Guid TestResultId { get; set; }

        [MaxLength(50)]
        public string ResultDetail { get; set; }

        public DateTime TestDate { get; set; }
        public DateTime SmapleReceivedDate { get; set; }
        public DateTime ResultDate { get; set; }

        public bool Status { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public Guid MedicalHistoryId { get; set; } // Foreign key trỏ về MedicalHistory
        public MedicalHistory MedicalHistory { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
