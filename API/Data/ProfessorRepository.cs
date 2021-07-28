using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
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

        public async Task<RegulationsGroupMinDto> AddRegulationsGroup(RegulationsGroupMinDto regulationsGroupMinDto)
        {
            RegulationsGroup regulationsGruop = _mapper.Map<RegulationsGroup>(regulationsGroupMinDto);

            await _context.AddAsync(regulationsGruop);
            await _context.SaveChangesAsync();

            return _mapper.Map<RegulationsGroupMinDto>(regulationsGruop);
        }

        public async Task AddStudentToGroup(ChangeGroupDto changeGroupDto)
        {
            AppUser student = await _userManager.FindByNameAsync(changeGroupDto.Username);
            if(await _userManager.IsInRoleAsync(student, "Student") == false) return;

            student.RegulationsGroupId = changeGroupDto.RegulationsGroupId;
        }

        public async Task<IEnumerable<RegulationsGroupMinDto>> GetRegulationsGroups()
        {
            return await _context.RegulationsGroups
                .ProjectTo<RegulationsGroupMinDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }

}