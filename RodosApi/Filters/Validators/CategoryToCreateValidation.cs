using FluentValidation;
using RodosApi.Contract.V1.Request;

namespace RodosApi.Filters.Validators
{
    public class CategoryToCreateValidation:AbstractValidator<CategoryToCreate>
    {
        public CategoryToCreateValidation()
        {
            RuleFor(s => s.Name).NotNull().MaximumLength(60).MinimumLength(3);
        }
    }
}