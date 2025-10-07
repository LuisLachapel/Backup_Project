using Persistence.Permissions;

namespace Services.Authorize
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IPermissionPersitence _function;

        public AuthorizationService(IPermissionPersitence function)
        {
            _function = function;
        }

        public bool UserHasPermission(int userId, string code)
        {
           var permissions = _function.GetAuthorization(userId);
            return permissions.Any(p => p.code == code);

        }
    }
}
