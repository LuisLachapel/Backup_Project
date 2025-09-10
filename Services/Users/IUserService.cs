using Services.Users.Models;
using Persistence.Users;

namespace Services.Users
{
    public interface IUserService
    {
        int DeleteUser(int id);
        List<GetAllUserModel> GetAllUsers();

        GetAllUserModel? GetUserById(int id);

        void InsertUser(CreateUserModel user);
        void UpdateUser(int id, CreateUserModel user);

        List<UserNotesSummary> GetUserNotesSummaries(DateTime? startDate, DateTime? endDate);


    }
}
