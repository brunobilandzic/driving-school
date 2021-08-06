using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;

namespace API.Interfaces
{
    public interface IInstructorRepository
    {
        Task<PagedList<PersonDto>> GetStudents(int instructorId, PaginationParams paginationParams);

        Task<DriverDto> GetDriver(string driverUsername);
    }   
}