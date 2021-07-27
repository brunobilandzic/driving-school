using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedData(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, DataContext context)
        {

            // if there are already records in database skip seeding data
            if (await userManager.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/SeedJson/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            foreach (var user in users)
            {
                user.FirstName = user.UserName;
                user.UserName = user.UserName.ToLower();
            }
            if (users == null) return;
            var roles = new List<AppRole>
            {
                new AppRole {Name="Admin"},
                new AppRole {Name="Student"},
                new AppRole {Name="Professor"},
                new AppRole {Name="Instructor"},
                new AppRole {Name="Examiner"},
                new AppRole {Name="Member"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
            // Here we are setting every seeded user as member
            // First two as a professor, but one professor is also instructor
            // One more instructor
            // One examiner
            // Six students

            for (int i = 0; i < users.Count; i++)
            {
                await userManager.CreateAsync(users[i], "password");
                await userManager.AddToRoleAsync(users[i], "Member");
                if (i > 0 && i < 3)
                {
                    await userManager.AddToRoleAsync(users[i], "Professor");
                    if (i == 2)
                    {
                        await userManager.AddToRoleAsync(users[i], "Instructor");
                    }
                }
                else if (i == 3)
                {
                    await userManager.AddToRoleAsync(users[i], "Instructor");
                }
                else if (i == 4)
                {
                    await userManager.AddToRoleAsync(users[i], "Examiner");
                }
                else
                {
                    await userManager.AddToRoleAsync(users[i], "Student");
                }
            }

            var admin = new AppUser
            {
                UserName = "admin"
            };

            await userManager.CreateAsync(admin, "password");
            await userManager.AddToRoleAsync(admin, "Admin");


            //***USER SEED DONE***//

            // Some lorem text for later

            var loremText = await System.IO.File.ReadAllTextAsync("Data/SeedJson/Lorem.json");
            var lorem = JsonSerializer.Deserialize<List<DummySmart>>(loremText);

            // Here we seed tables that only have primary key required, 
            // and other fields are not that important for development
            var regulationsGroupData = new List<RegulationsGroup>();
            var regulationTestData = new List<RegulationsTest>();

            // Adding 2 regulatons groups
            // And 2 regulation test          

            for (var i = 0; i < 2; i++)
            {
                regulationsGroupData.Add(new RegulationsGroup());
                regulationTestData.Add(new RegulationsTest());
                
                await context.RegulationsGroups.AddAsync(regulationsGroupData.Last());
                await context.RegulationsTests.AddAsync(regulationTestData.Last());
            }

            await context.SaveChangesAsync();

            // For seeding other data we need all users for all roles
            // So we dont e.g populate Professor field in Lecture with
            // user who doesnt have professor role

            var adminUsers = await userManager.GetUsersInRoleAsync("Admin");
            var professorUsers = await userManager.GetUsersInRoleAsync("Professor");
            var InstructorUsers = await userManager.GetUsersInRoleAsync("Instructor");
            var studentUsers = await userManager.GetUsersInRoleAsync("Student");

            var adminIds = adminUsers.Select(a => a.Id).ToList();
            var professorIds = professorUsers.Select(a => a.Id).ToList();
            var instructorIds = InstructorUsers.Select(a => a.Id).ToList();
            var studentIds = studentUsers.Select(a => a.Id).ToList();
        
            // Adding 15 * 5 driving sessions
            // 7 per day
            

            var drivingSessionData = new List<DrivingSession>();
            var initialDateStart = DateTime.Now.AddDays(5);
            for (var i=0; i < studentUsers.Count * 15; i++)
            {
                if(i % 7 == 0) initialDateStart = initialDateStart.AddDays(1);

                drivingSessionData.Add(
                    new DrivingSession
                    {
                    InstructorId = getRandomId(instructorIds),
                    DriverId =  studentIds[i % 5],
                    DateStart = initialDateStart,
                    InstructorRemarks = getRandomText(lorem),
                    DriverRemarks = getRandomText(lorem)
                    }
                );
                
                await context.AddAsync(drivingSessionData.Last());
            }

            await context.SaveChangesAsync();
        }

        private static int getRandomId(List<int> elements)
        {
            var random = new Random();
            var index = random.Next(elements.Count);

            return elements[index];

        }

        private static string getRandomText(List<DummySmart> lorem)
        {
            var random = new Random();
            var probabilty = random.NextDouble();

            if(0.0 < probabilty && probabilty < 0.3)
                return "";
            
            if(probabilty >= 0.3 && probabilty < 0.6)
                return lorem[random.Next(lorem.Count)].Dummy;
            else
                return lorem[random.Next(lorem.Count)].Smart;
        }

        private class DummySmart {
            public string Dummy { get; set; }
            public string Smart { get; set; }
        }
    }
}