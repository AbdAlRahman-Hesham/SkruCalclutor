using Microsoft.EntityFrameworkCore;
using Contexts.Models;
namespace Contexts.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{


    public DbSet<SavedPlayer> SavedPlayers { get; set; }

    // Optional: Configure the model if needed
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SavedPlayer>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
        });
    }
}