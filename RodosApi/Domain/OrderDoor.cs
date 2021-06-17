using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Domain
{
    public class OrderDoor
    {
        [Key]
        public long Id { get; set; }
        public long DoorId { get; set; }
        [ForeignKey(nameof(DoorId))]
        public Door Door { get; set; }
        public long OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }
        public int DoorQuantity { get; set; }

    }
}
