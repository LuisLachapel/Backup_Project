using System.ComponentModel.DataAnnotations;
namespace Services.Notes.Models
{
    public class UpdateNoteModel
    {
        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(50, MinimumLength = 1,
        ErrorMessage = "El titulo debe tener entre {2} y {1} caracteres.")]
        public string title { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        [Required(ErrorMessage = "El usuario es requerido")]
        public int userId { get; set; }
    }
}
