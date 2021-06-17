using RodosApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Contract.V1.Response
{
    public class DoorResponse
    {
        public string DoorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DoorModelResponse DoorModelResponse { get; set; }
        public CoatingResponse CoatingResponse { get; set; }
        public CollectionResponse CollectionResponse { get; set; }

        public CategoryResponse CategoryResponse { get; set; }

        public ColorResponse ColorResponse { get; set; }
        public MakerResponse MakerResponse { get; set; }

        public DoorHandleResponse DoorHandleResponse { get; set; }
        public HingesResponse HingesResponse { get; set; }

        public TypeOfDoorResponse TypeOfDoorResponse { get; set; }
    }
}
