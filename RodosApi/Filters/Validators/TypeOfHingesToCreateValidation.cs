using FluentValidation;
using RodosApi.Contract.V1.Request;

namespace RodosApi.Filters.Validators
{
    public class TypeOfHingesToCreateValidation :AbstractValidator<TypeOfHingesToCreate>
    {
        public TypeOfHingesToCreateValidation()
        {
            RuleFor(s => s.Name).MaximumLength(60).MinimumLength(3).NotNull();
        }
    }
}