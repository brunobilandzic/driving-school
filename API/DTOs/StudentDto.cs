using System.Collections.Generic;

namespace API.DTOs
{
    public class StudentDto : PersonDto
    {
        public int RegulationsGroupId { get; set; }
        
        public List<RegulationsTestDto> RegulationsTests {get; set;}

        public List<LectureDto> Lectures {get; set;}
        
    }
}