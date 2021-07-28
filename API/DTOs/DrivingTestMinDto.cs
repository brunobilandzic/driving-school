using System;

namespace API.DTOs
{
    public class DrivingTestMinDto
    {

        public DateTime DateStart { get; set; }

        public int Hours { get; set; }

        public string InstructorRemarks { get; set; }

        public string DriverRemarks { get; set; }

    }
}