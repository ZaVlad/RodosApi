using FluentValidation;
using RodosApi.Contract.V1.Request;

namespace RodosApi.Filters.Validators
{
    public class ColorToUpdateValidation:AbstractValidator<ColorToUpdate>
    {
        public ColorToUpdateValidation()
        {
            RuleFor(s => s.Name).NotNull().MinimumLength(3).MaximumLength(60);
        }
    }
}