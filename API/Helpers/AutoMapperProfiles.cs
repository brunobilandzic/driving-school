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

            CreateMap<DrivingSession, DrivingSessionDto>()
                .ForMember(
                    dest => dest.DriverUsername,
                    opt => opt.MapFrom(
                        src => src.Driver.UserName
                    ))
                .ForMember(
                    dest => dest.InstructorUsername,
                    opt => opt.MapFrom(
                        src => src.Instructor.UserName
                    ));

            CreateMap<DrivingSessionDto, DrivingSession>();

            CreateMap<DrivingTest, DrivingTestDto>()
                .ForMember(
                    dest => dest.ExaminerUsername,
                    opt => opt.MapFrom(
                        src => src.Examiner.UserName
                    )
                );

            CreateMap<Lecture, LectureDto>();
                

            CreateMap<LectureDto, Lecture>()
                .ForMember(
                    dest => dest.Professor,
                    opt => opt.Ignore()
                    )
                .ForMember(
                    dest => dest.LectureTopic,
                    opt => opt.Ignore()
                );

            CreateMap<RegulationsGroup, RegulationsGroupDto>();

            CreateMap<RegulationsGroupDto, RegulationsGroup>();

            CreateMap<RegulationsGroup, RegulationsGroupMinDto>();

            CreateMap<LectureTopic, LectureTopicDto>();

            CreateMap<LectureTopicDto, LectureTopic>();

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

            CreateMap<Lecture, LectureWithStudentsDto>()
                .ForMember(
                    ls => ls.Students,
                    opt => opt.MapFrom(
                        src => src.StudentLectures.Select(sl => sl.Student)
                    ));



        }
    }
}