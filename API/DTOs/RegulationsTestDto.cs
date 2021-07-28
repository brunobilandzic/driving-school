using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class RegulationsTestDto
    {
        public DateTime DateStart { get; set; } = DateTime.Now;

        public string Location { get; set; }

        public List<PersonDto> Students { get; set; }
    }
}