using System.ComponentModel.DataAnnotations;

namespace Services.Permissions.Models
{
    public class CreatePermissionModel
    {
        [Required(ErrorMessage ="El nombre es requerido")]
        [StringLength(50,ErrorMessage ="El maximo de caracteres de este campo es de 50")]
        public string name { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "El maximo de caracteres de este campo es de 100")]
        public string description { get; set; } = string.Empty ;

        [Required(ErrorMessage = "El codigo es requerido")]
        [StringLength(25, ErrorMessage = "El maximo de caracteres de este campo es de 25")]
        public string code {  get; set; } = string.Empty ;
    }
}
