using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Domain
{
    public class ErrorResponse
    {
        public List<ErrorModel> errors { get; set; }
    }
}
