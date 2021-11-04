using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Domain.Entities
{
    public class Pays
    {
        [Key, Required]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Intitule { get; set; }

        public string DeviseCode { get; set; }

        //public int? DeviseID { get; set; }
        //[ForeignKey("DeviseID")]
        //public virtual Devise Devise { get; set; }

      //  [JsonIgnore]
      //  public virtual ICollection<Depenses> Depenses { get; set; }
     // public virtual ICollection<Company> Company { get; set; }
     //   public virtual ICollection<OrdreMission> OrdreMission { get; set; }

        public override string ToString()
        {
            return Code + "-" + Intitule;
        }
    }
}
