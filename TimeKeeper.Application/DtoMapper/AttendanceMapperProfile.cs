using AutoMapper;
using TimeKeeper.Core.DTO;
using TimeKeeper.Core.Entities;

namespace TimeKeeper.Application.DtoMapper
{
    public class AttendanceMapperProfile : Profile
    {
        public AttendanceMapperProfile()
        {
            CreateMap<AttendanceEntry, AttendanceEntryDto>();
            CreateMap<AttendanceEntryDto, AttendanceEntry>();

            CreateMap<Employee, EmployeeDto>()
                       .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.UserDetails.FirstName))
                       .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.UserDetails.LastName))
                       .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserDetails.Email))
                       .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.UserDetails.PhoneNumber))
                       .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.UserDetails.RoleId));

            CreateMap<EmployeeDto, Employee>()
                .ForPath(dest => dest.UserDetails.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForPath(dest => dest.UserDetails.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForPath(dest => dest.UserDetails.Email, opt => opt.MapFrom(src => src.Email))
                .ForPath(dest => dest.UserDetails.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForPath(dest => dest.UserDetails.RoleId, opt => opt.MapFrom(src => src.RoleId));
        }
    }
}