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
  public  class TitreConge : BaseModel
    {
        public DateTime DateDemande { get; set; } = DateTime.Today;
        [Required]
        public DateTime DateDebutConge { get; set; } = DateTime.Today;
        [Required]
        public DateTime DateRepriseConge { get; set; } = DateTime.Today;
        public float NbrConge { get; set; }
        // public int? Remplacantconsumed { get; set; }
        //[ForeignKey("Remplacant")]

        //public virtual Employee EmpRp { get; set; }
        //   public int IdRemplacant { get; set; }
    
        public bool IsApremDebut { get; set; }
        public bool IsApremRetour { get; set; }
        public string NumeroTitre { get; set; }
        public string Commentaire { get; set; }
        public float NbrBonification { get; set; }
        public StatusDocument Statut { get; set; }
        public int? DelegationId { get; set; }
        [ForeignKey("DelegationId")]
        public virtual Delegation Delegation { get; set; }
        [NotMapped]
        public string StatutText
        {
            get
            {
                return this.Statut.ToString();
            }
        }
        public int TypeCongeID { get; set; }
        [ForeignKey("TypeCongeID")]
       public virtual TypeConge TypeConge { get; set; }
        //[ForeignKey("companyID")]
        //public virtual Company Company { get; set; }
   //   public int companyIDConsume { get; set; }
        public string NumeroDemande { get; set; }
        //public int EmployeeID { get; set; }
        //[ForeignKey("EmployeeID")]
        //public virtual Employee Employee { get; set; }
        public int IdEmployee { get; set; }
        public int IdRemplacant { get; set; }
    }
}
