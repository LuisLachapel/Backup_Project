
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Note
    {
        public int id {  get; set; }

        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(50, MinimumLength = 1,
        ErrorMessage = "El titulo debe tener entre {2} y {1} caracteres.")]
        public string title { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;
        public DateTime date { get; set; }
        public int userId { get; set; }
        public User? user { get; set; }
    }
}
