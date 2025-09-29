using Services.Permissions.Models;
using Entity;
namespace Services.Permissions
{
    public interface IPermissionService
    {
        void Insert(CreatePermissionModel model);

        List<Permission> GetAll();

    }
}
