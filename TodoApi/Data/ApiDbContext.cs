
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Data;
using TodoApi.Models;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;

    public DbSet<Grade> Grades { get; set; } = null!;
}