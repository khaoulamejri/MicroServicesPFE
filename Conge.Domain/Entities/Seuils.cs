using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Domain.Entities
{
    public class Seuils : BaseModel
    {
  [Required(ErrorMessageResourceType = typeof(Properties.Resources), ErrorMessageResourceName = "FieldRequired")]
        public float Seuil { get; set; }
    [Required(ErrorMessageResourceType = typeof(Properties.Resources), ErrorMessageResourceName = "FieldRequired")]
        public float Valeur { get; set; }
        //[ForeignKey("companyID")]
        //public virtual Company Company { get; set; }

    }
}
