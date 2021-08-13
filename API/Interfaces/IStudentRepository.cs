using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;

namespace API.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<LectureDto>> GetLectures(int studentId);


        
    }
}