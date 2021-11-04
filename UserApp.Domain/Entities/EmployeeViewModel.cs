using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UserApp.Domain.Entities
{
  public  class EmployeeViewModel
    {
        public string RandomPassword()
        {
            string lettre = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            string caractere = "!-_*+&$";
            string number = "0123456789";
            string ensemble = "";
            ensemble += lettre;
            ensemble += number;
            ensemble += caractere;
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            // Ici, ensemble contient donc la totalité des caractères autorisés
            string password = "";
            int taillePwd = 8;
            Random rand = new Random();
            for (int i = 0; i < taillePwd; i++)
            {
                // On ajoute un caractère parmi tous les caractères autorisés
                password += ensemble[rand.Next(0, ensemble.Length)];
                if ((password.Length == 8) && (!hasLowerChar.IsMatch(password) || !hasUpperChar.IsMatch(password)))
                {

                    i = -1;
                    password = "";
                }

            }
            return password;
        }
    }
}
