using FluentValidation;
using RodosApi.Contract.V1.Request;

namespace RodosApi.Filters.Validators
{
    public class TypeOfHingesToUpdateValidation : AbstractValidator<TypeOfHingesToUpdate>
    {
        public TypeOfHingesToUpdateValidation()
        {
            RuleFor(s => s.Name).MaximumLength(60).MinimumLength(3).NotNull();
        }
    }
}