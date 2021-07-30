using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class LectureDto
    {
        public int? LectureId { get; set; }
        public int RegulationsGroupId { get; set; }

        public PersonDto Professor { get; set; }

        public string ProfessorRemark { get; set; }

        public DateTime DateStart { get; set; }

        public LectureTopicDto LectureTopic { get; set; }
    }
}