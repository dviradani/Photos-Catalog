using System.ComponentModel.DataAnnotations;

namespace MyPhotosCatalog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PhotoId { get; set; }
        [StringLength(50)]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "You must write content")]
        public string Content { get; set; } = null!;
        public virtual Photo? Photo { get; set; }
    }
}
