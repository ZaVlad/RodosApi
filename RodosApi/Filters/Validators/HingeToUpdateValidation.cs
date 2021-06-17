using FluentValidation;
using RodosApi.Contract.V1.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Filters.Validators
{
    public class HingeToUpdateValidation : AbstractValidator<HingeToUpdate>
    {
        public HingeToUpdateValidation()
        {
            RuleFor(s => s.Name).NotNull().MaximumLength(60).MinimumLength(3);
            RuleFor(s => s.FurnitureTypeId).NotNull();
            RuleFor(s => s.CategoryId).NotNull();
            RuleFor(s => s.MakerId).NotNull();
            RuleFor(s => s.MaterialId).NotNull();
            RuleFor(s => s.TypeOfHingesId).NotNull();
            RuleFor(s => s.Price).NotNull(); ;
        }
    }
}
