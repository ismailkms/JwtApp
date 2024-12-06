using JwtApp.Back.Dtos;
using JwtApp.Back.Entities;

namespace JwtApp.Back.Interfaces
{
    public interface ITokenService
    {
        TokenResponseDto GenerateToken(User user);
    }
}
