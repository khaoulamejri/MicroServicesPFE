using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NoteDeFrais.Domain.Entities
{
    public class TypeOrdreMission : BaseModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Intitule { get; set; }
        [Required]
        public bool IsAbroad { get; set; }
    [JsonIgnore]
        public virtual ICollection<OrdreMission> OrdreMission { get; set; }
    }
}
