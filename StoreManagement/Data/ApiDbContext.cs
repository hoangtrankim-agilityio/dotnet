using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StoreManagement.Models;

namespace StoreManagement.Data;
public class ApiDbContext : IdentityDbContext<User>
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options):base(options) {  }

//     public DbSet<User> Users { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // modelBuilder
        //         .Entity<User>()
        //         .Property(p => p.Role)
        //         .HasConversion<short>();

        modelBuilder.Entity<User>()
                .HasOne(a => a.Cart)
                .WithOne(b => b.User)
                .HasForeignKey<Cart>(b => b.UserId);

        modelBuilder.Entity<User>()
                .HasMany(e => e.Orders)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);

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