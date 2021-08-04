using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(IMapper mapper, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, DataContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _mapper = mapper;

        }

        public async Task<AppUser> GetUser(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<StudentDto> GetStudent(string username)
        {
            return await _userManager.Users
                .Where(u => u.UserName == username)
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<PersonDto> GetPersonAsync(string username)
        {
            return await _userManager.Users
                .Where(u => u.UserName == username)
                .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<PagedList<PersonDto>> GetUsers(string username, PaginationParams paginationParams)
        {
            var query = _userManager.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .AsQueryable();


            query = query.Where(u => u.UserName != username);

            return await PagedList<PersonDto>.CreateAsync(
                query.ProjectTo<PersonDto>(_mapper.ConfigurationProvider), 
                paginationParams.PageNumber, 
                paginationParams.PageSize
            );
        }

        public async Task<bool> PassStudent(string username)
        {
            var student = await _userManager.Users
                .Where(s  => s.UserName == username)
                .FirstOrDefaultAsync();
            
            if(student == null) return false;
            if(! await _userManager.IsInRoleAsync(student, "Student")) return false;

            var drivingTest = await _context.DrivingTests
                .Include(dt => dt.DrivingSession)
                .Where(dt => dt.DrivingSession.DriverId == student.Id)
                .Where(dt => dt.Passed == true)
                .FirstOrDefaultAsync();

            if(drivingTest == null) return false;

            student.Passed = true;
            return true;
        }

        public async Task<PagedList<PersonDto>> GetStudents(PaginationParams paginationParams)
        {
            var students = _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Where(u => u.UserRoles.Select(ur => ur.Role.Name).Contains("Student"))
                .OrderByDescending(u => u.DateRegistered)
                .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            return await PagedList<PersonDto>.CreateAsync(students, paginationParams.PageNumber, paginationParams.PageSize);
        }
    }
}