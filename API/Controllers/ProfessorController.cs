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

        [HttpGet("regulations-groups")]
        public async Task<ActionResult<IEnumerable<RegulationsGroupMinDto>>> GetRegulationsGruops()
        {
            IEnumerable<RegulationsGroupMinDto> regulationGroups = await _unitOfWork.ProfessorRepository.GetRegulationsGroups();

            if(regulationGroups != null) return Ok(regulationGroups);

            return BadRequest("Something went wrong.");
        }
        [HttpPost("regulations-groups")]
        public async Task<ActionResult<RegulationsGroupMinDto>> AddRegulationsGroup(RegulationsGroupMinDto regulationsGroupMinDto)
        {
            RegulationsGroupMinDto regulationsGroup = await _unitOfWork.ProfessorRepository.AddRegulationsGroup(regulationsGroupMinDto);

            if(regulationsGroup != null) return Ok(regulationsGroup);

            return BadRequest("Something went wrong.");
        }

        [HttpPost("regulations-group-student")]
        public async Task<ActionResult> AddStudentToGroup(ChangeGroupDto changeGroupDto)
        {
            await _unitOfWork.ProfessorRepository.AddStudentToGroup(changeGroupDto);

            if(await _unitOfWork.SaveAllChanges() > 0) return Ok();

            return BadRequest("Student to group add fail");
            
        }

        



    }
}