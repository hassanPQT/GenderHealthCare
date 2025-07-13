using AutoMapper;
using DataAccess.Entities;
using GenderHealcareSystem.DTO;
using GenderHealcareSystem.DTO.Request;

namespace GenderHealcareSystem.CustomActionFilters
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Map Service
            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<Service, AddServiceRequest>().ReverseMap();
            CreateMap<Service, UpdateServiceRequest>().ReverseMap();

            // Map Blog
            CreateMap<Blog, BlogDto>()
                        .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.User))
                        .ReverseMap();
            CreateMap<Blog, AddBlogRequest>().ReverseMap();
            CreateMap<Blog, UpdateBlogRequest>().ReverseMap();

            // Map User
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UpdateUserRequest, User>().ForMember(dest => dest.Birthday,
                opt => opt.MapFrom(src => src.DateOfBirth));

            // Map Staff, Consultant
            CreateMap<User, StaffConsultantDto>().ReverseMap();
            CreateMap<AddStaffConsultantRequest, User>().ReverseMap();
            CreateMap<User, UpdateStaffConsultantRequest>().ReverseMap();

            // Map Role
            CreateMap<Role, RoleDto>().ReverseMap();

            // Map Staff Schedule
            CreateMap<StaffSchedule, StaffScheduleDto>().ReverseMap();
            CreateMap<StaffSchedule, AddScheduleRequest>().ReverseMap();
            CreateMap<StaffSchedule, UpdateScheduleRequest>().ReverseMap();

            // Map Appointment
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Consultant, opt => opt.MapFrom(src => src.Consultant))
                .ForMember(dest => dest.StaffSchedule, opt => opt.MapFrom(src => src.StaffSchedule))
                .ReverseMap();

            CreateMap<Appointment, AddAppointmentRequest>().ReverseMap();
            CreateMap<Appointment, UpdateAppointmentRequest>().ReverseMap();

            // Map test result
            CreateMap<TestResult, TestResultDto>().ReverseMap();
            CreateMap<TestResult, AddResultDto>().ReverseMap();
            CreateMap<TestResult, UpdateResultDto>().ReverseMap();


        }
    }
}
