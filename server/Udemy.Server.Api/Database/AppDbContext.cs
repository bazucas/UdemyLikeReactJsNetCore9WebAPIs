using Microsoft.EntityFrameworkCore;
using Udemy.Server.Api.Entities;

namespace Udemy.Server.Api.Database;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Video> Videos { get; set; }
    public DbSet<UserVideoProgress> UserVideoProgresses { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuração da chave composta para UserVideoProgress
        modelBuilder.Entity<UserVideoProgress>()
            .HasKey(uvp => new { uvp.UserId, uvp.VideoId });

        // Relacionamento entre User e UserVideoProgress
        modelBuilder.Entity<UserVideoProgress>()
            .HasOne(uvp => uvp.User)
            .WithMany(u => u.UserVideoProgresses)
            .HasForeignKey(uvp => uvp.UserId);

        // Relacionamento entre Video e UserVideoProgress
        modelBuilder.Entity<UserVideoProgress>()
            .HasOne(uvp => uvp.Video)
            .WithMany(v => v.UserVideoProgresses)
            .HasForeignKey(uvp => uvp.VideoId);
    }
}