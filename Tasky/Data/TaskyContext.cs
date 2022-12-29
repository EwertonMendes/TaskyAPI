using Microsoft.EntityFrameworkCore;
using Tasky.Models;

namespace Tasky.Data;

public class TaskyContext : DbContext
{
    public TaskyContext(DbContextOptions<TaskyContext> options) : base(options) { }

    public DbSet<Category> Category { get; set; }
    public DbSet<TaskList> TaskList { get; set; }
    public DbSet<Item> Item { get; set; }
    public DbSet<User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasOne(c => c.User).WithMany(c => c.Categories).HasForeignKey(c => c.UserId);
        modelBuilder.Entity<TaskList>().HasOne(t => t.Category).WithMany(t => t.TaskLists).HasForeignKey(t => t.CategoryId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<TaskList>().HasOne(t => t.User).WithMany(t => t.TaskLists).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Item>().HasOne(i => i.TaskList).WithMany(i => i.Items).HasForeignKey(i => i.TaskListId);
    }
}
