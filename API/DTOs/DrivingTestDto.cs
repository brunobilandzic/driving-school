namespace API.DTOs
{
    public class DrivingTestDto
    {
        public int DrivingSessionId { get; set; }

        public DrivingSessionDto DrivingSession { get; set; }

        public PersonDto Examiner { get; set; }

        public bool Passed { get; set; }
    }
}