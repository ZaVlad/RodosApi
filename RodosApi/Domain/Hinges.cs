using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Domain
{
    public class Hinges
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long HingesId { get; set; }

        [Column(TypeName = "money")]
        [Required]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        public long TypeOfHingesId { get; set; }
        [ForeignKey(nameof(TypeOfHingesId))]
        public TypeOfHinge TypeOfHinge { get; set; }


        public long MakerId { get; set; }
        [ForeignKey(nameof(MakerId))]
        public Maker Maker { get; set; }

        public long FurnitureTypeId { get; set; }
        [ForeignKey(nameof(FurnitureTypeId))]
        public FurnitureType FurnitureType { get; set; }


        public long MaterialId { get; set; }
        [ForeignKey(nameof(MaterialId))]
        public Material Material { get; set; }

        public long CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        public List<OrderHinges> OrderHinges{ get; set; }
    }
}
