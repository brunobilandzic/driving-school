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
    public class InstructorRepository : IInstructorRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        private readonly UserManager<AppUser> _userManager;

        public InstructorRepository(DataContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<DriverDto> GetDriver(string driverUsername)
        {
            var driver = await _context.Users
                .Where(d => d.UserName == driverUsername)
                .Include(d => d.DrivingSessionsTaken)
                .ThenInclude(ds => ds.Instructor)
                .FirstOrDefaultAsync();

            if(driver == null || await _userManager.IsInRoleAsync(driver, "Student") == false) return null;

            return _mapper.Map<DriverDto>(driver);
        }

        public async Task<PagedList<PersonDto>> GetStudents(int instructorId, PaginationParams paginationParams)
    {
        var driversUsernames = await _context.DrivingSessions
            .Where(ds => ds.InstructorId == instructorId)
            .ProjectTo<DrivingSessionDto>(_mapper.ConfigurationProvider)
            .Select(ds => ds.DriverUsername)
            .ToListAsync();

        var students = _context.Users
            .Where(u => driversUsernames.Contains(u.UserName))
            .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
            .AsEnumerable()
            .AsQueryable();

        return await PagedList<PersonDto>.CreateAsync(students, paginationParams.PageNumber, paginationParams.PageSize);

    }
}
}