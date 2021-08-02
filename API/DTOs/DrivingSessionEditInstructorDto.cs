using System;

namespace API.DTOs
{
    public class DrivingSessionEditInstructorDto
    {
        public int DrivingSessionId { get; set; }

        public string InstructorRemarks { get; set; }

        public DateTime DateStart { get; set; }
    
        public int Hours { get; set; }
    }
}