using JwtApp.Back.Entities;

namespace JwtApp.Back.Interfaces
{
    public interface IUserService
    {
        List<User> GetAll();
        User? CheckUser(string username, string password);
    }
}
