using DataAccess.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Feedback
{
    [Key]
    public Guid FeedbackId { get; set; }
    [Required, MaxLength(50)]
    public string Title { get; set; } 
    [Required, MaxLength(100)]
    public string Content { get; set; }
    [Range(1, 5)]
    public int Rating { get; set; }
    [Required]
    public DateTime PublishDate { get; set; }
    public Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    public Guid ServiceId { get; set; }
    [ForeignKey("ServiceId")]
    public Service Service { get; set; }
    public bool IsActive { get; set; } 
}