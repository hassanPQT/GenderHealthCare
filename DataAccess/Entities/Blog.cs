using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Blog
    {
        [Key]
        public Guid BlogId { get; set; }

        [MaxLength(50)]
        public string Tittle { get; set; }

        [MaxLength(500)]
        public string Content { get; set; }
        public DateTime PublistDate { get; set; }

        [MaxLength(10)]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
