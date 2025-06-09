namespace GenderHealcareSystem.DTO.Response
{
    public class LoginResponse
    {
        public string? Username { get; set; }
        public string? AccessToken { get; set; } 

        public string? RefreshToken { get; set; }
        public int ExpiresIn { get; set; } 
    }
}
