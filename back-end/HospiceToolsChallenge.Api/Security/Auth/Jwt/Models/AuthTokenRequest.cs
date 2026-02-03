namespace HospiceToolsChallenge.Api.Security.Auth.Jwt.Models
{
    public class AuthTokenRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
