using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ParamGeneral.Domain.Entities
{
    public class Position : BaseModel
      {
        public string Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(Properties.Resources), ErrorMessageResourceName = "FieldRequired")]
        public string Intitule { get; set; }
        [Required(ErrorMessageResourceType = typeof(Properties.Resources), ErrorMessageResourceName = "FieldRequired")]
        public int DepartementID { get; set; }
        [ForeignKey("DepartementID")]
        //// public virtual Departement Departement { get; set; }
        public virtual Departement Departement { get; set; }
        public int? UniteID { get; set; }
        [ForeignKey("UniteID")]
        public virtual Unite Unite { get; set; }
        public int? EmploiID { get; set; }
        [ForeignKey("EmploiID")]
        public virtual Emploi Emploi { get; set; }
        [JsonIgnore]
        public virtual ICollection<HierarchyPosition> HierarchyPosition { get; set; }
        [JsonIgnore]
        public virtual ICollection<AffectationEmployee> AffectationEmployee { get; set; }

        public override string ToString()
        {
            return Code + "-" + Intitule;
        }
    }
    public class PositionVM
    {
        public int Id { get; set; }
        public string Display { get; set; }
    }
}

