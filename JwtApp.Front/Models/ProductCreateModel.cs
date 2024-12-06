using System.ComponentModel.DataAnnotations;

namespace JwtApp.Front.Models
{
    public class ProductCreateModel
    {
        [Required(ErrorMessage = "Product adı boş geçilemez")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Price boş geçilemez")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock boş geçilemez")]
        public int Stock { get; set; }
    }
}
