using JwtApp.Back.Dtos;
using JwtApp.Back.Entities;
using JwtApp.Back.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtApp.Back.Concrete
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenResponseDto GenerateToken(User user)
        {
            
            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(user.Role))
                claims.Add(new Claim(ClaimTypes.Role, user.Role));

            if (!string.IsNullOrEmpty(user.Username))
                claims.Add(new Claim(ClaimTypes.Name, user.Username));

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:IssuerSigningKey"]));

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expireDate = DateTime.UtcNow.AddDays(double.Parse(_configuration["Token:Expire"]));

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: _configuration["Token:Issuer"], audience: _configuration["Token:Audience"], claims: claims, notBefore: DateTime.UtcNow, expires: expireDate, signingCredentials: credentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string token = handler.WriteToken(jwtSecurityToken);

            return new TokenResponseDto()
            {
                Token = token,
                ExpireDate = expireDate
            };
        }
    }
}
