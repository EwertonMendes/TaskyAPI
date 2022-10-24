using Microsoft.EntityFrameworkCore;
using Tasky.Models;

namespace Tasky.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base (options)
    {

    }

    public DbSet<Category> Category { get; set; }
    public DbSet<TaskList> TaskList { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>().HasOne(p => p.TaskList).WithMany(b => b.Items);
    }
}
