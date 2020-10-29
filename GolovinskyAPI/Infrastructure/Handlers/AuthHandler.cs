using GolovinskyAPI.Infrastructure.Interfaces;
using GolovinskyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GolovinskyAPI.Infrastructure.Handlers
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
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
            );

            return claimsIdentity;
        }
    }
}
