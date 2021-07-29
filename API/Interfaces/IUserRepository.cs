using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> GetUser(string username);
        
        Task<PersonDto> GetPersonAsync(string username);

        Task<IEnumerable<PersonDto>> GetUsers(string username);

        Task<StudentDto> GetStudent(string username);

    }
}