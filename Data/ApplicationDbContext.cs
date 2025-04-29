using Microsoft.EntityFrameworkCore;
using PetProfileApp.Models;

namespace PetProfileApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<PetProfile> PetProfile { get; set; }
    }
}
