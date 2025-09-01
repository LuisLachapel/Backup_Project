using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class User
    {
        public int id { get; set; }
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [StringLength(50, ErrorMessage = "Solo se permite un maximo de 50 caracteres")]
        public string name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El cargo es requerido")]
        public int positionId { get; set; }
        public Position? position { get; set; }

        public DateTime creationDate { get; set; }
        public Collection<Note>? notes { get; set; }



    }
}
