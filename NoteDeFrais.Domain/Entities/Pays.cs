using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Domain.Entities
{
 public   class Pays
    {
        [Key, Required]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Intitule { get; set; }

        public string DeviseCode { get; set; }
    }
}
