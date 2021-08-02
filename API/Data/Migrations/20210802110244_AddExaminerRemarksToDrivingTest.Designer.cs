﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210802110244_AddExaminerRemarksToDrivingTest")]
    partial class AddExaminerRemarksToDrivingTest
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("API.Entities.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("TEXT");

                    b.Property<int?>("RegulationsGroupId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("RegulationsGroupId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("API.Entities.AppUserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("API.Entities.DrivingSession", b =>
                {
                    b.Property<int>("DrivingSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("TEXT");

                    b.Property<int>("DriverId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DriverRemarks")
                        .HasColumnType("TEXT");

                    b.Property<int>("Hours")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InstructorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("InstructorRemarks")
                        .HasColumnType("TEXT");

                    b.HasKey("DrivingSessionId");

                    b.HasIndex("DriverId");

                    b.HasIndex("InstructorId");

                    b.ToTable("DrivingSessions");
                });

            modelBuilder.Entity("API.Entities.DrivingTest", b =>
                {
                    b.Property<int>("DrivingSessionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExaminerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ExaminerRemarks")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Passed")
                        .HasColumnType("INTEGER");

                    b.HasKey("DrivingSessionId");

                    b.HasIndex("ExaminerId");

                    b.ToTable("DrivingTests");
                });

            modelBuilder.Entity("API.Entities.Lecture", b =>
                {
                    b.Property<int>("LectureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("TEXT");

                    b.Property<int>("LectureTopicId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProfessorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProfessorRemark")
                        .HasColumnType("TEXT");

                    b.Property<int>("RegulationsGroupId")
                        .HasColumnType("INTEGER");

                    b.HasKey("LectureId");

                    b.HasIndex("LectureTopicId");

                    b.HasIndex("ProfessorId");

                    b.HasIndex("RegulationsGroupId");

                    b.ToTable("Lectures");
                });

            modelBuilder.Entity("API.Entities.LectureTopic", b =>
                {
                    b.Property<int>("LectureTopicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("LectureTopicId");

                    b.ToTable("LectureTopics");
                });

            modelBuilder.Entity("API.Entities.RegulationsGroup", b =>
                {
                    b.Property<int>("RegulationsGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProfessorId")
                        .HasColumnType("INTEGER");

                    b.HasKey("RegulationsGroupId");

                    b.HasIndex("ProfessorId");

                    b.ToTable("RegulationsGroups");
                });

            modelBuilder.Entity("API.Entities.RegulationsTest", b =>
                {
                    b.Property<int>("RegulationsTestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ExaminerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT");

                    b.HasKey("RegulationsTestId");

                    b.HasIndex("ExaminerId");

                    b.ToTable("RegulationsTests");
                });

            modelBuilder.Entity("API.Entities.StudentLecture", b =>
                {
                    b.Property<int>("LectureId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Attendance")
                        .HasColumnType("INTEGER");

                    b.HasKey("LectureId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentLectures");
                });

            modelBuilder.Entity("API.Entities.StudentRegulationsTest", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RegulationsTestId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.HasKey("StudentId", "RegulationsTestId");

                    b.HasIndex("RegulationsTestId");

                    b.ToTable("StudentRegulationsTest");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.HasOne("API.Entities.RegulationsGroup", "RegulationsGruop")
                        .WithMany("Students")
                        .HasForeignKey("RegulationsGroupId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("RegulationsGruop");
                });

            modelBuilder.Entity("API.Entities.AppUserRole", b =>
                {
                    b.HasOne("API.Entities.AppRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.AppUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API.Entities.DrivingSession", b =>
                {
                    b.HasOne("API.Entities.AppUser", "Driver")
                        .WithMany("DrivingSessionsTaken")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.AppUser", "Instructor")
                        .WithMany("DrivingSessionsGiven")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Driver");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("API.Entities.DrivingTest", b =>
                {
                    b.HasOne("API.Entities.DrivingSession", "DrivingSession")
                        .WithOne("DrivingTest")
                        .HasForeignKey("API.Entities.DrivingTest", "DrivingSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.AppUser", "Examiner")
                        .WithMany("DrivingTestsGiven")
                        .HasForeignKey("ExaminerId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("DrivingSession");

                    b.Navigation("Examiner");
                });

            modelBuilder.Entity("API.Entities.Lecture", b =>
                {
                    b.HasOne("API.Entities.LectureTopic", "LectureTopic")
                        .WithMany("LecturesHeld")
                        .HasForeignKey("LectureTopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.AppUser", "Professor")
                        .WithMany("Teaching")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("API.Entities.RegulationsGroup", "RegulationsGroup")
                        .WithMany("Lectures")
                        .HasForeignKey("RegulationsGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LectureTopic");

                    b.Navigation("Professor");

                    b.Navigation("RegulationsGroup");
                });

            modelBuilder.Entity("API.Entities.RegulationsGroup", b =>
                {
                    b.HasOne("API.Entities.AppUser", "Professor")
                        .WithMany("RegulationsGroupsTeaching")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("API.Entities.RegulationsTest", b =>
                {
                    b.HasOne("API.Entities.AppUser", "Examiner")
                        .WithMany("RegulationsTestsGiven")
                        .HasForeignKey("ExaminerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Examiner");
                });

            modelBuilder.Entity("API.Entities.StudentLecture", b =>
                {
                    b.HasOne("API.Entities.Lecture", "Lecture")
                        .WithMany("StudentLectures")
                        .HasForeignKey("LectureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.AppUser", "Student")
                        .WithMany("StudentLectures")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lecture");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("API.Entities.StudentRegulationsTest", b =>
                {
                    b.HasOne("API.Entities.RegulationsTest", "RegulationTest")
                        .WithMany("StudentRegulationsTest")
                        .HasForeignKey("RegulationsTestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.AppUser", "Student")
                        .WithMany("StudentRegulationsTest")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RegulationTest");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("API.Entities.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("API.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("API.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("API.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("API.Entities.AppRole", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Navigation("DrivingSessionsGiven");

                    b.Navigation("DrivingSessionsTaken");

                    b.Navigation("DrivingTestsGiven");

                    b.Navigation("RegulationsGroupsTeaching");

                    b.Navigation("RegulationsTestsGiven");

                    b.Navigation("StudentLectures");

                    b.Navigation("StudentRegulationsTest");

                    b.Navigation("Teaching");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("API.Entities.DrivingSession", b =>
                {
                    b.Navigation("DrivingTest");
                });

            modelBuilder.Entity("API.Entities.Lecture", b =>
                {
                    b.Navigation("StudentLectures");
                });

            modelBuilder.Entity("API.Entities.LectureTopic", b =>
                {
                    b.Navigation("LecturesHeld");
                });

            modelBuilder.Entity("API.Entities.RegulationsGroup", b =>
                {
                    b.Navigation("Lectures");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("API.Entities.RegulationsTest", b =>
                {
                    b.Navigation("StudentRegulationsTest");
                });
#pragma warning restore 612, 618
        }
    }
}
