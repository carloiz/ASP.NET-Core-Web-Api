using ASP.NetCoreWebApi.src.Users;
using Microsoft.EntityFrameworkCore;

namespace ASP.NetCoreWebApi.EFCore
{
    public class EF_DataContext : DbContext
    {
        public EF_DataContext(DbContextOptions<EF_DataContext> options) : base(options)
        {
        }

        public DbSet<UsersEntity> Users { get; set; }
    }
}
