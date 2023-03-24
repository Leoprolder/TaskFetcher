using AutoMapper;
using TaskFetcher.Models.DTO;
using TaskFetcher.Models.Entity;

namespace TaskFetcher.Models.MappingProfiles
{
    public class TimeTaskMappingProfile : Profile
    {
        public TimeTaskMappingProfile()
        {
            CreateMap<TimeTask, TimeTaskDTO>()
                .ForMember(m => m.Status, o => o.MapFrom(x => x.Status.ToString()))
                .ReverseMap();
        }
    }
}
