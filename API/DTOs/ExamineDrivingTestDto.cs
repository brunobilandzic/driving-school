namespace API.DTOs
{
    public class ExamineDrivingTestDto
    {
        public int DrivingTestId { get; set; }

        public bool Passed { get; set; }

        public string ExaminerRemarks { get; set; }
    }
}