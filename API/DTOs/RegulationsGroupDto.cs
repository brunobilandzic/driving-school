using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class RegulationsGroupDto
    {
        public int RegulationsGroupId { get; set; }

        public int ProfessorId { get; set; }
        public DateTime DateStart { get; set; }

        public DateTime DateEnd {get; set;}

        public List<LectureDto> Lectures { get; set; }

        public List<PersonDto> Students { get; set; }
    }
}