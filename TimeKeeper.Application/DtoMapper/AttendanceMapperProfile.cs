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
        }
    }
}