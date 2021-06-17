using RodosApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RodosApi.Contract.ApiRoutes;

namespace RodosApi.Contract.V1.Response
{
    public class DoorHandleResponse
    {
        public long DoorHandleId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public CategoryResponse CategoryResponse { get; set; }

        public MakerResponse MakerResponse { get; set; }
        public FurnitureTypeResponse FurnitureTypeResponse { get; set; }

        public ColorResponse ColorResponse { get; set; }

        public MaterialResponse MaterialResponse { get; set; }
    }
}
