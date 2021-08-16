using System;

namespace API.Helpers
{
    public class SessionParams : PaginationParams
    {
        public DateTime? DateStart { get; set; } = null;
        public Boolean? IsDriven { get; set; } = null;

        public string? SortDirection { get; set; } = null;
    }

}