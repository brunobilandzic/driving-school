using System;

namespace API.DTOs
{
    public class DrivingSessionDto
    {

        public PersonDto Instructor { get; set; }
        public PersonDto Driver { get; set; }

        public DateTime DateStart { get; set; }

        public int Hours { get; set; }

        public string InstructorRemarks { get; set; }

        public string DriverRemarks { get; set; }

    }
}