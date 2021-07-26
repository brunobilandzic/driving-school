using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUsername(this ClaimsPrincipal claims)
         {
             return claims.FindFirst(ClaimTypes.Name)?.Value;
         }
    }
}