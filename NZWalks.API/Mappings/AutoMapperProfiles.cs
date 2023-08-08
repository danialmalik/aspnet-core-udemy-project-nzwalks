using AutoMapper;
using NZWalks.API.Models.DTO;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<RegionDtoV2, Region>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom("RegionName"))
            .ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
            CreateMap<WalkDto, Walk>().ReverseMap();

            CreateMap<DifficultyDto, Difficulty>().ReverseMap();
        }

    }
}
