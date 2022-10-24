using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasky.Models
{
    [Table("Category")]
    public class Category
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("createdDate")]
        public DateTime CreatedDate { get; set; }

        public List<TaskList> TaskLists { get; set; } = new();

    }
}
