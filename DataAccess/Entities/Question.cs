using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Question
    {

        [Key]
        public Guid QuestionId { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Content { get; set; }

        [MaxLength(500)]
        public string Answer { get; set; }

        public DateTime? AnswerDate { get; set; }
        public DateTime PublishDate { get; set; }

        public Guid UserId { get; set; }

        public Guid? ConsultantId { get; set; }

        //[ForeignKey("UserId")]
        public User User { get; set; }

        //[ForeignKey("ConsultantId")]
        public User Consultant { get; set; }
        public bool IsActive { get; set; }
    }
}
