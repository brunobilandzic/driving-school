using System;

namespace API.DTOs
{
    public class StudentLectureDto
    {
        public string StudentUsername { get; set; }

        public string LectureTopic { get; set; }

        public int LectureId { get; set; }

        public bool Attendance { get; set; }

        public DateTime DateStart { get; set; }
    }
}