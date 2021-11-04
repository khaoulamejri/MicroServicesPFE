using Conge.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Domain.Entities
{
     public class Delegation : BaseModel
    {
        //public int Employee { get; set; }
        //public virtual Employee Employe { get; set; }
        public int IdEmployee { get; set; }

        //public int Remplacant { get; set; }
        //public virtual Employee Remplacan { get; set; }
        public int IdRemplacant { get; set; }


        [Required]
        public DateTime DateDebut { get; set; } = DateTime.Today;
        [Required]
        public DateTime DateFin { get; set; } = DateTime.Today;
        public StatusDelegation Statut { get; set; }
        public int TitreId { get; set; }
        [ForeignKey("TitreId")]
        public virtual TitreConge Titre { get; set; }

    }
}

