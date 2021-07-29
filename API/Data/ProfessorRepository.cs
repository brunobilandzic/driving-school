using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Errors;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public ProfessorRepository(DataContext context, IMapper mapper, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<RegulationsGroupDto> AddRegulationsGroup(RegulationsGroupDto regulationsGroupDto)
        {
            RegulationsGroup regulationsGruop = _mapper.Map<RegulationsGroup>(regulationsGroupDto);

            await _context.RegulationsGroups.AddAsync(regulationsGruop);

            // Save changes so regulationsGroup.Id populates
            await _context.SaveChangesAsync();

            return _mapper.Map<RegulationsGroupDto>(regulationsGruop);
        }

        public async Task<IEnumerable<ChangeGroupResultDto>> AddStudentToGroup(ChangeGroupDto changeGroupDto)
        {
            List<AppUser> students = await _userManager.Users
                .Where(u => changeGroupDto.Usernames.Any(x => x == u.UserName))
                .ToListAsync();

            List<ChangeGroupResultDto> changeGroupResults = new List<ChangeGroupResultDto>();

            foreach (AppUser student in students)
            {
                changeGroupResults.Add(new ChangeGroupResultDto(student.UserName));
                if(await _userManager.IsInRoleAsync(student, "Student") == false) continue;
                student.RegulationsGroupId = changeGroupDto.RegulationsGroupId;
                changeGroupResults.Last().Success = true;
            }

            return changeGroupResults;
        }

        public async Task<IEnumerable<RegulationsGroupDto>> GetRegulationsGroups()
        {
            List<RegulationsGroup> regulationsGroups =  await _context.RegulationsGroups
                .ToListAsync();

            return _mapper.Map<List<RegulationsGroupDto>>(regulationsGroups);
        }

        public async Task<RegulationsGroupDto> GetRegulationsGroup(int regulationsGroupId)
        {
            return await _context.RegulationsGroups
                .Include(rg => rg.Students)
                .Include(rg => rg.Lectures)
                .Where(rg => rg.RegulationsGroupId == regulationsGroupId)
                .ProjectTo<RegulationsGroupDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<RegulationsTestDto> AddRegulationsTest(RegulationsTestDto regulationsTestDto)
        {
            RegulationsTest regulationsTest = _mapper.Map<RegulationsTest>(regulationsTestDto);

            await _context.RegulationsTests.AddAsync(regulationsTest);

            // Save changes so regulationsTest.Id populates
            await _context.SaveChangesAsync();

            return _mapper.Map<RegulationsTestDto>(regulationsTest);
        }

        public async Task<IEnumerable<RegulationsTestDto>> GetRegulationsTests()
        {
            return await _context.RegulationsTests
                .ProjectTo<RegulationsTestDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task AddStudentToTest(string username, int regulationsTestId)
        {
            var user = await _userManager.Users
                .Include(u => u.StudentRegulationsTest)
                .ThenInclude(srt => srt.RegulationTest)
                .Where(u => u.UserName == username)
                .SingleOrDefaultAsync();
            
            var studentId = user.Id;

            var student = _mapper.Map<StudentDto>(user);
                        
            if(student == null) return;

            // Finding if student has regulations test that is not yet held
            // To which he is already assigned
            // Information about regulations tests that were held not needed here

            var regulationsTestToDelete = student.RegulationsTests
                .Where(rt => rt.DateStart > DateTime.Now)
                .FirstOrDefault();

            if(regulationsTestToDelete != null)
            {
                var studentRegulationsTestToDelete = await _context.StudentRegulationsTest
                    .Where(srt => srt.StudentId == studentId && srt.RegulationsTestId == regulationsTestToDelete.RegulationsTestId)
                    .FirstOrDefaultAsync();

                _context.StudentRegulationsTest.Remove(studentRegulationsTestToDelete);

            }

            await _context.StudentRegulationsTest.AddAsync(
                new StudentRegulationsTest
                {
                    StudentId = studentId,
                    RegulationsTestId = regulationsTestId
                }
            );
        }

        public async Task DeleteStudentFromTest(string username, int regulationsTestId)
        {
            var student = await _userManager.FindByNameAsync(username);
            
            var studentRegulationsTestToDelete = await _context.StudentRegulationsTest
                    .Where(srt => srt.StudentId == student.Id && srt.RegulationsTestId == regulationsTestId)
                    .FirstOrDefaultAsync();

            if(studentRegulationsTestToDelete == null) return;

            _context.StudentRegulationsTest.Remove(studentRegulationsTestToDelete);

        }

        public Task<RegulationsTestDto> GetRegulationsTest(int RegulationsTestId)
        {
            throw new System.NotImplementedException();
        }

        private async Task<StudentDto> GetStudentWithRegulationsTests(string username)
        {
            return await _userManager.Users
                .Where(s => s.UserName == username)
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task DeleteRegulationsTest(int regulationsTestId)
        {
            var regulationsTest = await _context.RegulationsTests.FindAsync(regulationsTestId);

            if(regulationsTest == null) return;

            _context.RegulationsTests.Remove(regulationsTest);
        }
    }

}