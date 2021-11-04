using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NoteDeFrais.Domain.Entities
{
    public class TypeDepense : BaseModel
    {
        public string Code { get; set; }
        public string Intitule { get; set; }
        public int? CompteComptableID { get; set; }
        public float? TVA { get; set; }
        [ForeignKey("CompteComptableID")]
        public virtual CompteComptable CompteComptable { get; set; }
        [JsonIgnore]
        public virtual ICollection<Depenses> Depenses { get; set; }
        [JsonIgnore]
        public virtual ICollection<GroupeFraisDepense> GroupeFraisDepense { get; set; }
    }
}
