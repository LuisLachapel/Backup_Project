using Services.Permissions.Models;
using Entity;
namespace Services.Permissions
{
    public interface IPermissionService
    {
        void Insert(CreatePermissionModel model);
        void Update(int id, UpdatePermissionModel model);

        List<Permission> GetAll();
        Permission GetById(int id);

    }
}
