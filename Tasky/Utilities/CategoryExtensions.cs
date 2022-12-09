using Tasky.Dtos.Response;
using Tasky.Models;

namespace Tasky.Utilities
{
    public static class CategoryExtensions
    {
        public static CategoryResponseDto ToDto(this Category category)
        {
            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                CreatedDate = category.CreatedDate,
            };
        }
    }
}
