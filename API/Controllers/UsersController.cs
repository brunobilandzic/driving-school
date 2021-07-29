using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
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
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetUsers()
        {
            var users = await _unitOfWork.UserRepository.GetUsers(User.GetUsername());

            return Ok(users);


        }

        [HttpGet("student/{username}")]
        public async Task<ActionResult<StudentDto>> GetStudent(string username)
        {
            var user = await _unitOfWork.UserRepository.GetStudent(username);

            return Ok(user);


        }



        [HttpGet("{username}")]
        public async Task<PersonDto> GetUser(string username)
        {
            return await _unitOfWork.UserRepository.GetPersonAsync(username);
        }


    }
}