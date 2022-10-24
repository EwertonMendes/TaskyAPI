using System.ComponentModel.DataAnnotations.Schema;

namespace Tasky.Models
{
    [Table("Category")]
    public class Category
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("createdDate")]
        public DateTime CreatedDate { get; set; }
        
    }
}
