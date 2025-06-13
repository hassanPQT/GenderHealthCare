using GenderHealcareSystem.Converters;
using System.Text.Json.Serialization;

namespace GenderHealcareSystem.DTO.Response
{
    public class UserUpdateResponse
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly? DateOfBirth { get; set; }
        public bool? Gender { get; set; }
        public string? Address { get; set; }
    }
}
