using LearnDartsMath.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnDartsMath.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Player> Players => Set<Player>();
    public DbSet<TrainingSession> TrainingSessions => Set<TrainingSession>();
    public DbSet<ThrowEntry> ThrowEntries => Set<ThrowEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Player>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<TrainingSession>()
            .Property(t => t.Mode)
            .IsRequired()
            .HasMaxLength(50);
    }
}