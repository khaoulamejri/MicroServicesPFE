using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Domain.Entities
{
    public class FraisKilometriques : BaseModel
    {
        public string Titre { get; set; }
        public string Depart { get; set; }
        public string Arrivee { get; set; }
        public float NombreTrajets { get; set; }
        public float DistanceParcourue { get; set; }
        public float DistanceParcouruetotal { get; set; }
        public float TotalTTC { get; set; }
        public float TotalRemboursable { get; set; }
        public DateTime DateDebut { get; set; } = DateTime.Today;
        public DateTime DateFin { get; set; } = DateTime.Today;
        public string Commentaire { get; set; }
        public Boolean TypeVehicule { get; set; }
        public string DepartMaps { get; set; }
        public string ArriveeMaps { get; set; }
        public int? NotesFraisId { get; set; }
        [ForeignKey("NotesFraisId")]
        public virtual NotesFrais NotesFrais { get; set; }
    }
}
