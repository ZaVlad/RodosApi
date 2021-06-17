using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Contract.V1.Request
{
    public class HingeToCreate
    {
        public decimal Price { get; set; }

        public string Name { get; set; }

        public long TypeOfHingesId { get; set; }


        public long MakerId { get; set; }

        public long FurnitureTypeId { get; set; }


        public long MaterialId { get; set; }

        public long CategoryId { get; set; }
    }
}
