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
            CreateMap<AppUser, PersonDto>()
                .ForMember(
                    dest => dest.Roles,
                    opt => opt.MapFrom(
                        src => src.UserRoles.Select(ur => ur.Role.Name)
                ));


            CreateMap<RegisterDto, AppUser>();

            CreateMap<DrivingSession, DrivingSessionDto>();

            CreateMap<DrivingTest, DrivingTestDto>();

            CreateMap<Lecture, LectureDto>();

            CreateMap<RegulationsGroup, RegulationsGroupDto>();

            CreateMap<RegulationsGroupDto, RegulationsGroup>();

            CreateMap<RegulationsTest, RegulationsTestDto>()
                .ForMember(
                    dest => dest.Students,
                    opt => opt.MapFrom(
                        src => src.StudentRegulationsTest.Select(srt => srt.Student)
                    )
                    );
            
            CreateMap<RegulationsTestDto, RegulationsTest>();

            CreateMap<AppUser, StudentDto>()
                .ForMember(
                    dest => dest.Lectures,
                    opt => opt.MapFrom(
                        src => src.StudentLectures.Select(sl => sl.Lecture)
                    )    
                )
                .ForMember(
                    dest => dest.RegulationsTests,
                    opt => opt.MapFrom(
                        src => src.StudentRegulationsTest.Select(srt => srt.RegulationTest)
                    ) 
                );



        }
    }
}