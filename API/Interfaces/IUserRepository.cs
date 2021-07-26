using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<PersonDto> GetPersonAsync(string username);

        Task<IEnumerable<PersonDto>> GetUsers(string username);

    }
}