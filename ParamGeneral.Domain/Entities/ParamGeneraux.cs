using ParamGeneral.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Domain.Entities
{
    public class ParamGeneraux : BaseModel
    {
        [Required(ErrorMessageResourceType = typeof(Properties.Resources), ErrorMessageResourceName = "FieldRequired")]
        public string DebutAnneeBonifiation { get; set; }
        [Required(ErrorMessageResourceType = typeof(Properties.Resources), ErrorMessageResourceName = "FieldRequired")]
        public string FinAnneeBonifiation { get; set; }

        public RhDays WeekEnd1 { get; set; }
        public RhDays WeekEnd2 { get; set; }
        public Boolean WeekEnd1_inclut { get; set; }
        public string Souche { get; set; }
        public Boolean WeekEnd2_inclut { get; set; }

        public Boolean AfficheOU { get; set; }

        public Boolean AfficheJB { get; set; }

        public int? TpHierPosID { get; set; }
        [ForeignKey("TpHierPosID")]
        public virtual TypeHierarchyPosition TypeHierarchyPosition { get; set; }

        public bool Remplacant_autre_company { get; set; }

        public string ServerName { get; set; }

        public string ProjectName { get; set; }

        public string RaportName { get; set; }
    }
}