using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Domain.Entities
{
    public class HierarchyPosition : BaseModel
    {
        public int PositionSupID { get; set; }
        [NotMapped]
        [ForeignKey("PositionSupID")]
        public virtual Position PositionSup { get; set; }

        public int TypeHierarchyPositionID { get; set; }

        [ForeignKey("TypeHierarchyPositionID")]
        public virtual TypeHierarchyPosition TypeHierarchyPosition { get; set; }

        public int PositionID { get; set; }

        public virtual Position Position { get; set; }
    }
}
