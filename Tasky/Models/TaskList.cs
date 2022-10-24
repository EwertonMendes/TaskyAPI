using System.ComponentModel.DataAnnotations.Schema;

namespace Tasky.Models
{
    [Table("TaskList")]
    public class TaskList
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("categoryId")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("checked")]
        public bool Checked { get; set; }

        public List<Item> Items { get; set; } = new();
    }
}
