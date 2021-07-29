using System.Collections.Generic;

namespace API.DTOs
{
    public class ChangeGroupResultDto
    {
        public ChangeGroupResultDto(string Username)
        {
            this.Username = Username;
        }

        public string Username { get; set; }

        public bool Success { get; set; } = false;

    }

    
}