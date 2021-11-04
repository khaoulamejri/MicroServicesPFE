using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Domain.Entities
{
  public  class JoursFeries : BaseModel
    {
        [Required]
        public DateTime jour { get; set; } = DateTime.Today;
        [Required]
        public string Description { get; set; }
        //[ForeignKey("companyID")]
        //public virtual Company Company { get; set; }
//      public int companyIDConsumed { get; set; }
    }
}
