using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Domain.Entities
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
 // [StringLength(100, ErrorMessageResourceType = typeof(Properties.Resources), ErrorMessageResourceName = "CompPassWd", MinimumLength = 6)]
     [DataType(DataType.Password)]
        public string Password { get; set; }

    //  [Compare("Password", ErrorMessageResourceType = typeof(Properties.Resources), ErrorMessageResourceName = "DiffPassWd")]
      [DataType(DataType.Password)]
     public string ConfirmPassword { get; set; }

        public string Language { get; set; }
    }
}
