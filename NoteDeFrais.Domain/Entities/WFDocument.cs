using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Domain.Entities
{
    public class WFDocument : BaseModel
    {
       
        public string TypeDocument { get; set; }
        public bool Finished { get; set; }
        public int AffectedToId { get; set; }
       // public Employee AffectedTo { get; set; }
        public int DocumentId { get; set; }
    }
}
