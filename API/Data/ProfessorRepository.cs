using System;
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

        public async Task<PagedList<PersonDto>> GetStudents(int professorId, PaginationParams paginationParams)
        {
            var groupsIds = await _context.RegulationsGroups
                .Where(g => g.ProfessorId == professorId)
                .Select(g => g.RegulationsGroupId)
                .ToListAsync();

            var users = _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Where(s => s.UserRoles.Select(ur => ur.Role.Name).Contains("Student"))
                .Where(s => !s.Passed)
                .Where(s => groupsIds.Contains((int) s.RegulationsGroupId))
                .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            return await PagedList<PersonDto>.CreateAsync(users, paginationParams.PageNumber, paginationParams.PageSize);    
        }

        public async Task<StudentDto> GetStudent(string username)
        {
            if(await _userManager.IsInRoleUsername(username, "Student") == false) return null;

            return await _userManager.Users
                .Where(u => u.UserName == username)
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            
            
        }

        public async Task<RegulationsGroupDto> AddRegulationsGroup(RegulationsGroupDto regulationsGroupDto)
        {
            RegulationsGroup regulationsGruop = _mapper.Map<RegulationsGroup>(regulationsGroupDto);

            await _context.RegulationsGroups.AddAsync(regulationsGruop);

            // Save changes so regulationsGroup.Id populates
            await _context.SaveChangesAsync();

            return _mapper.Map<RegulationsGroupDto>(regulationsGruop);
        }

        public async Task<IEnumerable<ChangeGroupResultDto>> AddStudentToGroup(UsernamesToIdDto changeGroupDto)
        {
            List<AppUser> students = await _userManager.Users
                .Where(u => changeGroupDto.Usernames.Any(x => x == u.UserName))
                .ToListAsync();

            List<ChangeGroupResultDto> changeGroupResults = new List<ChangeGroupResultDto>();

            foreach (AppUser student in students)
            {
                if(await _userManager.IsInRoleAsync(student, "Student") == false) continue;

                // Bind student to future lectures form 
                var studentLecturesToDelete = await _context.StudentLectures
                    .Include(sl => sl.Lecture)
                    .Where(sl => sl.Lecture.RegulationsGroupId == student.RegulationsGroupId &&
                        sl.Lecture.DateStart > DateTime.Now && sl.StudentId == student.Id
                    )
                    .ToListAsync();
                
                _context.StudentLectures.RemoveRange(studentLecturesToDelete);

                var studentLecturesToAdd = new List<StudentLecture>();

                var lecturesToAddIds = _context.Lectures
                    .Where(l => l.RegulationsGroupId == changeGroupDto.Id && l.DateStart > DateTime.Now)
                    .Select(l => l.LectureId);


                foreach (var lectureId in lecturesToAddIds)
                {
                    studentLecturesToAdd.Add(
                        new StudentLecture
                        {
                            LectureId = lectureId,
                            StudentId = student.Id
                        }
                    );
                }

                await _context.StudentLectures.AddRangeAsync(studentLecturesToAdd);

                changeGroupResults.Add(new ChangeGroupResultDto(student.UserName));
                student.RegulationsGroupId = changeGroupDto.Id;
                changeGroupResults.Last().Success = true;
            }

            return changeGroupResults;
        }

        public async Task<PagedList<RegulationsGroupMinDto>> GetRegulationsGroups(PaginationParams paginationParams)
        {
            var query = _context.RegulationsGroups
                .ProjectTo<RegulationsGroupMinDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            return await PagedList<RegulationsGroupMinDto>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);

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

        public async Task<PagedList<RegulationsTestDto>> GetRegulationsTests(PaginationParams paginationParams)
        {
            var regulationsTests = _context.RegulationsTests
                .ProjectTo<RegulationsTestDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            return await PagedList<RegulationsTestDto>.CreateAsync(regulationsTests, paginationParams.PageNumber, paginationParams.PageSize);

        }

        public async Task<RegulationsTestDto> GetRegulationsTest(int regulationsTestId)
        {
            return _mapper.Map<RegulationsTestDto>(
                await _context.RegulationsTests
                    .Include(rt => rt.StudentRegulationsTest)
                    .ThenInclude(rt => rt.Student)
                    .Include(rt => rt.Examiner)
                    .Where(rt => rt.RegulationsTestId == regulationsTestId)
                    .SingleOrDefaultAsync()
                );
        }

        
        public async Task<IEnumerable<StudentRegulationsTestDto>> GetRegulationsTestsForStudent(string username)
        {
            if(await _userManager.IsInRoleUsername(username, "Student") == false) return null;

            return await _userManager.Users
                .Where(u => u.UserName == username)
                .Include(u => u.StudentRegulationsTest)
                .ThenInclude(srt => srt.RegulationTest)
                .SelectMany(u => u.StudentRegulationsTest)
                .ProjectTo<StudentRegulationsTestDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

        }

        public async Task AddStudentToTest(string username, int regulationsTestId)
        {
            var user = await _userManager.Users
                .Include(u => u.StudentRegulationsTest)
                .ThenInclude(srt => srt.RegulationTest)
                .Where(u => u.UserName == username)
                .SingleOrDefaultAsync();
            
            if(user == null) return;


            var studentId = user.Id;

            var student = _mapper.Map<StudentDto>(user);
                        
            
            // Finding if student has regulations test that is not yet held
            // To which he is already assigned
            // Information about regulations tests that were held not needed here

            var regulationsTestToDelete = student.RegulationsTests
                .Where(rt => rt.RegulationsTestDate > DateTime.Now)
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

        public async Task DeleteRegulationsTest(int regulationsTestId)
        {
            var regulationsTest = await _context.RegulationsTests.FindAsync(regulationsTestId);

            if(regulationsTest == null) return;

            _context.RegulationsTests.Remove(regulationsTest);
        }

        public async Task<IEnumerable<LectureTopicDto>> GetLectureTopics()
        {
            return await _context.LectureTopics
                .ProjectTo<LectureTopicDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<PagedList<LectureDto>> GetLecturesHeld(PaginationParams paginationParams)
        {
            var lecturesHeld =  _context.Lectures
                .ProjectTo<LectureDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(l => l.DateStart)
                .AsQueryable();

            return await PagedList<LectureDto>.CreateAsync(lecturesHeld, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<IEnumerable<LectureDto>> GetLecturesForGroup(int regulationsGroupId)
        {
            return await _context.Lectures
                .Where(lh => lh.RegulationsGroupId == regulationsGroupId)
                .ProjectTo<LectureDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<LectureWithStudentsDto> GetLecture(int lectureId)
        {
            return await _context.Lectures
                    .Include(l => l.StudentLectures)
                    .ThenInclude(sl => sl.Student)
                    .Where(l => l.LectureId == lectureId)
                    .ProjectTo<LectureWithStudentsDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
                    
        }

        public async Task<LectureDto> HoldLecture(LectureDto lectureDto, int professorId, bool addStudents)
        {
            
            var lecture = _mapper.Map<Lecture>(lectureDto);

            if(lectureDto.LectureTopic == null) return null;
            
            // In POST Request body LectureDto is sent, it doesnt have professorId nor lectureTopicId
            // So we need to add them manually
            lecture.ProfessorId = professorId;
            lecture.LectureTopicId = (int) lectureDto.LectureTopic.LectureTopicId;
            lecture.LectureTopic = null;

            var students = await _userManager.Users
                .Where(s => s.RegulationsGroupId == lectureDto.RegulationsGroupId)
                .ToListAsync();
            
            

            await _context.Lectures.AddAsync(lecture);

            if(await _context.SaveChangesAsync() > 0)
            {   if(addStudents)
                {
                    var studentLectures = new List<StudentLecture>();
                    foreach (var student in students)
                    {
                        studentLectures.Add(new StudentLecture{StudentId = student.Id, LectureId = lecture.LectureId});   
                    }

                    await _context.StudentLectures.AddRangeAsync(studentLectures);

                    await _context.SaveChangesAsync();
                }

                return _mapper.Map<LectureDto>(lecture);
            }

            return null;
        }

        public async Task<LectureTopicDto> AddLectureTopic(LectureTopicDto lectureTopicDto)
        {
            LectureTopic lectureTopic = _mapper.Map<LectureTopic>(lectureTopicDto);

            await _context.AddAsync(lectureTopic);

            if(await _context.SaveChangesAsync() > 0) return _mapper.Map<LectureTopicDto>(lectureTopic);

            return null;

        }

        public async Task AddStudentsToLecture(UsernamesToIdDto studentsLectureDto)
        {
            var lecture = await _context.Lectures.FindAsync(studentsLectureDto.Id);

            if(lecture == null) return;

            var topicId = lecture.LectureTopicId;
            var students = await _userManager.Users
                .Where(u => studentsLectureDto.Usernames.Any(el => el == u.UserName))
                .ToListAsync();

            var lectureIds = await _context.Lectures
                .Where(l => l.LectureTopicId == topicId)
                .Select(l => l.LectureId)
                .ToListAsync();

            foreach (var student in students)
            {
                if(await _userManager.IsInRoleAsync(student, "Student") == false) continue;
                foreach (var lectureId in lectureIds)
                {
                    var studentLecture = await _context.StudentLectures
                        .Where(sl => sl.StudentId == student.Id && sl.LectureId == lectureId)
                        .FirstOrDefaultAsync();
                    
                    if(studentLecture != null)
                    {
                        _context.StudentLectures.Remove(studentLecture);
                    }
                }

                var newStudentLecture = new StudentLecture
                {
                    StudentId = student.Id,
                    LectureId = studentsLectureDto.Id
                };

                await _context.AddAsync(newStudentLecture);
            }
        }

        public async Task MarkAttendances(UsernamesToIdDto studentsLectureId)
        {
            var studentIds = await _userManager.Users
                .Where(s => studentsLectureId.Usernames.Any(u => u == s.UserName))
                .Select(s => s.Id)
                .ToListAsync();

            foreach (var studentId in studentIds)
            {
                var studentLecture = await _context.StudentLectures
                .Where(sl => sl.StudentId == studentId && sl.LectureId == studentsLectureId.Id)
                .FirstOrDefaultAsync();

                if(studentLecture != null)
                {
                    studentLecture.Attendance = true;
                }
            }
            
        }

        public async Task ToggleAttendance(UsernameToIdDto studentLectureId)
        {
            var studentId = await _userManager.GetUserIdFromUsername(studentLectureId.Username);
            var studentLecture = await _context.StudentLectures
                .Where(sl => sl.StudentId == studentId && sl.LectureId == studentLectureId.Id)
                .FirstOrDefaultAsync();

            if(studentLecture != null)
            {
                studentLecture.Attendance = !studentLecture.Attendance;
            }
        }

        public async Task<IEnumerable<UsernameToBool>> GetAttendanceForlecture(int lectureId)
        {
            var attendanceList = new List<UsernameToBool>();

            var studentLectures = await _context.StudentLectures
                .Where(sl => sl.LectureId == lectureId)
                .Include(sl => sl.Student)
                .ToListAsync();

            foreach (var studentLecture in studentLectures)
            {
                attendanceList.Add(
                    new UsernameToBool
                    {
                        Username = studentLecture.Student.UserName,
                        Thruth = studentLecture.Attendance
                    }
                );
            }

            return attendanceList;            

        }

        public async Task<IEnumerable<StudentLectureDto>> GetAttendancesForStudent(string username)
        {
            if(await _userManager.IsInRoleUsername(username, "Student") == false) return null;

            var studentLectures =  await _userManager.Users
                .Where(u => u.UserName == username)
                .SelectMany(u => u.StudentLectures)
                .Include(sl => sl.Lecture)
                .ThenInclude(l => l.LectureTopic)
                .Include(sl => sl.Student)
                .ToListAsync();

            var attendance = _mapper.Map<List<StudentLectureDto>>(studentLectures);

            return attendance;
                
        }

        public async Task<LectureDto> EditLecture(LectureDto lectureDto, int lectureId)
        {
            var lecture =   await  _context.Lectures.FindAsync(lectureId);
            if(lecture == null) return null;

            if(lectureDto.Professor != null)
            {
                var professor = await _userManager.Users
                    .Where(p => p.UserName == lectureDto.Professor.Username)
                    .FirstOrDefaultAsync();
                
                if(professor != null)
                {
                    lecture.ProfessorId = professor.Id;
                }
            }

            if(lectureDto.LectureTopic != null)
            {
                lecture.LectureTopicId = (int) lectureDto.LectureTopic.LectureTopicId;

            }   

            if(lectureDto.RegulationsGroupId == 0) lectureDto.RegulationsGroupId = lecture.RegulationsGroupId;

            lecture = _mapper.Map(lectureDto, lecture);

            return _mapper.Map<LectureDto>(lecture);


        }

        public async Task<LectureTopicDto> EditLectureTopic(LectureTopicDto lectureTopicDto, int lectureTopicId)
        {
            var lectureTopic = await _context.LectureTopics.FindAsync(lectureTopicId);

            if(lectureTopic == null) return null;

            lectureTopic = _mapper.Map(lectureTopicDto, lectureTopic);

            return _mapper.Map<LectureTopicDto>(lectureTopic);

        }

    }

}