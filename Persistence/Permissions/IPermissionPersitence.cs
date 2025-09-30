using Entity;

namespace Persistence.Permissions
{
    public interface IPermissionPersitence
    {
        void InsertPermission(Permission permission);
        void UpdatePermission(Permission permission);
        List<Permission> GetPermissions();
    }
}
