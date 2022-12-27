using Microsoft.EntityFrameworkCore;
using Tasky.Models;

namespace Tasky.Data;

public class TaskyContext : DbContext
{
    public TaskyContext(DbContextOptions<TaskyContext> options) : base (options){}

    public DbSet<Category> Category { get; set; }
    public DbSet<TaskList> TaskList { get; set; }
    public DbSet<Item> Item { get; set; }
    public DbSet<User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskList>().HasOne(t => t.Category).WithMany(t => t.TaskLists).IsRequired();
        modelBuilder.Entity<Item>().HasOne(p => p.TaskList).WithMany(b => b.Items);
    }
}
