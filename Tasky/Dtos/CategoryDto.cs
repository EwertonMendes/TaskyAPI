using System.ComponentModel.DataAnnotations.Schema;

namespace Tasky.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
