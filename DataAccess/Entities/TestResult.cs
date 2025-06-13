using DataAccess.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class TestResult
{
    [Key]
    public Guid TestResultId { get; set; }
    [Required, MaxLength(50)]
    public string ResultDetail { get; set; }
    [Required]
    public DateTime TestDate { get; set; }
    [Required]
    public DateTime SampleReceivedDate { get; set; }
    [Required]
    public DateTime ResultDate { get; set; }
    public bool Status { get; set; } 
    public Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    public Guid ServiceId { get; set; }
    [ForeignKey("ServiceId")]
    public Service Service { get; set; }
    public Guid TestBookingId { get; set; }

    [ForeignKey("TestBookingId")]
    public TestBooking TestBooking { get; set; }
}