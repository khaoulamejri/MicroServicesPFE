using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NoteDeFrais.Domain.Entities
{
    public class Depenses : BaseModel
    {
        public string Titre { get; set; }
        public DateTime DateDepense { get; set; } = DateTime.Today;
        public Boolean Facturable { get; set; }
        public string Libelle { get; set; }
        public string Commentaire { get; set; }
        public string Client { get; set; }
        public string ReferenceCommande { get; set; }
        public string Reference2 { get; set; }
        public float TVA { get; set; }
        public float TTC { get; set; }
        public float TotalRemboursable { get; set; }
        public int? NotesFraisID { get; set; }
        [ForeignKey("NotesFraisID")]
        public virtual NotesFrais NotesFrais { get; set; }
        public int? TypeDepenseID { get; set; }
        [ForeignKey("TypeDepenseID")]
        public virtual TypeDepense TypeDepense { get; set; }
        public int? MoyenPaiementID { get; set; }
        [ForeignKey("MoyenPaiementID")]
        public virtual MoyenPaiement MoyenPaiement { get; set; }
        public int? DeviseIDConsumed { get; set; }
        //[ForeignKey("DeviseID")]
        //public virtual Devise Devise { get; set; }
        public float ExchangeRate { get; set; }
        public int? PaysIDConsumed { get; set; }
        //[ForeignKey("PaysID")]
        //public virtual Pays Pays { get; set; }
        public bool Warning { get; set; }

        [JsonIgnore]
        public virtual ICollection<DocumentsDepenses> DocumentsDepenses { get; set; }
    }
}
