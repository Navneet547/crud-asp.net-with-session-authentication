using Microsoft.EntityFrameworkCore;

namespace AuthCrudApp.Models
{
    public class AuthDBContext: DbContext
    {
        public AuthDBContext (DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }

   
}
