
using Persistence.Permissions;
using Entity;
using Services.Permissions.Models;

namespace Services.Permissions
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionPersitence _function;

        public PermissionService(IPermissionPersitence function)
        {
            _function = function;
        }
        public List<Permission> GetAll()
        {
           return _function.GetPermissions();
        }

        public void Insert(CreatePermissionModel model)
        {
            var permission = new Permission
            {
                name = model.name,
                code = model.code,
                description = model.description
            };

            _function.InsertPermission(permission);

        }

        public void Update(int id, CreatePermissionModel model)
        {
            var permission = new Permission
            {
                id = id,
                name = model.name,
                code = model.code,
                description = model.description
            };
            _function.UpdatePermission(permission);
        }
    }
}
