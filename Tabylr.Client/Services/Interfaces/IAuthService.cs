using Tabylr.Client.Models.Responses;

namespace Tabylr.Client.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(string email, string password);
        Task<AuthResult> LogoutAsync();
        Task<AuthResult> RegisterAsync(string email, string password);
    }
}