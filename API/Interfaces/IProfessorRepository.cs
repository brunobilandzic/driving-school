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
        Task<RegulationsGroupDto> AddRegulationsGroup(RegulationsGroupDto regulationsGroup);

        Task<IEnumerable<RegulationsGroupDto>> GetRegulationsGroups();

        Task<IEnumerable<ChangeGroupResultDto>> AddStudentToGroup(ChangeGroupDto changeGroupDto);

        Task<RegulationsGroupDto> GetRegulationsGroup(int regulationsGroupId);


        Task<RegulationsTestDto> AddRegulationsTest(RegulationsTestDto regulationsTest);

        Task<IEnumerable<RegulationsTestDto>> GetRegulationsTests();

        Task<RegulationsTestDto> GetRegulationsTest(int regulationsTestId);

        Task AddStudentToTest(string username, int regulationsTestId);

        Task DeleteStudentFromTest(string username, int regulationsTestId);

        Task DeleteRegulationsTest(int regulationsTestId);

    }
}