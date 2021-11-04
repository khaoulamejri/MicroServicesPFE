using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Domain.Entities
{
    public class Employee : BaseModel
    {
      //  [Required(ErrorMessageResourceType = typeof(Properties.Resources), ErrorMessageResourceName = "FieldRequired")]
        public string NumeroPersonne { get; set; }



        //public virtual ICollection<WFDocument> WFDocument { get; set; }
        //public virtual ICollection<WFDocument> WFDocumentReq { get; set; }
        //public virtual ICollection<WFDocument> WFDocumentRemplacant { get; set; }

       // public virtual ICollection<Document> Documents { get; set; }
        //public virtual ICollection<Delegation> EmployeeDelegations { get; set; }
        //public virtual ICollection<Delegation> RemplacantDelegations { get; set; }

     //   public virtual ICollection<EmployeeVehicule> EmployeeVehicule { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }


        public DateTime? DateNaissance { get; set; }


    //    [StringLength(8, ErrorMessage = "The {0} must has at least {2} caracters.", MinimumLength = 8)]
        public string CIN { get; set; }


      //  public virtual ICollection<AffectationEmployee> AffectationEmployee { get; set; }

        //   public virtual ICollection<EmployeeGroupe> EmployeeGroupe { get; set; }

        //public RhGendar? Gender { get; set; }

        //public RhMaritalStatus? MaritalStatus { get; set; }

        public DateTime? DeliveryDateCin { get; set; }

        public string PlaceCin { get; set; }

        public string PassportNumber { get; set; }

        public DateTime? ValidityDateRP { get; set; }

        public DateTime? RecruitementDate { get; set; }

        public DateTime? TitularizationDate { get; set; }

        [Phone]
        public string Tel { get; set; }

        [Phone]
        public string TelGSM { get; set; }

        [EmailAddress]
        public string Mail { get; set; }

        public string Langue { get; set; }

        public string Adresse { get; set; }

        public string Ville { get; set; }

        public string CodePostal { get; set; }

      //  public byte[] Photo { get; set; }

        public string User { get; set; }

       

        public int? PlanDroitCongeIDConsumed { get; set; }
        //[ForeignKey("PlanDroitCongeID")]
        //public virtual PlanDroitConge PlanDroitConge { get; set; }

        public int? RegimeTravailID { get; set; }


        //[ForeignKey("RegimeTravailID")]
        //public virtual RegimeTravail RegimeTravail { get; set; }

        public Boolean ConsultantExterne { get; set; }
        public override string ToString()
        {
            return NumeroPersonne + "-" + Nom + " " + Prenom;
        }
    }
}