using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tasky.Interfaces;

namespace Tasky.Models
{
    [Table("TaskList")]
    public class TaskList : IModel
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("categoryId")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("checked")]
        public bool Checked { get; set; }

        [ForeignKey("taskListId")]
        public List<Item> Items { get; set; } = new();
    }
}
