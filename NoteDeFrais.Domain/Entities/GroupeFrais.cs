using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NoteDeFrais.Domain.Entities
{
    public class GroupeFrais : BaseModel
    {
        public string Code { get; set; }
        public string Intitule { get; set; }
        [JsonIgnore]
        public virtual ICollection<GroupeFraisDepense> GroupeFraisDepense { get; set; }
        [JsonIgnore]
        public virtual ICollection<EmployeeGroupe> EmployeeGroupe { get; set; }
        public float? PrixUPro { get; set; }
        public float? PrixUPerso { get; set; }
    }
}
