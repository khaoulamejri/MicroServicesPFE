using Conge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Domain.ViewsModels
{
  public  class DetailsDroitCongeViewModell : BaseModel
    {
        public float Droit { get; set; }
        public float DroitMisAJour { get; set; }
        public string Commentaire { get; set; }
        // public int EmployeeID { get; set; }

        //[ForeignKey("EmployeeID")]
        //public virtual Employee Employee { get; set; }
        public int IdEmployee { get; set; }

        public int DroitCongeId { get; set; }

        [ForeignKey("DroitCongeId")]
        public virtual DroitConge DroitConge { get; set; }
        //[ForeignKey("companyID")]
        //public virtual Company Company { get; set; }
     //   public int IdComapny { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
    }
}
