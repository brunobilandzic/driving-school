using System;

namespace API.DTOs
{
    public class DrivingSessionMinDto
    {
        public int? InstructorId { get; set; }
        public int DriverId { get; set; }

        public DateTime DateStart { get; set; }

        public int Hours { get; set; }

        public string InstructorRemarks { get; set; }

        public string DriverRemarks { get; set; }
    }
}