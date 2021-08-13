using System.Collections.Generic;
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
        public async Task<ActionResult<IEnumerable<LectureDto>>> GetLectures()
        {
            var lectures = await _unitOfWork.StudentRepository.GetLectures(User.GetUserId());

            if(lectures == null) return BadRequest("Failed to fetch lectures for student.");

            return Ok(lectures);
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
        public async Task<ActionResult<IEnumerable<DrivingTestDto>>> GetDrivingTests()
        {
            var tests = await _unitOfWork.DrivingRepository
                .GetDrivingTestsForStudent(User.GetUserId());

            if(tests == null) return BadRequest("Failed to fetch driving sessions.");

            return Ok(tests);
        }

        [HttpGet("lecture-topics")]
        public async Task<ActionResult<IEnumerable<LectureTopicDto>>> GetLectureTopics()
        {
            var lectureTopics = await _unitOfWork.ProfessorRepository
                .GetLectureTopics();

            if(lectureTopics == null) return BadRequest("Failed to fetch lecture topics.");

            return Ok(lectureTopics);
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