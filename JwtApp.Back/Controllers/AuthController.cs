using JwtApp.Back.Dtos;
using JwtApp.Back.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtApp.Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("[action]")]
        public IActionResult Login([FromBody] CheckUserDto checkUserDto)
        {
            var user = _userService.CheckUser(checkUserDto.Username, checkUserDto.Password);

            if (user == null)
                return BadRequest("Kullanıcı adı veya şifre hatalı");

            return Created("", _tokenService.GenerateToken(user));
        }
    }
}
