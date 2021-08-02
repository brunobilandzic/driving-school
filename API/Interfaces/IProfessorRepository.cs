using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

// Handles data manipulation for Professor role
// e.g.
// Creating and assigning to RegulationsGroups, RegulationsTests
// Lectures could be here or in an individual Repo *NOT YET DECIDED*

namespace API.Interfaces
{
    public interface IProfessorRepository
    {
        Task<RegulationsGroupDto> AddRegulationsGroup(RegulationsGroupDto regulationsGroup);

        Task<PagedList<RegulationsGroupMinDto>> GetRegulationsGroups(PaginationParams paginationParams);

        Task<IEnumerable<ChangeGroupResultDto>> AddStudentToGroup(UsernamesToIdDto changeGroupDto);

        Task<RegulationsGroupDto> GetRegulationsGroup(int regulationsGroupId);


        Task<RegulationsTestDto> AddRegulationsTest(RegulationsTestDto regulationsTest);

        Task<PagedList<RegulationsTestDto>> GetRegulationsTests(PaginationParams paginationParams);

        Task<RegulationsTestDto> GetRegulationsTest(int regulationsTestId);

        Task AddStudentToTest(string username, int regulationsTestId);

        Task DeleteStudentFromTest(string username, int regulationsTestId);

        Task DeleteRegulationsTest(int regulationsTestId);

        Task<PagedList<LectureTopicDto>> GetLectureTopics(PaginationParams paginationParams);

        Task<PagedList<LectureDto>> GetLecturesHeld(PaginationParams paginationParams);

        Task<IEnumerable<LectureDto>> GetLecturesForGroup(int regulationsGroupId);

        Task<LectureWithStudentsDto> GetLecture(int lectureId);

        Task<LectureDto> HoldLecture(LectureDto lectureDto, int professorId, bool addStudents);

        Task<LectureDto> EditLecture(LectureDto lectureDto, int lectureId);

        Task AddStudentsToLecture(UsernamesToIdDto studentsLectureDto);

        Task MarkAttendances(UsernamesToIdDto studentsLectureId);

        Task ToggleAttendance(UsernameToIdDto studentLectureId);

        Task<IEnumerable<UsernameToBool>> GetAttendanceForlecture(int lectureId);

        Task<LectureTopicDto> AddLectureTopic(LectureTopicDto lectureTopicDto);

        Task<LectureTopicDto> EditLectureTopic(LectureTopicDto lectureTopicDto, int lectureTopicId);


    }
}