using Login_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Login_Project.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Logins> Logins { get; set; }
        public DbSet<Student>Student { get; set; }
    }
    
}
