using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public InstructorRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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