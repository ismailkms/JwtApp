using JwtApp.Back.Entities;

namespace JwtApp.Back.Interfaces
{
    public interface IProductService
    {
        List<Product>GetAll();
        Product? GetById(int id);
    }
}
