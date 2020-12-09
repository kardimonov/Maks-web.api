using GolovinskyAPI.Logic.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;

namespace GolovinskyAPI.Logic.Handlers
{
    public class AuthHandler : IAuthHandler
    {
        public ClaimsIdentity GetIdentity(string userName, int userId, string role)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role),
                    new Claim("user_id", userId.ToString())
                };
            ClaimsIdentity claimsIdentity = new(
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
            );

            return claimsIdentity;
        }
    }
}