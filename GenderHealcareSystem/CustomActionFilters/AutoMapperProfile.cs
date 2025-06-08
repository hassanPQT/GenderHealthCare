using AutoMapper;
using GenderHealcareSystem.DTO;

namespace GenderHealcareSystem.CustomActionFilters
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<Service, AddServiceRequestDto>().ReverseMap();
            CreateMap<Service, UpdateServiceRequestDto>().ReverseMap();
        }
    }
}
