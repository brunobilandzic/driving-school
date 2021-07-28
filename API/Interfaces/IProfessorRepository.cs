using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;

// Handles data manipulation for Professor role
// e.g.
// Creating and assigning to RegulationsGroups, RegulationsTests
// Lectures could be here or in an individual Repo *NOT YET DECIDED*

namespace API.Interfaces
{
    public interface IProfessorRepository
    {
        Task<RegulationsGroupMinDto> AddRegulationsGroup(RegulationsGroupMinDto regulationsGroup);

        Task<IEnumerable<RegulationsGroupMinDto>> GetRegulationsGroups();

        Task AddStudentToGroup(ChangeGroupDto changeGroupDto);
    }
}