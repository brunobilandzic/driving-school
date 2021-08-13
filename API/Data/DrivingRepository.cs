using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DrivingRepository : IDrivingRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public DrivingRepository(DataContext context, UserManager<AppUser> userManager, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }

        public async Task AddExaminer(UsernameToIdDto examinerToTest)
        {
            var examiner = await _userManager.FindByNameAsync(examinerToTest.Username);
            var test = await _context.DrivingSessions.FindAsync(examinerToTest.Id);

            if(examiner == null || test == null) return;

            test.InstructorId = examiner.Id;
        }

        public async Task AddInstructor(UsernameToIdDto instructorToSession)
        {
            var instructor = await _userManager.FindByNameAsync(instructorToSession.Username);
            var session = await _context.DrivingSessions.FindAsync(instructorToSession.Id);

            if(instructor == null || session == null) return;

            session.InstructorId = instructor.Id;

        }

        public async Task<DrivingSessionDto> CreateDrivingSession(DrivingSessionDto drivingSessionDto, int instructorId)
        {
            var session = _mapper.Map<DrivingSession>(drivingSessionDto);

            var driverId = await _userManager.GetUserIdFromUsername(drivingSessionDto.DriverUsername);

            if(!(await _userManager.IsInRoleAsync(new AppUser{Id = driverId}, "Student")))
                return null;

            session.InstructorId = instructorId;
            session.DriverId =  driverId;

            await _context.DrivingSessions.AddAsync(session);
            await _context.SaveChangesAsync();

            return _mapper.Map<DrivingSessionDto>(session);            
        }

        public async Task<DrivingTestDto> CreateDrivingTest(DrivingSessionDto drivingSessionDto, int examinerId)
        {
            var driverId = await _userManager.GetUserIdFromUsername(drivingSessionDto.DriverUsername);
            var instructorId = await _userManager.GetUserIdFromUsername(drivingSessionDto.InstructorUsername);

            if(!(await _userManager.IsInRoleAsync(new AppUser{Id = driverId}, "Student")) ||
               !(await _userManager.IsInRoleAsync(new AppUser{Id = instructorId}, "Instructor")) ||
               !(await _userManager.IsInRoleAsync(new AppUser{Id = examinerId}, "Examiner"))
               )
                    return null;

            var session = _mapper.Map<DrivingSession>(drivingSessionDto);
            session.DriverId = driverId;
            session.InstructorId = instructorId;

            await _context.DrivingSessions.AddAsync(session);
            if(await _context.SaveChangesAsync() <= 0) return null;

            var test = new DrivingTest
            {
                DrivingSessionId = session.DrivingSessionId,
                ExaminerId = examinerId,
                Passed = false
            };

            await _context.DrivingTests.AddAsync(test);
            if(await _context.SaveChangesAsync() <= 0) return null;

            return _mapper.Map<DrivingTestDto>(test);

        }

        public async Task EditDrivingSession(DrivingSessionDto drivingSessionDto)
        {
            var drivingSession = await _context.DrivingSessions
                .FindAsync(drivingSessionDto.DrivingSessionId);

            if(drivingSession == null) return;

            drivingSession = _mapper.Map(drivingSessionDto, drivingSession);

            return;
        }

        public async Task EditDrivingSessionInstructor(DrivingSessionEditInstructorDto drivingSessionDto, int userId)
        {
            if(! await _userManager.IsInRoleAsync(new AppUser{Id = userId}, "Instructor")) return;

            var session = await _context.DrivingSessions
                .FindAsync(drivingSessionDto.DrivingSessionId);
            
            if(session == null || session.InstructorId != userId) return;

            session.InstructorRemarks = drivingSessionDto.InstructorRemarks;

            return;
        }

        public async Task<DrivingSessionDto> EditDrivingSessionStudent(DrivingSessionEditStudentDto drivingSessionDto, int userId)
        {
            if(! await _userManager.IsInRoleAsync(new AppUser{Id = userId}, "Student")) return null;

            var session = await _context.DrivingSessions
                .FindAsync(drivingSessionDto.DrivingSessionId);
            
            if(session == null || session.DriverId != userId) return null;

            session = _mapper.Map(drivingSessionDto, session);

            return _mapper.Map<DrivingSessionDto>(session);
        }

        public async Task DeleteDrivingSession(int drivingSessionId, int executorId)
        {
            var drivingSession = await _context.DrivingSessions
                .FindAsync(drivingSessionId);

            if(drivingSession == null) return;

            if(drivingSession.InstructorId != executorId) {
                var drivingTest = await _context.DrivingTests
                    .Where(dt => dt.DrivingSessionId == drivingSessionId)
                    .FirstOrDefaultAsync();

                if(drivingTest == null || drivingTest.ExaminerId != executorId) return;
            }

            _context.DrivingSessions.Remove(drivingSession);
        }

        public async Task<DrivingTestDto> ExamineDrivingTest(ExamineDrivingTestDto examineDrivingTestDto, int examinerId)
        {
            var drivingTest = await _context.DrivingTests
                .Include(dt => dt.DrivingSession)
                .ThenInclude(ds => ds.Driver)
                .Where(dt => dt.DrivingSessionId ==  examineDrivingTestDto.DrivingTestId)
                .FirstOrDefaultAsync();

            if(drivingTest == null) return null;
            if(drivingTest.ExaminerId != examinerId) return null;
            
            _mapper.Map(examineDrivingTestDto, drivingTest);

            var student = await _userManager.FindByNameAsync(drivingTest.DrivingSession.Driver.UserName);
            if(student != null) student.Passed = examineDrivingTestDto.Passed;
            

            return _mapper.Map<DrivingTestDto>(drivingTest);            
        }


        public async Task<PagedList<DrivingSessionDto>> GetDrivingSessions(PaginationParams paginationParams)
        {
            var drivingSessions = _context.DrivingSessions
                .OrderByDescending(ds => ds.DateStart)
                .ProjectTo<DrivingSessionDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            return await PagedList<DrivingSessionDto>.CreateAsync(drivingSessions, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<PagedList<DrivingSessionDto>> GetDrivingSessionsForInstructor(int instructorId, PaginationParams paginationParams)
        {
            var drivingSessions =  _context.DrivingSessions
                .OrderByDescending(ds => ds.DateStart)
                .Where(ds => ds.InstructorId == instructorId)
                .ProjectTo<DrivingSessionDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            return await PagedList<DrivingSessionDto>.CreateAsync(drivingSessions, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<PagedList<DrivingTestDto>> GetDrivingTestsForExaminer(int examinerId, PaginationParams paginationParams)
        {
            var drivingTests = _context.DrivingTests
                .Include(dt => dt.DrivingSession)
                .OrderByDescending(ds => ds.DrivingSession.DateStart)
                .Where(dt => dt.ExaminerId == examinerId)
                .ProjectTo<DrivingTestDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            return await PagedList<DrivingTestDto>.CreateAsync(drivingTests, paginationParams.PageNumber, paginationParams.PageSize);
        }
        public async Task<IEnumerable<DrivingTestDto>> GetDrivingTestsForStudent(int studentId)
        {
            var drivingTests = await _context.DrivingTests
                .Include(dt => dt.DrivingSession)
                .OrderByDescending(dt => dt.DrivingSession.DateStart)
                .Where(dt => dt.DrivingSession.DriverId == studentId)
                .ProjectTo<DrivingTestDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return drivingTests;
        }
    	
        public Task<PagedList<DrivingSessionDto>> GetDrivingSessionsForStudent(int studentId, PaginationParams paginationParams)
        {
            var sessions = _context.DrivingSessions
                .Where(s => s.DriverId == studentId)
                .ProjectTo<DrivingSessionDto>(_mapper.ConfigurationProvider)
                .AsQueryable();
            
            return PagedList<DrivingSessionDto>.CreateAsync(sessions, paginationParams.PageNumber, paginationParams.PageSize);

        }
        public async Task<PagedList<DrivingTestDto>> GetDrivingTestsForInstructor(int instructorId, PaginationParams paginationParams)
        {
            var drivingTests = _context.DrivingTests
                .Include(dt => dt.DrivingSession)
                .OrderByDescending(dt => dt.DrivingSession.DateStart)
                .Where(dt => dt.DrivingSession.InstructorId == instructorId)
                .ProjectTo<DrivingTestDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            return await PagedList<DrivingTestDto>.CreateAsync(drivingTests, paginationParams.PageNumber, paginationParams.PageSize);
        }


    }
}