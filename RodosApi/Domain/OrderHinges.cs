using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Domain
{
    public class OrderHinges
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey(nameof(HingesId))]
        public Hinges Hinges { get; set; }
        public long HingesId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }
        public long OrderId { get; set; }
        [Required]
        public int  HingesQuantity { get; set; }


    }
}
