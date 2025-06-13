using System.ComponentModel.DataAnnotations;

namespace GenderHealcareSystem.DTO.Request
{
    public class AddBlogRequest
    {
        public Guid BlogId { get; set; }
        [Required(ErrorMessage = "Tittle is required")]
        public string Tittle { get; set; }
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
        public DateTime PublistDate { get; set; }
        [Required(ErrorMessage = "AuthorId is required")]
        public Guid UserId { get; set; }
    }
}