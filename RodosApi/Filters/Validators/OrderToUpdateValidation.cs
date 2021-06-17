using FluentValidation;
using RodosApi.Contract.V1.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Filters.Validators
{
    public class OrderToUpdateValidation : AbstractValidator<OrderToUpdate>
    {
        public OrderToUpdateValidation()
        {
            RuleFor(s => s.ClientId).NotEmpty();
            RuleFor(s => s.DeliveryStatusId).NotEmpty().LessThan(4).GreaterThan(0);
            RuleFor(s => s.OrderDate).NotEmpty();
        }
    }
}
