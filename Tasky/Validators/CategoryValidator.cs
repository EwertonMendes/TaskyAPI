using System.ComponentModel.DataAnnotations;
using Tasky.Dtos.Request;
using Tasky.Interfaces.Validators;

namespace Tasky.Validators
{
    public class CategoryValidator : ICategoryValidator
    {
        public void Validate(CategoryRequestDto category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            if (category.Name.Length <= 3)
                throw new ValidationException("Name must have more than 3 characters");
        }
    }
}
