using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Domain
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ClientId { get; set; }
        [Required]
        [MaxLength(60),MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MaxLength(60), MinLength(3)]
        public string LastName { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(5),MaxLength(60)]
        public string Address { get; set; }

    }
}
