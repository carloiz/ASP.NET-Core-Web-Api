using ASP.NetCoreWebApi.common.IService;
using ASP.NetCoreWebApi.src.Users;

namespace ASP.NetCoreWebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            //// Register services
            //builder.Services
            //                .AddScoped<RegistryKeys>()
            //                .AddScoped<IUsers, UsersService>()
            //                .AddScoped<UsersService>()
            //                ;

            services.AddScoped<
                ICrudService<UsersEntity, string, UsersDto, CreateUserDto, UpdateUserDto>,
                CrudService<UsersEntity, string, UsersDto, CreateUserDto, UpdateUserDto>>();

            return services;
        }
    }
}
