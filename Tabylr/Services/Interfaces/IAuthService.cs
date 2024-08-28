using Tabylr.Client.Models.Responses;

namespace Tabylr.Services.Interfaces
{
    public interface IAuthService
    {
        
            Task<AuthResult> LoginAsync(string email, string password);
            Task<AuthResult> RegisterAsync(string email, string password);
            Task<AuthResult> LogoutAsync();
        
    }
}
