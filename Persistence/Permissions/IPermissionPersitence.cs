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
        List<Permission> GetAuthorization(int id);
        Permission GetPermission(int id);
        List<UserPermission> GetUserPermissionById(int id);
    }
}
