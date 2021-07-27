using System;
using System.Collections.Generic;

namespace API.Entities
{
    public class RegulationsTest
    {
        public int RegulationsTestId { get; set; }
        public DateTime DateStart { get; set; }

        public string Location { get; set; }

        public ICollection<StudentRegulationsTest> StudentRegulationsTest { get; set; }
        
    }
}