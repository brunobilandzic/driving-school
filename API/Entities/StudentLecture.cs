namespace API.Entities
{
    public class StudentLecture
    {
        public AppUser Student { get; set; }

        public int StudentId { get; set; }

        public Lecture Lecture { get; set; }

        public int LectureId { get; set; }

        public bool Attendance { get; set; } = false;
    }
}