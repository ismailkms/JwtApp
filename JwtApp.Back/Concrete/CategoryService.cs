using JwtApp.Back.Entities;
using JwtApp.Back.Interfaces;

namespace JwtApp.Back.Concrete
{
    public class CategoryService : ICategoryService
    {
        public List<Category> GetAll()
        {
            return new()
            {
                new() { Id = 1, Name = "Elektronik" },
                new() { Id = 2, Name = "Giyim" },
            };
        }
    }
}
