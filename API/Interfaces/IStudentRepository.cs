using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;

namespace API.Interfaces
{
    public interface IStudentRepository
    {
        Task<PagedList<LectureDto>> GetLectures(int studentId, PaginationParams paginationParams);


        
    }
}