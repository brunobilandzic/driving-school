using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
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
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(IMapper mapper, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;

        }

        public async Task<AppUser> GetUser(string username)
        {
            return await _userManager.Users
                .Where(u => u.UserName == username)
                .FirstOrDefaultAsync();
        }


        public async Task<PersonDto> GetPersonAsync(string username)
        {
            return await _userManager.Users
                .Where(u => u.UserName == username)
                .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PersonDto>> GetUsers(string username)
        {
            var query = _userManager.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .AsQueryable();


            query = query.Where(u => u.UserName != username);

            return await query
                .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }


    }
}