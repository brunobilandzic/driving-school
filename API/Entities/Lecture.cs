using System;

namespace API.Entities
{
    public class Lecture
    {
        public int LectureId { get; set; }

        public int RegulationsGruopId { get; set; }

        public RegulationsGroup RegulationsGroup { get; set; }

        public AppUser Professor { get; set; }

        public string  Topic  { get; set; }

        public string Remark { get; set; }

        public DateTime DateStart { get; set; }
    }
}