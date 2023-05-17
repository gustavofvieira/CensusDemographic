using CensusDemographic.Domain.Interfaces.Services;
using CensusDemographic.Domain.Models;
using CensusDemographic.Domain.Models.Auth;
using CensusDemographic.Domain.ViewModels;

namespace CensusDemographic.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseService<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<User> AuthenticateAsync(LoginVM loginVM);
        Task UpdatePassword(User user);
    }
}
