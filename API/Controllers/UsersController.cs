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
    [Authorize]
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

        [HttpGet("{username}")]
        public async Task<PersonDto> GetUser(string username)
        {
            return await _unitOfWork.UserRepository.GetPersonAsync(username);
        }


    }
}