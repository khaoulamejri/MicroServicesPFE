using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Domain.Entities
{
   public class Anciente : BaseModel
    {
        [Required]
        public long ToAnc { get; set; }
        [Required]
        public float JourIncrimente { get; set; }
        //[ForeignKey("companyID")]
        //public virtual Company Company { get; set; }
    //  public int IdComapny { get; set; }
        public int PlanDroitCongeID { get; set; }
        [ForeignKey("PlanDroitCongeID")]
        public virtual PlanDroitConge PlanDroitConge { get; set; }
    }
}
