using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }

        public string PhotoUrl { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public RegulationsGroup RegulationsGruop { get; set; }


        public int RegulationsGroupId { get; set; }

        //Only for students
        public ICollection<UserRegulationsTest> UserRegulationsTests { get; set; }

        //Only for professors
        public ICollection<UserLecture> Teaching { get; set; }

        //Only for students
        public ICollection<UserLecture> Listening { get; set; }

        //Only for students
        public ICollection<DrivingTest> DrivingTestsTaken { get; set; }

        //Only for examiners
        public ICollection<DrivingTest> DrivingTestsGiven { get; set; }

        //Only for students
        public ICollection<DrivingSession> DrivingSessionsTaken { get; set; }






        
    }
}