using BCrypt.Net;

namespace Services.Password
{
    public class PasswordService: IPasswordService
    {
        public string hashpassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
            

        }

        public bool verify(string password, string hashedpassword) 
        {
            Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("123456789"));
            return BCrypt.Net.BCrypt.Verify(password, hashedpassword);
            
        }
        
    }
}
