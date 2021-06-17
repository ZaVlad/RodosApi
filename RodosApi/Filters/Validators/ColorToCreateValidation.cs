using FluentValidation;
using RodosApi.Contract.V1.Request;

namespace RodosApi.Filters.Validators
{
    public class ColorToCreateValidation : AbstractValidator<ColorToCreate>
    {
        public ColorToCreateValidation()
        {
            RuleFor(s => s.Name).NotNull().MinimumLength(3).MaximumLength(60);
        }
    }
}