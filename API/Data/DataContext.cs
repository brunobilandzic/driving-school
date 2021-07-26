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

        public DbSet<UserLecture> UserLectures {get; set;}

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


            
        }
        
    }
}