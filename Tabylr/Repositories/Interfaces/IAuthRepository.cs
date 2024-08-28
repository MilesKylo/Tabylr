using Tabylr.Client.Models.Responses;
using Tabylr.Services;

namespace Tabylr.Repositories.Interfaces
{
    public interface IAuthRepository

    {
        Task<AuthResult> LoginAsync(string email, string password);
        Task<AuthResult> RegisterAsync(string email, string password);
        Task<AuthResult> LogoutAsync();
    }
}
