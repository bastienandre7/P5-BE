using ExpressVoitureApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitureApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        { 

        }
        public DbSet<User> User {  get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Finance> Finances { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
