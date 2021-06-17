using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using RodosApi.Contract.V1.Request;

namespace RodosApi.Filters.Validators
{
    public class TypeOfDoorToUpdateValidation : AbstractValidator<TypeOfDoorToUpdate>
    {
        public TypeOfDoorToUpdateValidation()
        {
            RuleFor(x => x.Name).NotNull().MaximumLength(60).MinimumLength(3);
        }
    }
}
