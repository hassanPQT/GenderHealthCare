using AutoMapper;
using DataAccess.Entities;
using GenderHealcareSystem.DTO;
using GenderHealcareSystem.DTO.Request;
using System.Globalization;

namespace GenderHealcareSystem.CustomActionFilters
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<Service, AddServiceRequestDto>().ReverseMap();
            CreateMap<Service, UpdateServiceRequestDto>().ReverseMap();
            CreateMap<UpdateUserRequest, User>()
                    .ForMember(dest => dest.Dob,
                        opt => opt.MapFrom(src => src.DateOfBirth));
                        }
    }
}
