namespace API.DTOs
{
    public class DrivingTestDto
    {
        public int DrivingSessionId { get; set; }

        public DrivingSessionDto DrivingSession { get; set; }

        public string ExaminerUsername { get; set; }

        public bool Passed { get; set; }

        public string ExaminerRemarks { get; set; }
    }
}