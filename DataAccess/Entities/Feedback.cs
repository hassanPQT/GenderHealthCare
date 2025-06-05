using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Feedback
    {
        [Key]
        public Guid FeedbackId { get; set; }

        public Guid ServiceId { get; set; }
        [MaxLength(50)]
        public string Tittle { get; set; }

        [MaxLength(100)]
        public string Content { get; set; }

        public int Rating { get; set; }
        public DateTime PublistDate { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<Service> Services { get; set; }

        public bool isActive { get; set; }
    }
}
