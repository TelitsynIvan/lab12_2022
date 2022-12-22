using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public class ApplicationContext :DbContext
{
    public DbSet<Auditorium> Auditoriums { get; set; } = null!;
    public DbSet<Building> Buildings { get; set; } = null!;
    public DbSet<AuditoriumGroup> AuditoriumGroups { get; set; } = null!;

    public ApplicationContext()
    {
        Database.EnsureCreated();
    }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
    :base(options)
    {
        Database.EnsureCreated();
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlite(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Building>().HasMany(u => u.AuditoriumGroups).
            WithOne(p => p.Building).OnDelete(DeleteBehavior.Cascade);
    }
}