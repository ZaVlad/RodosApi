using RodosApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Contract.V1.Response
{
    public class HingesResponse
    {
        public long HingesId { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }

        public TypeOfHingeResponse TypeOfHingeResponse { get; set; }


        public MakerResponse MakerResponse { get; set; }

        public FurnitureTypeResponse FurnitureTypeResponse { get; set; }


        public MaterialResponse MaterialResponse { get; set; }

        public CategoryResponse CategoryResponse { get; set; }
    }
}
