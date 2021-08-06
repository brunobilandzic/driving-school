using System;

namespace API.DTOs
{
    public class PersonDto
    {
        public string Username { get; set; }

        public string PhotoUrl { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string [] Roles { get; set; }

        public DateTime DateRegistered { get; set; }
    }
}