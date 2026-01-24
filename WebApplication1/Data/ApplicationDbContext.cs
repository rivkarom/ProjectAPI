using ChineseAuctionProject.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<OrderManagement> OrderManagements { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Donor> Donors { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Gift> Gifts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UserName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(10);
            entity.Property(e => e.HashPassword).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // Donor configuration
        modelBuilder.Entity<Donor>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(10);
            entity.HasMany(e => e.DonationsList)
                  .WithOne(e => e.Donor)
                  .HasForeignKey(e => e.DonorId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Category configuration
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();
            entity.HasMany(e => e.Gifts)
                  .WithOne(e => e.Category)
                  .HasForeignKey(e => e.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Gift configuration
        modelBuilder.Entity<Gift>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.WinnersCount).IsRequired();
            entity.Property(e => e.TicketPrice).IsRequired();

            entity.HasOne(e => e.Category)
                  .WithMany(c => c.Gifts)
                  .HasForeignKey(e => e.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Donor)
                  .WithMany(d => d.DonationsList)
                  .HasForeignKey(e => e.DonorId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // OrderManagement configuration
        modelBuilder.Entity<OrderManagement>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UserId).IsRequired().HasMaxLength(9);
            entity.Property(e => e.GiftId).IsRequired();
            entity.Property(e => e.TicketsCount).IsRequired();
            entity.Property(e => e.IsPaid).IsRequired();
        });
    }
}