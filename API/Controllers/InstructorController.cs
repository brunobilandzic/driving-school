using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using API.Helpers;

namespace API.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class InstructorController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public InstructorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("sessions")]
        public async Task<ActionResult<PagedList<DrivingSessionDto>>> GetSessions([FromQuery] PaginationParams paginationParams)
        {
            var sessions = await _unitOfWork.DrivingRepository
                .GetDrivingSessionsForInstructor(User.GetUserId(), paginationParams);

            if(sessions == null) return  BadRequest("Failed to fetch sessions for instructor.");

            Response.AddPaginationHeader(sessions.CurrentPage, sessions.PageSize, sessions.TotalCount, sessions.TotalPages);

            return Ok(sessions);
        }
        [HttpPost("sessions")]
        public async Task<ActionResult<DrivingSessionDto>> CreateSession(DrivingSessionDto drivingSessionDto)
        {
            var drivingSession = await _unitOfWork.DrivingRepository
                .CreateDrivingSession(drivingSessionDto, User.GetUserId());

            if(drivingSession == null) return BadRequest("Cannot create session with given data.");

            return Ok(drivingSession);

        }

        [HttpGet("tests")]
        public async Task<ActionResult<PagedList<DrivingTestDto>>> GetTests([FromQuery] PaginationParams paginationParams)
        {
            var tests = await _unitOfWork.DrivingRepository
                .GetDrivingTestsForInstructor(User.GetUserId(), paginationParams);

            if(tests == null) return BadRequest("Failed to fetch tests for instructor.");

            Response.AddPaginationHeader(tests.CurrentPage, tests.PageSize, tests.TotalCount, tests.TotalPages);

            return Ok(tests);
        }

        

    }
}