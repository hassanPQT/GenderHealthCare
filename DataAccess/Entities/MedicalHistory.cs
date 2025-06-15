using DataAccess.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class MedicalHistory
{
    [Key]
    public Guid MedicalHistoryId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [StringLength(500)]
    public string? Note { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }

    public ICollection<TestBooking> TestBookings { get; set; }

}