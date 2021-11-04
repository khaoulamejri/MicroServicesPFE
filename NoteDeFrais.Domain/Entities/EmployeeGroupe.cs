using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Domain.Entities
{
    public class EmployeeGroupe : BaseModel
    {
        public DateTime DateAffectation { get; set; } = DateTime.Today;
        public DateTime DateFinAffectation { get; set; } = DateTime.Today;
        public int GroupeFraisID { get; set; }
        [ForeignKey("GroupeFraisID")]
        public virtual GroupeFrais GroupeFrais { get; set; }
        public int EmployeeIDConsumed { get; set; }
        //[ForeignKey("EmployeeID")]
        //public virtual Employee Employee { get; set; }
    }
}
