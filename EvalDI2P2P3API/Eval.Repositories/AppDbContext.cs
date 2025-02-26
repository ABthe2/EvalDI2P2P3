using Eval.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Eval.Repositories;

public class AppDbContext : DbContext
{
    public DbSet<Application> Applications { get; set; }
    public DbSet<Account> Passwords { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.IdApplication).HasName("Application_pk");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.IdApplicationType).IsRequired();
            entity.HasOne(e => e.ApplicationType)
                  .WithMany()
                  .HasForeignKey(e => e.IdApplicationType)
                  .HasConstraintName("Application_ApplicationType_idApplicationType_fk");
        });
        
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.IdAccount).HasName("Password_pk");
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.IdApplication).IsRequired();
            entity.HasOne(e => e.Application)
                  .WithMany()
                  .HasForeignKey(e => e.IdApplication)
                  .HasConstraintName("Password_Application_idApplication_fk");
        });
        
        modelBuilder.Entity<ApplicationType>(entity =>
        {
            entity.HasKey(e => e.IdApplicationType).HasName("ApplicationType_pk");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
        });
        
        modelBuilder.Entity<ApplicationType>().HasData([
            new ApplicationType { IdApplicationType = 1, Name = "Grand public" },
            new ApplicationType { IdApplicationType = 2, Name = "Professionnelle" }
        ]);
    }
}