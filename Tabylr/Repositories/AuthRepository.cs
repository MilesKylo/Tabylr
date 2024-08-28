using Supabase;
using Tabylr.Models;
using System.Threading.Tasks;
using Tabylr.Repositories.Interfaces;
using Tabylr.Services;
using Tabylr.Client.Models.Responses;
using Tabylr.Client.Models;

namespace Tabylr.Repositories
{
    

    public class AuthRepository : IAuthRepository
    {
        private readonly Supabase.Client _supabaseClient;

        public AuthRepository(Supabase.Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            try
            {
                var session = await _supabaseClient.Auth.SignIn(email, password);
                if (session != null)
                {
                    return new AuthResult 
                    { 
                        Success = true, 
                        Token = session.AccessToken,
                        User = new User { Email = email } // You might want to fetch more user details if available
                    };
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
                var response = await _supabaseClient.Auth.SignUp(email, password);
                if (response.User != null)
                {
                    return new AuthResult 
                    { 
                        Success = true,
                        User = new User { Email = email } // You might want to map more properties if available
                    };
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
                await _supabaseClient.Auth.SignOut();
                return new AuthResult { Success = true };
            }
            catch (Exception ex)
            {
                return new AuthResult { Success = false, ErrorMessage = ex.Message };
            }
        }
    }
}

