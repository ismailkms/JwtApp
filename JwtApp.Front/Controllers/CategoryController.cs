﻿using JwtApp.Front.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace JwtApp.Front.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public CategoryController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> List()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;

            if(token != null)
            {
                var client = _httpClientFactory.CreateClient();

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"{_configuration["BaseUrl"]}/categories");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<List<CategoryListModel>>(jsonData, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                    return View(result);
                }
            }
            return View();
        }
    }
}
