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
        const int LecturesCount = 10;

        public static async Task SeedData(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, DataContext context)
        {

            // if there are already records in database skip seeding data
            if (await userManager.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/SeedJson/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            // Some lorem text for later

            var loremText = await System.IO.File.ReadAllTextAsync("Data/SeedJson/Lorem.json");
            var lorem = JsonSerializer.Deserialize<List<DummySmart>>(loremText);

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
            // Setting users
            // 2 professors
            // 2 instructors
            // 2 examiners
            // Rest students

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
                else if (i == 4 || i==5)
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


           

            await context.SaveChangesAsync();

            // For seeding other data we need all users for all roles
            // So we dont e.g populate Professor field in Lecture with
            // user who doesnt have professor role

            var adminUsers = await userManager.GetUsersInRoleAsync("Admin");
            var professorUsers = await userManager.GetUsersInRoleAsync("Professor");
            var InstructorUsers = await userManager.GetUsersInRoleAsync("Instructor");
            var studentUsers = await userManager.GetUsersInRoleAsync("Student");
            var examinerUsers = await userManager.GetUsersInRoleAsync("Examiner");

            var adminIds = adminUsers.Select(a => a.Id).ToList();
            var professorIds = professorUsers.Select(a => a.Id).ToList();
            var instructorIds = InstructorUsers.Select(a => a.Id).ToList();
            var studentIds = studentUsers.Select(a => a.Id).ToList();
            var examinerIds = examinerUsers.Select(a => a.Id).ToList();

            
             //***USER SEED DONE***//

            // Here we seed tables that only have primary key required, 
            // and other fields are not that important for development
            var regulationsGroupData = new List<RegulationsGroup>();
            var regulationTestData = new List<RegulationsTest>();

            // Adding 2 regulations groups
            // And 2 regulation test          

            for (var i = 0; i < 2; i++)
            {
                regulationsGroupData.Add(new RegulationsGroup{ProfessorId = professorIds[i]});
                regulationTestData.Add(new RegulationsTest{ExaminerId = examinerIds[i]});
                
            }

            await context.RegulationsGroups.AddRangeAsync(regulationsGroupData);
            await context.RegulationsTests.AddRangeAsync(regulationTestData);

            // Add Lecture Topics

            for(var i=0; i<LecturesCount; i++)
            {
                await context.LectureTopics.AddAsync(
                    new LectureTopic
                    {   
                        Title = getSmart(lorem),
                        Description = getDummy(lorem),

                    }
                );
            }


            await context.SaveChangesAsync();

            var regulationsGroupIds = await context
                .RegulationsGroups
                .Select(rg => rg.RegulationsGroupId)
                .ToListAsync();

            var regulationsTestsIds = await context
                .RegulationsTests
                .Select(rt => rt.RegulationsTestId)
                .ToListAsync();

            var lectureTopicIds = await context
                .LectureTopics
                .Select(lt => lt.LectureTopicId)
                .ToListAsync();

            foreach (var student in studentUsers)
            {
                student.RegulationsGroupId = getRandomId(regulationsGroupIds);
            }
        
            // Adding 15 * 5 Driving Sessions
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
                    DriverId =  studentIds[i % studentUsers.Count],
                    DateStart = initialDateStart,
                    InstructorRemarks = getRandomText(lorem),
                    DriverRemarks = getRandomText(lorem)
                    }
                );
                
            }
            await context.DrivingSessions.AddRangeAsync(drivingSessionData);
            await context.SaveChangesAsync();

            // Adding lectures 

            var lecturesData = new List<Lecture>();

            initialDateStart = DateTime.Now.AddDays(5);

            List<int> assignStudentIds;

            // For every regulation group
            for(var regulationsGroudIndex=0; regulationsGroudIndex < regulationsGroupIds.Count; regulationsGroudIndex++)
            {
                // For every Lecture Topic
                for(var i = 0; i < lectureTopicIds.Count; i++)
                {
                
                    lecturesData.Add(
                        new Lecture
                        {
                            RegulationsGroupId = regulationsGroupIds[regulationsGroudIndex],
                            ProfessorId = await context.RegulationsGroups
                                .Where(rg => rg.RegulationsGroupId == regulationsGroupIds[regulationsGroudIndex])
                                .Select(rg => rg.ProfessorId)
                                .SingleOrDefaultAsync(),
                            LectureTopicId = lectureTopicIds[i],
                            ProfessorRemark = getDummy(lorem),
                            DateStart = initialDateStart
                        }
                    );

                    initialDateStart = initialDateStart.AddDays(1);

                    await context.Lectures.AddAsync(lecturesData.Last());
                    await context.SaveChangesAsync();

                    assignStudentIds = studentUsers
                        .Where(u => u.RegulationsGroupId == regulationsGroupIds[regulationsGroudIndex])
                        .Select(u => u.Id)
                        .ToList();

                    foreach (var id in assignStudentIds)
                    {
                        await context.StudentLectures.AddAsync(
                            new StudentLecture {
                                StudentId = id,
                                LectureId = lecturesData.Last().LectureId
                            }
                        );

                        
                    }
                    
                }
                // Reset lecture time (same topic has to be on same day)
                initialDateStart = DateTime.Now.AddDays(5).AddHours(1);
            }
            for(var i = 0; i < studentIds.Count; i++) 
            {
                int regulationTestIndex = (i < studentIds.Count / 2) ? 0 : 1;

                await context.StudentRegulationsTest.AddAsync(
                    new StudentRegulationsTest
                    {
                        StudentId = studentIds[i],
                        RegulationsTestId= regulationsTestsIds[regulationTestIndex]
                    }
                );
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

        private static string getDummy(List<DummySmart> lorem)
        {
            var random = new Random();

            return lorem[random.Next(lorem.Count)].Dummy;

        }

        private static string getSmart(List<DummySmart> lorem)
        {
            var random = new Random();

            return lorem[random.Next(lorem.Count)].Smart;

        }



        private class DummySmart {
            public string Dummy { get; set; }
            public string Smart { get; set; }
        }


    }
}