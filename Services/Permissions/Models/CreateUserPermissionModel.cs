using System.ComponentModel.DataAnnotations;
namespace Services.Permissions.Models
{
    public class CreateUserPermissionModel
    {
        [Required(ErrorMessage ="El usuario es requerido")]
        public int userId {  get; set; }

        [Required(ErrorMessage = "Los permisos del usuario son requeridos")]
        public List<int> permissionIds { get; set; } = new();
    }
}
