using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Domain
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderId { get; set; }
        [Required]
        public long ClientId { get; set; }
        public Client Client { get; set; }
        public ICollection<OrderDoor> Doors { get; set; }
        public ICollection<OrderDoorHandle> DoorHandles { get; set; }
        public List<OrderHinges> Hinges{ get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        [Required]
        [Range(1,3)]
        public byte DeliveryStatusId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
    }
}
