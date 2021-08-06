using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {
        public async static Task<int> GetUserIdFromUsername(this UserManager<AppUser> userManager, string username)
        {
            var user = await userManager.FindByNameAsync(username);

            if(user != null) return user.Id;

            return -1;
        }

        public async static Task<bool> IsInRoleUsername(this UserManager<AppUser> userManager, string username, string roleName)
        {
            var user = await userManager.FindByNameAsync(username);

            return await userManager.IsInRoleAsync(user, roleName);
        }
    }
}