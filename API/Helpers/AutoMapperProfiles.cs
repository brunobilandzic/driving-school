using System.Linq;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, PersonDto>();

            CreateMap<RegisterDto, AppUser>();

            CreateMap<DrivingSession, DrivingSessionDto>();

            CreateMap<DrivingTest, DrivingTestDto>();

            CreateMap<Lecture, LectureDto>();

            CreateMap<RegulationsGroup, RegulationsGroupDto>();

            CreateMap<RegulationsTest, RegulationsTestDto>();

            

        }
    }
}