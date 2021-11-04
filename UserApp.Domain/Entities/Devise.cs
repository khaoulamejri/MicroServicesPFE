using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UserApp.Domain.Entities
{
    public class Devise
    {
        [Key, Required]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Intitule { get; set; }

        public int Decimal { get; set; }

        public float ExchangeRate { get; set; }
        public DateTime? DateModif { get; set; }

       // [JsonIgnore]
      //  public virtual ICollection<Depenses> Depenses { get; set; }

        //[JsonIgnore]
        //public virtual ICollection<Pays> Pays { get; set; }

        //[JsonIgnore]
        //public virtual ICollection<Company> Company { get; set; }

        public override string ToString()
        {
            return Code + "-" + Intitule;
        }
    }
}
