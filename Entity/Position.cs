
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Position
    {
        public int id {  get; set; }
        [Required(ErrorMessage = "el nombre de la posición no puede estar vacio.")]
        [StringLength(20,MinimumLength =1,ErrorMessage = "El nombre de la posición debe tener entre {2} y {1} caracteres.")]
        public string name { get; set; } = String.Empty;

        [StringLength(50,MinimumLength = 1,ErrorMessage = "")]
        public string description { get; set; } = String.Empty;
    }
}
