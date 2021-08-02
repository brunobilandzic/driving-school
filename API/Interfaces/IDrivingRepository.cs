using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;

namespace API.Interfaces
{
    public interface IDrivingRepository
    {
        
        Task<PagedList<DrivingSessionDto>> GetDrivingSessions(PaginationParams paginationParams);
        Task<PagedList<DrivingSessionDto>> GetDrivingSessionsForInstructor(int instructorId, PaginationParams paginationParams);
        Task<PagedList<DrivingTestDto>> GetDrivingTestsForInstructor(int instructorId, PaginationParams paginationParams);
        Task<PagedList<DrivingTestDto>> GetDrivingTestsForExaminer(int examinerId, PaginationParams paginationParams);
        Task<DrivingSessionDto> CreateDrivingSession(DrivingSessionDto drivingSessionDto, int instructorId);
        Task<DrivingTestDto> CreateDrivingTest(DrivingSessionDto drivingSessionDto, int examinerId);
        Task AddExaminer(UsernameToIdDto examinerToTest);
        Task AddInstructor(UsernameToIdDto intructorToSession);

        Task<DrivingSessionDto> EditDrivingSessionInstructor(DrivingSessionEditInstructorDto drivingSessionDto, int userId);

        Task<DrivingSessionDto> EditDrivingSessionStudent(DrivingSessionEditStudentDto drivingSessionDto, int userId);

        Task<DrivingTestDto> ExamineDrivingTest(ExamineDrivingTestDto examineDrivingTestDto);

    }
}