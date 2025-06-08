using DataAccess.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class MedicalHistory
{
    [Key]
    public Guid MedicalHistoryId { get; set; } 
    public Guid ServiceId { get; set; }
    public Guid UserId { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    [ForeignKey("ServiceId")]
    public Service Service { get; set; }
    public ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
}