using BCrypt.Net;

namespace Services.Password
{
    public class PasswordService
    {
        public string hashpassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool verify(string password, string hashedpassword) 
        { 
            return BCrypt.Net.BCrypt.Verify(password, hashedpassword);
        }
        
    }
}
