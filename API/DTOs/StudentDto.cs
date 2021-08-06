using System.Collections.Generic;

namespace API.DTOs
{
    public class StudentDto : PersonDto
    {
        public int RegulationsGroupId { get; set; }
        
        public List<StudentRegulationsTestDto> RegulationsTests {get; set;}

        public List<StudentLectureDto> Lectures {get; set;}

        public bool Passed { get; set; }
        
    }
}