using JwtApp.Back.Dtos;
using JwtApp.Back.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtApp.Back.Controllers
{
    [Authorize(Roles = "Admin,Member")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult List()
        {
            var result = _productService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _productService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(CreateProductDto dto)
        {
            //Eklendi
            return Created("", dto);
        }

        [HttpPut]
        public IActionResult Update(UpdateProductDto dto)
        {
            //Güncellendi
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //Silindi
            return Ok(id);
        }
    }
}
