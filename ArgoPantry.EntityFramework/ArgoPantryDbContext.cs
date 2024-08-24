using Microsoft.EntityFrameworkCore;
using ArgoPantry.Domain.Models.Entities;
using ArgoPantry.Domain.Models.JoinTables;

namespace ArgoPantry.EntityFramework;
public class ArgoPantryDbContext : DbContext {
    public DbSet<Student> Students { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    public ArgoPantryDbContext(DbContextOptions<ArgoPantryDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        // Configure composite key
        modelBuilder.Entity<OrderItem>()
            .HasKey(oi => new { oi.OrderId, oi.ItemId });

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade); // If Order is deleted, delete all associated OrderItems

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Item)
            .WithMany(i => i.OrderItems)
            .HasForeignKey(oi => oi.ItemId)
            .OnDelete(DeleteBehavior.Restrict); // Can't delete Item if it's associated with an Order

        base.OnModelCreating(modelBuilder);
    }
}