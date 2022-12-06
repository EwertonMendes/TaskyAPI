using System.ComponentModel.DataAnnotations.Schema;

namespace Tasky.Dtos.Response
{
    public class CategoryResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
