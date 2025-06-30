using GenderHealcareSystem.Converters;
using System.Text.Json.Serialization;

namespace GenderHealcareSystem.DTO.Request
{
    public class UpdateUserRequest
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly? DateOfBirth { get; set; }
        public bool? Gender { get; set; }
    }
}
