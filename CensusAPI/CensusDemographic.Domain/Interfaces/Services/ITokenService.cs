using CensusDemographic.Domain.Models;
using CensusDemographic.Domain.Models.Auth;

namespace CensusDemographic.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        Token GenerateToken(User user);
    }
}
