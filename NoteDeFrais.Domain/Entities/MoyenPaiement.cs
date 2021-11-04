using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NoteDeFrais.Domain.Entities
{
    public class MoyenPaiement : BaseModel
    {
        public string Code { get; set; }
        public string Intitule { get; set; }
        public Boolean Type { get; set; }
        [JsonIgnore]
        public virtual ICollection<Depenses> Depenses { get; set; }
        public override string ToString()
        {
            return Code + "-" + Intitule;
        }
    }
}

