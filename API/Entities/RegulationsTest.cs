using System;
using System.Collections.Generic;

namespace API.Entities
{
    public class RegulationsTest
    {
        public int RegulationsTestId { get; set; }
        public DateTime DateStart { get; set; } = DateTime.Now.AddDays(7);

        public AppUser Examiner { get; set; }

        public int? ExaminerId { get; set; }

        public string Location { get; set; }

        public bool Corrected { get; set; } = false;

        public ICollection<StudentRegulationsTest> StudentRegulationsTest { get; set; }
        
    }
}