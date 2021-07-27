using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class DrivingTest
    {
        public int DrivingSessionId { get; set; }

        public DrivingSession DrivingSession { get; set; }


        public AppUser Examiner { get; set; }

        public int ExaminerId { get; set; }

        public bool Passed { get; set; }
    }
}