namespace GenderHealcareSystem.DTO
{
    public class BlogDto
    {
        public Guid BlogId { get; set; }
        public string Tittle { get; set; }
        public string Content { get; set; }
        public DateTime PublistDate { get; set; }
        public UserDto Author { get; set; }
    }
}