using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Domain.Entities
{
 public   class AffectationEmployee :BaseModel
    {
        //[Key]
        //public int Id { get; set; }
        //public int EmployeeIDConsumed { get; set; }
        //public int CompanyIDConsumed { get; set; }
        //public DateTime DateDebut { get; set; } = DateTime.Today;
        //public DateTime DateFin { get; set; } = DateTime.MaxValue;
        public int EmployeeID { get; set; }
       
       
        public DateTime DateDebut { get; set; } = DateTime.Today;
        public DateTime DateFin { get; set; } = DateTime.MaxValue;
        public int PositionID { get; set; }
     

        public Boolean Principal { get; set; }
    }
}
