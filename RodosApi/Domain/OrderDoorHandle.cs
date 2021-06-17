using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Domain
{
    public class OrderDoorHandle
    {
        [Key]
        public long Id { get; set; }
        public long DoorHandleId { get; set; }
        [ForeignKey(nameof(DoorHandleId))]
        public DoorHandle DoorHandle { get; set; }

        public long OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }
        [Required]
        public int DoorHandleQuantity { get; set; }

    }
}
