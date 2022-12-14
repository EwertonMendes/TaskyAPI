using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tasky.Interfaces.Models;

namespace Tasky.Models;

[Table("TaskList")]
public class TaskList : IModel
{
    [Column("id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("name")]
    [Required]
    [StringLength(50, MinimumLength = 4)]
    public string Name { get; set; } = string.Empty;

    [Column("categoryId")]
    [Required]
    public int CategoryId { get; set; }

    public Category Category { get; set; }

    [Column("userId")]
    [Required]
    public int UserId { get; set; }

    public User User { get; set; }

    [ForeignKey("taskListId")]
    public List<Item> Items { get; set; } = new();
}
