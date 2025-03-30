using ASP.NetCoreWebApi.src.Users;
using AutoMapper;


namespace ASP.NetCoreWebApi.common
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            // Users Mapping
            CreateMap<UsersEntity, UsersDto>()
                //.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
                .ReverseMap();
            CreateMap<CreateUserDto, UsersEntity>().ReverseMap();
            CreateMap<UpdateUserDto, UsersEntity>().ReverseMap();
        }
    }
}
