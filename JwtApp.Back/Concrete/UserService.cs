using JwtApp.Back.Entities;
using JwtApp.Back.Interfaces;

namespace JwtApp.Back.Concrete
{
    public class UserService : IUserService
    {
        public List<User> GetAll()
        {
            return new()
            {
                new() { Id = 1, Username = "admin", Password = "123", Role = "Admin" },
                new() { Id = 2, Username = "member", Password = "123", Role = "Member" }
            };
        }

        public User? CheckUser(string username, string password)
        {
            return GetAll().FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
