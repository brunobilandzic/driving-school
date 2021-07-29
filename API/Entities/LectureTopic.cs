using System.Collections.Generic;

namespace API.Entities
{
    public class LectureTopic
    {
        public int LectureTopicId { get; set; }

        public string  Title  { get; set; }

        public string Description { get; set; }

        public ICollection<Lecture> LecturesHeld { get; set; }
    }
}