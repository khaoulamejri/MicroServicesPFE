using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Domain.Entities
{
    public class RolesPrivileges
    {
        [Key]
        [Required]
        public string IdRole { get; set; }
        [Key]
        [Required( ErrorMessageResourceName = "FieldRequired")]
        public string Privilege { get; set; }
    }
}