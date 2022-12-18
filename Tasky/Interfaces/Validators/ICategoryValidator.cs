using Tasky.Dtos.Request;
using Tasky.Models;

namespace Tasky.Interfaces.Validators
{
    public interface ICategoryValidator
    {
        void Validate(Category? category, int id);
        void Validate(CategoryRequestDto category);
    }
}
