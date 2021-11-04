using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Domain.Entities
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Language { get; set; }

        public bool IsActif { get; set; }

        public static explicit operator UserViewModel(ApplicationUser user)
        {
            UserViewModel fullUser = new UserViewModel();
            fullUser.UserName = user.UserName;
            fullUser.Email = user.Email;
            fullUser.Id = user.Id;
            fullUser.IsActif = user.IsActif;
            fullUser.Language = user.Language;
            return fullUser;
        }
    }
}