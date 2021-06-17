using FluentValidation;
using RodosApi.Contract.V1.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Filters.Validators
{
    public class OrderToCreateValidation : AbstractValidator<OrderToCreate>
    {
        public OrderToCreateValidation()
        {
            RuleFor(s => s.ClientId).NotEmpty();
        }
    }
}
