using NoteDeFrais.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NoteDeFrais.Domain.Entities
{
    public class NotesFrais : BaseModel
    {
        public string Code { get; set; }
        public DateTime DateDebut { get; set; } = DateTime.Today;
        public DateTime DateFin { get; set; } = DateTime.Today;
        public DateTime DateNote { get; set; } = DateTime.Today;
        public string NumeroNote { get; set; }
        public int EmployeeIDConsumed { get; set; }
        //[ForeignKey("EmployeeID")]
        //public virtual Employee Employee { get; set; }
        public string Validateur { get; set; }
        public int? OrdreMissionId { get; set; }
        [ForeignKey("OrdreMissionId")]
        public virtual OrdreMission OrdreMission { get; set; }

        [JsonIgnore]
        public virtual ICollection<Depenses> Depenses { get; set; }
        [JsonIgnore]
        public virtual ICollection<FraisKilometriques> FraisKilometriques { get; set; }

        [JsonIgnore]
        public virtual ICollection<DocumentsNoteFrais> DocumentsNoteFrais { get; set; }
        public float TotalKm { get; set; }
        public float TotalTTC { get; set; }
        public float TotalRembourser { get; set; }
        public string Description { get; set; }
        public string Commentaire { get; set; }
        public StatusDocument Statut { get; set; }
       // [NotMapped]
        //public string StatutText
        //{
        //    get
        //    {
        //        if (this.Statut == StatusDocument.soumetre)
        //            return Properties.Resources.Submitted;
        //        else if (this.Statut == StatusDocument.valider)
        //            return Properties.Resources.Validated;
        //        else if (this.Statut == StatusDocument.refuser)
        //            return Properties.Resources.Refused;
        //        else if (this.Statut == StatusDocument.annuler)
        //            return Properties.Resources.Canceled;
        //        else
        //            return Properties.Resources.Prepared;
        //    }
       // }
    }
}
