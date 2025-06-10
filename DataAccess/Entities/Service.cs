using System.ComponentModel.DataAnnotations;

public class Service
{
    [Key]
    public Guid ServiceId { get; set; }
    [Required, MaxLength(50)] 
    public string ServiceName { get; set; }
    [MaxLength(500)]
    public string Description { get; set; }
    [Required]
    public double Price { get; set; }
    public bool IsActive { get; set; }
    public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    public ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
    public ICollection<TestBooking> TestBookings { get; set; } = new List<TestBooking>();
    public ICollection<MedicalHistory> MedicalHistories { get; set; } = new List<MedicalHistory>();
}