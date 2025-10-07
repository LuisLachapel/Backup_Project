
namespace Services.Authorize
{
    public interface IAuthorizationService
    {
        bool UserHasPermission(int  userId, string code);
    }
}
