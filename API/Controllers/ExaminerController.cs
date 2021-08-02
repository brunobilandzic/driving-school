using System.Threading.Tasks;
using API.DTOs;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Examiner")]
    public class ExaminerController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExaminerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("tests")]
        public async Task<ActionResult<PagedList<DrivingTestDto>>> GetDrivingTests([FromQuery] PaginationParams paginationParams)
        {
            var tests = await _unitOfWork.DrivingRepository.GetDrivingTestsForExaminer(User.GetUserId(), paginationParams);

            if(tests == null) return BadRequest("Failed to fetch driving tests for examiner.");

            Response.AddPaginationHeader(tests.CurrentPage, tests.PageSize, tests.TotalCount, tests.TotalPages);

            return Ok(tests);
        }
 
        [HttpPost("tests")]
        public async Task<ActionResult<DrivingTestDto>> CreateDrivingTest(DrivingSessionDto drivingSessionDto)
        {
            var test = await _unitOfWork.DrivingRepository
                .CreateDrivingTest(drivingSessionDto, User.GetUserId());
            
            if(test == null) return BadRequest("Could not create regulations test with data given.");

            return Ok(test);
        }

        [HttpPost("examine")]
        public async Task<ActionResult<DrivingTestDto>> ExamineDrivingTest(ExamineDrivingTestDto examineDrivingTestDto)
        {
            var updatedDrivingTest = await _unitOfWork.DrivingRepository
                .ExamineDrivingTest(examineDrivingTestDto);

            if(await _unitOfWork.SaveAllChanges() > 0) return updatedDrivingTest;

            return BadRequest("Failed to examine test.");        
        }

        
    }
}