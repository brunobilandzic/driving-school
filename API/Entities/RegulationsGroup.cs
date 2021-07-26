using System;
using System.Collections.Generic;

namespace API.Entities
{
    public class RegulationsGroup
    {
        public int RegulationsGroupId { get; set; }

        public DateTime DateStart { get; set; } = DateTime.Now;

        public DateTime DateEnd {get; set;} = DateTime.Now.AddDays(30);

        public ICollection<Lecture> Lectures { get; set; }

        public ICollection<AppUser> Students { get; set; }

    }
}