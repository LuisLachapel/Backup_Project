
namespace Services.Password
{
    public interface IPasswordService
    {
        string hashpassword(string password);

        bool verify(string password, string hashedpassword);
    }
}
