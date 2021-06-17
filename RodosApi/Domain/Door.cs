using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RodosApi.Dtos;

namespace RodosApi.Domain
{
    public class Door
    {
        [Key]
        public long DoorId { get; set; }
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
        [Required]
        [MaxLength(250)]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Required]
        public long DoorModelId { get; set; }
        [ForeignKey(nameof(DoorModelId))]
        public DoorModel DoorModel { get; set; }
        [Required]
        public long CoatingId { get; set; }
        [ForeignKey(nameof(CoatingId))]
        public Coating Coating { get; set; }
        [Required]
        public long CollectionId { get; set; }
        [ForeignKey(nameof(CollectionId))]
        public Collection Collection { get; set; }

        [Required]
        public long CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        [Required]
        public long ColorId { get; set; }
        [ForeignKey(nameof(ColorId))]
        public Color Color { get; set; }
        [Required]
        public long MakerId { get; set; }
        [ForeignKey(nameof(MakerId))]
        public Maker Maker { get; set; }

        public long DoorHandleId { get; set; }
        [ForeignKey(nameof(DoorHandleId))]
        public DoorHandle DoorHandle { get; set; }
        [Required]
        public long HingesId { get; set; }
        [ForeignKey(nameof(HingesId))]
        public Hinges Hinges { get; set; }

        public long TypeOfDoorId { get; set; }
        [ForeignKey(nameof(TypeOfDoorId))]
        public TypeOfDoor TypeOfDoor { get; set; }

        public ICollection<OrderDoor> OrderDoors{ get; set; }
    }
}
