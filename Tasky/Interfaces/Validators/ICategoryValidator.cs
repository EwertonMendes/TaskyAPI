using Tasky.Dtos.Request;

namespace Tasky.Interfaces.Validators
{
    public interface ICategoryValidator
    {
        void Validate(CategoryRequestDto category);
    }
}
