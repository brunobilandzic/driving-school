namespace API.Entities
{
    public class UserLecture
    {
        public AppUser Student { get; set; }

        public int StudentId { get; set; }

        public AppUser Professor {get; set;}

        public int ProfessorId { get; set; }

        public Lecture Lecture { get; set; }

        public int LectureId { get; set; }

        public bool Attendance { get; set; }
    }
}