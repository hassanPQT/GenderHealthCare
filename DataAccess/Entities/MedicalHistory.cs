using DataAccess.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class MedicalHistory
{
    [Key]
    public Guid MedicalHistoryId { get; set; } 
    public Guid UserId { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }

    public ICollection<TestBooking> TestBookings { get; set; } = new List<TestBooking>();

}