using System;

namespace API.DTOs
{
    public class StudentRegulationsTestDto
    {
        public string StudentUsername { get; set; }

        public int RegulationsTestId { get; set; }

        public DateTime RegulationsTestDate { get; set; }

        public int Score { get; set; }
    }
}