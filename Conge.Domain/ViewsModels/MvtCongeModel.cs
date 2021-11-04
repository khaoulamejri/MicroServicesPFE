using Conge.Domain.Entities;
using Conge.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Domain.ViewsModels
{
   public  class MvtCongeModel : BaseModel
    {
        public DateTime Date { get; set; }
        public float SoldeApres { get; set; }
        public float NbreJours { get; set; }
        public int TypeCongeID { get; set; }
        [ForeignKey("TypeCongeID")]
        public virtual TypeConge TypeConge { get; set; }
        //[ForeignKey("companyID")]
        //public virtual Company Company { get; set; }
        public int IdComapny { get; set; }
        //public int EmployeeID { get; set; }
        //    [ForeignKey("EmployeeID")]
        //    public virtual Employee Employee { get; set; }
        public int IdEmployee { get; set; }
        public RhSens Sens { get; set; }
        public int Id { get; set; }
        public string UserCreat { get; set; }
        public string UserModif { get; set; }
        public DateTime DateCreat { get; set; }
        public DateTime? DateModif { get; set; }
        public int companyID { get; set; }
        public string Display { get { return ToString(); } }
        public string NumeroPersonne { get; set; }
        public string Nom { get; set; }

        public string Prenom { get; set; }


        public DateTime? DateNaissance { get; set; }
        public string CIN { get; set; }
       
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



        public string User { get; set; }
        public int? PlanDroitCongeIDConsumed { get; set; }

        public int? RegimeTravailID { get; set; }
        public Boolean ConsultantExterne { get; set; }

    }
}
