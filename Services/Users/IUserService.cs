using Services.Users.Models;
using Entity;
using Persistence.Users;

namespace Services.Users
{
    public interface IUserService
    {
        int DeleteUser(int id);
        ResponseModel GetAllUsers();

        ResponseModel GetUserById(int id);

        ResponseModel InsertUser(CreateUserModel user);
        void UpdateUser(int id, UpdateUserModel user);

        List<UserNotesSummary> GetUserNotesSummaries(DateTime? startDate, DateTime? endDate);
        ResponseModel Login(int userId, string password);


    }
}
