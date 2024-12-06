using JwtApp.Back.Entities;

namespace JwtApp.Back.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetAll();
    }
}
