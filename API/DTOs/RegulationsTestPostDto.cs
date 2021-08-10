using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class RegulationsTestPostDto
    {
        public int RegulationsTestId { get; set; }
        public DateTime DateStart { get; set; }

        public string Location { get; set; }

        public int? ExaminerId { get; set; }

        public PersonDto Examiner { get; set; }

        public List<String> Students { get; set; }
    }
}