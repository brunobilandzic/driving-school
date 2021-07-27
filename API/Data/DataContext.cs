using System;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext
    <
        AppUser, 
        AppRole, 
        int, 
        IdentityUserClaim<int>, 
        AppUserRole, 
        IdentityUserLogin<int>, 
        IdentityRoleClaim<int>,
        IdentityUserToken<int>
    >
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<DrivingSession> DrivingSessions { get; set; }

        public DbSet<Lecture> Lectures {get; set;}

        public DbSet<DrivingTest> DrivingTests { get; set; }


        public DbSet<RegulationsGroup> RegulationsGroups { get; set; }

        public DbSet<RegulationsTest> RegulationsTests { get; set; }

        public DbSet<StudentLecture> StudentLectures {get; set;}

        public DbSet<UserRegulationsTest> UserRegulationsTest { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        { 
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            // name: App User - Regulations Group Relationship
            // func: (1,N) <=> (0,N)
            // note: Only students can be in a regulations group, so the FK RegulationsGruopId in AppUser is nullable
            builder.Entity<AppUser>()
                .HasOne(u => u.RegulationsGruop)
                .WithMany(rg => rg.Students)
                .HasForeignKey(u => u.RegulationsGroupId);

            // name: App User - Lecture
            // func: (1,1) <=> (0,N)
            // note: Professor teaching a lecture
            builder.Entity<AppUser>()
                .HasMany(u => u.Teaching)
                .WithOne(l => l.Professor)
                .HasForeignKey(l => l.ProfessorId);

            // name: App User - Regulations Test Relationship
            // func: (1,N) <=> (1,N)
            // note: Many to many relationship, we need a helper table and configure two one to many relationships
            builder.Entity<AppUser>()
                .HasMany(u => u.UserRegulationsTests)
                .WithOne(urt => urt.Student)
                .HasForeignKey(urt => urt.StudentId);

            // name: App User - Regulations Test Relationship
            // func: (1,N) <=> (1,N)
            // note: Many to many relationship, we need a helper table and configure two one to many relationships
            builder.Entity<RegulationsTest>()
                .HasMany(rt => rt.UserRegulationTests)
                .WithOne(urt => urt.RegulationTest)
                .HasForeignKey(urt => urt.RegulationsTestId);

            // name: App User - Driving Session (App User is a driver)
            // func: (1,1) <=> (0,N)
            // note: 
            builder.Entity<DrivingSession>()
                .HasOne(ds => ds.Driver)
                .WithMany(dr => dr.DrivingSessionsTaken)
                .HasForeignKey(ds => ds.DriverId);
            
            // name: App User - Driving Session (App User is an instructor)
            // func: (1,1) <=> (0,N)
            // note: 
            builder.Entity<DrivingSession>()
                .HasOne(ds => ds.Instructor)
                .WithMany(dr => dr.DrivingSessionsGiven)
                .HasForeignKey(ds => ds.InstructorId);

            // name: Driving Test - Driving Session
            // func: (0,1) <=> (1,1)
            // note: A driving Test is a driving session and inherits its primary key, driving session does not need to be a driving test
            builder.Entity<DrivingTest>()
                .HasOne(ds => ds.DrivingSession)
                .WithOne(ds => ds.DrivingTest);

            // name: App User - Driving Test (App User is an examiner)
            // func: (1,1) <=> (0,N)
            // note: 
            builder.Entity<DrivingTest>()
                .HasOne(ds => ds.Examiner)
                .WithMany(dr => dr.DrivingTestsGiven)
                .HasForeignKey(ds => ds.ExaminerId);

            
            // WARNING!!
            // name: App User - Driving Test (App User is an driver)
            // func: (1,1) <=> (0,N)
            // note: App user is already connected to the driving session that is a parent of driving test entity
            //       Therefore, for now, I do not see any need to configure relationship between driving test and 
            //       app user (as a student)


            // name: App User - Lecture (App User is a student)
            // func: (1,N) <=> (0,N)
            // note: Many to many relationship, we need a helper table (StudentLecture) and configure two one to many relationships
            builder.Entity<AppUser>()
                .HasMany(u => u.StudentLectures)
                .WithOne(sl => sl.Student)
                .HasForeignKey(sl => sl.StudentId);

            // name: App User - Lecture (App User is a student)
            // func: (1,N) <=> (0,N)
            // note: Many to many relationship, we need a helper table (StudentLecture) and configure two one to many relationships
            builder.Entity<Lecture>()
                .HasMany(l => l.StudentLectures)
                .WithOne(sl => sl.Lecture)
                .HasForeignKey(sl => sl.LectureId);
            

            builder.Entity<StudentLecture>()
                .HasKey(sl => new {sl.LectureId, sl.StudentId});

            builder.Entity<DrivingTest>()
                .HasKey(dt => dt.DrivingSessionId);

            builder.Entity<UserRegulationsTest>()
                .HasKey(urt => new {urt.StudentId, urt.RegulationsTestId});


        }
        
    }
}