using Auth.API.Models;

namespace Auth.API.Services.IServices
{
    public interface IJwtGenerator
    {
        string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
    }
}
