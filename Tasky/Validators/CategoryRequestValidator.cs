using FluentValidation;
using Tasky.Dtos.Request;
using Tasky.Exceptions;
using Tasky.Interfaces.Repositories;

namespace Tasky.Validators;

public class CategoryRequestValidator : AbstractValidator<CategoryRequestDto>
{
	public CategoryRequestValidator(IUserRepository userRepository)
	{
		RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
		RuleFor(x => x.Name).MinimumLength(4).WithMessage("Name must have more than 3 characters");
        RuleFor(x => x.UserId).MustAsync(async (id, cancelationToken) =>
        {
            var category = await userRepository.GetByIdAsync(id);

            if (category == null)
                throw new NotFoundException($"A User with id {id} not found");

            return true;
        });
    }
}
