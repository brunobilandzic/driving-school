using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }

        public bool Passed { get; set; } = false;
        public DateTime DateRegistered { get; set; } = DateTime.Now;

        public string PhotoUrl { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public RegulationsGroup RegulationsGruop { get; set; }


        public int? RegulationsGroupId { get; set; }

        //Only for students
        public ICollection<StudentRegulationsTest> StudentRegulationsTest { get; set; }

        public ICollection<StudentLecture> StudentLectures { get; set; }

        public ICollection<DrivingSession> DrivingSessionsTaken { get; set; }

        //Only for examiners
        public ICollection<RegulationsTest> RegulationsTestsGiven { get; set; }
        public ICollection<DrivingTest> DrivingTestsGiven { get; set; }

        //Only for professors
        public ICollection<Lecture> Teaching { get; set; }

        public ICollection<RegulationsGroup> RegulationsGroupsTeaching { get; set; }
        

        //Only for instructors
        public ICollection<DrivingSession> DrivingSessionsGiven { get; set; }

        

        
    }
}