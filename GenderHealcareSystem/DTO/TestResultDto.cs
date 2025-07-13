using BusinessAccess.Services.Implements;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenderHealcareSystem.DTO
{
    public class TestResultDto
    {
        public Guid TestResultId { get; set; }

        public Guid TestBookingServiceId { get; set; }

        public string TestName { get; set; }

        public string? ResultDetail { get; set; }

        public DateTime TestDate { get; set; }

        public DateTime SampleReceivedDate { get; set; }

        public DateTime ResultDate { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public bool Status { get; set; }
        public TestBookingService TestBookingService { get; set; }
    }
}
