using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Domain.Entities
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
  //    [EmailAddress]
        public List<string> Role { get; set; }
 //     [EmailAddress]
        public string Company { get; set; }
        public bool RememberMe { get; set; }
    }

    public class Tokens
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public string userName { get; set; }
        public string companyName { get; set; }


    }
}
