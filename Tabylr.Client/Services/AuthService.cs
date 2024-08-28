using System.Net.Http.Json;
using System.Text.Json;
using Tabylr.Client.Models.Requests;
using Tabylr.Client.Models.Responses;
using Tabylr.Client.Services.Interfaces;

namespace Tabylr.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly CustomAuthStateProvider _authStateProvider;

        public AuthService(HttpClient httpClient, CustomAuthStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
        }

        public async Task<AuthResult> LoginAsync(string email, string password)
        {

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/login", new LoginRequest{ Email = email, Password = password });

                response.EnsureSuccessStatusCode();

                var contentString = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(contentString))
                {
                    return new AuthResult { Success = false, ErrorMessage = "Server returned an empty response" };
                }

                var result = await response.Content.ReadFromJsonAsync<AuthResult>();

                if (result == null)
                {
                    return new AuthResult { Success = false, ErrorMessage = "Failed to deserialize server response" };
                }

                if (result.Success)
                {
                    await ((CustomAuthStateProvider)_authStateProvider).LoginAsync(email, password);
                    
                }

                
                
               

                return result;

                //return result;

            }
            catch (HttpRequestException ex)
            {
                return new AuthResult { Success = false, ErrorMessage = $"HTTP request failed: {ex.Message}" };
            }
            catch (JsonException ex)
            {
                return new AuthResult { Success = false, ErrorMessage = $"JSON parsing failed: {ex.Message}" };
            }
            catch (Exception ex)
            {
                return new AuthResult { Success = false, ErrorMessage = $"An unexpected error occurred: {ex.Message}" };
            }
        }

        public async Task<AuthResult> RegisterAsync(string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", new { Email = email, Password = password });
            return await response.Content.ReadFromJsonAsync<AuthResult>();
        }

        public async Task<AuthResult> LogoutAsync()
        {
            var response = await _httpClient.PostAsync("api/auth/logout", null);
            var result = await response.Content.ReadFromJsonAsync<AuthResult>();
            if (result.Success)
            {
                await ((CustomAuthStateProvider)_authStateProvider).LogoutAsync();
            }
            return result;
        }
    }
}
