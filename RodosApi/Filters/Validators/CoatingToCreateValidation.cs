using System.Data;
using FluentValidation;
using FluentValidation.Validators;
using RodosApi.Contract.V1.Request;

namespace RodosApi.Filters.Validators
{
    public class CoatingToCreateValidation:AbstractValidator<CoatingToCreate>
    {
        public CoatingToCreateValidation()
        {
            RuleFor(s => s.Name).MaximumLength(60).MinimumLength(3).NotNull();
        }
    }
}