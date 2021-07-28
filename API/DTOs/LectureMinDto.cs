using System;

namespace API.DTOs
{
    public class LectureMinDto
    {
        public int RegulationsGroupId { get; set; }

        public string  Topic  { get; set; }

        public string Remark { get; set; }

        public DateTime DateStart { get; set; }


    }
}