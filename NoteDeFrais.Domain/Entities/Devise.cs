using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Domain.Entities
{
   public class Devise
    {
        [Key, Required]
        public int Id { get; set; }
        //public string Code { get; set; }
        //public string Intitule { get; set; }

        public int Decimal { get; set; }

        //public float ExchangeRate { get; set; }
        //public DateTime? DateModif { get; set; }
    }
}
