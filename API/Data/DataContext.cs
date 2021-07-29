using System;
using System.Linq;
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

        public DbSet<LectureTopic> LectureTopics { get; set; }

        public DbSet<Lecture> Lectures {get; set;}

        public DbSet<DrivingTest> DrivingTests { get; set; }


        public DbSet<RegulationsGroup> RegulationsGroups { get; set; }

        public DbSet<RegulationsTest> RegulationsTests { get; set; }

        public DbSet<StudentLecture> StudentLectures {get; set;}

        public DbSet<StudentRegulationsTest> StudentRegulationsTest { get; set; }

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
                .HasForeignKey(u => u.RegulationsGroupId)
                .OnDelete(DeleteBehavior.SetNull);

            // name: App User - Lecture
            // func: (1,1) <=> (0,N)
            // note: Professor teaching a lecture
            builder.Entity<AppUser>()
                .HasMany(u => u.Teaching)
                .WithOne(l => l.Professor)
                .HasForeignKey(l => l.ProfessorId)
                .OnDelete(DeleteBehavior.SetNull);

            // name: App User - Regulations Test Relationship
            // func: (1,N) <=> (1,N)
            // note: Many to many relationship, we need a helper table and configure two one to many relationships
            builder.Entity<AppUser>()
                .HasMany(u => u.StudentRegulationsTest)
                .WithOne(urt => urt.Student)
                .HasForeignKey(urt => urt.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // name: App User - Regulations Test Relationship (App User is an Examiner)
            // func: (0,1) <=> (0,N)
            // note: Examiner has to give scores for tests
            builder.Entity<AppUser>()
                .HasMany(u => u.RegulationsTestsGiven)
                .WithOne(rt => rt.Examiner)
                .HasForeignKey(rt => rt.ExaminerId)
                .OnDelete(DeleteBehavior.SetNull);

            // name: App User - Regulations Group Relationship (App User is an Professor)
            // func: (1,1) <=> (0,N)
            // note: A Regulations Group is assigned to one professor
            builder.Entity<AppUser>()
                .HasMany(u => u.RegulationsGroupsTeaching)
                .WithOne(rg => rg.Professor)
                .HasForeignKey(rg => rg.ProfessorId)
                .OnDelete(DeleteBehavior.SetNull);

            // name: App User - Regulations Test Relationship
            // func: (1,N) <=> (1,N)
            // note: Many to many relationship, we need a helper table and configure two one to many relationships
            builder.Entity<RegulationsTest>()
                .HasMany(rt => rt.StudentRegulationsTest)
                .WithOne(urt => urt.RegulationTest)
                .HasForeignKey(urt => urt.RegulationsTestId)
                .OnDelete(DeleteBehavior.Cascade);

            // name: App User - Driving Session (App User is a driver)
            // func: (1,1) <=> (0,N)
            // note: 
            builder.Entity<DrivingSession>()
                .HasOne(ds => ds.Driver)
                .WithMany(dr => dr.DrivingSessionsTaken)
                .HasForeignKey(ds => ds.DriverId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // name: App User - Driving Session (App User is an instructor)
            // func: (1,1) <=> (0,N)
            // note: 
            builder.Entity<DrivingSession>()
                .HasOne(ds => ds.Instructor)
                .WithMany(dr => dr.DrivingSessionsGiven)
                .HasForeignKey(ds => ds.InstructorId)
                .OnDelete(DeleteBehavior.SetNull);

            // name: Driving Test - Driving Session
            // func: (0,1) <=> (1,1)
            // note: A driving Test is a driving session and inherits its primary key, driving session does not need to be a driving test
            builder.Entity<DrivingTest>()
                .HasOne(ds => ds.DrivingSession)
                .WithOne(ds => ds.DrivingTest)
                .OnDelete(DeleteBehavior.Cascade);

            // name: App User - Driving Test (App User is an examiner)
            // func: (1,1) <=> (0,N)
            // note: 
            builder.Entity<DrivingTest>()
                .HasOne(ds => ds.Examiner)
                .WithMany(dr => dr.DrivingTestsGiven)
                .HasForeignKey(ds => ds.ExaminerId)
                .OnDelete(DeleteBehavior.SetNull);

            
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
                .HasForeignKey(sl => sl.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // name: App User - Lecture (App User is a student)
            // func: (1,N) <=> (0,N)
            // note: Many to many relationship, we need a helper table (StudentLecture) and configure two one to many relationships
            builder.Entity<Lecture>()
                .HasMany(l => l.StudentLectures)
                .WithOne(sl => sl.Lecture)
                .HasForeignKey(sl => sl.LectureId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // name: Lecture - Lecture Topic
            // func: (0,N) <=> (1,1)
            // note: A Lecture has and needs to have  oneLecture Topic
            builder.Entity<Lecture>()
                .HasOne(l => l.LectureTopic)
                .WithMany(lt => lt.LecturesHeld)
                .HasForeignKey(l => l.LectureTopicId);


            builder.Entity<StudentLecture>()
                .HasKey(sl => new {sl.LectureId, sl.StudentId});

            builder.Entity<DrivingTest>()
                .HasKey(dt => dt.DrivingSessionId);

            builder.Entity<StudentRegulationsTest>()
                .HasKey(urt => new {urt.StudentId, urt.RegulationsTestId});


        }
        
    }
}