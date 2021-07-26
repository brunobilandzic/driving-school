namespace API.Entities
{
    public class DrivingTest
    {
        public int DrivingTestId { get; set; }

        public DrivingSession DrivingSession { get; set; }

        public AppUser Examiner { get; set; }

        public int ExaminerId { get; set; }

        public bool Passed { get; set; }
    }
}