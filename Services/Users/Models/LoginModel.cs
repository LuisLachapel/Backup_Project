using System.ComponentModel.DataAnnotations;

namespace Services.Users.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="El id es requerido")]
        public int userId { get; set; }
        [Required(ErrorMessage = "la contraseña es requerida")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Solo se permite un minimo de 8 caracteres")]
        public string password { get; set; } = string.Empty;
    }
}
