using FluentValidation;
using RodosApi.Contract.V1.Request;

namespace RodosApi.Filters.Validators
{
    public class CategoryToUpdateValidation : AbstractValidator<CategoryToUpdate>
    {
        public CategoryToUpdateValidation()
        {
            RuleFor(s => s.Name).NotNull().MaximumLength(60).MinimumLength(3);
        }
    }
}