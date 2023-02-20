using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace MyPhotosCatalog.Models
{
    public class Photo
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; } = null!;
        public string? PictureName { get; set; }
        [Required]
        [MaxLength(20)]
        public string Owner { get; set; } = null!;
        [Required]
        public int CategoryId { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
        public virtual Category? Category { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }
    }
}
