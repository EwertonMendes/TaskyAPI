using System.ComponentModel.DataAnnotations.Schema;

namespace Tasky.Models
{
    [Table("Item")]
    public class Item
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("createdDate")]
        public DateTime CreatedDate { get; set; }

        [Column("taskListId")]
        public int TaskListId { get; set; }

        public TaskList TaskList { get; set; }
    }
}
