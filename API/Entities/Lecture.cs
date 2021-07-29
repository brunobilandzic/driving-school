using System;
using System.Collections.Generic;

namespace API.Entities
{
    public class Lecture
    {
        public int LectureId { get; set; }

        public RegulationsGroup RegulationsGroup { get; set; }

        public int RegulationsGroupId { get; set; }

        public AppUser Professor { get; set; }

        public int ProfessorId { get; set; }

        public LectureTopic LectureTopic  { get; set; }
        public int LectureTopicId { get; set; }

        public string ProfessorRemark { get; set; }

        public DateTime DateStart { get; set; }

        public ICollection<StudentLecture> StudentLectures { get; set; }
    }
}