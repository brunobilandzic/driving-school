using System.Collections.Generic;

namespace API.DTOs
{
    public class DriverDto : PersonDto
    {
        public List<DrivingSessionDto> DrivingSessionsTaken { get; set; }
        public bool Passed { get; set; }
    }
}