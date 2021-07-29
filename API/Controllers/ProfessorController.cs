using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Professor")]
    public class ProfessorController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProfessorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ------------------------
        // REGULATIONS GROUPS START
        // ------------------------

        [HttpGet("regulations-groups")]
        public async Task<ActionResult<IEnumerable<RegulationsGroupDto>>> GetRegulationsGruops()
        {
            IEnumerable<RegulationsGroupDto> regulationGroups = await _unitOfWork.ProfessorRepository.GetRegulationsGroups();

            if(regulationGroups != null) return Ok(regulationGroups);

            return BadRequest("Something went wrong.");
        }

        [HttpGet("regulations-groups/{regulationsGroupId}")]
        public async Task<ActionResult<IEnumerable<RegulationsGroupDto>>> GetRegulationsGroup(int regulationsGroupId)
        {
            RegulationsGroupDto regulationGroup = await _unitOfWork.ProfessorRepository.GetRegulationsGroup(regulationsGroupId);

            if(regulationGroup != null) return Ok(regulationGroup);

            return BadRequest("Something went wrong.");
        }
        [HttpPost("regulations-groups")]
        public async Task<ActionResult<RegulationsGroupDto>> AddRegulationsGroup(RegulationsGroupDto regulationsGroupDto)
        {
            RegulationsGroupDto regulationsGroup = await _unitOfWork.ProfessorRepository.AddRegulationsGroup(regulationsGroupDto);

            if(regulationsGroup != null) return Ok(regulationsGroup);

            return BadRequest("Something went wrong.");
        }

        [HttpPost("regulations-group-student")]
        public async Task<ActionResult<IEnumerable<ChangeGroupResultDto>>> AddStudentToGroup(ChangeGroupDto changeGroupDto)
        {
            IEnumerable<ChangeGroupResultDto> changeGroupResults = await _unitOfWork.ProfessorRepository.AddStudentToGroup(changeGroupDto);

            if(await _unitOfWork.SaveAllChanges() > 0) return Ok(changeGroupResults);

            return BadRequest("Did not add any student.");
            
        }

        // ------------------------
        // REGULATIONS GROUPS END
        // ------------------------

        // ------------------------
        // REGULATIONS TESTS START
        // ------------------------

        [HttpGet("regulations-tests")]
        public async Task<ActionResult<IEnumerable<RegulationsTestDto>>> GetRegulationsTests()
        {
            IEnumerable<RegulationsTestDto> regulationTests = await _unitOfWork.ProfessorRepository.GetRegulationsTests();

            if(regulationTests != null) return Ok(regulationTests);

            return BadRequest("Something went wrong.");
        }

        [HttpGet("regulations-tests/{regulationsGroupId}")]
        public async Task<ActionResult<IEnumerable<RegulationsTestDto>>> GetRegulationsTest(int regulationsTestId)
        {
            RegulationsTestDto regulationTest = await _unitOfWork.ProfessorRepository.GetRegulationsTest(regulationsTestId);

            if(regulationTest != null) return Ok(regulationTest);

            return BadRequest("Something went wrong.");
        }
        [HttpPost("regulations-tests")]
        public async Task<ActionResult<RegulationsTestDto>> AddRegulationsTest(RegulationsTestDto regulationsTestDto)
        {
            RegulationsTestDto regulationsTest = await _unitOfWork.ProfessorRepository.AddRegulationsTest(regulationsTestDto);

            if(regulationsTest != null) return Ok(regulationsTest);

            return BadRequest("Something went wrong.");
        }
        [HttpDelete("regulations-test/{id}")]
        public async Task<ActionResult> DeleteRegulationsTest(int id)
        {
            await _unitOfWork.ProfessorRepository.DeleteRegulationsTest(id);

            if(await _unitOfWork.SaveAllChanges() > 0) return Ok();

            return BadRequest("Failed to delete test.");
        }

        [HttpPost("regulations-test-student")]
        public async Task<ActionResult> AddStudentToTest(ChangeTestDto changeTestDto)
        {
            await _unitOfWork.ProfessorRepository.AddStudentToTest(changeTestDto.Username, changeTestDto.RegulationsTestId);

            if(await _unitOfWork.SaveAllChanges() > 0) return Ok();

            return BadRequest("Failed to add student to test.");
            
        }

        [HttpDelete("regulations-test-student")]
        public async Task<ActionResult> DeleteStudentFromTest(ChangeTestDto changeTestDto)
        {
            await _unitOfWork.ProfessorRepository.DeleteStudentFromTest(changeTestDto.Username, changeTestDto.RegulationsTestId);

            if(await _unitOfWork.SaveAllChanges() > 0) return Ok();

            return BadRequest("Failed to delete student from test");
        }

        // ------------------------
        // REGULATIONS TESTS END
        // ------------------------



    }
}