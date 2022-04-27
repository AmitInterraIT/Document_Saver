using Document_Saver.Models;

namespace JWTAuthentication.Controllers
{
    public interface JWTTokenServices
    {
        string BuildToken(string key, string issuer, User obj);
        bool IsTokenValid(string key, string issuer, string audience, string token);
    }
}