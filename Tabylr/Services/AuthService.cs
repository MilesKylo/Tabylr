using System;
using System.Threading.Tasks;
using Tabylr.Models;
using Tabylr.Repositories;
using Tabylr.Services.Interfaces;
using Tabylr.Repositories.Interfaces;
using Tabylr.Client.Models.Responses;

namespace Tabylr.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            try
            {
                var result = await _authRepository.LoginAsync(email, password);
                if (result.Success)
                {
                    return new AuthResult { Success = true, Token = result.Token, User = result.User };
                }
                return new AuthResult { Success = false, ErrorMessage = "Invalid email or password" };
            }
            catch (Exception ex)
            {
                return new AuthResult { Success = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<AuthResult> RegisterAsync(string email, string password)
        {
            try
            {
                var result = await _authRepository.RegisterAsync(email, password);
                if (result.Success)
                {
                    return new AuthResult { Success = true, User = result.User };
                }
                return new AuthResult { Success = false, ErrorMessage = "Registration failed" };
            }
            catch (Exception ex)
            {
                return new AuthResult { Success = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<AuthResult> LogoutAsync()
        {
            try
            {
                var result = await _authRepository.LogoutAsync();
                return new AuthResult { Success = result.Success, ErrorMessage = result.ErrorMessage };
            }
            catch (Exception ex)
            {
                return new AuthResult { Success = false, ErrorMessage = ex.Message };
            }
        }
    }

    //public class AuthResult
    //{
    //    public bool Success { get; set; }
    //    public string ErrorMessage { get; set; }
    //    public string Token { get; set; }
    //    public User User { get; set; }
    //}
}
