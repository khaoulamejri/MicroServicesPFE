using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Conge.Domain.Entities
{
 public   class PlanDroitConge : BaseModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Intitule { get; set; }
        //[ForeignKey("companyID")]
        //public virtual Company Company { get; set; }
   //   public int IdComapny { get; set; }

        //public virtual ICollection<Employee> Employee { get; set; }
        public int IdEmployee { get; set; }
        [JsonIgnore]
        public virtual ICollection<Anciente> Anciente { get; set; }
    }
}
