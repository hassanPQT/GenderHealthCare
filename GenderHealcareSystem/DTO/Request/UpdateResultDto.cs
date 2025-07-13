using System.ComponentModel.DataAnnotations;

namespace GenderHealcareSystem.DTO.Request
{
    public class UpdateResultDto
    {
        [MaxLength(200)]
        public string? ResultDetail { get; set; }

        public DateTime? SampleReceivedDate { get; set; }

        public DateTime? ResultDate { get; set; }

        public bool Status { get; set; }
    }
}
