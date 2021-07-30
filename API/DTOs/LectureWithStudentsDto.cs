using System.Collections.Generic;

namespace API.DTOs
{
    public class LectureWithStudentsDto : LectureDto
    {
        public List<PersonDto> Students { get; set; }
    }
}