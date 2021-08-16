using System;

namespace API.Entities
{
    public class DrivingSession
    {
        public int DrivingSessionId { get; set; }
        
        public AppUser Instructor { get; set; }

        public int InstructorId { get; set; }


        public AppUser Driver { get; set; }

        public int DriverId { get; set; }

        public DateTime DateStart { get; set; }

        public int Hours { get; set; } = 1;

        public string InstructorRemarks { get; set; }

        public string DriverRemarks { get; set; }

        public DrivingTest DrivingTest { get; set; }

        public bool IsDriven { get; set; } = false;

    }
}