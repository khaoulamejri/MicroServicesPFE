using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Domain.Entities
{
  public  class SoldeConge : BaseModel
    {
        public float Solde { get; set; }
        public int Annee { get; set; }
        public int TypeCongeID { get; set; }
        [ForeignKey("TypeCongeID")]

        public virtual TypeConge TypeConge { get; set; }
        //[ForeignKey("companyID")]

        //public virtual Company Company { get; set; }
      //public int IdComapny { get; set; }
        //public int EmployeeID { get; set; }
        //[ForeignKey("EmployeeID")]

        //public virtual Employee Employee { get; set; }
        public int IdEmployee { get; set; }
    }
}
