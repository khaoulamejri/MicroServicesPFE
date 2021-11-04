using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ParamGeneral.Domain.Entities
{
    public class Unite : BaseModel
    {
        [Required(ErrorMessageResourceType = typeof(Properties.Resources), ErrorMessageResourceName = "FieldRequired")]
        public string Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(Properties.Resources), ErrorMessageResourceName = "FieldRequired")]
        public string Intitule { get; set; }
        [JsonIgnore]
        public virtual ICollection<Position> Position { get; set; }

        public override string ToString()
        {
            return Code + "-" + Intitule;
        }
    }
}
