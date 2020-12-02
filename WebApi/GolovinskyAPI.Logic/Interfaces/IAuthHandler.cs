using System.Security.Claims;

namespace GolovinskyAPI.Logic.Interfaces
{
    public interface IAuthHandler
    {
        ClaimsIdentity GetIdentity(string userName, int userId, string role);
    }
}