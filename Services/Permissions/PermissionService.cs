
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

        public Permission GetById(int id)
        {
            var permission = _function.GetPermission(id);
            return permission;
        }

        public List<GetUserPermissionModel> GetUserPermissionById(int id)
        {
            var userPermissions = _function.GetUserPermissionById(id);
            var model = userPermissions.Select(u => new GetUserPermissionModel{
                id = u.id,
                permissionId = u.permissionId
            }).ToList();

            return model;
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

        public void InsertUserPermission(CreateUserPermissionModel model)
        {
            _function.InsertUserPermissions(model.userId,model.permissionIds);
        }

        public void Update(int id, UpdatePermissionModel model)
        {
            var permission = new Permission
            {
                id = id,
                name = model.name,
                code = model.code,
                description = model.description,
                active = model.active
            };
            _function.UpdatePermission(permission);
        }

        public void UpdateUserPermission(CreateUserPermissionModel model)
        {
            _function.UpdateUserPermissions(model.userId, model.permissionIds);
        }
    }
}
