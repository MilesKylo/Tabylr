namespace Tabylr.Client.Models.Responses
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        public User User { get; set; }
    }
}
