using Microsoft.EntityFrameworkCore;
using StoreManagement.Models;

namespace StoreManagement.Data;
public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options):base(options) {  }

    public DbSet<User> Users { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
                .Entity<User>()
                .Property(p => p.Role)
                .HasConversion<short>();

        modelBuilder
                .Entity<Cart>()
                .Property(p => p.Status)
                .HasConversion<short>();

        modelBuilder
                .Entity<Order>()
                .Property(p => p.Status)
                .HasConversion<short>();
    }
}