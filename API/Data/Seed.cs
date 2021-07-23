using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedData(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            // if there are already records in database skip seeding data
            if (await userManager.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            if(users == null) return;
            var roles = new List<AppRole>
            {
                new AppRole {Name="Admin"},
                new AppRole {Name="Student"},
                new AppRole {Name="Professor"},
                new AppRole {Name="Instructor"},
                new AppRole {Name="Examiner"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }


            for (int i = 0; i < users.Count; i++)
            {
                await userManager.CreateAsync(users[i], "password");
                if(i > 0 && i < 3 ) {
                    await userManager.AddToRoleAsync(users[i], "Professor");
                    if(i == 2) {
                        await userManager.AddToRoleAsync(users[i], "Instructor");
                    }
                } else if(i == 3) {
                    await userManager.AddToRoleAsync(users[i], "Instructor");
                } else if(i == 4) {
                    await userManager.AddToRoleAsync(users[i], "Examiner");
                } else {
                    await userManager.AddToRoleAsync(users[i], "Student");
                }
            }

                var admin = new AppUser
                {
                    UserName = "admin"
                };

                await userManager.CreateAsync(admin, "password");
                await userManager.AddToRoleAsync(admin, "Admin");    
        }
    }
}