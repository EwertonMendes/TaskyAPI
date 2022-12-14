using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tasky.Interfaces.Models;

namespace Tasky.Models;

[Table("Item")]
public class Item : IModel
{
    [Column("id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("name")]
    [Required]
    [StringLength(50, MinimumLength = 4)]
    public string Name { get; set; }

    [Column("checked")]
    public bool Checked { get; set; }

    [Column("createdDate")]
    [DataType(DataType.DateTime)]
    public DateTime CreatedDate { get; set; }

    [Column("taskListId")]
    [Required]
    public int TaskListId { get; set; }

    public TaskList TaskList { get; set; }
}
