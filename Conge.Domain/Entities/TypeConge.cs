using Conge.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Conge.Domain.Entities
{
   public class TypeConge : BaseModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Intitule { get; set; }
        public bool CongeAnnuel { get; set; }
        public float? NombreJours { get; set; }
        public int? NombreFois { get; set; }
        public TypeCongePeriod? Period { get; set; }
        public bool soldeNegatif { get; set; }
        public bool IsRemplacement { get; set; }
        public bool notification { get; set; }
        public string messageNotification { get; set; }
        //[ForeignKey("companyID")]
        //public virtual Company Company { get; set; }
        // public int CompanyIDConsumed { get; set; }
        [JsonIgnore]
        public virtual ICollection<DemandeConge> DemandeConge { get; set; }
        [JsonIgnore]
        public virtual ICollection<TitreConge> TitreConge { get; set; }
        [JsonIgnore]
        public virtual ICollection<SoldeConge> SoldeConge { get; set; }
        [JsonIgnore]
        public virtual ICollection<MvtConge> MvtConge { get; set; }
    }
}
