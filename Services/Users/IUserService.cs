using Services.Users.Models;

namespace Services.Users
{
    public interface IUserService
    {
        int DeleteUser(int id);
        List<GetAllUserModel> GetAllUsers();

        GetAllUserModel GetUserById(int id);

        void InsertUser(CreateUserModel user);
        void UpdateUser(int id, CreateUserModel user);

    }
}
