using Conge.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Conge.Domain.Entities
{
  public   class DroitConge : BaseModel
    {
        public string Numero { get; set; }

        public DateTime Date { get; set; } = DateTime.Today;
        public DateTime MoisAffectation { get; set; } = DateTime.Today;
        public RhStatus Status { get; set; }
        [JsonIgnore]
        public virtual ICollection<Details_DroitConge> Details_DroitConge { get; set; }
    }
}
