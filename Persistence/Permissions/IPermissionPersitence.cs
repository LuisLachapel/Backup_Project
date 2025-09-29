using Entity;

namespace Persistence.Permissions
{
    public interface IPermissionPersitence
    {
        void InsertPermission(Permission permission);
        List<Permission> GetPermissions();
    }
}
