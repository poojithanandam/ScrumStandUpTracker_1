using AutoMapper;
using ScrumStandUpTracker_1.DTOs;
using ScrumStandUpTracker_1.Models;

namespace ScrumStandUpTracker_1.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Developer, DeveloperDTO>().ReverseMap();
            CreateMap<StatusForm, StatusFormDTO>();
            CreateMap<StatusFormDTO, StatusForm>().ForMember(d => d.developer, o => o.Ignore());
        }
    }
}
