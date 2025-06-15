using DataAccess.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class TestResult
{
    [Key]
    public Guid TestResultId { get; set; }

    [Required]
    public Guid TestBookingServiceId { get; set; }

    [Required]
    [MaxLength(50)]
    public string TestName { get; set; }

    [MaxLength(50)]
    public string? ResultDetail { get; set; }

    [Required]
    public DateTime TestDate { get; set; }

    [Required]
    public DateTime SampleReceivedDate { get; set; }
    [Required]
    public DateTime ResultDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public bool Status { get; set; }
    [ForeignKey("TestBookingServiceId")]
    public TestBookingService TestBookingService { get; set; }
}