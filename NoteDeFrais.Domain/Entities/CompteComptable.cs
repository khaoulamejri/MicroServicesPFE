using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NoteDeFrais.Domain.Entities
{
    public class CompteComptable : BaseModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Compte { get; set; }
        [Required]
        public string Description { get; set; }
        [JsonIgnore]
        public virtual ICollection<TypeDepense> TypeDepense { get; set; }
        /*public override string ToString()
        {
            return Code + "_" + Compte ;
        }*/
    }
}
