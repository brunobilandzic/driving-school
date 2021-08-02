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
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public StudentRepository(DataContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
        

        public async Task<PagedList<LectureDto>> GetLectures(int studentId, PaginationParams paginationParams)
        {
            var lectureIds = await _context.StudentLectures
                .Where(sl => sl.StudentId == studentId)
                .Select(sl => sl.LectureId)
                .ToListAsync();

            var lectures =  _context.Lectures
                .Where(l => lectureIds.Contains(l.LectureId))
                .ProjectTo<LectureDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            return await PagedList<LectureDto>.CreateAsync(lectures, paginationParams.PageNumber, paginationParams.PageSize);
        }
    }
}