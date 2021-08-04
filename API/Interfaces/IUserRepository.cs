using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> GetUser(string username);
        
        Task<PersonDto> GetPersonAsync(string username);

        Task<PagedList<PersonDto>> GetUsers(string username, PaginationParams paginationParams);

        Task<StudentDto> GetStudent(string username);

        Task<PagedList<PersonDto>> GetStudents(PaginationParams paginationParams);

        Task<bool> PassStudent(string username);

    }
}