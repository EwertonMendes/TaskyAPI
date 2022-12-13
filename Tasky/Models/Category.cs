using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tasky.Interfaces.Models;

namespace Tasky.Models
{
    [Table("Category")]
    public class Category : IModel
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("createdDate")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public List<TaskList> TaskLists { get; set; } = new();
    }
}
