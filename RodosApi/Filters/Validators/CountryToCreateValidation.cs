using FluentValidation;
using RodosApi.Contract.V1.Request;

namespace RodosApi.Filters.Validators
{
    public class CountryToCreateValidation:AbstractValidator<CountryToCreate>
    {
        public CountryToCreateValidation()
        {
            RuleFor(s => s.Name).MaximumLength(60).MinimumLength(3).NotNull();
        }
    }
}