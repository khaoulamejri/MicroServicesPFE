using Conge.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Domain.Entities
{
  public  class MvtConge : BaseModel
    {
        public DateTime Date { get; set; }
        public float SoldeApres { get; set; }
        public float NbreJours { get; set; }
        public int TypeCongeID { get; set; }
        [ForeignKey("TypeCongeID")]
        public virtual TypeConge TypeConge { get; set; }
        //[ForeignKey("companyID")]
        //public virtual Company Company { get; set; }
  //    public int IdComapny { get; set; }
    //public int EmployeeID { get; set; }
    //    [ForeignKey("EmployeeID")]
    //    public virtual Employee Employee { get; set; }
        public int IdEmployee { get; set; }
        public RhSens Sens { get; set; }
    }
}
