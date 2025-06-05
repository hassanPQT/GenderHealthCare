using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class MedicalHistory
    {
        [Key]
        public Guid MedicallHistoryId { get; set; }

        public Guid ServiceId { get; set; }


        public Guid UserId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Guid? TestResultId { get; set; }

        public User User { get; set; }

        [ForeignKey("ServiceId")]
        public Service Service { get; set; }

        //[ForeignKey("TestResultId")]
        //public TestResult TestResult { get; set; }

        public ICollection<TestResult> TestResults { get; set; }
    }
}
