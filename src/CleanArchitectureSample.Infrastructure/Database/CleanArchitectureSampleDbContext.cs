using Microsoft.EntityFrameworkCore;
using CleanArchitectureSample.Core.Aggregates;

namespace CleanArchitectureSample.Infrastructure.Database;

public class CleanArchitectureSampleDbContext(DbContextOptions<CleanArchitectureSampleDbContext> options) : DbContext(options)
{
    public DbSet<ContactEntity> Contact { get; set; }
    public DbSet<CountryEntity> Country { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(InfrastructureConstants.Database.DefaultSchema);

        modelBuilder.Entity<ContactEntity>(entity =>
        {
            entity.HasKey(x => x.Id);
            
            entity.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            
            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(p => p.BirthDate)
                .HasColumnType("date");

            entity.Property(p => p.LastName)
                .IsRequired(false)
                .HasMaxLength(50);

            entity.Property(p => p.Phone)
                .IsRequired(false)
                .HasMaxLength(50);

            entity.Property(p => p.EMail)
                .IsRequired(false)
                .HasMaxLength(50);
        });

        modelBuilder.Entity<CountryEntity>(entity => { 
            
            entity.HasKey(x => x.Id);

            entity.HasMany(x => x.Contacts)
                .WithOne(x => x.Country);

            entity.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            entity.Property(p => p.Name)
                .IsRequired(true)
                .HasMaxLength(50);
        
            entity
                .Property(p => p.Code)
                .IsRequired(false)
                .HasMaxLength(6);
        });
    }
}
