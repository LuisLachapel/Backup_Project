using Entity;

namespace Persistence.Users
{
    public interface IUserPersistence
    {
        int DeleteUser(int id);
        List<User> GetAllUser();
        List<UserNotesSummary> GetAllSumary(DateTime? startDate, DateTime? endDate);
        User? GetById(int id);
        int InsertUser(User user);
        void UpdateUser(User user);
    }
}
