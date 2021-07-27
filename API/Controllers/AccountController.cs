using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, IMapper mapper, ITokenService tokenService, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        // Only admins, instructors and professor can register users.
        [Authorize(Policy = "UserRegistration")]
        public async Task<ActionResult<AuthUserDto>> Register(RegisterDto registerDto)
        {

            if (await UserExists(registerDto.Username)) return BadRequest("User already exists.");


            // Get information about the initiator of registration
            // Must determine if he is admin or not
            var loggedUser = await _userManager.GetUserAsync(this.User);
            var isAdmin = await _userManager.IsInRoleAsync(loggedUser, "Admin");

            if(!isAdmin && registerDto.Roles.Length != 0) return BadRequest("Only admin can assign roles.");

            if(registerDto.Roles.Any(el => el == "Examiner") && registerDto.Roles.Length > 1) return BadRequest("Examiner cannot have any other role.");

            // userManager.CreateAsnyc takes an AppUser as a parameter
            // so we have to map from registerDto to AppUser
            var user = _mapper.Map<AppUser>(registerDto);



            user.UserName = registerDto.Username.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRolesAsync(user, registerDto.Roles);

            if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

            return new AuthUserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                PhotoUrl = user.PhotoUrl,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthUserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null) return BadRequest("User does not exist.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Wrong Password.");

            return new AuthUserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                PhotoUrl = user.PhotoUrl,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }


        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username);
        }
    }
}