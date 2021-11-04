using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Domain.Entities
{
    public class GroupeFraisDepense : BaseModel
    {
        public int GroupeFraisID { get; set; }
        [ForeignKey("GroupeFraisID")]
        public virtual GroupeFrais GroupeFrais { get; set; }
        public int TypeDepenseID { get; set; }
        [ForeignKey("TypeDepenseID")]
        public virtual TypeDepense TypeDepense { get; set; }
        public float Plafond { get; set; }
        public float Forfait { get; set; }
    }
}
