using DataAccess.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class TestBooking
{
    [Key]
    public Guid TestBookingId { get; set; }
    public Guid ServiceId { get; set; }
    public Guid UserId { get; set; }
    public Guid? StaffId { get; set; }
    [Required]
    public DateTime BookingDate { get; set; }
    
    [Required, MaxLength(10)]
    public string Status { get; set; } 
    [MaxLength(100)]
    public string Note { get; set; }
    [ForeignKey("UserId")]
    [InverseProperty("TestBookings")]
    public User User { get; set; }
    [ForeignKey("ServiceId")]
    public Service Service { get; set; }
    [ForeignKey("StaffId")]
    [InverseProperty("HandledTestBookings")]

    public Guid MedicalHistoryId { get; set; }

    [ForeignKey("MedicalHistoryId")]
    public MedicalHistory MedicalHistory { get; set; }

    public ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
    public User Staff { get; set; }
}