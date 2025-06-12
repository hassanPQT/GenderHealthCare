namespace GenderHealcareSystem.DTO.Response
{
    public class UserResponse
    {
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public DateOnly? DateOfBirth { get; set; }
        public bool? Gender { get; set; }
        public string? Address { get; set; }
    }
}
