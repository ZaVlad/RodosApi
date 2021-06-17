using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Contract.V1.Request
{
    public class ClientToCreate
    {
        [Required]
        [MaxLength(60), MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MaxLength(60), MinLength(3)]
        public string LastName { get; set; }
        [Phone]
        [MaxLength(12)]
        [MinLength(12)]
        public string Phone { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(5), MaxLength(60)]
        public string Adress { get; set; }
    }
}
