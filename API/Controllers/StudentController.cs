using System.Threading.Tasks;
using API.DTOs;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("lectures")]
        public async Task<ActionResult<PagedList<LectureDto>>> GetLectures([FromQuery] PaginationParams paginationParams)
        {
            var lectures = await _unitOfWork.StudentRepository.GetLectures(User.GetUserId(), paginationParams);

            if(lectures == null) return BadRequest("Failed to fetch lectures for student.");

            Response.AddPaginationHeader(lectures.CurrentPage, lectures.PageSize, lectures.TotalCount, lectures.TotalPages);

            return lectures;
        }

        [HttpGet("sessions")]
        public async Task<ActionResult<PagedList<DrivingSessionDto>>> GetDrivingSessions([FromQuery] PaginationParams paginationParams)
        {
            var sessions = await _unitOfWork.DrivingRepository
                .GetDrivingSessionsForStudent(User.GetUserId(), paginationParams);

            if(sessions == null) return BadRequest("Failed to fetch driving sessions.");

            Response.AddPaginationHeader(sessions.CurrentPage, sessions.PageSize, sessions.TotalCount, sessions.TotalPages);

            return sessions;
        }

        [HttpGet("tests")]
        public async Task<ActionResult<PagedList<DrivingTestDto>>> GetDrivingTests([FromQuery] PaginationParams paginationParams)
        {
            var tests = await _unitOfWork.DrivingRepository
                .GetDrivingTestsForStudent(User.GetUserId(), paginationParams);

            if(tests == null) return BadRequest("Failed to fetch driving sessions.");

            Response.AddPaginationHeader(tests.CurrentPage, tests.PageSize, tests.TotalCount, tests.TotalPages);

            return tests;
        }

        [HttpGet("lecture-topics")]
        public async Task<ActionResult<PagedList<LectureTopicDto>>> GetLectureTopics([FromQuery] PaginationParams paginationParams)
        {
            var lectureTopics = await _unitOfWork.ProfessorRepository
                .GetLectureTopics(paginationParams);

            if(lectureTopics == null) return BadRequest("Failed to fetch lecture topics.");

            Response.AddPaginationHeader(lectureTopics.CurrentPage, lectureTopics.PageSize, lectureTopics.TotalCount, lectureTopics.TotalPages);

            return lectureTopics;
        }


        [HttpPut("sessions")]
        public async Task<ActionResult<DrivingSessionDto>> EditSession(DrivingSessionEditStudentDto drivingSessionDto)
        {   
            var drivingSession = await _unitOfWork.DrivingRepository
                .EditDrivingSessionStudent(drivingSessionDto, User.GetUserId());
            
            if(await _unitOfWork.SaveAllChanges() > 0) return drivingSession;
        
            return BadRequest("Failed to update driving session.");
        }
    }
}