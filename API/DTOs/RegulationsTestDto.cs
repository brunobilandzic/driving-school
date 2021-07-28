using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class RegulationsTestDto
    {
        public DateTime DateStart { get; set; }

        public string Location { get; set; }

        public List<PersonDto> Students { get; set; }
    }
}