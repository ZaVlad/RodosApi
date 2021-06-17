using FluentValidation;
using RodosApi.Contract.V1.Request;

namespace RodosApi.Filters.Validators
{
    public class FurnitureTypeToUpdateValidation:AbstractValidator<FurnitureTypeToCreate>
    {
        public FurnitureTypeToUpdateValidation()
        {
            RuleFor(s => s.Name).NotNull().MaximumLength(60).MinimumLength(3);
        }
    }
}