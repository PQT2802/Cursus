using Cursus_Data.Models.DTOs;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Cursus_Business.Common
{
    public class TokenHelper
    {
        private static TokenHelper instance;
        public static TokenHelper Instance
        {
            get { if (instance == null) instance = new TokenHelper(); return Common.TokenHelper.instance; }
            private set { Common.TokenHelper.instance = value; }
        }
        public async Task<CurrentUserObject> GetThisUserInfo(HttpContext httpContext)
        {
            CurrentUserObject currentUser = new();

            var checkUser = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (checkUser != null)
            {
                currentUser.UserId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
                var roleClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (int.TryParse(roleClaim, out int roleId))
                {
                    currentUser.RoleId = roleId;
                }
                else
                {
                    currentUser.RoleId = -1;
                }
                currentUser.Email = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                currentUser.Fullname = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
                currentUser.Phone = httpContext.User.Claims.FirstOrDefault(c => c.Type == "Phone").Value;
                return currentUser;
            }
            else
            {
                return null;
            }
        }
    }
}
