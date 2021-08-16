using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CommonController: BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommonController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("lecture-topics")]
        public async Task<ActionResult<IEnumerable<LectureTopicDto>>> GetLectureTopics()
        {
            var lectureTopics = await _unitOfWork.ProfessorRepository
                .GetLectureTopics();

            if(lectureTopics == null) return BadRequest("Failed to fetch lecture topics.");

            return Ok(lectureTopics);
        }

        [Authorize]
        [HttpGet("regulations-groups-active")]
        public async Task<ActionResult<IEnumerable<RegulationsGroupMinDto>>> GetRegulationsGruopsActive()
        {
            var regulationGroups = await _unitOfWork.ProfessorRepository.GetRegulationsGroupsActive();

            if (regulationGroups == null) return BadRequest("Something went wrong."); 

            return Ok(regulationGroups);
        }
    }
}