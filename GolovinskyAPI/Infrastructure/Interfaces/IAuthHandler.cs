using System.Security.Claims;

namespace GolovinskyAPI.Infrastructure.Interfaces
{
    public interface IAuthHandler
    {
        ClaimsIdentity GetIdentity(string userName, int userId, string role);
    }
}
