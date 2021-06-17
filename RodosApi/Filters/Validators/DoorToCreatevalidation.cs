using FluentValidation;
using RodosApi.Contract.V1.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Filters.Validators
{
    public class DoorToCreatevalidation : AbstractValidator<DoorToCreate>
    {
        public DoorToCreatevalidation()
        {
            RuleFor(s => s.Name).MinimumLength(5).MaximumLength(70).NotNull();
            RuleFor(s => s.CategoryId).NotNull();
            RuleFor(s => s.CoatingId).NotNull();
            RuleFor(s => s.CollectionId).NotNull();
            RuleFor(s => s.ColorId).NotNull();
            RuleFor(s => s.DoorHandleId).NotNull();
            RuleFor(s => s.DoorModelId).NotNull();
            RuleFor(s => s.HingesId).NotNull();
            RuleFor(s => s.MakerId).NotNull();
            RuleFor(s => s.TypeOfDoorId).NotNull();
            RuleFor(s => s.Price).GreaterThan(20).NotEmpty();
            RuleFor(c => c.Description).MinimumLength(3).MaximumLength(500).NotNull();
        }
    }
}
