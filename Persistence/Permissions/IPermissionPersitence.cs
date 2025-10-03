using Entity;

namespace Persistence.Permissions
{
    public interface IPermissionPersitence
    {
        void InsertPermission(Permission permission);
        void InsertUserPermissions(int userId, List<int> permissionIds);
        void UpdatePermission(Permission permission);
        void UpdateUserPermissions(int userId, List<int> newPermissionIds);
        List<Permission> GetPermissions();
        Permission GetPermission(int id);
        List<UserPermission> GetUserPermissionById(int id);
    }
}
