using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Professor")]
    public class ProfessorController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public ProfessorController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // ------------------------
        // REGULATIONS GROUPS START
        // ------------------------

        [HttpGet("regulations-groups")]
        public async Task<ActionResult<PagedList<RegulationsGroupMinDto>>> GetRegulationsGruops([FromQuery] PaginationParams paginationParams)
        {
            var regulationGroups = await _unitOfWork.ProfessorRepository.GetRegulationsGroups(paginationParams);

            if (regulationGroups == null) return BadRequest("Something went wrong."); 

            Response.AddPaginationHeader(regulationGroups.CurrentPage, regulationGroups.PageSize, regulationGroups.TotalCount, regulationGroups.TotalPages);

            return Ok(regulationGroups);
        }

        [HttpGet("regulations-groups/{regulationsGroupId}")]
        public async Task<ActionResult<RegulationsGroupDto>> GetRegulationsGroup(int regulationsGroupId)
        {
            RegulationsGroupDto regulationGroup = await _unitOfWork.ProfessorRepository.GetRegulationsGroup(regulationsGroupId);

            if (regulationGroup != null) return Ok(regulationGroup);

            return NotFound("Something went wrong.");
        }
        [HttpPost("regulations-groups")]
        public async Task<ActionResult<RegulationsGroupDto>> AddRegulationsGroup(RegulationsGroupDto regulationsGroupDto)
        {
            RegulationsGroupDto regulationsGroup = await _unitOfWork.ProfessorRepository.AddRegulationsGroup(regulationsGroupDto);

            if (regulationsGroup != null) return Ok(regulationsGroup);

            return BadRequest("Something went wrong.");
        }

        [HttpPost("regulations-group-student")]
        public async Task<ActionResult<IEnumerable<ChangeGroupResultDto>>> AddStudentToGroup(UsernamesToIdDto changeGroupDto)
        {
            IEnumerable<ChangeGroupResultDto> changeGroupResults = await _unitOfWork.ProfessorRepository.AddStudentToGroup(changeGroupDto);

            if (await _unitOfWork.SaveAllChanges() > 0) return Ok(changeGroupResults);

            return BadRequest("Did not add any student.");

        }

        // ------------------------
        // REGULATIONS GROUPS END
        // ------------------------

        // ------------------------
        // REGULATIONS TESTS START
        // ------------------------

        [HttpGet("regulations-tests")]
        public async Task<ActionResult<PagedList<RegulationsTestDto>>> GetRegulationsTests([FromQuery] PaginationParams paginationParams)
        {
            PagedList<RegulationsTestDto> regulationTests = await _unitOfWork.ProfessorRepository.GetRegulationsTests(paginationParams);

            if (regulationTests == null) return BadRequest("Something went wrong.");

            Response.AddPaginationHeader(regulationTests.CurrentPage, regulationTests.PageSize, regulationTests.TotalCount, regulationTests.TotalPages);
            
            return Ok(regulationTests);
             
        }

        [HttpGet("regulations-tests/{regulationsTestId}")]
        public async Task<ActionResult<RegulationsTestDto>> GetRegulationsTest(int regulationsTestId)
        {
            RegulationsTestDto regulationTest = await _unitOfWork.ProfessorRepository.GetRegulationsTest(regulationsTestId);

            if (regulationTest != null) return Ok(regulationTest);

            return NotFound("Something went wrong.");
        }
        [HttpPost("regulations-tests")]
        public async Task<ActionResult<RegulationsTestDto>> AddRegulationsTest(RegulationsTestDto regulationsTestDto)
        {
            RegulationsTestDto regulationsTest = await _unitOfWork.ProfessorRepository.AddRegulationsTest(regulationsTestDto);

            if (regulationsTest != null) return Ok(regulationsTest);

            return BadRequest("Something went wrong.");
        }
        [HttpDelete("regulations-test/{id}")]
        public async Task<ActionResult> DeleteRegulationsTest(int id)
        {
            await _unitOfWork.ProfessorRepository.DeleteRegulationsTest(id);

            if (await _unitOfWork.SaveAllChanges() > 0) return Ok();

            return BadRequest("Failed to delete test.");
        }

        [HttpPost("regulations-test-student")]
        public async Task<ActionResult> AddStudentToTest(UsernameToIdDto changeTestDto)
        {
            await _unitOfWork.ProfessorRepository.AddStudentToTest(changeTestDto.Username, changeTestDto.Id);

            if (await _unitOfWork.SaveAllChanges() > 0) return Ok();

            return BadRequest("Failed to add student to test.");

        }

        [HttpDelete("regulations-test-student")]
        public async Task<ActionResult> DeleteStudentFromTest(UsernameToIdDto changeTestDto)
        {
            await _unitOfWork.ProfessorRepository.DeleteStudentFromTest(changeTestDto.Username, changeTestDto.Id);

            if (await _unitOfWork.SaveAllChanges() > 0) return Ok();

            return BadRequest("Failed to delete student from test");
        }

        // ------------------------
        // REGULATIONS TESTS END
        // ------------------------

        // ------------------------
        // LECTURES START
        // ------------------------

        [HttpGet("lecture-topics")]
        public async Task<ActionResult<PagedList<LectureTopicDto>>> GetLectureTopics([FromQuery] PaginationParams paginationParams)
        {
            var lectureTopics = await _unitOfWork.ProfessorRepository.GetLectureTopics(paginationParams);

            if (lectureTopics == null) return BadRequest("Something went wrong.");

            Response.AddPaginationHeader(lectureTopics.CurrentPage, lectureTopics.PageSize, lectureTopics.TotalCount, lectureTopics.TotalPages);
            
            return Ok(lectureTopics);
        }

        [HttpGet("lectures")]
        public async Task<ActionResult<PagedList<LectureDto>>> GetLectures([FromQuery] PaginationParams paginationParams)
        {
            var lectures = await _unitOfWork.ProfessorRepository.GetLecturesHeld(paginationParams);

            if (lectures == null) return BadRequest("Something went wrong.");

            Response.AddPaginationHeader(lectures.CurrentPage, lectures.PageSize, lectures.TotalCount, lectures.TotalPages);
            
            return Ok(lectures);

            
        }

        [HttpGet("lectures/{lectureId}")]
        public async Task<ActionResult<IEnumerable<LectureWithStudentsDto>>> GetLecture(int lectureId)
        {
            var lecture = await _unitOfWork.ProfessorRepository.GetLecture(lectureId);

            if (lecture != null) return Ok(lecture);

            return NotFound("Failed to fetch lecture.");
        }

        [HttpGet("lectures/group/{regulationsGroupId}")]
        public async Task<ActionResult<IEnumerable<LectureDto>>> GetLecturesForGroup(int regulationsGroupId)
        {
            var lectures = await _unitOfWork.ProfessorRepository.GetLecturesForGroup(regulationsGroupId);

            if (lectures != null) return Ok(lectures);

            return BadRequest("Failed to fetch lectures.");
        }

        [HttpPost("hold-lecture")]
        public async Task<ActionResult<LectureDto>> HoldLecture(LectureDto lectureDto, [FromQuery] string addStudents)
        {
            var professorId = User.GetUserId();
            lectureDto = await _unitOfWork.ProfessorRepository.HoldLecture(lectureDto, professorId, addStudents == "true" ? true: false);

            if(lectureDto != null) return Ok(lectureDto);

            return BadRequest("Failed to hold a lecture.");
        }

        [HttpPut("edit-lecture/{lectureId}")]
        public async Task<ActionResult<LectureDto>> EditLecture(int lectureId, LectureDto lectureDto)
        {
            lectureDto.LectureId = lectureId;
            var updatedLecture = await _unitOfWork.ProfessorRepository.EditLecture(lectureDto, lectureId);

            if(await _unitOfWork.SaveAllChanges() > 0)
                return Ok(updatedLecture);
            
            return BadRequest("Failed to update lecture.");
        }

        [HttpPost("lecture-students")]
        public async Task<ActionResult<LectureDto>> AddStudentsToLecture(UsernamesToIdDto studentsLecture)
        {
            await _unitOfWork.ProfessorRepository.AddStudentsToLecture(studentsLecture);

            if(await _unitOfWork.SaveAllChanges() > 0) return Ok();

            return BadRequest("Failed to add students to lecture.");
        }
        [HttpGet("attendances/{lectureId}")]
        public async Task<ActionResult<IEnumerable<UsernameToBool>>> GetAttendanceForlecture(int lectureId)
        {
            return Ok(await _unitOfWork.ProfessorRepository.GetAttendanceForlecture(lectureId));
        }

         [HttpPost("attendances")]
        public async Task<ActionResult<LectureDto>> MarkAttendance(UsernamesToIdDto studentsLecture)
        {            
            await _unitOfWork.ProfessorRepository.MarkAttendances(studentsLecture);

            if(await _unitOfWork.SaveAllChanges() > 0) return Ok();

            return BadRequest("Failed to mark attendance.");
        }

        [HttpPost("attendances-toggle")]
        public async Task<ActionResult<LectureDto>> ToggleAttendance(UsernameToIdDto studentLecture)
        {            
            await _unitOfWork.ProfessorRepository.ToggleAttendance(studentLecture);

            if(await _unitOfWork.SaveAllChanges() > 0) return Ok();

            return BadRequest("Failed to mark attendance.");
        }


        


        [HttpPost("lecture-topics")]
        public async Task<ActionResult<LectureTopicDto>> PostLectureTopic(LectureTopicDto lectureTopicDto)
        {
            var lectureTopic = await _unitOfWork.ProfessorRepository.AddLectureTopic(lectureTopicDto);

            if (lectureTopic != null) return Ok(lectureTopic);

            return BadRequest("Failed to add lecture topic.");
        }

        [HttpPut("edit-lecture-topic/{lectureTopicId}")]
        public async Task<ActionResult<LectureTopicDto>> EditLectureTopic(int lectureTopicId, LectureTopicDto lectureTopicDto)
        {
            lectureTopicDto.LectureTopicId = lectureTopicId;
            var updatedLectureTopic = await _unitOfWork.ProfessorRepository.EditLectureTopic(lectureTopicDto, lectureTopicId);

            if(await _unitOfWork.SaveAllChanges() > 0)
                return Ok(updatedLectureTopic);
            
            return BadRequest("Failed to update lecture Topic.");
        }

    }
}