using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public class AssemblyContext :DbContext
{
    public DbSet<Detail> Details { get; set; } = null!;
     public DbSet<Assembly> Assemblies { get; set; } = null!;
     public DbSet<Part> Parts { get; set; } = null!;

    public AssemblyContext()
    {
        Database.EnsureCreated();
    }
    
    public AssemblyContext(DbContextOptions<AssemblyContext> options)
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
    }
}