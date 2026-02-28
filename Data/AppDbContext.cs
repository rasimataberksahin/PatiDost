using App.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<OwnerEntity> Owners => Set<OwnerEntity>();
        public DbSet<AnimalEntity> Animals => Set<AnimalEntity>();
        public DbSet<VeterinarianEntity> Veterinarians => Set<VeterinarianEntity>();
        public DbSet<AppointmentEntity> Appointments => Set<AppointmentEntity>();
    }
}