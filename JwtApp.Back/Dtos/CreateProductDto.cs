﻿namespace JwtApp.Back.Dtos
{
    public class CreateProductDto
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
