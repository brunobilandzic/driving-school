using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class LectureDto
    {

        public RegulationsGroupDto RegulationsGroup { get; set; }

        public PersonDto Professor { get; set; }

        public string  Topic  { get; set; }

        public string Remark { get; set; }

        public DateTime DateStart { get; set; }

        public List<PersonDto> Students { get; set; }
    }
}