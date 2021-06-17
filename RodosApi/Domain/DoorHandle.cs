using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RodosApi.Domain
{
    public class DoorHandle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DoorHandleId { get; set; }
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
        [Column(TypeName = "money")]
        [Required]
        public decimal Price { get; set; }
        [Required]

        public long CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        [Required]
        public long MakerId { get; set; }
        [ForeignKey(nameof(MakerId))]
        public Maker Maker { get; set; }
        [Required]

        public long FurnitureTypeId { get; set; }
        [ForeignKey(nameof(FurnitureTypeId))]
        public FurnitureType FurnitureType { get; set; }

        public long ColorId { get; set; }
        [ForeignKey(nameof(ColorId))]
        public Color Color { get; set; }

        public long MaterialId { get; set; }
        [ForeignKey(nameof(MaterialId))]
        public Material Material { get; set; }

        public ICollection<OrderDoorHandle> OrderDoorHandle { get; set; }

    }
}
