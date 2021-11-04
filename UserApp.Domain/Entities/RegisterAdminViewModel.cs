using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Domain.Entities
{
   public  class RegisterAdminViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        // [StringLength(100, ErrorMessageResourceType = typeof(Properties.Resources), ErrorMessageResourceName = "CompPassWd", MinimumLength = 6)]
      
        public string Password { get; set; }
        public string Language { get; set; }
    }
}
