using System.ComponentModel.DataAnnotations;

namespace MyPhotosCatalog.Models.ViewModels
{
    public class LoginModel
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
