using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class UsersController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetUsers([FromQuery] PaginationParams paginationParams)
        {
            var users = await _unitOfWork.UserRepository.GetUsers(User.GetUsername(), paginationParams);

            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            
            return Ok(users);


        }

        [HttpGet("student/{username}")]
        public async Task<ActionResult<StudentDto>> GetStudent(string username)
        {
            var user = await _unitOfWork.UserRepository.GetStudent(username);

            return Ok(user);


        }

        [Authorize(Policy = "NotOnlyStudent")]
        [HttpGet("students")]
        public async Task<ActionResult<PagedList<PersonDto>>> GetStudents([FromQuery] PaginationParams paginationParams)
        {
            var students = await _unitOfWork.UserRepository.GetStudents(paginationParams);

            if(students == null) return BadRequest("Failed to fetch all students.");
        
            Response.AddPaginationHeader(students.CurrentPage, students.PageSize, students.TotalCount, students.TotalPages);

            return students;
        }

        [Authorize(Policy = "NotOnlyStudent")]
        [HttpGet("all-students")]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAllStudents([FromQuery] int regulationsGroupId)
        {
            var students = await _unitOfWork.UserRepository.GetAllStudents(regulationsGroupId);

            if(students == null) return BadRequest("Failed to fetch all students.");

            return Ok(students);
        }
        
        
        [HttpGet("{username}")]
        public async Task<ActionResult<PersonDto>> GetUser(string username)
        {
            return await _unitOfWork.UserRepository.GetPersonAsync(username);
        }
        [Authorize(Roles = "Examiner")]
        [HttpPost("student-finished/{username}")]
        public async Task<ActionResult<bool>> MarkStudentFinished(string username)
        {
            var isPassed = await _unitOfWork.UserRepository.PassStudent(username);

            if(isPassed) return Ok();

            return BadRequest("Failed to mark student as passed.");
        }


    }
}