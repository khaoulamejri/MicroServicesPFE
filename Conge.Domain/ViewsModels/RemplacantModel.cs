using Conge.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Domain.ViewsModels
{
  public  class RemplacantModel
    {
        [Key, Required]
        public int Id { get; set; }
        public string UserCreatR { get; set; }
        public string UserModifR { get; set; }
        public DateTime DateCreatR { get; set; }
        public DateTime? DateModifR { get; set; }
        public int companyIDR { get; set; }
        public string Display { get { return ToString(); } }
        public string NumeroPersonneR { get; set; }
        public string NomR { get; set; }

        public string PrenomR { get; set; }


        public DateTime? DateNaissanceR { get; set; }


        [StringLength(8, ErrorMessage = "The {0} must has at least {2} caracters.", MinimumLength = 8)]
        public string CINR { get; set; }
     

        public DateTime? DeliveryDateCinR { get; set; }

        public string PlaceCinR { get; set; }

        public string PassportNumberR { get; set; }

        public DateTime? ValidityDateRPR { get; set; }

        public DateTime? RecruitementDateR { get; set; }

        public DateTime? TitularizationDateR { get; set; }

        [Phone]
        public string TelR { get; set; }

        [Phone]
        public string TelGSMR { get; set; }

        [EmailAddress]
        public string MailR { get; set; }

        public string LangueR { get; set; }

        public string AdresseR { get; set; }

        public string VilleR { get; set; }

        public string CodePostalR { get; set; }

      

        public string UserR { get; set; }
        public int? PlanDroitCongeIDConsumed { get; set; }
      
        public int? RegimeTravailID { get; set; }
        public Boolean ConsultantExterneR { get; set; }


    }
}
