using NoteDeFrais.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NoteDeFrais.Domain.Entities
{
    public class OrdreMission : BaseModel
    {
        [Required]
        public string Titre { get; set; }
        [Required]
        public DateTime DateDebut { get; set; } = DateTime.Today;
        [Required]
        public DateTime DateFin { get; set; } = DateTime.Today;
        public int? PaysIdConsumed { get; set; }
        //[Required]
        //[ForeignKey("PaysId")]
        //public virtual Pays Pays { get; set; }
        //[Required]
       public int EmployeeIDConsumed { get; set; }
        //[ForeignKey("EmployeeID")]
        //public virtual Employee Employee { get; set; }
        [Required]
        public int TypeMissionOrderId { get; set; }
        public string NumeroOM { get; set; }
        [ForeignKey("TypeMissionOrderId")]
        public virtual TypeOrdreMission typeOrdreMission { get; set; }
        [JsonIgnore]
        public virtual ICollection<NotesFrais> NotesFrais { get; set; }
        public string Description { get; set; }
      public StatusDocument Statut { get; set; }
    //    [NotMapped]
    //    public string StatutText
    //    {
    //        get
    //        {
    //            if (this.Statut == StatusDocument.soumetre)
    //                return Properties.Resources.Submitted;
    //            else if (this.Statut == StatusDocument.valider)
    //                return Properties.Resources.Validated;
    //            else if (this.Statut == StatusDocument.refuser)
    //                return Properties.Resources.Refused;
    //            else if (this.Statut == StatusDocument.annuler)
    //                return Properties.Resources.Canceled;
    //            else
    //                return Properties.Resources.Prepared;
    //        }
    //    }
   }

}
