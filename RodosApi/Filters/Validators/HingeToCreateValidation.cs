using FluentValidation;
using RodosApi.Contract.V1.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Filters.Validators
{
    public class HingeToCreateValidation : AbstractValidator<HingeToCreate>
    {
        public HingeToCreateValidation()
        {
            RuleFor(s => s.Name).NotNull().MaximumLength(60).MinimumLength(3);
            RuleFor(s => s.FurnitureTypeId).NotEqual(0);
            RuleFor(s => s.CategoryId).NotEqual(0);
            RuleFor(s => s.MakerId).NotEqual(0);
            RuleFor(s => s.MaterialId).NotEqual(0);
            RuleFor(s => s.TypeOfHingesId).NotEqual(0);
            RuleFor(s => s.Price).NotEqual(0);
        }
    }
}
