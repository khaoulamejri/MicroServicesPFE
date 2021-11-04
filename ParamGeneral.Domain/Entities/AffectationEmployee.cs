using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Domain.Entities
{
  public   class AffectationEmployee: BaseModel
    {
        public int EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }
        public DateTime DateDebut { get; set; } = DateTime.Today;
        public DateTime DateFin { get; set; } = DateTime.MaxValue;
        public int PositionID { get; set; }
        [ForeignKey("PositionID")]
        public virtual Position Position { get; set; }

        public Boolean Principal { get; set; }
    }
}
