using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenderHealcareSystem.DTO
{
    public class TestBookingServiceDto
    {
        public Guid Id { get; set; }

        public Guid TestBookingId { get; set; }

        public Guid ServiceId { get; set; }

        public TestBooking TestBooking { get; set; }
        public Service Service { get; set; }
        //public ICollection<TestResult> TestResults { get; set; }
    }
}
