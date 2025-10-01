using Entity;

namespace Persistence.Permissions
{
    public interface IPermissionPersitence
    {
        void InsertPermission(Permission permission);
        void InsertUserPermissions(int userId, List<int> permissionIds);
        void UpdatePermission(Permission permission);
        List<Permission> GetPermissions();
        Permission GetPermission(int id);
    }
}
