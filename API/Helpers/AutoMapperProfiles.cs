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

            CreateMap<RegulationsGroupMinDto, RegulationsGroup>();

            CreateMap<RegulationsGroup, RegulationsGroupMinDto>();

            CreateMap<LectureTopic, LectureTopicDto>();

            CreateMap<LectureTopicDto, LectureTopic>();

            CreateMap<RegulationsTest, RegulationsTestDto>();
            
            CreateMap<RegulationsTestDto, RegulationsTest>();

            CreateMap<RegulationsTestPostDto, RegulationsTest>()
                .ForMember(
                    dest => dest.RegulationsTestId,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.ExaminerId,
                    opt => opt.Ignore()
                );

            CreateMap<AppUser, DriverDto>();
            CreateMap<AppUser, StudentDto>()
                .ForMember(
                    dest => dest.Lectures,
                    opt => opt.MapFrom(
                        src => src.StudentLectures.OrderByDescending(sl => sl.Lecture.DateStart)
                    )    
                )
                .ForMember(
                    dest => dest.RegulationsTests,
                    opt => opt.MapFrom(
                        src => src.StudentRegulationsTest.OrderByDescending(rt => rt.RegulationTest.DateStart)
                    ) 
                );

            CreateMap<Lecture, LectureWithStudentsDto>()
                .ForMember(
                    ls => ls.Students,
                    opt => opt.MapFrom(
                        src => src.StudentLectures.Select(sl => sl.Student)
                    ));

            CreateMap<ExamineDrivingTestDto, DrivingTest>();

            CreateMap<DrivingSessionEditInstructorDto, DrivingSession>();

            CreateMap<DrivingSessionEditStudentDto, DrivingSession>();

            CreateMap<StudentLecture, IdToBool>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(
                        src => src.LectureId
                    )
                )
                .ForMember(
                    dest => dest.Thruth,
                    opt => opt.MapFrom(
                        src => src.Attendance
                    )
                );

            CreateMap<StudentLecture, StudentLectureDto>()
                .ForMember(
                    dest => dest.StudentUsername,
                    opt => opt.MapFrom(
                        src => src.Student.UserName
                    )
                )
                .ForMember(
                    dest => dest.LectureTopic,
                    opt => opt.MapFrom(
                        src => src.Lecture.LectureTopic.Title
                    )
                )
                .ForMember(
                    dest => dest.DateStart,
                    opt => opt.MapFrom(
                        src => src.Lecture.DateStart
                    )
                );

            CreateMap<StudentRegulationsTest, StudentRegulationsTestDto>()
                .ForMember(
                    dest => dest.StudentUsername,
                    opt => opt.MapFrom(
                        src => src.Student.UserName
                    )
                )
                .ForMember(
                    dest => dest.RegulationsTestDate,
                    opt => opt.MapFrom(
                        src => src.RegulationTest.DateStart
                    )
                );
        }
    }
}