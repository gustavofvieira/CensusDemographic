using CensusDemographic.Domain.Models;
using CensusDemographic.Domain.Models.Auth;
using CensusDemographic.Domain.ViewModels;

namespace CensusDemographic.Domain.Interfaces.Services
{
    public interface IUserService : IBaseService<User>
    {
        //Task<User> GetUserByEmail(string email);
        Task<Token> AuthenticateAsync(LoginVM loginVM);
        //Task RecoverEmail(string email);
        //Task UpdatePassword(Guid id, string password);
    }
}
