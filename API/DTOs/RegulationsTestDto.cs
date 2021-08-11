using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class RegulationsTestDto
    {
        public int RegulationsTestId { get; set; }
        public DateTime DateStart { get; set; }

        public string Location { get; set; }

        public int? ExaminerId { get; set; }

        public PersonDto Examiner { get; set; }

        public bool Corrected { get; set; }

        public List<StudentRegulationsTestDto> StudentRegulationsTest { get; set; }
    }
}