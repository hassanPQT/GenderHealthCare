using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public bool IsActive { get; set; }


        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
