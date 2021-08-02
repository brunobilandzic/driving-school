using System;

namespace API.DTOs
{
    public class DrivingSessionDto
    {
        public int DrivingSessionId { get; set; }
        public string InstructorUsername { get; set; }
        public string DriverUsername { get; set; }

        public DateTime DateStart { get; set; }

        public int Hours { get; set; }

        public string InstructorRemarks { get; set; }

        public string DriverRemarks { get; set; }

    }
}