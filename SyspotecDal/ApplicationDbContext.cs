using SyspotecDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SyspotecDal
{
    public class ApplicationDbContext : DbContext, IDisposable
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {

        }
        public DbSet<Company> Company { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<TypeIdentification> TypeIdentification { get; set; }
        public DbSet<User> User { get; set; }

    }
}
