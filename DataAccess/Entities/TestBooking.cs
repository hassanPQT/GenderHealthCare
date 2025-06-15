using DataAccess.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class TestBooking
{
    [Key]
    public Guid TestBookingId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid MedicalHistoryId { get; set; }

    public Guid? StaffId { get; set; }

    [Required]
    public DateTime BookingDate { get; set; }
    
    [Required, MaxLength(10)]
    public string Status { get; set; } 

    [MaxLength(500)]
    public string Note { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("TestBookings")]
    public User User { get; set; }

    [ForeignKey("BookingStaffId")]
    public User BookingStaff { get; set; }

    [ForeignKey("MedicalHistoryId")]
    public MedicalHistory MedicalHistory { get; set; }

    public ICollection<TestBookingService> TestBookingServices { get; set; }
}