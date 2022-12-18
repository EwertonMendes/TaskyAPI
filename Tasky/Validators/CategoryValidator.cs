using System.ComponentModel.DataAnnotations;
using Tasky.Dtos.Request;
using Tasky.Interfaces.Validators;
using Tasky.Models;

namespace Tasky.Validators
{
    public class CategoryValidator : ICategoryValidator
    {
        public void Validate(Category? category, int id)
        {
            if (category == null)
                throw new ValidationException($"Category with id {id} not found");
        }

        public void Validate(CategoryRequestDto category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            if (category.Name.Length <= 3)
                throw new ValidationException("Name must have more than 3 characters");
        }
    }
}
