using JwtApp.Back.Entities;
using JwtApp.Back.Interfaces;

namespace JwtApp.Back.Concrete
{
    public class ProductService : IProductService
    {
        public List<Product> GetAll()
        {
            return new()
            {
                new() { Id = 1, Name = "Telefon", Price = 10000, Stock = 100 },
                new() { Id = 2, Name = "Bilgisayar", Price = 30000, Stock = 50 },
                new() { Id = 3, Name = "Mont", Price = 1000, Stock = 200 },
                new() { Id = 4, Name = "Şapka", Price = 500, Stock = 300 }
            };
        }

        public Product? GetById(int id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }
    }
}
