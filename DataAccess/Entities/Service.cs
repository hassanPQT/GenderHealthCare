using DataAccess.Entities;
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
    public bool IsActive { get; set; } = true;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public ICollection<TestBookingService> TestBookingServices { get; set; }
    public ICollection<Feedback> Feedbacks { get; set; }
}